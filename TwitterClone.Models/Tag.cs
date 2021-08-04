using System.Collections.Generic;
namespace TwitterClone.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string TagsText { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Repost> Reposts { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }

    }
}
