using System.Collections.Generic;
using TwitterClone.ASP.Models;

namespace TwitterClone.ASP.Services.ServiceInterface
{
    public interface IUserService
    {
        User GetUser(string idUser);
        User GetUser(string idUser, string emailUser);
        void Follow(string idUser, string targetUser);
        void UnFollow(string idUser, string targetUser);

        ICollection<Post> GetPostsForUser(string idUser, int numberOfPost);

        ICollection<User> GetFollowers(string idUser);

        ICollection<User> GetSubscriptions(string idUser);

        void AddPhoto(string idUser, string pathPhoto);
    }
}
