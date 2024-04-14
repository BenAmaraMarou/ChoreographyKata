namespace ChoreographyKata;

public sealed class Orchestration
{
    private readonly InventoryService _inventoryService;
    private readonly TicketingService _ticketingService;
    private readonly NotificationService _notificationService;

    public Orchestration(InventoryService inventoryService, 
        TicketingService ticketingService,
        NotificationService notificationService)
    {
        _inventoryService = inventoryService;
        _ticketingService = ticketingService;
        _notificationService = notificationService;
    }

    public void Launch(int numberOfSeats)
    {
        if (_inventoryService.DecrementCapacity(numberOfSeats))
        {
            _ticketingService.PrintTicket(numberOfSeats);
        }
        else
        {
            _notificationService.NotifyFailure(numberOfSeats);
        }
    }
}