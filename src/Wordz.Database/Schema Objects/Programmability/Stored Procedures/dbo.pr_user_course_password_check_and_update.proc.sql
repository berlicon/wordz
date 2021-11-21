CREATE PROCEDURE [dbo].[pr_user_course_password_check_and_update]
	@user_id int, 
	@course_id int,
	@password nvarchar(100)
AS
	declare @current_course_password nvarchar(100);
	select @current_course_password = c.[password]
	from dbo.course c
	where c.id = @course_id

	declare @stored_password nvarchar(100);
	select @stored_password = u.[password]
	from dbo.user_password_for_course u
	where u.course_id = @course_id
		and u.[user_id] = @user_id

	if ((@current_course_password is null or @current_course_password = '') 
		and @current_course_password <> @stored_password)
	begin
		update dbo.user_password_for_course
		set
			[password] = @current_course_password
		where 
			[user_id] = @user_id
			and [course_id] = @course_id
		return 0;
	end

	if (@current_course_password <> @password)
	begin
		return -1;
	end
	else
	begin
		update dbo.user_password_for_course
		set
			[password] = @current_course_password
		where 
			[user_id] = @user_id
			and [course_id] = @course_id
		
		if (@@ROWCOUNT = 0)
		begin
			insert into dbo.user_password_for_course ([user_id], [course_id], [password])
			values (@user_id, @course_id, @current_course_password)
		end

		return 0;
	end
	