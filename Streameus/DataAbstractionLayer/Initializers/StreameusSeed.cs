using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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

            var userManager = new StreameusUserManager(new StreameusUserStore(context));
            //var userManager = HttpContext.Current.GetOwinContext().GetUserManager<StreameusUserManager>();
            users.ForEach(s =>
            {
                s.Pseudo = s.FullName.Replace(" ", "");
                s.Email = s.FirstName + "." + s.LastName + "@epitech.eu";
                var result = userManager.Create(s, "123123");
                //context.Users.Add(s);
                if (result.Succeeded)
                    Console.WriteLine("User added");
                else
                    Console.WriteLine("Seed user failed");
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

            var messagesGroups = new List<MessageGroup>();
            users.Where(u => u.Id > 1).ToList().ForEach(u => messagesGroups.Add(new MessageGroup { Members = { users[0], u } }));
            context.MessagesGroups.AddRange(messagesGroups);
            context.SaveChanges();
            var date = DateTime.Now.Subtract(new TimeSpan(31, 0, 0, 0));
            foreach (var group in messagesGroups)
            {
                foreach (var user in group.Members)
                {
                    var content = String.Format("Message from '{0}' to group with Id '{1}'", user.UserName, group.Id);
                    var message = new Message { Content = content, Date = date.AddHours(group.Id), Sender = user };
                    group.Messages.Add(message);
                }
                foreach (var user in group.Members)
                {
                    var content = String.Format("Answer from '{0}' to group with Id '{1}'", user.UserName, group.Id);
                    var message = new Message { Content = content, Date = date.AddHours(group.Id), Sender = user };
                    group.Messages.Add(message);
                }
            }
            context.SaveChanges();
        }
    }
}