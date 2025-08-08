namespace TestFabric.Data;

/// <summary>
///     Exception thrown by <see cref="Factory" /> when an error occurs during object creation.
/// </summary>
/// <param name="methodName">The method that caused the exception.</param>
/// <param name="innerException">The inner exception that occurred.</param>
public class FactoryException(
    string methodName,
    Exception innerException) : Exception($"An exception occurred in {methodName}.",
    innerException);
