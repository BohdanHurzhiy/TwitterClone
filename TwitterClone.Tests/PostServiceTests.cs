using Xunit;
using TwitterClone.Models;
using TwitterClone.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using Moq;

namespace TwitterClone.Tests
{
    public class PostServiceTests
    {
        private DbContextOptions<DbTwitterCloneContex> GetOptions()
        {
            var builder = new DbContextOptionsBuilder<DbTwitterCloneContex>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            return builder.Options;
        }
        [Fact]
        public void Can_Add_Post()
        {           
            var options = GetOptions();
            var dbContex = new DbTwitterCloneContex(options);
            
            dbContex.Users.Add(new User { Id = 1 });
            dbContex.SaveChanges();    
            
            PostService postService = new PostService(dbContex);
            postService.AddPost(1, "Some text for post");

            var equalPost = dbContex.Posts
                .Where(p => p.UserId == 1)
                .Where(p => p.TextPost == "Some text for post")
                .FirstOrDefault();

            Assert.Equal("Some text for post", equalPost.TextPost);
            Assert.Equal(1, equalPost.UserId);            
        }

        [Fact]
        public void Can_Add_Tag_For_Post()
        {
            var options = GetOptions();
            var dbContex = new DbTwitterCloneContex(options);
            var tag = new Tag { Id = 1, TagsText = "Some Tag" };
            dbContex.Users.Add(
                new User {
                    Id = 1 
                });
            dbContex.Posts.Add(
                new Post {  
                    Id = 1,
                    UserId = 1,
                    TextPost = "Post for test add Tag"
                }
                );
            dbContex.Tags.Add(tag);
                
            dbContex.SaveChanges();              
            
            PostService postService = new PostService(dbContex);
            postService.AddTagForPost(1, tag);

            /*var postWithTag = dbContex.TagsPosts
                .Where(tp => tp.PostId == 1)               
                .FirstOrDefault();
            
            var tagInPost = dbContex.Tags
              .Where(t => t.Id == postWithTag.TagId)
              .FirstOrDefault();*/

            var post1 = dbContex.Posts
                .Where(p => p.Id == 1)
                .FirstOrDefault();           
                            
            Assert.Equal(1, post1.Tags.Count);
        }
    }
}
