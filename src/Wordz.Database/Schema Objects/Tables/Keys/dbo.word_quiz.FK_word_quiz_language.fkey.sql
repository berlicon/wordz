ALTER TABLE [dbo].[word_quiz]
    ADD CONSTRAINT [FK_word_quiz_language] FOREIGN KEY ([language_id]) REFERENCES [dbo].[language] ([id]) ON DELETE CASCADE ON UPDATE CASCADE;

