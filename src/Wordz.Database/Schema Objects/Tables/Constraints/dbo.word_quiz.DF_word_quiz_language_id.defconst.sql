ALTER TABLE [dbo].[word_quiz]
    ADD CONSTRAINT [DF_word_quiz_language_id] DEFAULT (2) FOR [language_id];

