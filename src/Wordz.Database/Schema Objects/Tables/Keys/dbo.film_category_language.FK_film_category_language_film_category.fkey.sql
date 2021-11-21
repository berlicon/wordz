ALTER TABLE [dbo].[film_category_language]
    ADD CONSTRAINT [FK_film_category_language_film_category] FOREIGN KEY ([film_category_id]) REFERENCES [dbo].[film_category] ([id]) ON DELETE CASCADE ON UPDATE CASCADE;

