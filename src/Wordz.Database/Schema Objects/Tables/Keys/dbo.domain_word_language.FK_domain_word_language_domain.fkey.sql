ALTER TABLE [dbo].[domain_word_language]
    ADD CONSTRAINT [FK_domain_word_language_domain] FOREIGN KEY ([domain_id]) REFERENCES [dbo].[domain] ([id]) ON DELETE CASCADE ON UPDATE CASCADE;

