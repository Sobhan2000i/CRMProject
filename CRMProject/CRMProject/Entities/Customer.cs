namespace CRMProject.Entities
{
    public sealed class Customer
    {
        public int CustomerId { get; set; }
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? CellPhone { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
        public CustomerLevel? Level { get; set; }
        public DateTime RegisteredAt { get; set; }
        public int LoyaltyScore {get; set;}
        public int Credit {get; set;}
        public int OpenTickets {get; set;}
        public long MobilePurchaseAmountLastSixMonths { get; set; }
        public long NonMobilePurchaseAmountLastSixMonths { get; set; }
        public DateTime LastLogin { get; set; }
        public TimeSpan TimeSinceLastPurchase { get; set; }
        public List<ExpertNote>? ExpertNotes { get; set; }
        public List<Ticket>? Tickets { get; set; }
        public DateTime? UpdatedAt {  get; set; }
        public int? UpdaterId { get; set; } 

    }

}
