using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TwitterClone.Models
{
    public class BaseOperations
    {
        private DbTwitterCloneContex _dbContex;
        public BaseOperations(DbTwitterCloneContex dbContex) 
        {
            this._dbContex = dbContex;
        }
        static public void CalculateAverageNumberOfLikesPerPost(int userID)
        { 
            using (DbTwitterCloneContex twitterClone = new DbTwitterCloneContex())
            {
                var userPosts = twitterClone.Posts
                                .Where(p => p.UserId == userID)
                                .Include(l => l.Likes)
                                .Select(l => l.Likes.Count).ToList();

                var avarageCount = userPosts.Average();
                Console.WriteLine(avarageCount);                
            }            
        }
        static public void CalculateMostLikesOnPosts()
        {
            using (DbTwitterCloneContex twitterClone = new DbTwitterCloneContex())
            {
                var countPosts = twitterClone.Posts
                                .Include(p => p.Likes)
                                .Select(p => p.Likes.Count).ToList();

                Console.WriteLine(countPosts.Max());
            }
        }
        static public void CalculateMostLikesOnAnswers()
        {
            using (DbTwitterCloneContex twitterClone = new DbTwitterCloneContex())
            {
                var countAnswers = twitterClone.Answers
                                .Include(a => a.Likes)
                                .Select(p => p.Likes.Count).ToList();
                int? returnAnswer = countAnswers.Count == 0 ? null : countAnswers.Max();
                Console.WriteLine(returnAnswer);
                
            }
        }
        static public void CalculateMostRepostInPost()
        {
            using (DbTwitterCloneContex twitterClone = new DbTwitterCloneContex())
            {
                var countPosts = twitterClone.Posts
                                .Include(p => p.RePosts)
                                .Select(p => p.RePosts.Count).ToList();
                int? returnAnswer = countPosts.Count == 0 ? null : countPosts.Max();
                Console.WriteLine(returnAnswer);
            }
        }
        static public void CalculateMostAnswersInPost()
        {
            using (DbTwitterCloneContex twitterClone = new DbTwitterCloneContex())
            {
                var countAnswers = twitterClone.Posts
                                .Include(p => p.Answers)
                                .Select(p => p.Answers.Count).ToList();
                int? returnAnswer = countAnswers.Count == 0 ? null : countAnswers.Max();
                Console.WriteLine(returnAnswer);
            }
            
        }
        static public void CalculateAverageNumberFollowers()
        {
            using (DbTwitterCloneContex twitterClone = new DbTwitterCloneContex())
            {
                var userFollower = twitterClone.Users                                
                                .Include(f => f.Followers)
                                .Select(f => f.Followers.Count)
                                .ToList();
                double? returnAnswer = userFollower.Count == 0 ? null : userFollower.Average();
                Console.WriteLine(returnAnswer);
            }
        }
        static public void TakeSomeMostPopularUsers(int countUser)
        {
            using (DbTwitterCloneContex twitterClone = new DbTwitterCloneContex())
            {
               
            }
        }
        static public void TakeSomeMostPopularPost(int countUser)
        {
            using (DbTwitterCloneContex twitterClone = new DbTwitterCloneContex())
            {

            }
        }
        static public void CalculateAverageNumberFollowed()
        {
            using (DbTwitterCloneContex twitterClone = new DbTwitterCloneContex())
            {
                var userFollower = twitterClone.Users
                                .Include(f => f.Subscriptions)
                                .ToList();
                var avarageCount = userFollower.Average(f => f.Subscriptions.Count);
                Console.WriteLine(avarageCount);
            }
        }
        static public void FindPostsWithTag(string tagPost)
        {
            using (DbTwitterCloneContex twitterClone = new DbTwitterCloneContex())
            {
                var tagId = twitterClone.Tags
                                .Where(t => t.TagsText == tagPost)
                                .FirstOrDefault();

                var posts = twitterClone.Tags
                                    .Where(t => t.TagsText == tagPost)
                                    .Select(t => t.Posts)
                                    .ToList();

                //var posts = twitterClone.Posts
                //                    .Where(post =>
                //                    twitterClone.TagsPosts.Any(tp => tp.TagId == tagId.Id && tp.PostId == post.Id)
                //                    )
                //                    .ToList();
            }
        }
    }
    
}
