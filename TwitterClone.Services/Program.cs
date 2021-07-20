using System;

namespace TwitterClone.Services
{
    class Program
    {
        static void Main(string[] args)
        {
            PostService postService = new PostService();
            //postService.AddPost(1, "Some Text");
            //postService.LikeUnLikePost(1, 1);
            //postService.LikeUnLikePost(1, 2);
            //postService.LikeUnLikePost(1, 1);
            //postService.AddTagForPost(1, "Пезда");
            //postService.RemoveTagForPost(1, "Пезда");
            UserService userService = new();
            userService.Follow(1, 2);
            userService.Follow(1, 4);
            userService.Follow(2, 3);
           // userService.GetSubscriptions(1);
            userService.GetPostsForUser(1);
        }
    }
}
