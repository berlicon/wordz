ALTER TABLE [dbo].[verb]
    ADD CONSTRAINT [FK_verb_verb_type] FOREIGN KEY ([verb_type_id]) REFERENCES [dbo].[verb_type] ([id]) ON DELETE CASCADE ON UPDATE CASCADE;

