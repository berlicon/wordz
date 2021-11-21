ALTER TABLE [dbo].[word_language]
    ADD CONSTRAINT [FK_word_language_word] FOREIGN KEY ([word_id]) REFERENCES [dbo].[word] ([id]) ON DELETE CASCADE ON UPDATE CASCADE;

