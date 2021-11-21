ALTER TABLE [dbo].[tv_language]
    ADD CONSTRAINT [DF_tv_language_description] DEFAULT ('') FOR [description];

