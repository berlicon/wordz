/****** Object:  Table [dbo].[language]    Script Date: 23.06.2009 15:01:55 ******/
CREATE TABLE [dbo].[language] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[name] [nvarchar] (20) COLLATE Cyrillic_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[article_category_language]    Script Date: 23.06.2009 15:01:55 ******/
CREATE TABLE [dbo].[article_category_language] (
	[language_id] [int] NOT NULL ,
	[category_id] [int] NOT NULL ,
	[name] [nvarchar] (50) COLLATE Cyrillic_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[domain_word_language]    Script Date: 23.06.2009 15:01:55 ******/
CREATE TABLE [dbo].[domain_word_language] (
	[language_id] [int] NOT NULL ,
	[domain_id] [int] NOT NULL ,
	[name] [nvarchar] (20) COLLATE Cyrillic_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[film_category_language]    Script Date: 23.06.2009 15:01:56 ******/
CREATE TABLE [dbo].[film_category_language] (
	[language_id] [int] NOT NULL ,
	[film_category_id] [int] NOT NULL ,
	[name] [nvarchar] (30) COLLATE Cyrillic_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[fm]    Script Date: 23.06.2009 15:01:56 ******/
CREATE TABLE [dbo].[fm] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[image_url] [nvarchar] (50) COLLATE Cyrillic_General_CI_AS NULL ,
	[url] [nvarchar] (500) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[use_media_player] [bit] NOT NULL ,
	[language_id] [int] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[tv]    Script Date: 23.06.2009 15:01:56 ******/
CREATE TABLE [dbo].[tv] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[image_url] [nvarchar] (50) COLLATE Cyrillic_General_CI_AS NULL ,
	[url] [nvarchar] (500) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[language_id] [int] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[word_language]    Script Date: 23.06.2009 15:01:56 ******/
CREATE TABLE [dbo].[word_language] (
	[language_id] [int] NOT NULL ,
	[word_id] [int] NOT NULL ,
	[text] [nvarchar] (254) COLLATE Cyrillic_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[fm_language]    Script Date: 23.06.2009 15:01:57 ******/
CREATE TABLE [dbo].[fm_language] (
	[fm_id] [int] NOT NULL ,
	[language_id] [int] NOT NULL ,
	[name] [nvarchar] (20) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[description] [nvarchar] (500) COLLATE Cyrillic_General_CI_AS NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[tv_language]    Script Date: 23.06.2009 15:01:57 ******/
CREATE TABLE [dbo].[tv_language] (
	[tv_id] [int] NOT NULL ,
	[language_id] [int] NOT NULL ,
	[name] [nvarchar] (20) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[description] [nvarchar] (500) COLLATE Cyrillic_General_CI_AS NULL 
) ON [PRIMARY]
GO