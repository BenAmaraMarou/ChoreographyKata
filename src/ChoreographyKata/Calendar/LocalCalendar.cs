namespace ChoreographyKata.Calendar;

public sealed class LocalCalendar : ICalendar
{
    public DateTime Now() => DateTime.Now;
}