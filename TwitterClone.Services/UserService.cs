﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterClone.Models;

namespace TwitterClone.Services
{
    class UserService
    {
        public void Follow(int idUser, int targetUser)
        {
            using DbTwitterCloneContex twitterClone = new DbTwitterCloneContex();
            var user = twitterClone.Users.Where(u => u.Id == idUser).FirstOrDefault();
            var tUser = twitterClone.Users.Where(u => u.Id == targetUser).FirstOrDefault();
            if (user == null || tUser == null)
            {
                throw new ArgumentException();
            }

            var relationshipsUser = twitterClone.Relationships
                                                .Where(r => r.FolowerId == user.Id)
                                                .Where(r => r.FollowedId == tUser.Id)
                                                .FirstOrDefault();

            //var relationshipsUsers = twitterClone.Relationships.ToList();
            //Console.WriteLine("List Followers:");
            //foreach (RelationshipsUser ru in relationshipsUsers)
            //{
            //    Console.WriteLine($"{ru.Id}  {ru.FolowerId} - {ru.FollowedId}");
            //}

            if (relationshipsUser == null)
            {
                twitterClone.Relationships.Add(
                    new RelationshipsUser { FolowerId = user.Id, FollowedId = tUser.Id }
                    );
                twitterClone.SaveChanges();
            }

            //relationshipsUsers = twitterClone.Relationships.ToList();
            //Console.WriteLine("List Followers:");
            //foreach (RelationshipsUser ru in relationshipsUsers)
            //{
            //    Console.WriteLine($"{ru.Id}  {ru.FolowerId} - {ru.FollowedId}");
            //}

        }
        public void UnFollow(int idUser, int targetUser)
        {
            using DbTwitterCloneContex twitterClone = new DbTwitterCloneContex();
            var user = twitterClone.Users.Where(u => u.Id == idUser).FirstOrDefault();
            var tUser = twitterClone.Users.Where(u => u.Id == targetUser).FirstOrDefault();
            if (user == null || tUser == null)
            {
                throw new ArgumentException();
            }

            var relationshipsUser = twitterClone.Relationships
                                                .Where(r => r.FolowerId == user.Id)
                                                .Where(r => r.FollowedId == tUser.Id)
                                                .FirstOrDefault();

            //var relationshipsUsers = twitterClone.Relationships.ToList();
            //Console.WriteLine("List Followers:");
            //foreach (RelationshipsUser ru in relationshipsUsers)
            //{
            //    Console.WriteLine($"{ru.Id}  {ru.FolowerId} - {ru.FollowedId}");
            //}


            if (relationshipsUser == null)
            {
                throw new ArgumentException();
            }
            twitterClone.Relationships.Remove(relationshipsUser);
            twitterClone.SaveChanges();

            //relationshipsUsers = twitterClone.Relationships.ToList();
            //Console.WriteLine("List Followers:");
            //foreach (RelationshipsUser ru in relationshipsUsers)
            //{
            //    Console.WriteLine($"{ru.Id}  {ru.FolowerId} - {ru.FollowedId}");
            //}

        }
        public void GetPostsForUser(int idUser)
        {
            using DbTwitterCloneContex twitterClone = new DbTwitterCloneContex();
            var user = twitterClone.Users.Where(u => u.Id == idUser).FirstOrDefault();          
            if (user == null)
            {
                throw new ArgumentException();
            }

            var followings = twitterClone.Relationships
                                         .Where(r => r.FolowerId == user.Id)
                                         .Select(f => f.FollowedId);

            var posts = followings.SelectMany(u => twitterClone.Posts
                                  .Where(p => p.UserId == u))
                                  .ToList();
            
            Console.WriteLine("List Posts:");
            foreach (Post p in posts)
            {
                Console.WriteLine($"Post Id: {p.Id} UserId: {p.UserId}");
            }
        }
    }
}
