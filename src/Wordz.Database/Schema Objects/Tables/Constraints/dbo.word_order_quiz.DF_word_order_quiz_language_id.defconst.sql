ALTER TABLE [dbo].[word_order_quiz]
    ADD CONSTRAINT [DF_word_order_quiz_language_id] DEFAULT (2) FOR [language_id];

