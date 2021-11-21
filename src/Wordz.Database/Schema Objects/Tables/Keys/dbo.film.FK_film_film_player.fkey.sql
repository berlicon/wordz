ALTER TABLE [dbo].[film]
    ADD CONSTRAINT [FK_film_film_player] FOREIGN KEY ([film_player_id]) REFERENCES [dbo].[film_player] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

