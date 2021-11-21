ALTER TABLE [dbo].[film_category_language]
    ADD CONSTRAINT [PK_film_category_language] PRIMARY KEY CLUSTERED ([language_id] ASC, [film_category_id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

