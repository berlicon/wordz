ALTER TABLE [dbo].[article]
    ADD CONSTRAINT [FK_article_category] FOREIGN KEY ([category_id]) REFERENCES [dbo].[category] ([id]) ON DELETE CASCADE ON UPDATE CASCADE;

