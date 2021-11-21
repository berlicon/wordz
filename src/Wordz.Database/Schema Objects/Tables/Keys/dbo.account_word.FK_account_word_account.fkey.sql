ALTER TABLE [dbo].[account_word]
    ADD CONSTRAINT [FK_account_word_account] FOREIGN KEY ([account_id]) REFERENCES [dbo].[account] ([id]) ON DELETE CASCADE ON UPDATE CASCADE;

