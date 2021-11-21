ALTER TABLE [dbo].[fm_language]
    ADD CONSTRAINT [DF_fm_language_description] DEFAULT ('') FOR [description];

