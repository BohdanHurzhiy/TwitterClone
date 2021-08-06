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

            var countPostsBeforAddPost = dbContex.Posts.Count();

            postService.AddPost(1, "Some text for post");

            var countPostsAfterAddPost = dbContex.Posts.Count();
            

            Assert.Equal(0, countPostsBeforAddPost);
            Assert.Equal(1, countPostsAfterAddPost);            
        }

        [Fact]
        public void Can_Add_Tag_For_Post()
        {
            var options = GetOptions();
            var dbContex = new DbTwitterCloneContex(options);

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
            dbContex.Tags.Add(
                new Tag {
                    Id = 1,
                    TagsText = "Text for Tag"
                }
                );

            dbContex.SaveChanges();

            PostService postService = new PostService(dbContex);
            postService.AddTagForPost(1, 1);

            var post1 = dbContex.Posts
                .Where(p => p.Id == 1)
                .FirstOrDefault();
            var tag1 = dbContex.Tags
                .Where(t => t.Id == 1)
                .FirstOrDefault();
            
            Assert.Equal(tag1, post1.Tags.FirstOrDefault());
        }

    }
}
