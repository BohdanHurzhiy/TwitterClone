﻿using System.Collections.Generic;

namespace TwitterClone.ASP.Models
{
    public class TagsPost
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int TagId { get; set; }
    }
}
