using System.Collections.Generic;

namespace TwitterClone.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int? PostId { get; set; }
        public int UserId { get; set; }
        public string TextAnswer { get; set; }
        public int NumberLikes { get; set; }       

        public virtual ICollection<LikedAnswer> Likes { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }

    }
}
