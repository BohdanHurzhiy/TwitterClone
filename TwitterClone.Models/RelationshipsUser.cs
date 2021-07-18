using System.Collections.Generic;

namespace ModelsForTwitter
{
    public class RelationshipsUser
    {
        public int Id { get; set; }
        public int FolowerId { get; set; }
        public int? FollowedId { get; set; }
    }
}
