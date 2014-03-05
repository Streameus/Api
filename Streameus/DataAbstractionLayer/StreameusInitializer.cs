﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Streameus.Models;

namespace Streameus.DataAbstractionLayer
{
    public class StreameusInitializer : System.Data.Entity.DropCreateDatabaseAlways<StreameusContainer>
    {
        protected override void Seed(StreameusContainer context)
        {
            if (context.Users.Any()) return;
            var users = new List<User>
            {
                new User {Parameter = new Parameters(), FirstName = "Carson", LastName = "Alexander"},
                new User {Parameter = new Parameters(), FirstName = "Meredith", LastName = "Alonso"},
                new User {Parameter = new Parameters(), FirstName = "Arturo", LastName = "Anand", Gender = true},
                new User {Parameter = new Parameters(), FirstName = "Gytis", LastName = "Barzdukas"},
                new User {Parameter = new Parameters(), FirstName = "Yan", LastName = "Li"},
                new User {Parameter = new Parameters(), FirstName = "Peggy", LastName = "Justice", Gender = false},
                new User {Parameter = new Parameters(), FirstName = "Laura", LastName = "Norman", Gender = false},
                new User {Parameter = new Parameters(), FirstName = "Nino", LastName = "Olivetto", Gender = true}
            };

            users.ForEach(s =>
            {
                s.Pseudo = s.FullName;
                s.Email = s.FirstName + "." + s.LastName + "@epitech.eu";
                context.Users.Add(s);
            });
            context.SaveChanges();
            var conference = new List<Conference>
            {
                new Conference
                {
                    Owner = users[0],
                    ConferenceParameter = new ConferenceParameters(),
                    Name = "Chemistry",
                    ScheduledDuration = 3,
                },
                new Conference
                {
                    Owner = users[1],
                    ConferenceParameter = new ConferenceParameters(),
                    Name = "Microeconomics",
                    ScheduledDuration = 3,
                },
                new Conference
                {
                    Owner = users[2],
                    ConferenceParameter = new ConferenceParameters(),
                    Name = "Macroeconomics",
                    ScheduledDuration = 3,
                },
                new Conference
                {
                    Owner = users[3],
                    ConferenceParameter = new ConferenceParameters(),
                    Name = "Calculus",
                    ScheduledDuration = 4,
                },
                new Conference
                {
                    Owner = users[4],
                    ConferenceParameter = new ConferenceParameters(),
                    Name = "Trigonometry",
                    ScheduledDuration = 4,
                },
                new Conference
                {
                    Owner = users[5],
                    ConferenceParameter = new ConferenceParameters(),
                    Name = "Composition",
                    ScheduledDuration = 3,
                },
                new Conference
                {
                    Owner = users[6],
                    ConferenceParameter = new ConferenceParameters(),
                    Name = "Literature",
                    ScheduledDuration = 4,
                }
            };
            conference.ForEach(s =>
            {
                s.ConferenceParameter.Conference = s;
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