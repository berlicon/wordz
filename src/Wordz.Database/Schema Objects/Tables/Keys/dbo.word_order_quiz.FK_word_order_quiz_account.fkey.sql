ALTER TABLE [dbo].[word_order_quiz]
    ADD CONSTRAINT [FK_word_order_quiz_account] FOREIGN KEY ([account_id]) REFERENCES [dbo].[account] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

