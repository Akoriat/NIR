namespace NIR.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? BookName { get; set; }
        public string? Author { get; set; }
        public int Rating { get; set; } = 0;
        public int? GenreId { get; set; }
        public virtual Genre? Genres { get; set; }
    }
}
