using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterClone.ASP.Models;

namespace TwitterClone.ASP.Services.ServiceInterface
{
    public interface IUserService
    {
        User GetUser(string idUser);
        User GetUser(string idUser, string emailUser);
        User GetUserByAlias(string alias);
        User GetUserByEmail(string email);
        bool CheckUserByEmail(string emailUser);
        void Follow(string idUser, string targetUser);
        void UnFollow(string idUser, string targetUser);
        bool SubscriptionCheck(string idUser, string targetUser);

        ICollection<Post> GetPostsForUser(string idUser, int numberOfPost);
        Task<ICollection<Post>> GetPostsForUserAsync(string idUser, int numberOfPost = 10);

        ICollection<Post> GetUserPosts(string idUser, int numberOfPost);


        ICollection<User> GetFollowers(string idUser);

        ICollection<User> GetSubscriptions(string idUser);

        void AddPhoto(string idUser, string pathPhoto);
    }
}
