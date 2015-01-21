namespace Streameus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        Message = c.String(nullable: false, unicode: false),
                        Date = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pseudo = c.String(nullable: false, unicode: false),
                        Language = c.Int(nullable: false),
                        Email = c.String(nullable: false, unicode: false),
                        EmailVisibility = c.Boolean(nullable: false),
                        FirstName = c.String(nullable: false, unicode: false),
                        FirstNameVisibility = c.Boolean(nullable: false),
                        LastName = c.String(nullable: false, unicode: false),
                        LastNameVisibility = c.Boolean(nullable: false),
                        Gender = c.Boolean(),
                        GenderVisibility = c.Boolean(nullable: false),
                        Reputation = c.Double(nullable: false),
                        AbonnementsVisibility = c.Boolean(nullable: false),
                        BirthDayVisibility = c.Boolean(nullable: false),
                        PhoneVisibility = c.Boolean(nullable: false),
                        AddressVisibility = c.Boolean(nullable: false),
                        CityVisibility = c.Boolean(nullable: false),
                        CountryVisibility = c.Boolean(nullable: false),
                        WebsiteVisibility = c.Boolean(nullable: false),
                        ProfessionVisibility = c.Boolean(nullable: false),
                        DescriptionVisibility = c.Boolean(nullable: false),
                        BirthDay = c.DateTime(precision: 0),
                        Phone = c.String(nullable: false, unicode: false),
                        Address = c.String(nullable: false, unicode: false),
                        City = c.String(nullable: false, unicode: false),
                        Country = c.String(nullable: false, unicode: false),
                        Website = c.String(nullable: false, unicode: false),
                        Profession = c.String(nullable: false, unicode: false),
                        Description = c.String(nullable: false, unicode: false),
                        Parameters_NotifMail = c.Boolean(nullable: false),
                        StripeCustomerId = c.String(unicode: false),
                        Balance = c.Double(nullable: false),
                        UserName = c.String(unicode: false),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(unicode: false),
                        SecurityStamp = c.String(unicode: false),
                        PhoneNumber = c.String(unicode: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 0),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(unicode: false),
                        ClaimValue = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Conferences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OwnerId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        Name = c.String(nullable: false, unicode: false),
                        Description = c.String(nullable: false, unicode: false),
                        Status = c.Int(nullable: false),
                        Time = c.DateTime(nullable: false, precision: 0),
                        ScheduledDuration = c.Int(nullable: false),
                        FinalDuration = c.Int(nullable: false),
                        RoomId = c.String(unicode: false),
                        PrezziewUrl = c.String(unicode: false),
                        EntranceFee = c.Double(nullable: false),
                        Mark = c.Double(nullable: false),
                        ConferenceParameters_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ConferenceCategories", t => t.CategoryId)
                .ForeignKey("dbo.ConferenceParameters", t => t.ConferenceParameters_Id)
                .ForeignKey("dbo.Users", t => t.OwnerId)
                .Index(t => t.OwnerId)
                .Index(t => t.CategoryId)
                .Index(t => t.ConferenceParameters_Id);
            
            CreateTable(
                "dbo.ConferenceCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Description = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ConferenceParameters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Double(nullable: false),
                        FreeTime = c.Int(nullable: false),
                        CanAskQuestions = c.Boolean(nullable: false),
                        CansAskVoiceQuestions = c.Boolean(nullable: false),
                        Visibility = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConferenceId = c.Int(nullable: false),
                        FileName = c.String(nullable: false, unicode: false),
                        Path = c.String(nullable: false, unicode: false),
                        Size = c.Int(nullable: false),
                        Downloads = c.Int(nullable: false),
                        UploadDate = c.DateTime(nullable: false, precision: 0),
                        LastDownloadDate = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conferences", t => t.ConferenceId, cascadeDelete: true)
                .Index(t => t.ConferenceId);
            
            CreateTable(
                "dbo.UserMarks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ConferenceId = c.Int(nullable: false),
                        Mark = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conferences", t => t.ConferenceId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ConferenceId);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Visibility = c.Boolean(nullable: false),
                        Type = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false, precision: 0),
                        AuthorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AuthorId, cascadeDelete: true)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.EventItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TargetId = c.Int(nullable: false),
                        TargetType = c.Int(nullable: false),
                        Pos = c.Int(nullable: false),
                        Content = c.String(unicode: false),
                        Event_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.Event_Id)
                .Index(t => t.Event_Id);
            
            CreateTable(
                "dbo.CustomUserLogins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LoginProvider = c.String(unicode: false),
                        ProviderKey = c.String(unicode: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.MessageGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, unicode: false),
                        Date = c.DateTime(nullable: false, precision: 0),
                        SenderId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MessageGroups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.SenderId, cascadeDelete: true)
                .Index(t => t.SenderId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.CustomUserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        CustomRole_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.CustomRoles", t => t.CustomRole_Id)
                .Index(t => t.UserId)
                .Index(t => t.CustomRole_Id);
            
            CreateTable(
                "dbo.CustomRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ConferenceParametersUser",
                c => new
                    {
                        ConferenceParametersUser_User_Id = c.Int(nullable: false),
                        FreeUsers_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ConferenceParametersUser_User_Id, t.FreeUsers_Id })
                .ForeignKey("dbo.ConferenceParameters", t => t.ConferenceParametersUser_User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.FreeUsers_Id, cascadeDelete: true)
                .Index(t => t.ConferenceParametersUser_User_Id)
                .Index(t => t.FreeUsers_Id);
            
            CreateTable(
                "dbo.ConferenceParametersIntervenants",
                c => new
                    {
                        ConferenceParametersIntervenants_User_Id = c.Int(nullable: false),
                        Intervenants_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ConferenceParametersIntervenants_User_Id, t.Intervenants_Id })
                .ForeignKey("dbo.ConferenceParameters", t => t.ConferenceParametersIntervenants_User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Intervenants_Id, cascadeDelete: true)
                .Index(t => t.ConferenceParametersIntervenants_User_Id)
                .Index(t => t.Intervenants_Id);
            
            CreateTable(
                "dbo.ConferenceParticipants",
                c => new
                    {
                        Conference_Id = c.Int(nullable: false),
                        Participant_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Conference_Id, t.Participant_Id })
                .ForeignKey("dbo.Conferences", t => t.Conference_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Participant_Id, cascadeDelete: true)
                .Index(t => t.Conference_Id)
                .Index(t => t.Participant_Id);
            
            CreateTable(
                "dbo.ConferenceRegistred",
                c => new
                    {
                        Conference_Id = c.Int(nullable: false),
                        Registred_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Conference_Id, t.Registred_Id })
                .ForeignKey("dbo.Conferences", t => t.Conference_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Registred_Id, cascadeDelete: true)
                .Index(t => t.Conference_Id)
                .Index(t => t.Registred_Id);
            
            CreateTable(
                "dbo.ConferenceSpeakers",
                c => new
                    {
                        Conference_Id = c.Int(nullable: false),
                        Speaker_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Conference_Id, t.Speaker_Id })
                .ForeignKey("dbo.Conferences", t => t.Conference_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Speaker_Id, cascadeDelete: true)
                .Index(t => t.Conference_Id)
                .Index(t => t.Speaker_Id);
            
            CreateTable(
                "dbo.AbonnementsFollowers",
                c => new
                    {
                        Followers_Id = c.Int(nullable: false),
                        Abonnements_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Followers_Id, t.Abonnements_Id })
                .ForeignKey("dbo.Users", t => t.Followers_Id)
                .ForeignKey("dbo.Users", t => t.Abonnements_Id)
                .Index(t => t.Followers_Id)
                .Index(t => t.Abonnements_Id);
            
            CreateTable(
                "dbo.MessagesGroupsMembers",
                c => new
                    {
                        Group_Id = c.Int(nullable: false),
                        Member_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Group_Id, t.Member_Id })
                .ForeignKey("dbo.MessageGroups", t => t.Group_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Member_Id, cascadeDelete: true)
                .Index(t => t.Group_Id)
                .Index(t => t.Member_Id);
            
            CreateTable(
                "dbo.MessageGroupReaders",
                c => new
                    {
                        MessageGroup_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MessageGroup_Id, t.User_Id })
                .ForeignKey("dbo.MessageGroups", t => t.MessageGroup_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.MessageGroup_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomUserRoles", "CustomRole_Id", "dbo.CustomRoles");
            DropForeignKey("dbo.Comments", "EventId", "dbo.Events");
            DropForeignKey("dbo.Comments", "Id", "dbo.Users");
            DropForeignKey("dbo.CustomUserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.MessageGroupReaders", "User_Id", "dbo.Users");
            DropForeignKey("dbo.MessageGroupReaders", "MessageGroup_Id", "dbo.MessageGroups");
            DropForeignKey("dbo.Messages", "SenderId", "dbo.Users");
            DropForeignKey("dbo.Messages", "GroupId", "dbo.MessageGroups");
            DropForeignKey("dbo.MessagesGroupsMembers", "Member_Id", "dbo.Users");
            DropForeignKey("dbo.MessagesGroupsMembers", "Group_Id", "dbo.MessageGroups");
            DropForeignKey("dbo.CustomUserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.AbonnementsFollowers", "Abonnements_Id", "dbo.Users");
            DropForeignKey("dbo.AbonnementsFollowers", "Followers_Id", "dbo.Users");
            DropForeignKey("dbo.EventItems", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Events", "AuthorId", "dbo.Users");
            DropForeignKey("dbo.ConferenceSpeakers", "Speaker_Id", "dbo.Users");
            DropForeignKey("dbo.ConferenceSpeakers", "Conference_Id", "dbo.Conferences");
            DropForeignKey("dbo.ConferenceRegistred", "Registred_Id", "dbo.Users");
            DropForeignKey("dbo.ConferenceRegistred", "Conference_Id", "dbo.Conferences");
            DropForeignKey("dbo.ConferenceParticipants", "Participant_Id", "dbo.Users");
            DropForeignKey("dbo.ConferenceParticipants", "Conference_Id", "dbo.Conferences");
            DropForeignKey("dbo.Conferences", "OwnerId", "dbo.Users");
            DropForeignKey("dbo.UserMarks", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserMarks", "ConferenceId", "dbo.Conferences");
            DropForeignKey("dbo.Documents", "ConferenceId", "dbo.Conferences");
            DropForeignKey("dbo.Conferences", "ConferenceParameters_Id", "dbo.ConferenceParameters");
            DropForeignKey("dbo.ConferenceParametersIntervenants", "Intervenants_Id", "dbo.Users");
            DropForeignKey("dbo.ConferenceParametersIntervenants", "ConferenceParametersIntervenants_User_Id", "dbo.ConferenceParameters");
            DropForeignKey("dbo.ConferenceParametersUser", "FreeUsers_Id", "dbo.Users");
            DropForeignKey("dbo.ConferenceParametersUser", "ConferenceParametersUser_User_Id", "dbo.ConferenceParameters");
            DropForeignKey("dbo.Conferences", "CategoryId", "dbo.ConferenceCategories");
            DropForeignKey("dbo.CustomUserClaims", "UserId", "dbo.Users");
            DropIndex("dbo.MessageGroupReaders", new[] { "User_Id" });
            DropIndex("dbo.MessageGroupReaders", new[] { "MessageGroup_Id" });
            DropIndex("dbo.MessagesGroupsMembers", new[] { "Member_Id" });
            DropIndex("dbo.MessagesGroupsMembers", new[] { "Group_Id" });
            DropIndex("dbo.AbonnementsFollowers", new[] { "Abonnements_Id" });
            DropIndex("dbo.AbonnementsFollowers", new[] { "Followers_Id" });
            DropIndex("dbo.ConferenceSpeakers", new[] { "Speaker_Id" });
            DropIndex("dbo.ConferenceSpeakers", new[] { "Conference_Id" });
            DropIndex("dbo.ConferenceRegistred", new[] { "Registred_Id" });
            DropIndex("dbo.ConferenceRegistred", new[] { "Conference_Id" });
            DropIndex("dbo.ConferenceParticipants", new[] { "Participant_Id" });
            DropIndex("dbo.ConferenceParticipants", new[] { "Conference_Id" });
            DropIndex("dbo.ConferenceParametersIntervenants", new[] { "Intervenants_Id" });
            DropIndex("dbo.ConferenceParametersIntervenants", new[] { "ConferenceParametersIntervenants_User_Id" });
            DropIndex("dbo.ConferenceParametersUser", new[] { "FreeUsers_Id" });
            DropIndex("dbo.ConferenceParametersUser", new[] { "ConferenceParametersUser_User_Id" });
            DropIndex("dbo.CustomUserRoles", new[] { "CustomRole_Id" });
            DropIndex("dbo.CustomUserRoles", new[] { "UserId" });
            DropIndex("dbo.Messages", new[] { "GroupId" });
            DropIndex("dbo.Messages", new[] { "SenderId" });
            DropIndex("dbo.CustomUserLogins", new[] { "UserId" });
            DropIndex("dbo.EventItems", new[] { "Event_Id" });
            DropIndex("dbo.Events", new[] { "AuthorId" });
            DropIndex("dbo.UserMarks", new[] { "ConferenceId" });
            DropIndex("dbo.UserMarks", new[] { "UserId" });
            DropIndex("dbo.Documents", new[] { "ConferenceId" });
            DropIndex("dbo.Conferences", new[] { "ConferenceParameters_Id" });
            DropIndex("dbo.Conferences", new[] { "CategoryId" });
            DropIndex("dbo.Conferences", new[] { "OwnerId" });
            DropIndex("dbo.CustomUserClaims", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "EventId" });
            DropIndex("dbo.Comments", new[] { "Id" });
            DropTable("dbo.MessageGroupReaders");
            DropTable("dbo.MessagesGroupsMembers");
            DropTable("dbo.AbonnementsFollowers");
            DropTable("dbo.ConferenceSpeakers");
            DropTable("dbo.ConferenceRegistred");
            DropTable("dbo.ConferenceParticipants");
            DropTable("dbo.ConferenceParametersIntervenants");
            DropTable("dbo.ConferenceParametersUser");
            DropTable("dbo.CustomRoles");
            DropTable("dbo.CustomUserRoles");
            DropTable("dbo.Messages");
            DropTable("dbo.MessageGroups");
            DropTable("dbo.CustomUserLogins");
            DropTable("dbo.EventItems");
            DropTable("dbo.Events");
            DropTable("dbo.UserMarks");
            DropTable("dbo.Documents");
            DropTable("dbo.ConferenceParameters");
            DropTable("dbo.ConferenceCategories");
            DropTable("dbo.Conferences");
            DropTable("dbo.CustomUserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Comments");
        }
    }
}
