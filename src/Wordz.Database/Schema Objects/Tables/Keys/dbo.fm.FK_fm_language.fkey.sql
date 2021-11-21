ALTER TABLE [dbo].[fm]
    ADD CONSTRAINT [FK_fm_language] FOREIGN KEY ([language_id]) REFERENCES [dbo].[language] ([id]) ON DELETE CASCADE ON UPDATE CASCADE;

