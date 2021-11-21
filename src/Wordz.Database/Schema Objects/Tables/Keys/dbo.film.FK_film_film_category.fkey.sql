ALTER TABLE [dbo].[film]
    ADD CONSTRAINT [FK_film_film_category] FOREIGN KEY ([film_category_id]) REFERENCES [dbo].[film_category] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

