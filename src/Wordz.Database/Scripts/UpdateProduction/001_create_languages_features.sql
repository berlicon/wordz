--create [language] table and data
/*move table and data to server*/

--create [word_language] table and data
/*move table and data to server*/

--[word] table changes
ALTER TABLE dbo.word DROP COLUMN original
ALTER TABLE dbo.word DROP COLUMN [translation]
ALTER TABLE dbo.word DROP COLUMN original_sound
ALTER TABLE dbo.word DROP COLUMN translation_sound
GO

--[account_word] table changes
ALTER TABLE [account_word]
ADD language_id int NOT NULL
CONSTRAINT [DF_account_word_language_id] DEFAULT (2)
GO

ALTER TABLE [account_word]
DROP CONSTRAINT PK_account_word
GO

ALTER TABLE [account_word]
ADD CONSTRAINT [PK_account_word] PRIMARY KEY  CLUSTERED 
	(
		[account_id],
		[word_id],
		[language_id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [account_word]
ADD	CONSTRAINT [FK_account_word_language] FOREIGN KEY 
	(
		[language_id]
	) REFERENCES [language] (
		[id]
	) ON DELETE CASCADE  ON UPDATE CASCADE
GO

--[article] table changes
ALTER TABLE [article]
ADD [native_language_id] [int] NOT NULL CONSTRAINT [DF_article_native_language_id] DEFAULT (1)
GO

ALTER TABLE [article]
ADD [learn_language_id] [int] NOT NULL CONSTRAINT [DF_article_learn_language_id] DEFAULT (2)
GO

ALTER TABLE [article]
ADD CONSTRAINT [FK_article_language] FOREIGN KEY 
	(
		[learn_language_id]
	) REFERENCES [language] (
		[id]
	)
GO

ALTER TABLE [article]
ADD CONSTRAINT [FK_article_language1] FOREIGN KEY 
	(
		[native_language_id]
	) REFERENCES [language] (
		[id]
	)
GO

--create [article_category_language] table and data
/*move table and data to server*/

--[category] table changes
ALTER TABLE category DROP COLUMN [name]
GO

--create [domain_word_language] table and data
/*move table and data to server*/

--[domain] table changes
ALTER TABLE domain DROP COLUMN [name]
GO

--[film] table changes
ALTER TABLE [film]
ADD [language_id] [int] NOT NULL CONSTRAINT [DF_film_language_id] DEFAULT (2)
GO

ALTER TABLE [film]
ADD CONSTRAINT [FK_film_language] FOREIGN KEY 
	(
		[language_id]
	) REFERENCES [language] (
		[id]
	) ON DELETE CASCADE  ON UPDATE CASCADE 
GO

--create [film_category_language] table and data
/*move table and data to server*/

--[film_category] table changes
ALTER TABLE film_category DROP COLUMN [name]
GO

--[word_order] table changes
ALTER TABLE [word_order]
ADD [language_id] [int] NOT NULL CONSTRAINT [DF_word_order_language_id] DEFAULT (2)
GO

ALTER TABLE [word_order]
ADD CONSTRAINT [FK_word_order_language] FOREIGN KEY 
	(
		[language_id]
	) REFERENCES [language] (
		[id]
	) ON DELETE CASCADE  ON UPDATE CASCADE 
GO

--[word_order_quiz] table changes
ALTER TABLE [word_order_quiz]
ADD [language_id] [int] NOT NULL CONSTRAINT [DF_word_order_quiz_language_id] DEFAULT (2)
GO

ALTER TABLE [word_order_quiz]
ADD CONSTRAINT [FK_word_order_quiz_language] FOREIGN KEY 
	(
		[language_id]
	) REFERENCES [language] (
		[id]
	) ON DELETE CASCADE  ON UPDATE CASCADE 
GO

--[word_quiz] table changes
ALTER TABLE [word_quiz]
ADD [language_id] [int] NOT NULL CONSTRAINT [DF_word_quiz_language_id] DEFAULT (2)
GO

ALTER TABLE [word_quiz]
ADD CONSTRAINT [FK_word_quiz_language] FOREIGN KEY 
	(
		[language_id]
	) REFERENCES [language] (
		[id]
	) ON DELETE CASCADE  ON UPDATE CASCADE 
GO

--create [tv] table and data
/*move table and data to server*/

--create [tv_language] table and data
/*move table and data to server*/

--create [fm] table and data
/*move table and data to server*/

--create [fm_language] table and data
/*move table and data to server*/
