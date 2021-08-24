using TwitterClone.ASP.Models;

namespace TwitterClone.ASP.Services.ServiceInterface
{
    public interface IPostService
    {
        void AddPost(string idUser, string text);

        void RemovePost(int idPost);

        void LikeUnLikePost(string idUser, int idPost);

        void AddTagToBase(Tag tag);

        void AddTagForPost(int idPost, int tagId);

        void RemoveTagForPost(int idPost, int tagId);

        void AddRepostToPost(string idUser, string text, int idPost);

        void AddAnswerToPost(string idUser, string text, int idPost);
    }
}
