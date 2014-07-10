using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Streameus.Exceptions.HttpErrors;
using Streameus.Enums;
using Streameus.Models;

namespace Streameus.DataAbstractionLayer.Initializers
{
    /// <summary>
    /// This class is used to seed Streameus Db
    /// </summary>
    public static class StreameusSeeder
    {

#if DEBUG
        public static int userCount = 22;
        public static int conferencesCount = 22;
#else
        public static int userCount = 100;
        public static int conferencesCount = 100;
#endif

        /// <summary>
        /// This methods fills the database with dummy datas
        /// </summary>
        /// <param name="context">The dbcontext to be seeded</param>
        public static void Seed(StreameusContext context)
        {
            // Seed only if needed
            if (context.Users.Any()) return;


            // Users
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
            for (var i = 0; i < userCount; i++)
            {
                users.Add(new User
                {
                    Parameters = new Parameters(),
                    FirstName = Faker.Name.First(),
                    LastName = Faker.Name.Last(),
                    Email = Faker.Internet.Email(),
                    Description = Faker.Lorem.Sentence(),
                    Country = Faker.Address.Country(),
                    City = Faker.Address.City(),
                    Address = Faker.Address.StreetAddress(),
                    Phone = Faker.Phone.Number(),
                    Website = Faker.Internet.DomainName(),
                    Profession = Faker.Company.Name(),
                });
                users.Last().Pseudo = (users.Last().FirstName + users.Last().LastName).Replace(" ", "");
            }
            var userManager = new StreameusUserManager(new StreameusUserStore(context));
            users.ForEach(s =>
            {
                // Static users
                if (String.IsNullOrWhiteSpace(s.Email))
                {
                    s.Pseudo = s.FullName.Replace(" ", "");
                    s.Email = s.FirstName + "." + s.LastName + "@epitech.eu";
                    var result = userManager.Create(s, "123123");
                    if (!result.Succeeded)
                        throw new Exception("Seed user failed");
                }
            });
            context.SaveChanges();

            foreach (var user in users)
            {
                var i = 0;
                foreach (var follower in users)
                {
                    if (i++ % 3 == 0)
                        user.Followers.Add(follower);
                }
            }
            context.SaveChanges();


            // Categories
            var confCategories = new List<string> {
                "IT",
                "Economics",
                "Science",
                "Life",
                "Politics",
                "Arts",
                "Miscellaneous",
            };
            foreach (var confCategory in confCategories)
            {
                context.ConferenceCategories.Add(new ConferenceCategory
                {
                    Name = confCategory,
                    Description = Faker.Lorem.Sentence(),
                });
            }
            context.SaveChanges();

            // Conferences
            var conferences = new List<string> {
                "Introduction au Javascript",
                "Integration de Swagger",
                "Les changements windows 8",
                "Economiser la batterie de son portable",
                "Changer son disque dur",
                "Echange autour de la bourse de Paris",
                "Les taxes, comment s'en sortir ?",
                "Introduction à la comptabilité",
                "Comment trouver un bon business plan ",
                "commerce international, les premières notions",
                "Discussion autour des lois de Newton",
                "Le réchauffement climatique en chiffres",
                "Imprimante 3D, vers l'impression d'organes ?",
                "Les différentes planêtes de l'univers",
                "Préparation au bac : Le nucléaire",
                "Un régime facile et efficace",
                "Comment bien dormir",
                "Mode : Comment s'habiller pour pas chère",
                "Les conseils pour parfaire sa silhouete",
                "Coaching yoga, détente et relaxation",
                "Pourquoi Nicolas Sarkozy risque gros",
                "Etude de la monté du FN lors des européennes",
                "Discussion autour de l'UMP",
                "Le système banquaire des USA",
                "Le conflit israélo-palestinien",
                "L'art, reflet de la société",
                "Interpréter l'art : entre voir et savoirs",
                "l'allégorie de la caverne",
                "Les tableaux de dali",
                "Kant : Qu'est ce que les lumières",
                "Créer un meuble télé pour les nuls",
                "Des conseils pour s'organiser dans la vie",
                "Présentation d'un pays : la Suède",
                "Que demander lors de l'achat d'une voiture d'occasion",
                "Romans de fiction : présentation et recommandation",
            };

            var confNumber = 0;
            userCount = context.Users.Count();
            var random = new Random();
            foreach (var conference in conferences)
            {
                var index = random.Next(userCount - 1);
                var user = context.Users.Find(index + 1);
                var category = context.ConferenceCategories.Find(confNumber / 5 + 1);
                var conferenceEntity = new Conference
                {
                    Category = category,
                    Name = conference,
                    Description = Faker.Lorem.Sentence(),
                    Owner = user,
                    Time = DateTime.Now.Subtract(new TimeSpan(1, confNumber, 0, 0)),
                };
                category.Conferences.Add(conferenceEntity);
                context.Conferences.Add(conferenceEntity);
                user.ConferencesCreated.Add(conferenceEntity);
            }
            var errors = context.GetValidationErrors();
            context.SaveChanges();
            foreach (var user in context.Users)
            {

                for (var i = 0; i < 5; i++)
                {
                    var conference = context.Conferences.Find(i + 1);
                    context.Events.Add(new Event
                    {
                        Author = users[1],
                        Type = DataBaseEnums.EventType.ParticipateConf,
                        Date = DateTime.Now,
                        EventItems = new List<EventItem>
                        {
                            new EventItem {Pos = 0, TargetType = DataBaseEnums.EventItemType.User, TargetId = user.Id, Content = users[1].Pseudo},
                            new EventItem {Pos = 1, TargetType = DataBaseEnums.EventItemType.Conference, TargetId = conference.Id, Content = conference.Name},
                        }
                    });
                    context.Events.Add(new Event
                    {
                        Author = users[1],
                        Type = DataBaseEnums.EventType.CreateConf,
                        Date = DateTime.Now,
                        EventItems = new List<EventItem>
                        {
                            new EventItem {Pos = 0, TargetType = DataBaseEnums.EventItemType.User, TargetId = users[2].Id, Content = users[2].Pseudo},
                            new EventItem {Pos = 1, TargetType = DataBaseEnums.EventItemType.Conference, TargetId = conference.Id, Content = conference.Name},
                            new EventItem {Pos = 2, TargetType = DataBaseEnums.EventItemType.DateTime, TargetId = conference.Id, Content = conference.Time.ToShortDateString()},
                            new EventItem {Pos = 3, TargetType = DataBaseEnums.EventItemType.DateTime, TargetId = conference.Id, Content = conference.Time.ToShortTimeString()},
                        }
                    });
                    context.Events.Add(new Event
                    {
                        Author = users[1],
                        Type = DataBaseEnums.EventType.StartFollow,
                        Date = DateTime.Now,
                        EventItems = new List<EventItem>
                        {
                            new EventItem {Pos = 0, TargetType = DataBaseEnums.EventItemType.User, TargetId = users[2].Id, Content = users[2].Pseudo},
                            new EventItem {Pos = 1, TargetType = DataBaseEnums.EventItemType.User, TargetId = users[3].Id, Content = users[3].Pseudo},
                        }
                    });
                    context.Events.Add(new Event
                    {
                        Author = users[2],
                        Type = DataBaseEnums.EventType.SuscribeConf,
                        Date = DateTime.Now,
                        EventItems = new List<EventItem>
                            {
                                new EventItem {Pos = 0, TargetType = DataBaseEnums.EventItemType.User, TargetId = users[3].Id, Content = users[3].Pseudo},
                                new EventItem {Pos = 1, TargetType = DataBaseEnums.EventItemType.Conference, TargetId = conference.Id, Content = conference.Name},
                            }
                    });
                    context.Events.Add(new Event
                    {
                        Author = users[4],
                        Type = DataBaseEnums.EventType.CreateConf,
                        Date = DateTime.Now,
                        EventItems = new List<EventItem>
                        {
                            new EventItem {Pos = 0, TargetType = DataBaseEnums.EventItemType.User, TargetId = users[5].Id, Content = users[5].Pseudo},
                            new EventItem {Pos = 1, TargetType = DataBaseEnums.EventItemType.Conference, TargetId = conference.Id, Content = conference.Name},
                            new EventItem {Pos = 2, TargetType = DataBaseEnums.EventItemType.DateTime, TargetId = conference.Id, Content = conference.Time.ToShortDateString()},
                            new EventItem {Pos = 3, TargetType = DataBaseEnums.EventItemType.DateTime, TargetId = conference.Id, Content = conference.Time.ToShortTimeString()},
                        }
                    });
                    context.Events.Add(new Event
                    {
                        Author = users[5],
                        Type = DataBaseEnums.EventType.StartFollow,
                        Date = DateTime.Now,
                        EventItems = new List<EventItem>
                        {
                            new EventItem {Pos = 0, TargetType = DataBaseEnums.EventItemType.User, TargetId = users[6].Id, Content = users[6].Pseudo},
                            new EventItem {Pos = 1, TargetType = DataBaseEnums.EventItemType.User, TargetId = users[1].Id, Content = users[1].Pseudo},
                        }
                    });
                }
            }
            context.SaveChanges();


            // Messages
            var messagesGroups = new List<MessageGroup>();
            foreach (var sender in users)
            {
                foreach (var recipient in users)
                {
                    if (sender.Id < recipient.Id && random.Next(5) <= 3)
                        messagesGroups.Add(new MessageGroup { Members = { sender, recipient } });
                }
            }
            users.Where(u => u.Id > 1).ToList().ForEach(u => messagesGroups.Add(new MessageGroup { Members = { users[0], u } }));
            context.MessagesGroups.AddRange(messagesGroups);
            context.SaveChanges();
            var date = DateTime.Now.Subtract(new TimeSpan(31, 0, 0, 0));
            const int msgPerGroup = 70;
            foreach (var group in messagesGroups)
            {
                for (var i = 0; i < msgPerGroup; i++)
                {
                    var user = i % 2 == 0 ? group.Members.First() : group.Members.Last();
                    var content = Faker.Lorem.Paragraph();
                    var message = new Message { Content = content, Date = date.AddMinutes(i * 5), Sender = user };
                    group.Messages.Add(message);
                }
            }
            context.SaveChanges();
        }
    }
}