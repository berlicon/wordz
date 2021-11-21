ALTER TABLE [dbo].[rating]
	ADD CONSTRAINT [FK_rating_account] 
	FOREIGN KEY (account_id)
	REFERENCES dbo.account (id)	

