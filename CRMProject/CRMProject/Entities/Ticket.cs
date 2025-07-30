namespace CRMProject.Entities
{
    public sealed class Ticket
    {
        public int TicketId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ExternalLink { get; set; }

    }
}
