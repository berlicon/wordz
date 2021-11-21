ALTER TABLE [dbo].[tv]
    ADD CONSTRAINT [FK_tv_language] FOREIGN KEY ([language_id]) REFERENCES [dbo].[language] ([id]) ON DELETE CASCADE ON UPDATE CASCADE;

