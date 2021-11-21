CREATE FUNCTION [dbo].[fn_is_course_allowed_approve]
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
	
	return @retVal;
END