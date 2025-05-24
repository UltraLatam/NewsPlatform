namespace NewsPlatformApp.Models
{
    public class DetallePostViewModel
    {
        public Post Post { get; set; } = new();
        public User? Autor { get; set; }
        public List<Comment> Comentarios { get; set; } = new();
        public Feedback? Feedback { get; set; }
    }
}
