namespace CRMProject.Entities
{
    public class ExpertNote
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreaterId { get; set; }
    }
}
