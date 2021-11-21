CREATE PROCEDURE [dbo].[pr_module_update]
	@account_id int,
	@module_id int,
	@course_id int,
	@name varchar(max),
	@description varchar(max),
	@detailed_description varchar(max),
	@picture_id int = null,
	@currency_id int,
	@price decimal(18,2),
	@url varchar(max),
	@tags varchar(max),
	@links varchar(max),
	@exercise_max_number int,
	@order_in_course int
AS
	update dbo.module
	set course_id = @course_id,
		name = @name,
		[description] = @description,
		[detailed_description] = @detailed_description,
		picture_id = case when @picture_id = -1 then null
							when @picture_id is not null then @picture_id
						else picture_id end,
		currency_id = @currency_id,
		price = @price,
		url = @url,
		tags = @tags,
		links = @links,
		exercise_max_number = @exercise_max_number,
		order_in_course = @order_in_course
	where 
		parent_id = @module_id
		and is_approved = 0

	if (@@ROWCOUNT = 0)
	begin
		declare @parent_id int;
		select @parent_id = case
								when @module_id > 0 then @module_id
								when @module_id = 0 then null
								else -1
							end;
		declare @parent_picture_id int;
		set @parent_picture_id = null;
		declare @parent_number uniqueidentifier;
		set @parent_number = NEWID();

		select 
			@parent_picture_id = picture_id
			,@parent_number = number
		from dbo.module
		where id = @module_id

		insert into dbo.module
			(course_id, 
			currency_id,
			[description],
			detailed_description,
			links,
			name,
			number,
			order_in_course,
			picture_id,
			price,
			tags,
			url,
			exercise_max_number,
			is_approved,
			parent_id)
		values (@course_id,
			@currency_id,
			@description,
			@detailed_description,
			@links,
			@name,
			NEWID(),
			@order_in_course,
			case when @picture_id > 0 then @picture_id
						else null end,
			@price,
			@tags,
			@url,
			@exercise_max_number,
			0,
			@parent_id)

		if ( @parent_id is null)
		begin
			select @module_id = SCOPE_IDENTITY();
		end
	end
	
	select @module_id;
