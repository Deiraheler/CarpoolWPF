
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/02/2024 11:52:01
-- Generated from EDMX file: C:\Users\sever\source\repos\Carpool App\Carpool App\CarpoolDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [CarpoolDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UsersPosts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Posts] DROP CONSTRAINT [FK_UsersPosts];
GO
IF OBJECT_ID(N'[dbo].[FK_UsersRating]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ratings] DROP CONSTRAINT [FK_UsersRating];
GO
IF OBJECT_ID(N'[dbo].[FK_UsersHistory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Histories] DROP CONSTRAINT [FK_UsersHistory];
GO
IF OBJECT_ID(N'[dbo].[FK_HistoryPosts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Histories] DROP CONSTRAINT [FK_HistoryPosts];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Posts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Posts];
GO
IF OBJECT_ID(N'[dbo].[Ratings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ratings];
GO
IF OBJECT_ID(N'[dbo].[Histories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Histories];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Posts'
CREATE TABLE [dbo].[Posts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UsersId] int  NOT NULL,
    [From] nvarchar(max)  NOT NULL,
    [To] nvarchar(max)  NOT NULL,
    [Cost] nvarchar(max)  NOT NULL,
    [Departure] nvarchar(max)  NOT NULL,
    [Seats] nvarchar(max)  NOT NULL,
    [Type] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Ratings'
CREATE TABLE [dbo].[Ratings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] nvarchar(max)  NOT NULL,
    [RatingNum] nvarchar(max)  NOT NULL,
    [PostsId] int  NOT NULL,
    [UsersId] int  NOT NULL
);
GO

-- Creating table 'Histories'
CREATE TABLE [dbo].[Histories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UsersId] int  NOT NULL,
    [PostsId] int  NOT NULL,
    [As] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [PK_Posts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Ratings'
ALTER TABLE [dbo].[Ratings]
ADD CONSTRAINT [PK_Ratings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Histories'
ALTER TABLE [dbo].[Histories]
ADD CONSTRAINT [PK_Histories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UsersId] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [FK_UsersPosts]
    FOREIGN KEY ([UsersId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersPosts'
CREATE INDEX [IX_FK_UsersPosts]
ON [dbo].[Posts]
    ([UsersId]);
GO

-- Creating foreign key on [UsersId] in table 'Histories'
ALTER TABLE [dbo].[Histories]
ADD CONSTRAINT [FK_UsersHistory]
    FOREIGN KEY ([UsersId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersHistory'
CREATE INDEX [IX_FK_UsersHistory]
ON [dbo].[Histories]
    ([UsersId]);
GO

-- Creating foreign key on [PostsId] in table 'Histories'
ALTER TABLE [dbo].[Histories]
ADD CONSTRAINT [FK_HistoryPosts]
    FOREIGN KEY ([PostsId])
    REFERENCES [dbo].[Posts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HistoryPosts'
CREATE INDEX [IX_FK_HistoryPosts]
ON [dbo].[Histories]
    ([PostsId]);
GO

-- Creating foreign key on [PostsId] in table 'Ratings'
ALTER TABLE [dbo].[Ratings]
ADD CONSTRAINT [FK_RatingPosts]
    FOREIGN KEY ([PostsId])
    REFERENCES [dbo].[Posts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RatingPosts'
CREATE INDEX [IX_FK_RatingPosts]
ON [dbo].[Ratings]
    ([PostsId]);
GO

-- Creating foreign key on [UsersId] in table 'Ratings'
ALTER TABLE [dbo].[Ratings]
ADD CONSTRAINT [FK_UsersRating]
    FOREIGN KEY ([UsersId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersRating'
CREATE INDEX [IX_FK_UsersRating]
ON [dbo].[Ratings]
    ([UsersId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------