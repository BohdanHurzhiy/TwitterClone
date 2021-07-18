using System;

namespace ModelsForTwitter
{
    class Program
    {
        static void Main(string[] args)
        {
            using (DbTwitterCloneContex twitterClone = new DbTwitterCloneContex())
            {
                Console.WriteLine("END!!");
            }
            BaseOperations.CalculateAverageNumberOfLikesPerPost(1);
            BaseOperations.CalculateMostLikesOnPosts();
            BaseOperations.CalculateMostLikesOnAnswers();
            BaseOperations.FindPostsWithTag("хуй");
           // BaseOperations.CalculateAverageNumberFollowers();
        }
    }
}
