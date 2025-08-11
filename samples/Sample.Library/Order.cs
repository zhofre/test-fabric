namespace Sample.Library;

public record Order(Guid Id, Person Customer, DateTimeOffset OrderDate);
