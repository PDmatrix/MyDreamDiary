﻿using System;
using System.Collections.Generic;

namespace DB.Models
{
    public class Post
    {
        public Post()
        {
            Comment = new HashSet<Comment>();
            PostTag = new HashSet<PostTag>();
        }

        public int Id { get; set; }
        public int? LikesCount { get; set; }
        public string Title { get; set; }
        public int DreamId { get; set; }
        public DateTime? DateCreated { get; set; }
        public int UserId { get; set; }

        public Dream Dream { get; set; }
        public IdentityUser User { get; set; }
        public ICollection<Comment> Comment { get; set; }
        public ICollection<PostTag> PostTag { get; set; }
    }
}