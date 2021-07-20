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
            postService.AddTagForPost(1, "Пезда");
            postService.RemoveTagForPost(1, "Пезда");
        }
    }
}
