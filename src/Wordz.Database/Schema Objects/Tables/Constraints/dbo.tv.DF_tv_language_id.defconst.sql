ALTER TABLE [dbo].[tv]
    ADD CONSTRAINT [DF_tv_language_id] DEFAULT (1) FOR [language_id];

