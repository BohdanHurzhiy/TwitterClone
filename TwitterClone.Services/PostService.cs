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
            using (DbTwitterCloneContex twitterClone = new DbTwitterCloneContex())
            {
                var user = twitterClone.Users.Where(u => u.Id == idUser).FirstOrDefault();
                if (user == null)
                {
                    throw new ArgumentException();
                }
                if (text == null || text == "")
                {
                    throw new ArgumentException();
                }
                Post post = new Post() { UserId = idUser, TextPost = text };
                twitterClone.Posts.Add(post);
                twitterClone.SaveChanges();
            }
        }
    }
}
