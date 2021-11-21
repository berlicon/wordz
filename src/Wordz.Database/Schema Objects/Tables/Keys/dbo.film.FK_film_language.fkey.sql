ALTER TABLE [dbo].[film]
    ADD CONSTRAINT [FK_film_language] FOREIGN KEY ([language_id]) REFERENCES [dbo].[language] ([id]) ON DELETE CASCADE ON UPDATE CASCADE;

