using System;
using System.Linq;
using TwitterClone.ASP.Models;
using System.IO;
using System.Collections.Generic;
using TwitterClone.ASP.Services.ServiceInterface;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace TwitterClone.ASP.Services
{
    public class UserService : IUserService
    {
        private DbTwitterCloneContex _dbTwitterContex;
        
        public UserService(DbTwitterCloneContex dbTwitterContex)
        {
            _dbTwitterContex = dbTwitterContex;
        }
        
        public void Follow(string idUser, string targetUser)
        {
            var user = _dbTwitterContex.Users
                .Where(u => u.Id == idUser)
                .FirstOrDefault();
            var tUser = _dbTwitterContex.Users
                .Where(u => u.Id == targetUser)
                .FirstOrDefault();
            if (user == null || tUser == null)
            {
                throw new ArgumentException();
            }

            var relationshipsUser = _dbTwitterContex.Relationships
                .Where(r => r.FollowerId == user.Id)
                .Where(r => r.FollowedId == tUser.Id)
                .FirstOrDefault();
            if (relationshipsUser == null)
            {
                _dbTwitterContex.Relationships.Add(
                    new RelationshipsUser { FollowerId = user.Id, FollowedId = tUser.Id }
                    );
                _dbTwitterContex.SaveChanges();
            }           

        }
        public void UnFollow(string idUser, string targetUser)
        {
            var user = _dbTwitterContex.Users
                .Where(u => u.Id == idUser)
                .FirstOrDefault();
            var tUser = _dbTwitterContex.Users
                .Where(u => u.Id == targetUser)
                .FirstOrDefault();
            if (user == null || tUser == null)
            {
                throw new ArgumentException();
            }

            var relationshipsUser = _dbTwitterContex.Relationships
                .Where(r => r.FollowerId == user.Id)
                .Where(r => r.FollowedId == tUser.Id)
                .FirstOrDefault();           

            if (relationshipsUser == null)
            {
                throw new ArgumentException();
            }

            _dbTwitterContex.Relationships.Remove(relationshipsUser);
            _dbTwitterContex.SaveChanges();            

        }
        
        public ICollection<Post> GetPostsForUser(string idUser, int numberOfPost = 10)
        {
            var user = _dbTwitterContex.Users
                .Where(u => u.Id == idUser)
                .FirstOrDefault();          
            if (user == null)
            {
                throw new ArgumentException();
            }
            var followings = _dbTwitterContex.Relationships
                .Where(r => r.FollowerId == user.Id)
                .Select(f => f.FollowedId)
                .ToList();

            followings.Add(idUser);

            var posts = followings
                .SelectMany(u => _dbTwitterContex.Posts
                .Include(p => p.Tags)
                .Include(p => p.User)
                .Where(p => p.UserId == u))                
                .Distinct()                
                .ToList();

            posts = posts.OrderByDescending(p => p.PublicationDate)                
                .Take(numberOfPost)
                .ToList();

            return posts;
        }
        
        public ICollection<User> GetFollowers(string idUser)
        {
            var user = _dbTwitterContex.Users                
                .Where(u => u.Id == idUser)         
                .FirstOrDefault();
            if (user == null)
            {
                throw new ArgumentException();
            }

            var followers = _dbTwitterContex.Relationships
                .Where(r => r.FollowedId == user.Id)
                .Select(f => f.FollowerId);

            var users = followers
                .SelectMany(f => _dbTwitterContex.Users
                .Where(u => u.Id == f))
                .ToList();
            return users;
        }
        
        public ICollection<User> GetSubscriptions(string idUser)
        {
            var user = _dbTwitterContex.Users
                .Where(u => u.Id == idUser)
                .FirstOrDefault();
            if (user == null)
            {
                throw new ArgumentException();
            }

            var subscriptions = _dbTwitterContex.Relationships
                .Where(r => r.FollowedId == user.Id)
                .Select(f => f.FollowerId);

            var users = subscriptions
                .SelectMany(f => _dbTwitterContex.Users
                .Where(u => u.Id == f))
                .ToList();
            return users;
        }
        
        public void AddPhoto(string idUser, string pathPhoto) 
        {
            var user = _dbTwitterContex.Users
                .Where(u => u.Id == idUser)
                .FirstOrDefault();
            if (user == null)
            {
                throw new ArgumentException();
            }

            string dirPhoto = @"~\img\";
            string extFile = Path.GetExtension(pathPhoto);
            string nameFile = Path.GetFileName(pathPhoto);            
            string pathFileOnServer = Path.Combine(dirPhoto, nameFile);
           
            var checkPhoto = _dbTwitterContex.Photos
                .Where(ph => ph.PathOnTheServer == pathFileOnServer)
                .FirstOrDefault();
            
            if (checkPhoto != null)
            {
                Photos photo = new Photos {
                    UserId = user.Id,
                    PathOnTheServer = pathFileOnServer,
                    Extension = extFile
                };
            }         
        }

        public User GetUser(string idUser)
        {
            if (idUser == null)
            {
                throw new ArgumentNullException();
            }
            var user = _dbTwitterContex.Users
                .Where(u => u.Id == idUser)
                .FirstOrDefault();
            if (user == null)
            {
                throw new NullReferenceException();
            }
            return user;
        }
       
        public User GetUser(string idUser, string emailUser)
        {
            User user = null;
            if (idUser != null)
            {
                user = GetUser(idUser);
            }

            user = _dbTwitterContex.Users
                .Where(u => u.Email == emailUser)
                .FirstOrDefault();

            if (user == null)
            {
                throw new NullReferenceException();
            }
            return user;
        }
        
        public User GetUserByAlias(string alias)
        {
            if (alias == null)
            {
                throw new ArgumentNullException();
            }
            var user = _dbTwitterContex.Users
                .Where(u => u.Alias == alias)
                .FirstOrDefault();
            if (user == null)
            {
                throw new NullReferenceException();
            }
            return user;
        }
        
        public User GetUserByEmail(string email)
        {

            if (email == null)
            {
                throw new ArgumentNullException();
            }
            var user = _dbTwitterContex.Users
                .Where(u => u.Email == email)
                .FirstOrDefault();
            if (user == null)
            {
                throw new NullReferenceException();
            }
            return user;   
        }

        public bool SubscriptionCheck(string idUser, string targetUser)
        {
            var relationship = _dbTwitterContex.Relationships
                .Where(rel => rel.FollowerId == idUser)
                .Where(rel => rel.FollowedId == targetUser)
                .FirstOrDefault();

            return relationship != null;
        }

        public ICollection<Post> GetUserPosts(string idUser, int numberOfPost)
        {
            var user = _dbTwitterContex.Users
                 .Where(u => u.Id == idUser)
                 .FirstOrDefault();
            if (user == null)
            {
                throw new ArgumentException();
            }
            var posts = _dbTwitterContex.Posts
                .Where(p => p.UserId == idUser)
                .ToList();
            return posts;
        }

        public bool CheckUserByEmail(string emailUser)
        {
            if (emailUser == null)
            {
                throw new ArgumentNullException();
            }
            var user = _dbTwitterContex.Users
                .Where(u => u.Email == emailUser)
                .FirstOrDefault();

            return user != null;
        }
        public async Task<ICollection<Post>> GetPostsForUserAsync(string idUser, int numberOfPost = 10)
        {
            var posts = await Task.Run(()=> GetPostsForUser(idUser, numberOfPost));
            return posts;
        }

        public ICollection<Post> GetPageUserPosts(string idUser, int numberPage, int numberOfPosts = 10)
        {
            var user = _dbTwitterContex.Users
                .Where(u => u.Id == idUser)
                .FirstOrDefault();
            if (user == null)
            {
                throw new ArgumentException();
            }
            var followings = _dbTwitterContex.Relationships
                .Where(r => r.FollowerId == user.Id)
                .Select(f => f.FollowedId)
                .ToList();

            followings.Add(idUser);

            var posts = followings
                .SelectMany(u => _dbTwitterContex.Posts
                .Include(p => p.Tags)
                .Include(p => p.User)
                .Where(p => p.UserId == u))
                .Distinct()
                .OrderByDescending(p => p.PublicationDate)
                .Skip((numberPage - 1) * numberOfPosts)
                .Take(numberOfPosts)
                .ToList();           
            //    .Where(p => p.UserId == idUser)
            //    .OrderByDescending(p => p.PublicationDate)
            //    .Skip((numberPage - 1) * numberOfPosts)
            //    .Take(numberOfPosts)
            //    .ToQueryString();
            return posts;
        }
    }   
}
