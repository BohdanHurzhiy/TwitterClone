namespace TwitterClone.Models
{
    public class Photos
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string PathOnTheServer { get; set; }
        public string Extension { get; set; }       

    }
}
