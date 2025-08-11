using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using TestFabric.Data;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable StaticMemberInGenericType

namespace TestFabric;

public class TestSuite<TDataFactoryBuilder> where TDataFactoryBuilder : IFactoryBuilder, new()
{
    /// <summary>
    ///     Array of email domains used for generating test data.
    /// </summary>
    protected static readonly string[] EmailDomains =
    [
        "@example.com", "@testcorp.net", "@fictional.org", "@demo.io",
        "@sample.dev", "@company.com", "@test.org"
    ];

    /// <summary>
    ///     Array of first names used for generating test data.
    /// </summary>
    protected static readonly string[] FirstNames =
    [
        "John", "Jane", "Alex", "Sam", "Maria", "Pedro", "Luisa", "Ana", "Diego", "Isabel",
        "Carlos", "Valentina", "Federico", "Camila", "Rafael", "Esther", "Manuel", "Lucia",
        "David", "Sofía", "Alberto"
    ];

    /// <summary>
    ///     Array of last names used for generating test data.
    /// </summary>
    protected static readonly string[] LastNames =
    [
        "Smith", "Doe", "Johnson", "Brown",
        "Martinez", "Garcia", "Hernandez", "Lopez",
        "Perez", "Rivera", "Williams", "Jones", "Moore",
        "Taylor", "Anderson", "Thomas", "Jackson",
        "White", "Harris", "Martin", "Thompson",
        "Lee", "Young", "Allen", "King"
    ];

    /// <summary>
    ///     Array of fictional company names used for generating test data.
    /// </summary>
    protected static readonly string[] CompanyNames =
    [
        "NimbusWorks", "Aurora Dynamics", "Quantum Harbor Labs", "BluePine Robotics",
        "Starlight Logistics", "VerdantGrid Energy", "Crimson Peak Analytics", "Silverleaf Financial",
        "EchoWave Media", "Ironclad Fabrication", "Celestial Cartography", "GoldenSprout Foods",
        "MarinerNet Shipping", "PixelForge Studios", "EverTrust Insurance", "Skyline Biotech",
        "CopperTrail Mining", "NovaNest Realty", "WhisperWind Apparel", "TidalSpark Software"
    ];

    /// <summary>
    ///     Array of country names used for generating test data.
    /// </summary>
    protected static readonly string[] Countries =
    [
        "USA", "Canada", "Mexico", "Brazil", "Argentina", "Chile", "Peru", "Colombia", "Venezuela",
        "Germany", "France", "Italy", "Spain", "Belgium", "Netherlands", "United Kingdom", "Sweden",
        "Denmark", "Norway", "Switzerland", "Australia", "New Zealand", "UK", "Italy", "Portugal",
        "Greece", "Cyprus", "Finland"
    ];

    /// <summary>
    ///     Dictionary of countries and their associated cities used for generating test data.
    /// </summary>
    protected static readonly Dictionary<string, string[]> Cities = new()
    {
        { "USA", ["New York", "Los Angeles", "Chicago", "Houston", "Miami"] },
        { "Canada", ["Toronto", "Vancouver", "Montreal", "Calgary", "Ottawa"] },
        { "Mexico", ["Mexico City", "Guadalajara", "Monterrey", "Puebla", "Tijuana"] },
        { "Brazil", ["São Paulo", "Rio de Janeiro", "Brasília", "Salvador", "Fortaleza"] },
        { "Argentina", ["Buenos Aires", "Córdoba", "Rosario", "Mendoza", "La Plata"] },
        { "Chile", ["Santiago", "Valparaíso", "Concepción", "Antofagasta", "Temuco"] },
        { "Peru", ["Lima", "Arequipa", "Trujillo", "Cusco", "Chiclayo"] },
        { "Colombia", ["Bogotá", "Medellín", "Cali", "Barranquilla", "Cartagena"] },
        { "Venezuela", ["Caracas", "Maracaibo", "Valencia", "Barquisimeto", "Maracay"] },
        { "Germany", ["Berlin", "Munich", "Hamburg", "Frankfurt", "Cologne"] },
        { "France", ["Paris", "Marseille", "Lyon", "Toulouse", "Nice"] },
        { "Italy", ["Rome", "Milan", "Naples", "Turin", "Florence"] },
        { "Spain", ["Madrid", "Barcelona", "Valencia", "Seville", "Bilbao"] },
        { "Belgium", ["Brussels", "Antwerp", "Ghent", "Bruges", "Leuven"] },
        { "Netherlands", ["Amsterdam", "Rotterdam", "The Hague", "Utrecht", "Eindhoven"] },
        { "United Kingdom", ["London", "Manchester", "Birmingham", "Glasgow", "Liverpool"] },
        { "Sweden", ["Stockholm", "Gothenburg", "Malmö", "Uppsala", "Västerås"] },
        { "Denmark", ["Copenhagen", "Aarhus", "Odense", "Aalborg", "Esbjerg"] },
        { "Norway", ["Oslo", "Bergen", "Trondheim", "Stavanger", "Drammen"] },
        { "Switzerland", ["Zurich", "Geneva", "Basel", "Bern", "Lausanne"] },
        { "Australia", ["Sydney", "Melbourne", "Brisbane", "Perth", "Adelaide"] },
        { "New Zealand", ["Auckland", "Wellington", "Christchurch", "Hamilton", "Dunedin"] },
        { "UK", ["London", "Manchester", "Bristol", "Leeds", "Edinburgh"] },
        { "Portugal", ["Lisbon", "Porto", "Braga", "Coimbra", "Faro"] },
        { "Greece", ["Athens", "Thessaloniki", "Patras", "Heraklion", "Larissa"] },
        { "Cyprus", ["Nicosia", "Limassol", "Larnaca", "Paphos", "Famagusta"] },
        { "Finland", ["Helsinki", "Espoo", "Tampere", "Vantaa", "Oulu"] }
    };

