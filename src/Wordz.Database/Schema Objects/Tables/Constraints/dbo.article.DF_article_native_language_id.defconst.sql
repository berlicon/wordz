ALTER TABLE [dbo].[article]
    ADD CONSTRAINT [DF_article_native_language_id] DEFAULT (1) FOR [native_language_id];

