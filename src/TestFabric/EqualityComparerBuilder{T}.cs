using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace TestFabric;

public class EqualityComparerBuilder<T>
{
    private const string DefaultProperty = "Default";
    private const string EqualsMethod = "Equals";
    private const string InvokeMethod = "Invoke";
    private const string CountProperty = "Count";
    private const string LengthProperty = "Length";
    private readonly Dictionary<MemberInfo, object> _customMemberComparers = new();
    private readonly Dictionary<Type, object> _customTypeComparers = new();

    private readonly List<MemberInfo> _ignoredMembers = [];
    private bool _detailedTracing;
    private Action<string> _lineWriter;

    public EqualityComparerBuilder<T> ConfigureType<TType>(
        IEqualityComparer<TType> typeEqualityComparer)
    {
        return ConfigureType<TType>(typeEqualityComparer.Equals);
    }

    public EqualityComparerBuilder<T> ConfigureType<TType>(
        Func<TType, TType, bool> typeEqualityComparer)
    {
        _customTypeComparers[typeof(TType)] = typeEqualityComparer;

        return this;
    }

    public EqualityComparerBuilder<T> IgnoreType<TType>()
    {
        return ConfigureType((Func<TType, TType, bool>)Delegate);

        bool Delegate(TType type, TType type1)
        {
            return true;
        }
    }

    public EqualityComparerBuilder<T> ConfigureMember<TType, TMember>(
        Expression<Func<TType, TMember>> expression,
        IEqualityComparer<TMember> customComparer)
    {
        return ConfigureMember(expression, customComparer.Equals);
    }

    public EqualityComparerBuilder<T> ConfigureMember<TType, TMember>(
        Expression<Func<TType, TMember>> expression,
        Func<TMember, TMember, bool> customComparer)
    {
        if (expression.TryGetMemberInfo(out var memberInfo))
        {
            _customMemberComparers[memberInfo] = customComparer;
        }

        return this;
    }

    public EqualityComparerBuilder<T> ConfigureMember<TMember>(
        Expression<Func<T, TMember>> expression,
        IEqualityComparer<TMember> customComparer)
    {
        return ConfigureMember<T, TMember>(expression, customComparer.Equals);
    }

    public EqualityComparerBuilder<T> ConfigureMember<TMember>(
        Expression<Func<T, TMember>> expression,
        Func<TMember, TMember, bool> customComparer)
    {
        return ConfigureMember<T, TMember>(expression, customComparer);
    }

    public EqualityComparerBuilder<T> IgnoreMember<TType, TMember>(
        Expression<Func<TType, TMember>> expression)
    {
        if (expression.TryGetMemberInfo(out var memberInfo))
        {
            _ignoredMembers.Add(memberInfo);
        }

        return this;
    }

    public EqualityComparerBuilder<T> IgnoreMember<TMember>(
        Expression<Func<T, TMember>> expression)
    {
        return IgnoreMember<T, TMember>(expression);
    }

    public EqualityComparerBuilder<T> EnableTracing(
        Action<string> lineWriter,
        bool detailed = false)
    {
        _lineWriter = lineWriter;
        _detailedTracing = detailed;
        return this;
    }

    public IEqualityComparer<T> Create()
    {
        var mock = new Mock<IEqualityComparer<T>>(MockBehavior.Strict);
        mock
            .Setup(x => x.Equals(It.IsAny<T>(), It.IsAny<T>()))
            .Returns((T x, T y) => EqualsImpl(typeof(T), x, y));

        return mock.Object;
    }

    private bool EqualsImpl(Type type, object x, object y)
    {
        TraceInfo($"Check equality of {type.Name}");

        if (_customTypeComparers.TryGetValue(type, out var typeComparer))
        {
            TraceDebug("using a custom type comparer");
            var customResult = Invoke(typeComparer, x, y);
            if (!customResult)
            {
                TraceInfo($"{type.Name}: custom comparer failed");
            }

            return customResult;
        }

        if (TryNullOrReferenceSucceeded(x, y, out var result))
        {
            TraceDebug("using null or reference comparison");
            if (!result)
            {
                TraceInfo($"{type.Name}: null or reference comparison failed");
            }

            return result;
        }

        if (TryArray(type, x, y, out result))
        {
            TraceDebug("using array comparison");
            if (!result)
            {
                TraceInfo($"{type.Name}: array comparison failed");
            }

            return result;
        }

        if (TryList(type, x, y, out result))
        {
            if (!result)
            {
                TraceInfo($"{type.Name}: list comparison failed");
            }

            return result;
        }

        TraceDebug("using member comparison");
        result = EqualMembers(type, x, y);
        if (!result)
        {
            TraceInfo($"{type.Name}: member comparison failed");
        }

        return result;
    }

    private bool TryArray(Type type, object o1, object o2, out bool result)
    {
        result = true;
        if (!type.IsArray)
        {
            return false;
        }

        result = EqualEnumerableObjects(
            type,
            type.GetElementType(),
            LengthProperty,
            o1,
            o2,
            (index, arrayObj) => ((Array)arrayObj).GetValue(index));
        return true;
    }

    private bool TryList(Type type, object o1, object o2, out bool result)
    {
        result = true;

        if (!type.IsGenericType)
        {
            return false;
        }

        if (!(type.GetGenericTypeDefinition() == typeof(List<>)))
        {
            return false;
        }

        var indexer = type.GetProperties().First(x => x.GetIndexParameters().Length > 0);
        result = EqualEnumerableObjects(
            type,
            type.GenericTypeArguments[0],
            CountProperty,
            o1,
            o2,
            (index, list) => indexer.GetValue(list, [index]));
        return true;
    }

