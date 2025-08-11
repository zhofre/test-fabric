namespace Sample.Library;

public class OrderFactory(IGuidGenerator guidGenerator, ITimeStampGenerator timeStampGenerator)
{
    public Order Create(Person customer)
    {
        return new Order(
            guidGenerator.Generate(),
            customer,
            timeStampGenerator.Generate());
    }
}
