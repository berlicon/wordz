ALTER TABLE [dbo].[fm_language]
    ADD CONSTRAINT [PK_fm_language] PRIMARY KEY CLUSTERED ([language_id] ASC, [fm_id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