    // ReSharper disable once StaticMemberInGenericType
    // it is intended that there is a different instance per type of test class
    protected static readonly IFactory Factory;

    static TestSuite()
    {
        Factory = new TDataFactoryBuilder()
            .Create();
    }

    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    ///     Sets the current culture for both CurrentCulture and CurrentUICulture.
    /// </summary>
    /// <param name="info">The CultureInfo to set as the current culture.</param>
    protected void SetCurrentCulture(CultureInfo info)
    {
        Thread.CurrentThread.CurrentCulture = info;
        Thread.CurrentThread.CurrentUICulture = info;
    }

    /// <summary>
    ///     Sets the current culture for both CurrentCulture and CurrentUICulture.
    /// </summary>
    /// <param name="culture">The name of the culture info to set as the current culture.</param>
    protected void SetCurrentCulture(string culture)
    {
        SetCurrentCulture(CultureInfo.CreateSpecificCulture(culture));
    }

    /// <summary>
    ///     Sets the current culture for both CurrentCulture and CurrentUICulture to Invariant.
    /// </summary>
    protected void SetCurrentCultureInvariant()
    {
        SetCurrentCulture(CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///     Creates an anonymous object.
    /// </summary>
    /// <typeparam name="T">type to create</typeparam>
    /// <returns>anonymous object</returns>
    protected static T Random<T>()
    {
        return Factory.Create<T>();
    }

    /// <summary>
    ///     Creates an anonymous object from a range.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="minInclusive">minimum value in range</param>
    /// <param name="maxExclusive">maximum value not in range</param>
    /// <returns>object in the provided range</returns>
    protected static T InRange<T>(T minInclusive, T maxExclusive) where T : IComparable<T>
    {
        return Factory.CreateFromRange(minInclusive, maxExclusive);
    }

    /// <summary>
    ///     Creates an anonymous object from a range.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items">values in range</param>
    /// <returns>object from the provided items</returns>
    protected static T InRange<T>(IEnumerable<T> items)
    {
        return Factory.CreateFromRange(items);
    }

    /// <summary>
    ///     Returns a random first name from the predefined list.
    /// </summary>
    /// <returns>A string representing a random first name.</returns>
    protected static string FirstName()
    {
        return InRange(FirstNames);
    }

    /// <summary>
    ///     Returns a random city for a given country.
    /// </summary>
    /// <param name="country">The country to get the city from.</param>
    /// <returns>A string representing a random city in the specified country.</returns>
    protected static string City(string country)
    {
        if (Cities.TryGetValue(country, out var cities))
        {
            return InRange(cities);
        }

        throw new ArgumentException("Country not found", nameof(country));
    }

    /// <summary>
    ///     Retrieves a random last name from the predefined list.
    /// </summary>
    /// <returns>A random last name as a string.</returns>
    protected static string LastName()
    {
        return InRange(LastNames);
    }

    /// <summary>
    ///     Generates a random full name by concatenating a random first name and last name.
    /// </summary>
    /// <param name="hasSecondLastName">Indicates whether to use two last names in the full name (e.g. Latin America).</param>
    /// <returns>A string representing a random full name.</returns>
    protected static string FullName(bool hasSecondLastName = false)
    {
        var last = LastName();
        if (hasSecondLastName)
        {
            last += " " + LastName();
        }

        return $"{FirstName()} {last}";
    }

    /// <summary>
    ///     Generates a random email address.
    /// </summary>
    /// <returns>A string representing a random email address in the format "first.last@domain.com".</returns>
    protected static string Email()
    {
        return $"{FirstName()}.{LastName()}".ToLower() + InRange(EmailDomains);
    }

    /// <summary>
    ///     Generates a random company name.
    /// </summary>
    /// <returns>A randomly selected company name.</returns>
    protected static string CompanyName()
    {
        return InRange(CompanyNames);
    }

    /// <summary>
    ///     Generates a random country name from the predefined list.
    /// </summary>
    /// <returns>A string representing a country name.</returns>
    protected static string Country()
    {
        return InRange(Countries);
    }

    /// <summary>
    ///     Generates a random date and time within a specified number of days back from the current date.
    /// </summary>
    /// <param name="daysBack">
    ///     The maximum number of days back from today for the generated date. Defaults to 30 if not
    ///     specified.
    /// </param>
    /// <returns>A <see cref="DateTime" /> object representing a random date and time within the specified range.</returns>
    protected static DateTime RecentDateTime(int daysBack = 30)
    {
        return RecentDateTime(TimeSpan.FromDays(daysBack));
    }

    /// <summary>
    ///     Generates a recent random <see cref="DateTime" /> within the specified range.
    /// </summary>
    /// <param name="maximumTimeBack">The maximum TimeSpan to go back from the current date and time.</param>
    /// <returns>A DateTime value representing a recent date and time.</returns>
    protected static DateTime RecentDateTime(TimeSpan maximumTimeBack)
    {
        var ticks = -InRange(0, maximumTimeBack.Ticks);
        return DateTime.Now.Add(TimeSpan.FromTicks(ticks));
    }

    /// <summary>
    ///     Generates a recent <see cref="DateTimeOffset" /> by subtracting a random number of days
    ///     within the specified range (0 to <paramref name="daysBack" />) from the current date and time.
    /// </summary>
    /// <param name="daysBack">
    ///     The maximum number of days to subtract from the current date to calculate the recent date.
    ///     Defaults to 30 if no value is provided.
    /// </param>
    /// <returns>
    ///     A <see cref="DateTimeOffset" /> that represents a date and time randomly chosen within the range of 0 to the
    ///     specified number of days back.
    /// </returns>
    protected static DateTimeOffset RecentDateTimeOffset(int daysBack = 30)
    {
        return RecentDateTimeOffset(TimeSpan.FromDays(daysBack));
    }

    /// <summary>
    ///     Generates a recent random <see cref="DateTimeOffset" /> within the specified range.
    /// </summary>
    /// <param name="maximumTimeBack">The maximum TimeSpan to go back from the current date and time.</param>
    /// <returns>A DateTimeOffset value representing a recent date and time.</returns>
    protected static DateTimeOffset RecentDateTimeOffset(TimeSpan maximumTimeBack)
    {
        var ticks = -InRange(0, maximumTimeBack.Ticks);
        return DateTimeOffset.Now.Add(TimeSpan.FromTicks(ticks));
    }

    /// <summary>
    ///     Creates a new instance of type <typeparamref name="T" /> by copying the properties
    ///     from a given template object and applying the specified overrides.
    /// </summary>
    /// <typeparam name="T">The type of the object to create.</typeparam>
    /// <param name="template">The template instance from which property values are copied.</param>
    /// <param name="overrides">
    ///     An array of lambda expressions specifying the properties to override in the new object.
    /// </param>
    /// <returns>A new instance of type <typeparamref name="T" /> with the specified overrides applied.</returns>
    protected static T FromTemplate<T>(T template, params Expression<Func<T, object>>[] overrides)
    {
        var properties = GetPropertyInfos<T>();
        var overriddenProperties = GetOverriddenProperties(overrides);
        return FromTemplate(template, properties, overriddenProperties);
    }

    /// <summary>
    ///     Creates a sequence of objects by copying a given template and applying optional property overrides to each.
    /// </summary>
    /// <param name="count">The number of objects to generate.</param>
    /// <param name="template">The template object to use as the basis for each generated item.</param>
    /// <param name="overrides">
    ///     An array of lambda expressions specifying which properties to override in the generated objects
    ///     and their values.
    /// </param>
    /// <returns>An enumerable containing the generated objects based on the template and applied overrides.</returns>
    protected static IEnumerable<T> FromTemplate<T>(
        int count,
        T template,
        params Expression<Func<T, object>>[] overrides)
    {
        var properties = GetPropertyInfos<T>();
        var overriddenProperties = GetOverriddenProperties(overrides);
        for (var i = 0; i < count; i++)
        {
            yield return FromTemplate(template, properties, overriddenProperties);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static PropertyInfo[] GetPropertyInfos<T>()
    {
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        return properties;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string[] GetOverriddenProperties<T>(Expression<Func<T, object>>[] overrides)
    {
        var overriddenProperties = overrides
            .Select(expr =>
            {
                var success = expr.TryGetMemberInfo(out var memberInfo);
                return success ? memberInfo : null;
            })
            .Where(x => x != null)
            .Select(info => info.Name)
            .ToArray();
        return overriddenProperties;
    }

    private static T FromTemplate<T>(T template, PropertyInfo[] properties, string[] overriddenProperties)
    {
        var result = Random<T>();

        foreach (var prop in properties)
        {
            if (!prop.CanWrite)
            {
                continue;
            }

            if (overriddenProperties.Contains(prop.Name))
            {
                continue;
            }

            var value = prop.GetValue(template);
            prop.SetValue(result, value);
        }

        return result;
    }
}
