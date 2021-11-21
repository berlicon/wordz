ALTER TABLE [dbo].[film_part]
    ADD CONSTRAINT [FK_film_part_film] FOREIGN KEY ([film_id]) REFERENCES [dbo].[film] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

