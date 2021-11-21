﻿ALTER TABLE [dbo].[account_word]
    ADD CONSTRAINT [PK_account_word] PRIMARY KEY CLUSTERED ([account_id] ASC, [word_id] ASC, [language_id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
