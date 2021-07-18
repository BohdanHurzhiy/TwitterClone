using System.Collections.Generic;

namespace ModelsForTwitter
{
    public class User
    {
        public int Id { get; set; }
        public string NameUser { get; set; }
        public string AliasUser { get; set; }
        public string SecondNameUser { get; set; }
        public string PhotoUser { get; set; }
        public string MailUser { get; set; }
        public string NumberPhone { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Repost> RePosts { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Liked> Likes { get; set; }
        public virtual ICollection<LikedAnswer> LikedAnswers { get; set; }
        public virtual ICollection<RelationshipsUser> RelationshipsFolower { get; set; }
        public virtual ICollection<RelationshipsUser> RelationshipsFollowed { get; set; }
        public virtual ICollection<Photos> Photos { get; set; }
    }
}
