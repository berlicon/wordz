ALTER TABLE [dbo].[domain_word]
    ADD CONSTRAINT [FK_domain_word_domain] FOREIGN KEY ([domain_id]) REFERENCES [dbo].[domain] ([id]) ON DELETE CASCADE ON UPDATE CASCADE;

