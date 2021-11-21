ALTER TABLE [dbo].[word_order_quiz]
    ADD CONSTRAINT [FK_word_order_quiz_language] FOREIGN KEY ([language_id]) REFERENCES [dbo].[language] ([id]) ON DELETE CASCADE ON UPDATE CASCADE;

