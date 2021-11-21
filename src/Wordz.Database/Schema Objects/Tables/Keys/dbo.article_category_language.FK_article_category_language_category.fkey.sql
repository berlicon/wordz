ALTER TABLE [dbo].[article_category_language]
    ADD CONSTRAINT [FK_article_category_language_category] FOREIGN KEY ([category_id]) REFERENCES [dbo].[category] ([id]) ON DELETE CASCADE ON UPDATE CASCADE;

