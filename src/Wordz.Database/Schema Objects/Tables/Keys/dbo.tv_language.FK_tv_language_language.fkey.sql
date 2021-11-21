ALTER TABLE [dbo].[tv_language]
    ADD CONSTRAINT [FK_tv_language_language] FOREIGN KEY ([language_id]) REFERENCES [dbo].[language] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

