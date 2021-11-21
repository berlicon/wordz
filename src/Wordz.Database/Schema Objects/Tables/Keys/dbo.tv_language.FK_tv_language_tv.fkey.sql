ALTER TABLE [dbo].[tv_language]
    ADD CONSTRAINT [FK_tv_language_tv] FOREIGN KEY ([tv_id]) REFERENCES [dbo].[tv] ([id]) ON DELETE CASCADE ON UPDATE CASCADE;

