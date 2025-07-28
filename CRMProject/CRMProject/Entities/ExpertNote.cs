namespace CRMProject.Entities
{
    public sealed class ExpertNote
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreaterId { get; set; }
    }
}
