using System;
using System.Linq;
using TwitterClone.ASP.Services.ServiceInterface;
using TwitterClone.ASP.Models;

namespace TwitterClone.ASP.Services
{
    public class PostService : IPostService
    {
        private DbTwitterCloneContex _dbTwitterContex;
        
        public PostService(DbTwitterCloneContex dbTwitterContex)
        {
            _dbTwitterContex = dbTwitterContex;
        }
        
        public int AddPost(string idUser, string text)
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

            Post post = new () { UserId = idUser, User = user };
            _dbTwitterContex.Posts.Add(post);
            _dbTwitterContex.SaveChanges();

            if (!text.Contains('#'))
            {
                post.TextPost = text;
            }
            else
            {
                var textWithOutTags = SearchTagsInPost(post.Id, text);
                post.TextPost = textWithOutTags;
            }
            _dbTwitterContex.SaveChanges();
            return post.Id;
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
       
        public void LikeUnLikePost(string idUser, int idPost)
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

        public void AddTagToBase(Tag tag) 
        {
            if (tag == null)
            {
                throw new ArgumentNullException();
            }

            var tagInBase = _dbTwitterContex
                .Tags
                .Where(t => t.TagsText == tag.TagsText)
                .FirstOrDefault();

            if (tagInBase == null)
            {
                _dbTwitterContex.Tags.Add(tag);
                _dbTwitterContex.SaveChanges();
            }
        }

        public int GetTagId(string tagText)
        {
            var tagId = _dbTwitterContex.Tags
                .Where(tag => tag.TagsText == tagText)
                .Select(t => t.Id)
                .FirstOrDefault();
            return tagId;
        }
       
        public void AddTagForPost(int idPost, int tagId) 
        {
           
            var tagInBase = _dbTwitterContex.Tags
                .Where(t => t.Id == tagId)
                .FirstOrDefault();

            var postInBase = _dbTwitterContex.Posts
                .Where(p => p.Id == idPost)
                .FirstOrDefault();

            if (tagInBase == null || postInBase == null)
            {
                throw new ArgumentNullException();
            }

            postInBase.Tags.Add(tagInBase);
            _dbTwitterContex.SaveChanges();
        }
       
        public void RemoveTagForPost(int idPost, int tagId)
        { 
            var tagInBase = _dbTwitterContex.Tags
                .Where(t => t.Id == tagId)
                .FirstOrDefault();
            var postInBase = _dbTwitterContex.Posts
                .Where(p => p.Id == idPost)
                .FirstOrDefault();

            if (tagInBase == null || postInBase == null)
            {
                throw new ArgumentException();
            }

            if (postInBase.Tags.Contains(tagInBase))
            {
                postInBase.Tags.Remove(tagInBase);
            }
            else
            {
                throw new ArgumentException();
            }
           

                    
        }
        
        public void AddRepostToPost(string idUser, string text, int idPost)
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
        
        public void AddAnswerToPost(string idUser, string text, int idPost)
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

        public Post GetPostToId(int id)
        {
            var post = _dbTwitterContex.Posts
                 .Where(p => p.Id == id)
                 .FirstOrDefault();
            if (post == null)
            {
                throw new NullReferenceException();
            }
            return post;
        }

        public string SearchTagsInPost(int idPost, string text)
        {
            string textPostWithOutTags = text;

            while (textPostWithOutTags.Contains('#'))
            {
                string strTag;
                string textBeforeTag = "";
                var indexTag = textPostWithOutTags.IndexOf('#');
                var indexSpace = textPostWithOutTags.IndexOf(' ');
                
                if (indexTag != -1)
                {
                    textBeforeTag = textPostWithOutTags.Substring(0, indexTag);
                    strTag = textPostWithOutTags.Substring(indexTag + 1, indexSpace);
                    
                    Tag tag = new() { TagsText = strTag.Trim() };
                    AddTagToBase(tag);
                    int tagId = GetTagId(tag.TagsText);
                    AddTagForPost(idPost, tagId);
                }

                textPostWithOutTags = textBeforeTag + textPostWithOutTags.Substring(indexSpace + 1);
            }
            return textPostWithOutTags.Trim();           
        }
    }
}
