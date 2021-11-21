ALTER TABLE [dbo].[domain_word_language]
    ADD CONSTRAINT [FK_domain_word_language_language] FOREIGN KEY ([language_id]) REFERENCES [dbo].[language] ([id]) ON DELETE CASCADE ON UPDATE CASCADE;

