namespace TestFabric.Test;

public class EqualityComparerBuilderTests
{
    private static readonly Fixture _fix = new();

    #region Other

    [Fact]
    public void Given_ArraysWithDifferentLengths_When_Equals_Then_False()
    {
        // Arrange
        var array1 = new[] { 1, 2, 3 };
        var array2 = new[] { 1, 2 };
        var sut = new EqualityComparerBuilder<int[]>().Create();

        // Act
        var result = sut.Equals(array1, array2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Given_IListNotList_When_Equals_Then_UsesOtherComparison()
    {
        // Arrange
        IList<int> list1 = new List<int> { 1, 2, 3 };
        IList<int> list2 = new List<int> { 1, 2, 3 };
        var sut = new EqualityComparerBuilder<IList<int>>().Create();

        // Act
        var result = sut.Equals(list1, list2);

        // Assert
        Assert.False(result);
    }

    #endregion

    #region DummyA

    public class DummyA
    {
        public int Field;
        public string? Property { get; set; }
    }

    private static DummyA Clone(DummyA obj)
    {
        return new DummyA { Field = obj.Field, Property = obj.Property };
    }

    [Fact]
    public void Given_DummyANullExpression_When_TryGetMemberInfo_Then_FalseAndNull()
    {
        // Arrange
        // Act
        var actual = Extension.TryGetMemberInfo<DummyA, int>(
            null,
            out _);

        // Assert
        Assert.False(actual);
    }

    [Fact]
    public void Given_DummyAFieldExpression_When_TryGetMemberInfo_Then_TrueAndMemberInfo()
    {
        // Arrange
        var expectedMemberInfo = typeof(DummyA).GetMember(nameof(DummyA.Field))[0];

        // Act
        var actual = Extension.TryGetMemberInfo<DummyA, int>(
            x => x.Field,
            out var actualMemberInfo);

        // Assert
        Assert.True(actual);
        Assert.Equal(expectedMemberInfo, actualMemberInfo);
    }

    [Fact]
    public void Given_DummyABoxedFieldExpression_When_TryGetMemberInfo_Then_TrueAndMemberInfo()
    {
        // Arrange
        var expectedMemberInfo = typeof(DummyA).GetMember(nameof(DummyA.Field))[0];

        // Act
        var actual = Extension.TryGetMemberInfo<DummyA, object>(
            x => x.Field,
            out var actualMemberInfo);

        // Assert
        Assert.True(actual);
        Assert.Equal(expectedMemberInfo, actualMemberInfo);
    }

    [Fact]
    public void Given_DummyAPropertyExpression_When_TryGetMemberInfo_Then_TrueAndMemberInfo()
    {
        // Arrange
        var expectedMemberInfo = typeof(DummyA).GetMember(nameof(DummyA.Property))[0];

        // Act
        var actual = Extension.TryGetMemberInfo<DummyA, string?>(
            x => x.Property,
            out var actualMemberInfo);

        // Assert
        Assert.True(actual);
        Assert.Equal(expectedMemberInfo, actualMemberInfo);
    }

    [Fact]
    public void Given_ConstantExpression_When_TryGetMemberInfo_Then_FalseAndNull()
    {
        // Arrange
        // Act
        var actual = Extension.TryGetMemberInfo<DummyA, string>(
            x => "constant value", // Returns a constant, not a member
            out var actualMemberInfo);

        // Assert
        Assert.False(actual);
        Assert.Null(actualMemberInfo);
    }

    [Theory]
    [MemberData(nameof(GetDefaultDummyATheoryData))]
    public void Given_Dummies_When_Equals_Then_Expected(
        DummyA? x,
        DummyA? y,
        bool expected)
    {
        // Arrange
        var sut = new EqualityComparerBuilder<DummyA>()
            .Create();

        // Act
        var actual = sut.Equals(x, y);

        // Act
        Assert.Equal(expected, actual);
    }

    public static TheoryData<DummyA?, DummyA?, bool> GetDefaultDummyATheoryData()
    {
        var dummy = _fix.Create<DummyA>();
        var dummyClone = new DummyA { Field = dummy.Field, Property = dummy.Property };
        var other = _fix.Create<DummyA>();
        var dummyToLower = new DummyA { Field = dummy.Field, Property = dummy.Property?.ToLowerInvariant() };

        return new TheoryData<DummyA?, DummyA?, bool>
        {
            { null, null, true },
            { dummy, null, false },
            { null, dummy, false },
            { dummy, dummy, true },
            { dummy, dummyClone, true },
            { dummy, other, false },
            { dummy, dummyToLower, false }
        };
    }

    [Theory]
    [MemberData(nameof(GetIgnoreCaseDummyATheoryData))]
    public void Given_DummiesAndIgnoreStringCase_When_Equals_Then_Expected(
        DummyA? x,
        DummyA? y,
        bool expected)
    {
        // Arrange
        var sut = new EqualityComparerBuilder<DummyA>()
            .ConfigureType((string s1, string s2) => string.Equals(s1, s2, StringComparison.InvariantCultureIgnoreCase))
            .Create();

        // Act
        var actual = sut.Equals(x, y);

        // Act
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Given_DummiesAndAlwaysFalseStringCase_When_Equals_Then_Expected()
    {
        // Arrange
        var x = _fix.Create<DummyA>();
        var y = Clone(x);
        var sut = new EqualityComparerBuilder<DummyA>()
            .ConfigureType((string _, string _) => false)
            .Create();

        // Act
        var actual = sut.Equals(x, y);

        // Act
        Assert.False(actual);
    }

    [Theory]
    [MemberData(nameof(GetIgnoreCaseDummyATheoryData))]
    public void Given_DummiesAndIgnoreStringCaseFromProperty_When_Equals_Then_Expected(
        DummyA? d1,
        DummyA? d2,
        bool expected)
    {
        // Arrange
        var sut = new EqualityComparerBuilder<DummyA>()
            .ConfigureMember(
                x => x.Property,
                (s1, s2) => string.Equals(s1, s2, StringComparison.InvariantCultureIgnoreCase))
            .Create();

        // Act
        var actual = sut.Equals(d1, d2);

        // Act
        Assert.Equal(expected, actual);
    }

    public static TheoryData<DummyA?, DummyA?, bool> GetIgnoreCaseDummyATheoryData()
    {
        var dummy = _fix.Create<DummyA>();
        var dummyClone = new DummyA { Field = dummy.Field, Property = dummy.Property };
        var other = _fix.Create<DummyA>();
        var dummyToLower = new DummyA { Field = dummy.Field, Property = dummy.Property?.ToLowerInvariant() };

        return new TheoryData<DummyA?, DummyA?, bool>
        {
            { null, null, true },
            { dummy, null, false },
            { null, dummy, false },
            { dummy, dummy, true },
            { dummy, dummyClone, true },
            { dummy, other, false },
            { dummy, dummyToLower, true }
        };
    }


    [Theory]
    [MemberData(nameof(GetIgnoreFieldATheoryData))]
    public void Given_DummiesAndIgnoreField_When_Equals_Then_Expected(
        DummyA? d1,
        DummyA? d2,
        bool expected)
    {
        // Arrange
        var sut = new EqualityComparerBuilder<DummyA>()
            .IgnoreMember(x => x.Field)
            .Create();

        // Act
        var actual = sut.Equals(d1, d2);

        // Act
        Assert.Equal(expected, actual);
    }

    public static TheoryData<DummyA?, DummyA?, bool> GetIgnoreFieldATheoryData()
    {
        var dummy = _fix.Create<DummyA>();
        var dummyClone = new DummyA { Field = dummy.Field, Property = dummy.Property };
        var other = _fix.Create<DummyA>();
        var otherWithSameProperty = new DummyA { Field = other.Field, Property = dummy.Property };

        return new TheoryData<DummyA?, DummyA?, bool>
        {
            { null, null, true },
            { dummy, null, false },
            { null, dummy, false },
            { dummy, dummy, true },
            { dummy, dummyClone, true },
            { dummy, other, false },
            { dummy, otherWithSameProperty, true }
        };
    }

    [Fact]
    public void Given_FirstNullSecondNotNull_When_Equals_Then_False()
    {
        // Arrange
        var dummy = _fix.Create<DummyA>();
        var sut = new EqualityComparerBuilder<DummyA>().Create();

        // Act
        var result = sut.Equals(null, dummy);

        // Assert
        Assert.False(result);
    }

    #endregion

    #region DummyB

    public class DummyB
    {
        public List<int>? Numbers { get; init; }
        public string[]? Lines { get; init; }
    }

    private static DummyB Clone(DummyB obj)
    {
        return new DummyB { Lines = obj.Lines?.ToArray(), Numbers = obj.Numbers?.ToList() };
    }

    [Fact]
    public void Given_DummyBNumbersExpression_When_TryGetMemberInfo_Then_TrueAndMemberInfo()
    {
        // Arrange
        var expectedMemberInfo = typeof(DummyB).GetMember(nameof(DummyB.Numbers))[0];

        // Act
        var actual = Extension.TryGetMemberInfo<DummyB, List<int>?>(
            x => x.Numbers,
            out var actualMemberInfo);

        // Assert
        Assert.True(actual);
        Assert.Equal(expectedMemberInfo, actualMemberInfo);
    }

    [Theory]
    [MemberData(nameof(GetDefaultDummyBTheoryData))]
    public void Given_DummyBs_When_Equals_Then_Expected(
        DummyB? d1,
        DummyB? d2,
        bool expected)
    {
        // Arrange
        var sut = new EqualityComparerBuilder<DummyB>()
            .Create();

        // Act
        var actual = sut.Equals(d1, d2);

        // Assert
        Assert.Equal(expected, actual);
    }

    public static TheoryData<DummyB?, DummyB?, bool> GetDefaultDummyBTheoryData()
    {
        var dummy = _fix.Create<DummyB>();
        var otherWithReferenceFields = new DummyB { Numbers = dummy.Numbers, Lines = dummy.Lines };
        var otherWithCopyFields = new DummyB { Numbers = dummy.Numbers?.ToList(), Lines = dummy.Lines?.ToArray() };
        var otherWithNullLines = new DummyB { Numbers = dummy.Numbers?.ToList(), Lines = null };

        return new TheoryData<DummyB?, DummyB?, bool>
        {
            { null, null, true },
            { dummy, null, false },
            { null, dummy, false },
            { dummy, dummy, true },
            { dummy, otherWithReferenceFields, true },
            { dummy, otherWithCopyFields, true },
            { dummy, otherWithNullLines, false }
        };
    }

    [Theory]
    [MemberData(nameof(GetStringTypeIgnoreCaseDummyBTheoryData))]
    public void Given_DummyBsAndStringTypeIgnoreCase_When_Equals_Then_Expected(
        DummyB? d1,
        DummyB? d2,
        bool expected)
    {
        // Arrange
        var sut = new EqualityComparerBuilder<DummyB>()
            .ConfigureType((string s1, string s2) => string.Equals(s1, s2, StringComparison.InvariantCultureIgnoreCase))
            .Create();

        // Act
        var actual = sut.Equals(d1, d2);

        // Assert
        Assert.Equal(expected, actual);
    }

    public static TheoryData<DummyB?, DummyB?, bool> GetStringTypeIgnoreCaseDummyBTheoryData()
    {
        var dummy = _fix.Create<DummyB>();
        var otherWithReferenceFields = new DummyB { Numbers = dummy.Numbers, Lines = dummy.Lines };
        var otherWithCopyFields = new DummyB { Numbers = dummy.Numbers?.ToList(), Lines = dummy.Lines?.ToArray() };
        var otherWithLinesToLowerInvariant = new DummyB
        {
            Numbers = dummy.Numbers?.ToList(), Lines = dummy.Lines?.Select(x => x.ToLowerInvariant()).ToArray()
        };

        return new TheoryData<DummyB?, DummyB?, bool>
        {
            { null, null, true },
            { dummy, null, false },
            { null, dummy, false },
            { dummy, dummy, true },
            { dummy, otherWithReferenceFields, true },
            { dummy, otherWithCopyFields, true },
            { dummy, otherWithLinesToLowerInvariant, true }
        };
    }

    private class AlwaysTrueStringComparer : IEqualityComparer<string?>
    {
        public bool Equals(string? x, string? y)
        {
            return true;
        }

        public int GetHashCode(string obj)
        {
            throw new NotImplementedException();
        }
    }

    [Fact]
    public void Given_StringAndAlwaysTrueStringComparer_When_GetHashCode_Then_NotImplementedException()
    {
        // Arrange
        var sut = new AlwaysTrueStringComparer();
        var x = _fix.Create<string>();

        // Act + Assert
        Assert.Throws<NotImplementedException>(() => sut.GetHashCode(x));
    }

    [Theory]
    [MemberData(nameof(GetAlwaysTrueItemComparerOnArrayDummyBTheoryData))]
    public void Given_DummyBsAndAlwaysTrueItemComparerOnArray_When_Equals_Then_Expected(
        DummyB? d1,
        DummyB? d2,
        bool expected)
    {
        // Arrange
        var arrayComparer = new ArrayEqualityComparer<string>(
            new AlwaysTrueStringComparer());
        var sut = new EqualityComparerBuilder<DummyB>()
            .ConfigureMember(
                x => x.Lines,
                arrayComparer)
            .Create();

        // Act
        var actual = sut.Equals(d1, d2);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(GetAlwaysTrueItemComparerOnArrayDummyBTheoryData))]
    public void Given_DummyBsAndAlwaysTrueOnString_When_Equals_Then_Expected(
        DummyB? d1,
        DummyB? d2,
        bool expected)
    {
        // Arrange
        var sut = new EqualityComparerBuilder<DummyB>()
            .ConfigureType(new AlwaysTrueStringComparer())
            .Create();

        // Act
        var actual = sut.Equals(d1, d2);

        // Assert
        Assert.Equal(expected, actual);
    }

    public static TheoryData<DummyB?, DummyB?, bool> GetAlwaysTrueItemComparerOnArrayDummyBTheoryData()
    {
        var dummy = _fix.Create<DummyB>();
        var dummy2 = new DummyB { Numbers = dummy.Numbers, Lines = new string[dummy.Lines!.Length] };
        var dummy2WrongCount = new DummyB { Numbers = dummy.Numbers, Lines = new string[dummy.Lines.Length + 4] };

        return new TheoryData<DummyB?, DummyB?, bool>
        {
            { null, null, true },
            { dummy, null, false },
            { null, dummy, false },
            { dummy, dummy, true },
            { dummy, dummy2, true },
            { dummy, dummy2WrongCount, false }
        };
    }

    #endregion

    #region DummyC (nested Dummies)

    public class DummyC
    {
        public List<DummyA>? ListField;
        public string? OtherField;
        public DummyB? MyProperty { get; init; }
    }

    [Theory]
    [MemberData(nameof(GetDefaultDummyCTheoryData))]
    public void Given_TwoDummyCs_When_Equals_Then_Expected(
        DummyC? d1,
        DummyC? d2,
        bool expected)
    {
        // Arrange
        var sut = new EqualityComparerBuilder<DummyC>()
            .Create();

        // Act
        var actual = sut.Equals(d1, d2);

        // Assert
        Assert.Equal(expected, actual);
    }

    public static TheoryData<DummyC?, DummyC?, bool> GetDefaultDummyCTheoryData()
    {
        var dummy = _fix.Create<DummyC>();
        var dummyClone = new DummyC
        {
            ListField = dummy.ListField?.Select(Clone).ToList(),
            MyProperty = Clone(dummy.MyProperty!),
            OtherField = dummy.OtherField
        };
        var differentProperty = new DummyC
        {
            ListField = dummy.ListField!.Select(Clone).ToList(),
            MyProperty = _fix.Create<DummyB>(),
            OtherField = dummy.OtherField
        };

        return new TheoryData<DummyC?, DummyC?, bool>
        {
            { null, null, true },
            { dummy, null, false },
            { null, dummy, false },
            { dummy, dummy, true },
            { dummy, dummyClone, true },
            { dummy, differentProperty, false }
        };
    }

    [Fact]
    public void Given_TwoDummyCsWithDifferentMyPropertyAndIgnoreDummyBProperties_When_Equals_Then_True()
    {
        // Arrange
        var d1 = _fix.Create<DummyC>();
        var d2 = new DummyC
        {
            ListField = d1.ListField!.Select(Clone).ToList(),
            MyProperty = _fix.Create<DummyB>(),
            OtherField = d1.OtherField
        };
        var sut = new EqualityComparerBuilder<DummyC>()
            .IgnoreMember<DummyB, List<int>>(x => x.Numbers!)
            .IgnoreMember<DummyB, string[]>(x => x.Lines!)
            .Create();

        // Act
        var actual = sut.Equals(d1, d2);

        // Assert
        Assert.True(actual);
    }

    [Fact]
    public void Given_TwoDummyCsWithDifferentMyPropertyAndIgnoreDummyBType_When_Equals_Then_True()
    {
        // Arrange
        var d1 = _fix.Create<DummyC>();
        var d2 = new DummyC
        {
            ListField = d1.ListField!.Select(Clone).ToList(),
            MyProperty = _fix.Create<DummyB>(),
            OtherField = d1.OtherField
        };
        var sut = new EqualityComparerBuilder<DummyC>()
            .IgnoreType<DummyB>()
            .Create();

        // Act
        var actual = sut.Equals(d1, d2);

        // Assert
        Assert.True(actual);
    }

    [Fact]
    public void Given_TwoDummyCsWithDifferentOtherFieldAndAlwaysTrueStringProperty_When_Equals_Then_True()
    {
        // Arrange
        var d1 = _fix.Create<DummyC>();
        var d2 = new DummyC
        {
            ListField = d1.ListField!.Select(Clone).ToList(),
            MyProperty = Clone(d1.MyProperty!),
            OtherField = _fix.Create<string>()
        };
        var sut = new EqualityComparerBuilder<DummyC>()
            .ConfigureMember(
                x => x.OtherField,
                new AlwaysTrueStringComparer())
            .Create();

        // Act
        var actual = sut.Equals(d1, d2);

        // Assert
        Assert.True(actual);
    }

    [Fact]
    public void Given_TwoDummyCsWithDifferentOtherFieldAndDummyAPropertyAlwaysTrue_When_Equals_Then_True()
    {
        // Arrange
        var d1 = _fix.Create<DummyC>();
        var d2 = new DummyC
        {
            ListField = d1.ListField
                !.Select(x =>
                {
                    var y = Clone(x);
                    y.Property = _fix.Create<string>();
                    return y;
                })
                .ToList(),
            MyProperty = Clone(d1.MyProperty!),
            OtherField = d1.OtherField
        };
        var sut = new EqualityComparerBuilder<DummyC>()
            .ConfigureMember<DummyA, string>(
                x => x.Property!,
                new AlwaysTrueStringComparer())
            .Create();

        // Act
        var actual = sut.Equals(d1, d2);

        // Assert
        Assert.NotEqual(d1.ListField![0].Property, d2.ListField[0].Property);
        Assert.True(actual);
    }

    [Fact]
    public void Given_TwoDummyCsClones_When_Equals_Then_Traced()
    {
        // Arrange
        var d1 = _fix.Create<DummyC>();
        var d2 = new DummyC
        {
            ListField = d1.ListField
                !.Select(Clone)
                .ToList(),
            MyProperty = Clone(d1.MyProperty!),
            OtherField = d1.OtherField
        };
        var tracing = new List<string>();
        var sut = new EqualityComparerBuilder<DummyC>()
            .EnableTracing(s => tracing.Add(s), true)
            .Create();

        // Act
        var actual = sut.Equals(d1, d2);

        // Assert
        Assert.True(actual);
        Assert.NotEmpty(tracing);
    }

    #endregion

    #region DummyD (nullable)

    public class DummyD
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public int? MyProperty { get; set; }
    }

    [Theory]
    [MemberData(nameof(GetDefaultDummyTheoryData))]
    public void Given_TwoDummyDs_When_Equals_Then_Expected(
        DummyD? d1,
        DummyD? d2,
        bool expected)
    {
        // Arrange
        var sut = new EqualityComparerBuilder<DummyD>()
            .Create();

        // Act
        var actual = sut.Equals(d1, d2);

        // Assert
        Assert.Equal(expected, actual);
    }

    public static TheoryData<DummyD?, DummyD?, bool> GetDefaultDummyTheoryData()
    {
        var dummy = _fix.Create<DummyD>();
        var dummyWithNull = new DummyD { MyProperty = null };
        return new TheoryData<DummyD?, DummyD?, bool>
        {
            { null, null, true },
            { dummy, null, false },
            { null, dummy, false },
            { dummy, dummy, true },
            { dummy, dummyWithNull, false }
        };
    }

    #endregion

    #region DummyE (circular)

    public class DummyEParent
    {
        public Guid Id { get; init; }
        public List<DummyEChild>? Children { get; init; }
    }

    public class DummyEChild
    {
        public DummyEParent? Parent;
        public int Value { get; init; }
    }

    [Fact]
    public void Given_CircularListAndCustomSetup_When_Equals_Then_True()
    {
        // Arrange
        var children = _fix
            .Build<DummyEChild>()
            .Without(x => x.Parent)
            .CreateMany()
            .ToList();
        var parent = CreateParent(_fix.Create<Guid>(), children);
        var childrenClone = children
            .Select(x => new DummyEChild { Value = x.Value })
            .ToList();
        var parentClone = CreateParent(parent.Id, childrenClone);

        var sut = new EqualityComparerBuilder<DummyEParent>()
            .ConfigureMember<DummyEChild, DummyEParent>(
                x => x.Parent!,
                (o1, o2) => o1.Id == o2.Id)
            .Create();

        // Act
        var actual = sut.Equals(parent, parentClone);

        // Assert
        Assert.True(actual);
    }

    private static DummyEParent CreateParent(Guid id, List<DummyEChild> children)
    {
        var parent = new DummyEParent { Id = id, Children = children };

        foreach (var child in parent.Children)
        {
            child.Parent = parent;
        }

        return parent;
    }

    #endregion
}
