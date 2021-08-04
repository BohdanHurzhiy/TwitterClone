using System;
using System.Linq;
using TwitterClone.Models;
using System.Collections.Generic;

namespace TwitterClone.Services
{
    public class PostService
    {
        private DbTwitterCloneContex _dbTwitterContex;
        public PostService(DbTwitterCloneContex dbTwitterContex)
        {
            _dbTwitterContex = dbTwitterContex;
        }
        public void AddPost(int idUser, string text)
        {
            var user = _dbTwitterContex.Users
                .Where(u => u.Id == idUser)
                .FirstOrDefault();
            if (user == null)
            {
                throw new ArgumentException();
            }
            if (text == null || text == "")
            {
                throw new ArgumentException();
            }

            Post post = new Post() { UserId = idUser, TextPost = text };
            _dbTwitterContex.Posts.Add(post);
            _dbTwitterContex.SaveChanges();
        }
        public void RemovePost(int idPost)
        {
            var post = _dbTwitterContex.Posts
                .Where(p => p.Id == idPost)
                .FirstOrDefault();
            if (post == null)
            {
                throw new ArgumentException();
            }          

            _dbTwitterContex.Posts.Remove(post);
            _dbTwitterContex.SaveChanges();
        }
        public void LikeUnLikePost(int idUser, int idPost)
        {
            var user = _dbTwitterContex.Users
                .Where(u => u.Id == idUser)
                .FirstOrDefault();
            var post = _dbTwitterContex.Posts
                .Where(p => p.Id == idPost)
                .FirstOrDefault();

            if (post == null || user == null)
            {
                throw new ArgumentException();
            }

            var Userlike = _dbTwitterContex.Likeds
                .Where(l => l.UserId == idUser)
                .Where(l => l.PostId == idPost)
                .FirstOrDefault();

            if (Userlike == null)
            {
                _dbTwitterContex.Likeds.Add(new Liked { UserId = idUser, PostId = idPost });
            }
            else
            {
                _dbTwitterContex.Likeds.Remove(Userlike);
            }
            _dbTwitterContex.SaveChanges();
        }
        public void AddTagForPost(int idPost, string textTags) 
        {            
            var tag = _dbTwitterContex.Tags
                .Where(t => t.TagsText == textTags)                
                .FirstOrDefault();
            
            var post = _dbTwitterContex.Posts
                .Where(p => p.Id == idPost)
                .FirstOrDefault();

            List<Post> posts;

            if (tag == null)
            {
                posts = new List<Post> {post};
                var createTag = new Tag { TagsText = textTags, Posts = posts };

                _dbTwitterContex.Tags.Add(createTag);     
            }
            else if (tag.Posts == null)
            {
                posts = new List<Post> { post };  
                tag.Posts = posts;                   
            }
            else if (!tag.Posts.Contains(post))
            {
                tag.Posts.Add(post);                
            }

            _dbTwitterContex.SaveChanges();

        }
        public void RemoveTagForPost(int idPost, string textTags)
        { 
            var tag = _dbTwitterContex.Tags
                .Where(t => t.TagsText == textTags)
                .FirstOrDefault();
            var post = _dbTwitterContex.Posts
                .Where(p => p.Id == idPost)
                .FirstOrDefault();
            if (tag == null || post == null)
            {
                throw new ArgumentException();
            }
            if (tag.Posts.Contains(post))
            {
                tag.Posts.Remove(post);
            }
            else
            {
                throw new ArgumentException();
            }
            //var tagsPost = _dbTwitterContex.TagsPosts
            //    .Where(tp => tp.PostId == idPost)
            //    .Where(tp => tp.TagId == tag.Id)
            //    .FirstOrDefault();            
            
            //if (tagsPost == null)
            //{
            //    throw new ArgumentException();
            //}
            //else 
            //{
            //    _dbTwitterContex.TagsPosts.Remove(tagsPost);
            //    _dbTwitterContex.SaveChanges();
            //}

                    
        }
        public void AddRepostToPost(int idUser,string text, int idPost) 
        {
            var user = _dbTwitterContex.Users
                .Where(u => u.Id == idUser)
                .FirstOrDefault();

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
            var user = _dbTwitterContex.Users
                .Where(u => u.Id == idUser)
                .FirstOrDefault();
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
