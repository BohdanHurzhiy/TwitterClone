using System.Collections.Generic;

namespace TwitterClone.ASP.Models
{
    public class RelationshipsUser
    {
        public int Id { get; set; }
        public string FollowerId { get; set; }
        public string FollowedId { get; set; }
    }
}
