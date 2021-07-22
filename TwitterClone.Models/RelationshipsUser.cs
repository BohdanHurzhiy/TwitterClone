using System.Collections.Generic;

namespace TwitterClone.Models
{
    public class RelationshipsUser
    {
        public int Id { get; set; }
        public int FollowerId { get; set; }
        public int? FollowedId { get; set; }
    }
}
