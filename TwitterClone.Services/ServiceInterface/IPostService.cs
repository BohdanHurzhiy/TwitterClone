using System;
using System.Linq;
using TwitterClone.Models;
using System.Collections.Generic;

namespace TwitterClone.Services.ServiceInterface
{
    public interface IPostService
    {
        void AddPost(int idUser, string text);

        void RemovePost(int idPost);

        void LikeUnLikePost(int idUser, int idPost);

        void AddTagToBase(Tag tag);

        void AddTagForPost(int idPost, int tagId);

        void RemoveTagForPost(int idPost, int tagId);

        void AddRepostToPost(int idUser, string text, int idPost);

        void AddAnswerToPost(int idUser, string text, int idPost);
    }
}
