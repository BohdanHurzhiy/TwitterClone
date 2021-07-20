using System.Collections.Generic;

namespace TwitterClone.Models
{
    public class Repost
    {
        public int Id { get; set; }
        public int? PostId { get; set; }       
        public int UserId { get; set; }        
        public string TextPost { get; set; }
        public int NumberLikes { get; set; }
        public int NumberAnswers { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Repost> RePosts { get; set; }
        public virtual ICollection<Liked> Likes { get; set; }
        public virtual ICollection<TagsPost> Tags_posts { get; set; }
    }
}
