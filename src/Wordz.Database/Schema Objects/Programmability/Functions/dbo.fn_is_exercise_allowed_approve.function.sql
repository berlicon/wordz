CREATE FUNCTION [dbo].[fn_is_exercise_allowed_approve]
(
	@account_id int, 
	@exercise_type int,
	@exercise_id int
)
RETURNS INT
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