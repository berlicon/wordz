ALTER TABLE [dbo].[fm_language]
    ADD CONSTRAINT [FK_fm_language_language] FOREIGN KEY ([language_id]) REFERENCES [dbo].[language] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

