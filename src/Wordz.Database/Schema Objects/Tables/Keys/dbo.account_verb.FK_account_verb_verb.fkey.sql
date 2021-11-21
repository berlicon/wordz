ALTER TABLE [dbo].[account_verb]
    ADD CONSTRAINT [FK_account_verb_verb] FOREIGN KEY ([verb_id]) REFERENCES [dbo].[verb] ([id]) ON DELETE CASCADE ON UPDATE CASCADE;

