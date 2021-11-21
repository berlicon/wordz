CREATE FUNCTION [dbo].[fn_is_show_unapproved_course]
(
	@account_id int, 
	@course_id int
)
RETURNS bit
AS
BEGIN
	declare @retVal bit;
	
	select @retVal = ISNULL(is_admin, 0)
	from dbo.account
	where id = @account_id

	if (@retVal is null)
	begin
		select @retVal = 0
	end
	
	if (@retVal = 0)
	begin
		select @retVal = convert(bit, (select COUNT(*)
										from dbo.course
										where id = @course_id
											and owner_id = @account_id))
	end

	return @retVal;
END