using System;
using Xunit;
using Moq;
using TwitterClone.Models;
using TwitterClone.Services;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TwitterClone.Tests
{
    public class PostServiceTests
    {
        [Fact]
        public void Can_Add_Post()
        {
            using (var dbContex = new DbTwitterCloneContex())
            {                
                PostService postService = new PostService(dbContex);
                postService.AddPost(1, "Some text for post");                
            }

            using (var dbContex = new DbTwitterCloneContex())
            {
                var equalPost = dbContex.Posts
                    .Where(p => p.UserId == 1)
                    .Where(p => p.TextPost == "Some text for post")
                    .FirstOrDefault();

                Assert.Equal("Some text for post", equalPost.TextPost);
                Assert.Equal(1, equalPost.UserId);
            }
           
        }
        [Fact]
        public void Can_Add_Tag_For_Post()
        {
            using (var dbContex = new DbTwitterCloneContex())
            {               
                dbContex.Posts.Add(
                    new Post {  
                        UserId = 1,
                        TextPost = "Post for test add Tag"
                    }
                    );      
                dbContex.SaveChanges();   
            }

            using (var dbContex = new DbTwitterCloneContex())
            {
                var postTest = dbContex.Posts
                    .Where(p => p.TextPost == "Post for test add Tag")
                    .FirstOrDefault();
                PostService postService = new PostService(dbContex);
                postService.AddTagForPost(postTest.Id, "Some Tag");               
            }

            using (var dbContex = new DbTwitterCloneContex())
            {
                var equalTags = dbContex.Tags
                    .Where(t => t.TagsText == "Some Tag")
                    .FirstOrDefault();

                Assert.Equal("Some Tag", equalTags.TagsText);
            }

        }

    }
}
