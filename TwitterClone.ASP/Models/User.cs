using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace TwitterClone.ASP.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public string SecondName { get; set; }
        public string Photo { get; set; }
        public string NumberPhone { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Repost> RePosts { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Liked> Likes { get; set; }
        public virtual ICollection<LikedAnswer> LikedAnswers { get; set; }
        public virtual ICollection<RelationshipsUser> Followers { get; set; }
        public virtual ICollection<RelationshipsUser> Subscriptions { get; set; }
        public virtual ICollection<Photos> Photos { get; set; }
    }
}
