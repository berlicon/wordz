ALTER TABLE [dbo].[fm_language]
    ADD CONSTRAINT [DF_fm_language_language_id] DEFAULT (1) FOR [language_id];

