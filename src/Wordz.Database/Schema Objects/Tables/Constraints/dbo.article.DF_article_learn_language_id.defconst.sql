ALTER TABLE [dbo].[article]
    ADD CONSTRAINT [DF_article_learn_language_id] DEFAULT (2) FOR [learn_language_id];

