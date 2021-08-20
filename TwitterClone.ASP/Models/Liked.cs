namespace TwitterClone.ASP.Models
{
    public class Liked
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int? UserId { get; set; }
    }
}
