namespace ChoreographyKata;

public record TheaterEvent
{
    private readonly string _name;
    private readonly int _value;

    public TheaterEvent(string name, int value)
    {
        _name = name;
        _value = value;
    }
}