    private bool EqualEnumerableObjects(
        Type type,
        Type elementType,
        string countPropertyName,
        object o1,
        object o2,
        Func<int, object, object> selectElement)
    {
        var countProperty = type.GetProperty(countPropertyName);
        Debug.Assert(countProperty != null, nameof(countProperty) + " != null");
        var count1 = (int)countProperty.GetValue(o1);
        var count2 = (int)countProperty.GetValue(o2);
        if (count1 != count2)
        {
            TraceInfo($"{type.Name}: count comparison failed {count1} != {count2}");
            return false;
        }

        for (var i = 0; i < count1; i++)
        {
            var item1 = selectElement(i, o1);
            var item2 = selectElement(i, o2);

            var itemComparison = EqualsImpl(elementType, item1, item2);
            if (itemComparison)
            {
                continue;
            }

            TraceInfo($"{type.Name}: element {i} comparison failed");
            return false;
        }

        return true;
    }

    private bool EqualMembers(Type type, object x, object y)
    {
        var membersForComparison = GetAllMembersForComparison(type, x, y);
        if (membersForComparison.Count == 0)
        {
            TraceDebug("there were no members to compare: use the default comparer");
            return DefaultComparerEquals(type, x, y);
        }

        if (FoundNoCustomizations(membersForComparison)
            && TryGetEquals(type, out var equalsMethod))
        {
            TraceDebug("no customizations found and equality method found");
            return (bool)equalsMethod.Invoke(x, [y]);
        }

        foreach (var member in membersForComparison)
        {
            if (_ignoredMembers.Contains(member.Info))
            {
                TraceDebug($"member {member.Info.Name} ignored");
                continue;
            }

            if (_customMemberComparers.TryGetValue(member.Info, out var memberComparer))
            {
                TraceDebug($"member {member.Info.Name} has a custom comparer");
                if (!Invoke(memberComparer, member.X, member.Y))
                {
                    return false;
                }

                continue;
            }

            if (!EqualsImpl(member.ReturnType, member.X, member.Y))
            {
                return false;
            }
        }

        return true;
    }

    private static bool TryNullOrReferenceSucceeded(object o1, object o2, out bool result)
    {
        result = true;

        if (o1 == null && o2 == null)
        {
            return true;
        }

        if (o1 == null || o2 == null)
        {
            result = false;
            return true;
        }

        return ReferenceEquals(o1, o2);
    }

    private static bool Invoke(object comparer, object x, object y)
    {
        var method = comparer.GetType().GetMethod(InvokeMethod);
        Debug.Assert(method != null, nameof(method) + " != null");
        return (bool)method.Invoke(comparer, [x, y]);
    }

    private bool FoundNoCustomizations(List<FieldOrProperty> members)
    {
        var infos = members.Select(x => x.Info).ToList();
        return _ignoredMembers.All(x => !infos.Contains(x))
               && _customMemberComparers.Keys.All(x => !infos.Contains(x));
    }

    private static bool TryGetEquals(Type type, out MethodInfo info)
    {
        info = GetEqualsMethod(type, type);
        return info != null;
    }

    private static bool DefaultComparerEquals(Type type, object x, object y)
    {
        var equalityComparerType = typeof(EqualityComparer<>).MakeGenericType(type);
        var defaultGet = equalityComparerType.GetProperty(
            DefaultProperty,
            BindingFlags.Public | BindingFlags.Static);
        // ReSharper disable once PossibleNullReferenceException
        var defaultComparer = defaultGet.GetValue(null);

        var equalsMethod = GetEqualsMethod(equalityComparerType, type);
        return (bool)equalsMethod.Invoke(defaultComparer, [x, y]);
    }

    private static MethodInfo GetEqualsMethod(Type typeWithEqualsMethod, Type parameterType)
    {
        return typeWithEqualsMethod.GetMethods()
            .Where(m => m.Name == EqualsMethod)
            .FirstOrDefault(m => m.GetParameters().All(p => p.ParameterType == parameterType));
    }

    private static List<FieldOrProperty> GetAllMembersForComparison(
        Type type,
        object x,
        object y)
    {
        var result = new List<FieldOrProperty>();
        result.AddRange(
            type.GetFields(BindingFlags.Instance | BindingFlags.Public)
                .Select(fi => new FieldOrProperty
                {
                    Info = fi, ReturnType = fi.FieldType, X = fi.GetValue(x), Y = fi.GetValue(y)
                }));
        result.AddRange(
            type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(fi => fi.CanRead)
                .Where(fi => fi.GetIndexParameters().Length == 0)
                .Select(fi => new FieldOrProperty
                {
                    Info = fi, ReturnType = fi.PropertyType, X = fi.GetValue(x), Y = fi.GetValue(y)
                }));
        return result;
    }

    private void TraceInfo(string line)
    {
        _lineWriter?.Invoke(line);
    }

    private void TraceDebug(string line)
    {
        if (!_detailedTracing)
        {
            return;
        }

        TraceInfo(line);
    }

    private class FieldOrProperty
    {
        public MemberInfo Info { get; set; }
        public Type ReturnType { get; set; }
        public object X { get; set; }
        public object Y { get; set; }
    }
}
