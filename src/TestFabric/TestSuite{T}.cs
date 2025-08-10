using System.Globalization;
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
}
