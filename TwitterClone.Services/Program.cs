using System;
using TwitterClone.Models;

namespace TwitterClone.Services
{
    class Program
    {
        static void Main(string[] args)
        {
            DbTwitterCloneContex dbTwitterCloneContex = new DbTwitterCloneContex();
            PostService postService = new PostService(dbTwitterCloneContex);
            postService.AddPost(1, "some text");
            postService.AddTagForPost(1, "тег1");
            postService.AddTagForPost(1, "тег1");
            postService.AddTagForPost(1, "тег2");
            // postService.RemoveTagForPost(1, "пезда");

            UserService userService = new(dbTwitterCloneContex);
            userService.Follow(1, 2);
            userService.Follow(1, 4);
            userService.Follow(2, 1);
            //userService.GetSubscriptions(1);
            //userService.GetFollowers(1);
            userService.GetPostsForUser(1, 10);
        }
    }
}
