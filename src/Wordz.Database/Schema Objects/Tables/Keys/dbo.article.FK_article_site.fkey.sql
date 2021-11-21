ALTER TABLE [dbo].[article]
    ADD CONSTRAINT [FK_article_site] FOREIGN KEY ([site_id]) REFERENCES [dbo].[site] ([id]) ON DELETE CASCADE ON UPDATE CASCADE;

