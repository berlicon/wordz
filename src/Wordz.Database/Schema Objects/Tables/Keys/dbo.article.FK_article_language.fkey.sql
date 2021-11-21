ALTER TABLE [dbo].[article]
    ADD CONSTRAINT [FK_article_language] FOREIGN KEY ([learn_language_id]) REFERENCES [dbo].[language] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

