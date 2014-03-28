﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Streameus.Models;

namespace Streameus.DataAbstractionLayer.Initializers
{
    /// <summary>
    /// This class is used to seed Streameus Db
    /// </summary>
    public static class StreameusSeeder
    {
        /// <summary>
        /// This methods fills the database with dummy datas
        /// </summary>
        /// <param name="context">The dbcontext to be seeded</param>
        public static void Seed(StreameusContext context)
        {
            if (context.Users.Any()) return;
            var users = new List<User>
            {
                new User {Parameters = new Parameters(), FirstName = "Carson", LastName = "Alexander"},
                new User {Parameters = new Parameters(), FirstName = "Meredith", LastName = "Alonso"},
                new User {Parameters = new Parameters(), FirstName = "Arturo", LastName = "Anand", Gender = true},
                new User {Parameters = new Parameters(), FirstName = "Gytis", LastName = "Barzdukas"},
                new User {Parameters = new Parameters(), FirstName = "Yan", LastName = "Li"},
                new User {Parameters = new Parameters(), FirstName = "Peggy", LastName = "Justice", Gender = false},
                new User {Parameters = new Parameters(), FirstName = "Laura", LastName = "Norman", Gender = false},
                new User {Parameters = new Parameters(), FirstName = "Nino", LastName = "Olivetto", Gender = true}
            };

            users.ForEach(s =>
            {
                s.Pseudo = s.FullName;
                s.Email = s.FirstName + "." + s.LastName + "@epitech.eu";
                context.Users.Add(s);
            });
            context.SaveChanges();
            users.First().Followers.Add(users[2]);
            users.First().Followers.Add(users[4]);
            users.First().Followers.Add(users[3]);
            users.First().Followers.Add(users[5]);
            users.First().Followers.Add(users[1]);

            users[1].Followers.Add(users[0]);
            users[1].Followers.Add(users[3]);
            users[1].Followers.Add(users[4]);

            users[4].Followers.Add(users[6]);
            users[4].Followers.Add(users[5]);
            users[4].Followers.Add(users[1]);
            context.SaveChanges();


            var conference = new List<Conference>
            {
                new Conference
                {
                    Owner = users[0],
                    ConferenceParameters = new ConferenceParameters(),
                    Name = "Chemistry",
                    ScheduledDuration = 3,
                },
                new Conference
                {
                    Owner = users[1],
                    ConferenceParameters = new ConferenceParameters(),
                    Name = "Microeconomics",
                    ScheduledDuration = 3,
                },
                new Conference
                {
                    Owner = users[2],
                    ConferenceParameters = new ConferenceParameters(),
                    Name = "Macroeconomics",
                    ScheduledDuration = 3,
                },
                new Conference
                {
                    Owner = users[3],
                    ConferenceParameters = new ConferenceParameters(),
                    Name = "Calculus",
                    ScheduledDuration = 4,
                },
                new Conference
                {
                    Owner = users[4],
                    ConferenceParameters = new ConferenceParameters(),
                    Name = "Trigonometry",
                    ScheduledDuration = 4,
                },
                new Conference
                {
                    Owner = users[5],
                    ConferenceParameters = new ConferenceParameters(),
                    Name = "Composition",
                    ScheduledDuration = 3,
                },
                new Conference
                {
                    Owner = users[6],
                    ConferenceParameters = new ConferenceParameters(),
                    Name = "Literature",
                    ScheduledDuration = 4,
                }
            };
            conference.ForEach(s =>
            {
                s.OwnerId = s.Owner.Id;
                s.Description = "Description de " + s.Name;
                s.Time = DateTime.Now;
                context.Conferences.Add(s);
            });
            context.SaveChanges();
            var posts = new List<Post>
            {
                new Post {Author = users[1], Content = "Texte de post 1050", Date = DateTime.Now},
                new Post {Author = users[1], Content = "Texte de post 4022", Date = DateTime.Now},
                new Post {Author = users[1], Content = "Texte de post 4041", Date = DateTime.Now},
                new Post {Author = users[2], Content = "Texte de post 1045", Date = DateTime.Now},
                new Post {Author = users[2], Content = "Texte de post 3141", Date = DateTime.Now},
                new Post {Author = users[2], Content = "Texte de post 2021", Date = DateTime.Now},
                new Post {Author = users[3], Content = "Texte de post 1050", Date = DateTime.Now},
                new Post {Author = users[4], Content = "Texte de post 1050", Date = DateTime.Now},
                new Post {Author = users[4], Content = "Texte de post 4022", Date = DateTime.Now},
                new Post {Author = users[5], Content = "Texte de post 4041", Date = DateTime.Now},
                new Post {Author = users[6], Content = "Texte de post 1045", Date = DateTime.Now},
                new Post {Author = users[7], Content = "Texte de post 3141", Date = DateTime.Now},
            };
            posts.ForEach(s => context.Posts.Add(s));
            context.SaveChanges();
        }
    }
}