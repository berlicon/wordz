ALTER TABLE [dbo].[word_order]
    ADD CONSTRAINT [FK_word_order_language] FOREIGN KEY ([language_id]) REFERENCES [dbo].[language] ([id]) ON DELETE CASCADE ON UPDATE CASCADE;

