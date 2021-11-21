ALTER TABLE [dbo].[level]
    ADD CONSTRAINT [DF_level_language_id] DEFAULT (2) FOR [language_id];

