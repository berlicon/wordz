ALTER TABLE [dbo].[word_language]
    ADD CONSTRAINT [FK_word_language_language] FOREIGN KEY ([language_id]) REFERENCES [dbo].[language] ([id]) ON DELETE CASCADE ON UPDATE CASCADE;

