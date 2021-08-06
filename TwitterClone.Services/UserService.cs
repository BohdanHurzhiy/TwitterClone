using System;
using System.Linq;
using TwitterClone.Models;
using System.IO;

namespace TwitterClone.Services
{
    class UserService
    {
        private DbTwitterCloneContex _dbTwitterContex;
        
        public UserService(DbTwitterCloneContex dbTwitterContex)
        {
            _dbTwitterContex = dbTwitterContex;
        }
        
        public void Follow(int idUser, int targetUser)
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
        public void UnFollow(int idUser, int targetUser)
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
        
        public void GetPostsForUser(int idUser, int numberOfPost)
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
                .Select(f => f.FollowedId);

            var posts = followings
                .SelectMany(u => _dbTwitterContex.Posts
                .Where(p => p.UserId == u))
                .ToList();
            
            posts = posts.OrderBy(p => p.PublicationDate)
                .Take(numberOfPost)
                .ToList(); 
        }
        
        public void GetFollowers(int idUser)
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
        }
        
        public void GetSubscriptions(int idUser)
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
        }
        
        public void AddPhoto(int idUser, string pathPhoto) 
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
    }
}
