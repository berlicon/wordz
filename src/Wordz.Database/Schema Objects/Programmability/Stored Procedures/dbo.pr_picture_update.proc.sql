CREATE PROCEDURE [dbo].[pr_picture_update]
	@account_id int,
	@picture_data varbinary(max),
	@result_id int output
AS
	insert into dbo.picture
		(data)
	values (@picture_data)

	select @result_id = SCOPE_IDENTITY()