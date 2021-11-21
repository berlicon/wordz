ALTER TABLE [dbo].[level_quiz]
    ADD CONSTRAINT [DF_level_quiz_language_id] DEFAULT (2) FOR [language_id];

