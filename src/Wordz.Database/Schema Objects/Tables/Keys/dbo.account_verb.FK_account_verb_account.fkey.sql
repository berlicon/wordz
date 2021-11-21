ALTER TABLE [dbo].[account_verb]
    ADD CONSTRAINT [FK_account_verb_account] FOREIGN KEY ([account_id]) REFERENCES [dbo].[account] ([id]) ON DELETE CASCADE ON UPDATE CASCADE;

