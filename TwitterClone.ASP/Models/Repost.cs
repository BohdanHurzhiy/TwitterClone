using System.Collections.Generic;

namespace TwitterClone.ASP.Models
{
    public class Repost
    {
        public int Id { get; set; }
        public int? PostId { get; set; }       
        public string UserId { get; set; }        
        public string TextPost { get; set; }
        public int NumberLikes { get; set; }
        public int NumberAnswers { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Repost> RePosts { get; set; }
        public virtual ICollection<Liked> Likes { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
