namespace NIR.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string? NameGenre { get; set; }
        public virtual ICollection<Book>? Books { get; set; }
    }
}
