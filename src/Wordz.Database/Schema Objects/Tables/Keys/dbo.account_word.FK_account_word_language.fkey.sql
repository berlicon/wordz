ALTER TABLE [dbo].[account_word]
    ADD CONSTRAINT [FK_account_word_language] FOREIGN KEY ([language_id]) REFERENCES [dbo].[language] ([id]) ON DELETE CASCADE ON UPDATE CASCADE;

