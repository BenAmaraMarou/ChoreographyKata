namespace ChoreographyKata;

public static class DomainEventCatalog
{
    public const string BookingRequested = "BookingRequested";
    public const string InventoryReserved = "InventoryReserved";
    public const string InventoryUpdated = "InventoryUpdated";
    public const string CapacityExceeded = "CapacityExceeded";
    public const string TicketPrinted = "TicketPrinted";
    public const string NotificationSent = "NotificationSent";
}