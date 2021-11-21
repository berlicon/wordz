ALTER TABLE [dbo].[domain_word]
    ADD CONSTRAINT [FK_domain_word_word] FOREIGN KEY ([word_id]) REFERENCES [dbo].[word] ([id]) ON DELETE CASCADE ON UPDATE CASCADE;

