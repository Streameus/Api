
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/03/2014 18:34:02
-- Generated from EDMX file: C:\Users\bruyer_k\documents\visual studio 2013\Projects\Streameus\Streameus\DataAbstractionLayer\Streameus.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Streameus];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PostComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_PostComment];
GO
IF OBJECT_ID(N'[dbo].[FK_UserComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_UserComment];
GO
IF OBJECT_ID(N'[dbo].[FK_OwnerConference]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Conferences] DROP CONSTRAINT [FK_OwnerConference];
GO
IF OBJECT_ID(N'[dbo].[FK_ConferenceMembers_Conference]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ConferenceMembers] DROP CONSTRAINT [FK_ConferenceMembers_Conference];
GO
IF OBJECT_ID(N'[dbo].[FK_ConferenceMembers_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ConferenceMembers] DROP CONSTRAINT [FK_ConferenceMembers_User];
GO
IF OBJECT_ID(N'[dbo].[FK_ConferenceDocument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Documents] DROP CONSTRAINT [FK_ConferenceDocument];
GO
IF OBJECT_ID(N'[dbo].[FK_ConferenceConferenceParameters]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Conferences] DROP CONSTRAINT [FK_ConferenceConferenceParameters];
GO
IF OBJECT_ID(N'[dbo].[FK_ConferenceParametersUser_ConferenceParameters]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ConferenceParametersUser] DROP CONSTRAINT [FK_ConferenceParametersUser_ConferenceParameters];
GO
IF OBJECT_ID(N'[dbo].[FK_ConferenceParametersUser_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ConferenceParametersUser] DROP CONSTRAINT [FK_ConferenceParametersUser_User];
GO
IF OBJECT_ID(N'[dbo].[FK_ConferenceParametersIntervenants_ConferenceParameters]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ConferenceParametersIntervenants] DROP CONSTRAINT [FK_ConferenceParametersIntervenants_ConferenceParameters];
GO
IF OBJECT_ID(N'[dbo].[FK_ConferenceParametersIntervenants_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ConferenceParametersIntervenants] DROP CONSTRAINT [FK_ConferenceParametersIntervenants_User];
GO
IF OBJECT_ID(N'[dbo].[FK_PostAuthor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Posts] DROP CONSTRAINT [FK_PostAuthor];
GO
IF OBJECT_ID(N'[dbo].[FK_AbonnementsFollowers_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AbonnementsFollowers] DROP CONSTRAINT [FK_AbonnementsFollowers_User];
GO
IF OBJECT_ID(N'[dbo].[FK_AbonnementsFollowers_User1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AbonnementsFollowers] DROP CONSTRAINT [FK_AbonnementsFollowers_User1];
GO
IF OBJECT_ID(N'[dbo].[FK_UserParameters]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Parameters] DROP CONSTRAINT [FK_UserParameters];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Comments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comments];
GO
IF OBJECT_ID(N'[dbo].[Conferences]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Conferences];
GO
IF OBJECT_ID(N'[dbo].[Documents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Documents];
GO
IF OBJECT_ID(N'[dbo].[ConferenceParameters]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConferenceParameters];
GO
IF OBJECT_ID(N'[dbo].[Parameters]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Parameters];
GO
IF OBJECT_ID(N'[dbo].[Posts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Posts];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[ConferenceMembers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConferenceMembers];
GO
IF OBJECT_ID(N'[dbo].[ConferenceParametersUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConferenceParametersUser];
GO
IF OBJECT_ID(N'[dbo].[ConferenceParametersIntervenants]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConferenceParametersIntervenants];
GO
IF OBJECT_ID(N'[dbo].[AbonnementsFollowers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AbonnementsFollowers];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Comments'
CREATE TABLE [dbo].[Comments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PostId] int  NOT NULL,
    [AuthorId] int  NOT NULL,
    [Message] nvarchar(max)  NOT NULL,
    [Date] datetime  NOT NULL
);
GO

-- Creating table 'Conferences'
CREATE TABLE [dbo].[Conferences] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OwnerId] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Status] int  NOT NULL,
    [Time] datetime  NOT NULL,
    [ScheduledDuration] int  NOT NULL,
    [FinalDuration] int  NOT NULL,
    [ConferenceParameter_Id] int  NOT NULL
);
GO

-- Creating table 'Documents'
CREATE TABLE [dbo].[Documents] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ConferenceId] int  NOT NULL,
    [FileName] nvarchar(max)  NOT NULL,
    [Path] nvarchar(max)  NOT NULL,
    [Size] int  NOT NULL,
    [Downloads] int  NOT NULL,
    [UploadDate] datetime  NOT NULL,
    [LastDownloadDate] datetime  NULL
);
GO

-- Creating table 'ConferenceParameters'
CREATE TABLE [dbo].[ConferenceParameters] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Price] float  NOT NULL,
    [FreeTime] int  NOT NULL,
    [CanAskQuestions] bit  NOT NULL,
    [CansAskVoiceQuestions] bit  NOT NULL,
    [Visibility] bit  NOT NULL
);
GO

-- Creating table 'Parameters'
CREATE TABLE [dbo].[Parameters] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NotifMail] bit  NOT NULL
);
GO

-- Creating table 'Posts'
CREATE TABLE [dbo].[Posts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Visibility] bit  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [Date] datetime  NOT NULL,
    [Author_Id] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Pseudo] nvarchar(max)  NOT NULL,
    [Language] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [EmailVisibility] bit  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [FirstNameVisibility] bit  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [LastNameVisibility] bit  NOT NULL,
    [PicturePath] nvarchar(max)  NOT NULL,
    [Gender] bit  NULL,
    [GenderVisibility] bit  NOT NULL,
    [Reputation] int  NOT NULL,
    [AbonnementsVisibility] bit  NOT NULL,
    [BirthDay] datetime  NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NOT NULL,
    [Country] nvarchar(max)  NOT NULL,
    [Website] nvarchar(max)  NOT NULL,
    [Profession] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Parameter_Id] int  NOT NULL
);
GO

-- Creating table 'ConferenceMembers'
CREATE TABLE [dbo].[ConferenceMembers] (
    [Conferences_Id] int  NOT NULL,
    [Members_Id] int  NOT NULL
);
GO

-- Creating table 'ConferenceParametersUser'
CREATE TABLE [dbo].[ConferenceParametersUser] (
    [ConferenceParametersUser_User_Id] int  NOT NULL,
    [FreeUsers_Id] int  NOT NULL
);
GO

-- Creating table 'ConferenceParametersIntervenants'
CREATE TABLE [dbo].[ConferenceParametersIntervenants] (
    [ConferenceParametersIntervenants_User_Id] int  NOT NULL,
    [Intervenants_Id] int  NOT NULL
);
GO

-- Creating table 'AbonnementsFollowers'
CREATE TABLE [dbo].[AbonnementsFollowers] (
    [Followers_Id] int  NOT NULL,
    [Abonnements_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [PK_Comments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Conferences'
ALTER TABLE [dbo].[Conferences]
ADD CONSTRAINT [PK_Conferences]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Documents'
ALTER TABLE [dbo].[Documents]
ADD CONSTRAINT [PK_Documents]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ConferenceParameters'
ALTER TABLE [dbo].[ConferenceParameters]
ADD CONSTRAINT [PK_ConferenceParameters]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Parameters'
ALTER TABLE [dbo].[Parameters]
ADD CONSTRAINT [PK_Parameters]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [PK_Posts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Conferences_Id], [Members_Id] in table 'ConferenceMembers'
ALTER TABLE [dbo].[ConferenceMembers]
ADD CONSTRAINT [PK_ConferenceMembers]
    PRIMARY KEY CLUSTERED ([Conferences_Id], [Members_Id] ASC);
GO

-- Creating primary key on [ConferenceParametersUser_User_Id], [FreeUsers_Id] in table 'ConferenceParametersUser'
ALTER TABLE [dbo].[ConferenceParametersUser]
ADD CONSTRAINT [PK_ConferenceParametersUser]
    PRIMARY KEY CLUSTERED ([ConferenceParametersUser_User_Id], [FreeUsers_Id] ASC);
GO

-- Creating primary key on [ConferenceParametersIntervenants_User_Id], [Intervenants_Id] in table 'ConferenceParametersIntervenants'
ALTER TABLE [dbo].[ConferenceParametersIntervenants]
ADD CONSTRAINT [PK_ConferenceParametersIntervenants]
    PRIMARY KEY CLUSTERED ([ConferenceParametersIntervenants_User_Id], [Intervenants_Id] ASC);
GO

-- Creating primary key on [Followers_Id], [Abonnements_Id] in table 'AbonnementsFollowers'
ALTER TABLE [dbo].[AbonnementsFollowers]
ADD CONSTRAINT [PK_AbonnementsFollowers]
    PRIMARY KEY CLUSTERED ([Followers_Id], [Abonnements_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [PostId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_PostComment]
    FOREIGN KEY ([PostId])
    REFERENCES [dbo].[Posts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PostComment'
CREATE INDEX [IX_FK_PostComment]
ON [dbo].[Comments]
    ([PostId]);
GO

-- Creating foreign key on [AuthorId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_UserComment]
    FOREIGN KEY ([AuthorId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserComment'
CREATE INDEX [IX_FK_UserComment]
ON [dbo].[Comments]
    ([AuthorId]);
GO

-- Creating foreign key on [OwnerId] in table 'Conferences'
ALTER TABLE [dbo].[Conferences]
ADD CONSTRAINT [FK_OwnerConference]
    FOREIGN KEY ([OwnerId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OwnerConference'
CREATE INDEX [IX_FK_OwnerConference]
ON [dbo].[Conferences]
    ([OwnerId]);
GO

-- Creating foreign key on [Conferences_Id] in table 'ConferenceMembers'
ALTER TABLE [dbo].[ConferenceMembers]
ADD CONSTRAINT [FK_ConferenceMembers_Conference]
    FOREIGN KEY ([Conferences_Id])
    REFERENCES [dbo].[Conferences]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Members_Id] in table 'ConferenceMembers'
ALTER TABLE [dbo].[ConferenceMembers]
ADD CONSTRAINT [FK_ConferenceMembers_User]
    FOREIGN KEY ([Members_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ConferenceMembers_User'
CREATE INDEX [IX_FK_ConferenceMembers_User]
ON [dbo].[ConferenceMembers]
    ([Members_Id]);
GO

-- Creating foreign key on [ConferenceId] in table 'Documents'
ALTER TABLE [dbo].[Documents]
ADD CONSTRAINT [FK_ConferenceDocument]
    FOREIGN KEY ([ConferenceId])
    REFERENCES [dbo].[Conferences]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ConferenceDocument'
CREATE INDEX [IX_FK_ConferenceDocument]
ON [dbo].[Documents]
    ([ConferenceId]);
GO

-- Creating foreign key on [ConferenceParameter_Id] in table 'Conferences'
ALTER TABLE [dbo].[Conferences]
ADD CONSTRAINT [FK_ConferenceConferenceParameters]
    FOREIGN KEY ([ConferenceParameter_Id])
    REFERENCES [dbo].[ConferenceParameters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ConferenceConferenceParameters'
CREATE INDEX [IX_FK_ConferenceConferenceParameters]
ON [dbo].[Conferences]
    ([ConferenceParameter_Id]);
GO

-- Creating foreign key on [ConferenceParametersUser_User_Id] in table 'ConferenceParametersUser'
ALTER TABLE [dbo].[ConferenceParametersUser]
ADD CONSTRAINT [FK_ConferenceParametersUser_ConferenceParameters]
    FOREIGN KEY ([ConferenceParametersUser_User_Id])
    REFERENCES [dbo].[ConferenceParameters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [FreeUsers_Id] in table 'ConferenceParametersUser'
ALTER TABLE [dbo].[ConferenceParametersUser]
ADD CONSTRAINT [FK_ConferenceParametersUser_User]
    FOREIGN KEY ([FreeUsers_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ConferenceParametersUser_User'
CREATE INDEX [IX_FK_ConferenceParametersUser_User]
ON [dbo].[ConferenceParametersUser]
    ([FreeUsers_Id]);
GO

-- Creating foreign key on [ConferenceParametersIntervenants_User_Id] in table 'ConferenceParametersIntervenants'
ALTER TABLE [dbo].[ConferenceParametersIntervenants]
ADD CONSTRAINT [FK_ConferenceParametersIntervenants_ConferenceParameters]
    FOREIGN KEY ([ConferenceParametersIntervenants_User_Id])
    REFERENCES [dbo].[ConferenceParameters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Intervenants_Id] in table 'ConferenceParametersIntervenants'
ALTER TABLE [dbo].[ConferenceParametersIntervenants]
ADD CONSTRAINT [FK_ConferenceParametersIntervenants_User]
    FOREIGN KEY ([Intervenants_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ConferenceParametersIntervenants_User'
CREATE INDEX [IX_FK_ConferenceParametersIntervenants_User]
ON [dbo].[ConferenceParametersIntervenants]
    ([Intervenants_Id]);
GO

-- Creating foreign key on [Author_Id] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [FK_PostAuthor]
    FOREIGN KEY ([Author_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PostAuthor'
CREATE INDEX [IX_FK_PostAuthor]
ON [dbo].[Posts]
    ([Author_Id]);
GO

-- Creating foreign key on [Followers_Id] in table 'AbonnementsFollowers'
ALTER TABLE [dbo].[AbonnementsFollowers]
ADD CONSTRAINT [FK_AbonnementsFollowers_User]
    FOREIGN KEY ([Followers_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Abonnements_Id] in table 'AbonnementsFollowers'
ALTER TABLE [dbo].[AbonnementsFollowers]
ADD CONSTRAINT [FK_AbonnementsFollowers_User1]
    FOREIGN KEY ([Abonnements_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AbonnementsFollowers_User1'
CREATE INDEX [IX_FK_AbonnementsFollowers_User1]
ON [dbo].[AbonnementsFollowers]
    ([Abonnements_Id]);
GO

-- Creating foreign key on [Parameter_Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_UserParameters]
    FOREIGN KEY ([Parameter_Id])
    REFERENCES [dbo].[Parameters]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserParameters'
CREATE INDEX [IX_FK_UserParameters]
ON [dbo].[Users]
    ([Parameter_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------