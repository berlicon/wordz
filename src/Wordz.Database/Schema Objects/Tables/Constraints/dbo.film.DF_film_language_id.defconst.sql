ALTER TABLE [dbo].[film]
    ADD CONSTRAINT [DF_film_language_id] DEFAULT (2) FOR [language_id];

