ALTER TABLE [dbo].[word_order]
    ADD CONSTRAINT [DF_word_order_language_id] DEFAULT (2) FOR [language_id];

