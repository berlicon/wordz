ALTER TABLE [dbo].[article]
    ADD CONSTRAINT [FK_article_language1] FOREIGN KEY ([native_language_id]) REFERENCES [dbo].[language] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

