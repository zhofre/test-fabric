namespace Sample.Library;

public class PersonFactory(IGuidGenerator guidGenerator)
{
    public Person Create(string name, int age)
    {
        return new Person(
            guidGenerator.Generate(),
            name,
            age);
    }
}
