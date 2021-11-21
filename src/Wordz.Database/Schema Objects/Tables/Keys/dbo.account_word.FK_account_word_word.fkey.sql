ALTER TABLE [dbo].[account_word]
    ADD CONSTRAINT [FK_account_word_word] FOREIGN KEY ([word_id]) REFERENCES [dbo].[word] ([id]) ON DELETE CASCADE ON UPDATE CASCADE;

