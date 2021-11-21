ALTER TABLE [dbo].[tv_language]
    ADD CONSTRAINT [DF_tv_language_language_id] DEFAULT (1) FOR [language_id];

