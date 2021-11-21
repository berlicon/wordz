ALTER TABLE [dbo].[tv]
	ADD CONSTRAINT [FK_tv_account_id] 
	FOREIGN KEY (account_id)
	REFERENCES account (id)	

