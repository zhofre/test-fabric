namespace Sample.Library;

public class PersonFactory(IGuidGenerator guidGenerator)
{
    public Person Create(string name)
    {
        return new Person(
            guidGenerator.Generate(),
            name);
    }
}
