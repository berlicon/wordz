ALTER TABLE [dbo].[fm_language]
    ADD CONSTRAINT [FK_fm_language_fm] FOREIGN KEY ([fm_id]) REFERENCES [dbo].[fm] ([id]) ON DELETE CASCADE ON UPDATE CASCADE;

