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
        private DbTwitterCloneContex _dbTwitterContex;
        public PostService(DbTwitterCloneContex dbTwitterContex)
        {
            _dbTwitterContex = dbTwitterContex;
        }
        public void AddPost(int idUser, string text)
        {
            var user = _dbTwitterContex.Users.Where(u => u.Id == idUser).FirstOrDefault();
            if (user == null)
            {
                throw new ArgumentException();
            }
            if (text == null || text == "")
            {
                throw new ArgumentException();
            }

            //var posts = _dbTwitterContex.Posts.ToList();
            //Console.WriteLine("List Posrts:");
            //foreach (Post p in posts)
            //{
            //    Console.WriteLine($"{p.Id}.{p.UserId} - {p.TextPost}");
            //}

            Post post = new Post() { UserId = idUser, TextPost = text };
            _dbTwitterContex.Posts.Add(post);
            _dbTwitterContex.SaveChanges();

            //posts = _dbTwitterContex.Posts.ToList();
            //Console.WriteLine("List Posrts:");
            //foreach (Post p in posts)
            //{
            //    Console.WriteLine($"{p.Id} User id: {p.UserId} - {p.TextPost}");
            //}

        }
        public void RemovePost(int idPost)
        {
            var post = _dbTwitterContex.Posts.Where(p => p.Id == idPost).FirstOrDefault();
            if (post == null)
            {
                throw new ArgumentException();
            }

            //var posts = _dbTwitterContex.Posts.ToList();
            //Console.WriteLine("List Posts:");
            //foreach (Post p in posts)
            //{
            //    Console.WriteLine($"{p.Id}  {p.UserId} - {p.TextPost}");
            //}

            _dbTwitterContex.Posts.Remove(post);
            _dbTwitterContex.SaveChanges();

            //posts = _dbTwitterContex.Posts.ToList();
            //Console.WriteLine("List Posts:");
            //foreach (Post p in posts)
            //{
            //    Console.WriteLine($"{p.Id} User id: {p.UserId} - {p.TextPost}");
            //}

        }
        public void LikeUnLikePost(int idUser, int idPost)
        {
            var user = _dbTwitterContex.Posts.Where(u => u.Id == idUser).FirstOrDefault();
            var post = _dbTwitterContex.Posts.Where(p => p.Id == idPost).FirstOrDefault();

            if (post == null || user == null)
            {
                throw new ArgumentException();
            }

            var Userlike = _dbTwitterContex.Likeds
                .Where(l => l.UserId == idUser)
                .Where(l => l.PostId == idPost)
                .FirstOrDefault();


            //var likeds = _dbTwitterContex.Likeds.ToList();
            //Console.WriteLine("List Likeds:");
            //foreach (Liked l in likeds)
            //{
            //    Console.WriteLine($"{l.Id} User id: {l.UserId} - {l.PostId}");
            //}

            if (Userlike == null)
            {
                _dbTwitterContex.Likeds.Add(new Liked { UserId = idUser, PostId = idPost });
            }
            else
            {
                _dbTwitterContex.Likeds.Remove(Userlike);
            }
            _dbTwitterContex.SaveChanges();

            //likeds = _dbTwitterContex.Likeds.ToList();
            //Console.WriteLine("List Likeds:");
            //foreach (Liked l in likeds)
            //{
            //    Console.WriteLine($"{l.Id} User id: {l.UserId} - {l.PostId}");
            //}
        }
        public void AddTagForPost(int idPost, string textTags) 
        {
            using DbTwitterCloneContex _dbTwitterContex = new DbTwitterCloneContex();
            var tag = _dbTwitterContex.Tags.Where(t => t.TagsText == textTags).FirstOrDefault();
            if (tag == null)
            {
                _dbTwitterContex.Tags.Add(new Tag { TagsText = textTags });
                _dbTwitterContex.SaveChanges();
                tag = _dbTwitterContex.Tags.Where(t => t.TagsText == textTags).FirstOrDefault();
            }
            var tagsPost = _dbTwitterContex.TagsPosts
                                       .Where(tp => tp.PostId == idPost)
                                       .Where(tp => tp.TagId == tag.Id)
                                       .FirstOrDefault();
            if (tagsPost == null)
            {
                _dbTwitterContex.TagsPosts.Add(new TagsPost { PostId = idPost, TagId = tag.Id });
                _dbTwitterContex.SaveChanges();
            }           
        }
        public void RemoveTagForPost(int idPost, string textTags)
        { 
            var tag = _dbTwitterContex.Tags.Where(t => t.TagsText == textTags).FirstOrDefault();
            
            if (tag == null)
            {
                throw new ArgumentException();
            }

            var tagsPost = _dbTwitterContex.TagsPosts
                                       .Where(tp => tp.PostId == idPost)
                                       .Where(tp => tp.TagId == tag.Id)
                                       .FirstOrDefault();

            var tagsPostList = _dbTwitterContex.TagsPosts.ToList();
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
                _dbTwitterContex.TagsPosts.Remove(tagsPost);
                _dbTwitterContex.SaveChanges();
            }

            tagsPostList = _dbTwitterContex.TagsPosts.ToList();
            Console.WriteLine("TagsPost after Remove:");
            foreach (TagsPost tp in tagsPostList)
            {
                Console.WriteLine($"{tp.Id} User id: {tp.PostId} - {tp.TagId}");
            }
        }
        public void AddRepostToPost(int idUser,string text, int idPost) 
        {
            var user = _dbTwitterContex.Users.Where(u => u.Id == idUser).FirstOrDefault();
            if (user == null)
            {
                throw new ArgumentException();
            }
            if (text == null || text == "")
            {
                throw new ArgumentException();
            }

            Repost rePost = new() { UserId = idUser, TextPost = text, PostId = idPost };
            _dbTwitterContex.RePosts.Add(rePost);
            _dbTwitterContex.SaveChanges();
        }
        public void AddAnswerToPost(int idUser, string text, int idPost)
        {
            var user = _dbTwitterContex.Users.Where(u => u.Id == idUser).FirstOrDefault();
            if (user == null)
            {
                throw new ArgumentException();
            }
            if (text == null || text == "")
            {
                throw new ArgumentException();
            }

            Answer answer = new() { UserId = idUser, TextAnswer = text, PostId = idPost };
            _dbTwitterContex.Answers.Add(answer);
            _dbTwitterContex.SaveChanges();
        }
    }
}
