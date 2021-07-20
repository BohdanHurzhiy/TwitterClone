using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterClone.Models;

namespace TwitterClone.Services
{
    class PostService
    {
        public void AddPost(int idUser, string text)
        {
            using DbTwitterCloneContex twitterClone = new DbTwitterCloneContex();
            var user = twitterClone.Users.Where(u => u.Id == idUser).FirstOrDefault();
            if (user == null)
            {
                throw new ArgumentException();
            }
            if (text == null || text == "")
            {
                throw new ArgumentException();
            }

            //var posts = twitterClone.Posts.ToList();
            //Console.WriteLine("List Posrts:");
            //foreach (Post p in posts)
            //{
            //    Console.WriteLine($"{p.Id}.{p.UserId} - {p.TextPost}");
            //}

            Post post = new Post() { UserId = idUser, TextPost = text };
            twitterClone.Posts.Add(post);
            twitterClone.SaveChanges();

            //posts = twitterClone.Posts.ToList();
            //Console.WriteLine("List Posrts:");
            //foreach (Post p in posts)
            //{
            //    Console.WriteLine($"{p.Id} User id: {p.UserId} - {p.TextPost}");
            //}

        }
        public void RemovePost(int idPost)
        {
            using DbTwitterCloneContex twitterClone = new DbTwitterCloneContex();
            var post = twitterClone.Posts.Where(p => p.Id == idPost).FirstOrDefault();
            if (post == null)
            {
                throw new ArgumentException();
            }

            //var posts = twitterClone.Posts.ToList();
            //Console.WriteLine("List Posts:");
            //foreach (Post p in posts)
            //{
            //    Console.WriteLine($"{p.Id}  {p.UserId} - {p.TextPost}");
            //}

            twitterClone.Posts.Remove(post);
            twitterClone.SaveChanges();

            //posts = twitterClone.Posts.ToList();
            //Console.WriteLine("List Posts:");
            //foreach (Post p in posts)
            //{
            //    Console.WriteLine($"{p.Id} User id: {p.UserId} - {p.TextPost}");
            //}

        }
        public void LikeUnLikePost(int idUser, int idPost)
        {
            using DbTwitterCloneContex twitterClone = new DbTwitterCloneContex();
            var user = twitterClone.Posts.Where(u => u.Id == idUser).FirstOrDefault();
            var post = twitterClone.Posts.Where(p => p.Id == idPost).FirstOrDefault();

            if (post == null || user == null)
            {
                throw new ArgumentException();
            }

            var Userlike = twitterClone.Likeds
                .Where(l => l.UserId == idUser)
                .Where(l => l.PostId == idPost)
                .FirstOrDefault();


            //var likeds = twitterClone.Likeds.ToList();
            //Console.WriteLine("List Likeds:");
            //foreach (Liked l in likeds)
            //{
            //    Console.WriteLine($"{l.Id} User id: {l.UserId} - {l.PostId}");
            //}

            if (Userlike == null)
            {
                twitterClone.Likeds.Add(new Liked { UserId = idUser, PostId = idPost });
            }
            else
            {
                twitterClone.Likeds.Remove(Userlike);
            }
            twitterClone.SaveChanges();

            //likeds = twitterClone.Likeds.ToList();
            //Console.WriteLine("List Likeds:");
            //foreach (Liked l in likeds)
            //{
            //    Console.WriteLine($"{l.Id} User id: {l.UserId} - {l.PostId}");
            //}
        }
        public void AddTagForPost(int idPost, string textTags) 
        {
            using DbTwitterCloneContex twitterClone = new DbTwitterCloneContex();
            var tag = twitterClone.Tags.Where(t => t.TagsText == textTags).FirstOrDefault();
            if (tag == null)
            {
                twitterClone.Tags.Add(new Tag { TagsText = textTags });
                twitterClone.SaveChanges();
                tag = twitterClone.Tags.Where(t => t.TagsText == textTags).FirstOrDefault();
            }
            var tagsPost = twitterClone.TagsPosts
                                       .Where(tp => tp.PostId == idPost)
                                       .Where(tp => tp.TagId == tag.Id)
                                       .FirstOrDefault();
            if (tagsPost == null)
            {
                twitterClone.TagsPosts.Add(new TagsPost { PostId = idPost, TagId = tag.Id });
                twitterClone.SaveChanges();
            }           
        }
        public void RemoveTagForPost(int idPost, string textTags)
        {
            using DbTwitterCloneContex twitterClone = new DbTwitterCloneContex();
            var tag = twitterClone.Tags.Where(t => t.TagsText == textTags).FirstOrDefault();
            
            if (tag == null)
            {
                throw new ArgumentException();
            }

            var tagsPost = twitterClone.TagsPosts
                                       .Where(tp => tp.PostId == idPost)
                                       .Where(tp => tp.TagId == tag.Id)
                                       .FirstOrDefault();

            var tagsPostList = twitterClone.TagsPosts.ToList();
            Console.WriteLine("TagsPost before Remove:");
            foreach (TagsPost tp in tagsPostList)
            {
                Console.WriteLine($"{tp.Id} User id: {tp.PostId} - {tp.TagId}");
            }

            if (tagsPost == null)
            {
                throw new ArgumentException();
            }
            else 
            {
                twitterClone.TagsPosts.Remove(tagsPost);
                twitterClone.SaveChanges();
            }

            tagsPostList = twitterClone.TagsPosts.ToList();
            Console.WriteLine("TagsPost after Remove:");
            foreach (TagsPost tp in tagsPostList)
            {
                Console.WriteLine($"{tp.Id} User id: {tp.PostId} - {tp.TagId}");
            }
        }        
    }
}
