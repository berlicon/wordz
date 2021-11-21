ALTER TABLE [dbo].[account_word]
    ADD CONSTRAINT [DF_account_word_language_id] DEFAULT (2) FOR [language_id];

