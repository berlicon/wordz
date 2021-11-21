ALTER TABLE [dbo].[article_category_language]
    ADD CONSTRAINT [FK_article_category_language_language] FOREIGN KEY ([language_id]) REFERENCES [dbo].[language] ([id]) ON DELETE CASCADE ON UPDATE CASCADE;

