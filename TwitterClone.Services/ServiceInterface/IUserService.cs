using System.Collections.Generic;
using TwitterClone.Models;

namespace TwitterClone.Services.ServiceInterface
{
    public interface IUserService
    {
        void Follow(int idUser, int targetUser);
        void UnFollow(int idUser, int targetUser);

        ICollection<Post> GetPostsForUser(int idUser, int numberOfPost);

        ICollection<User> GetFollowers(int idUser);

        ICollection<User> GetSubscriptions(int idUser);

        void AddPhoto(int idUser, string pathPhoto);
    }
}
