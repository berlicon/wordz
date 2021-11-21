CREATE PROCEDURE [dbo].[pr_film_update_or_insert]
	@account_id int,
    @language_id int,
	@native_language_id int,
	@id int,
	@player_code nvarchar(max),
	@image_url nvarchar(max), 
	@name nvarchar(max), 
	@description nvarchar(max),
	@is_editable bit,
	@category_id int
AS
	declare @isEditableField bit;
	declare @accountIdField int;
	declare @existingFilmId int;
	select @existingFilmId = id, @isEditableField = is_editable, @accountIdField = account_id
	from dbo.film
	where id = @id

	if @existingFilmId is not null
		and 
		(
			@accountIdField <> @account_id
			or ISNULL(@isEditableField, 0) = 0
		)
	begin
		-- Нельзя редактировать данную запись, т.к.
		-- она не помечена на редактирование 
		-- или не пренадлежит текущему пользователю
		select -1;
		return;
	end
	
	update dbo.film
	set
		--account_id = @account_id
		--,is_editable = @is_editable
		name = @name
		,image_url = @image_url
		,player_code = @player_code
		,language_id = @language_id
		,film_category_id = @category_id
		,[description] = @description
	where
		id = @id

	if (@@ROWCOUNT = 0)
	begin
		insert into dbo.film
			(account_id, image_url, is_editable, language_id, player_code, film_category_id, name, [description], [status])
		values
			(@account_id, @image_url, 1, @language_id, @player_code, @category_id, @name, @description, 1)
		select @id = SCOPE_IDENTITY()

		insert into dbo.film_order
			(order_in_list, film_id, account_id)
		values
			--( (
			--	select case when MAX(order_in_list) > 0 then MAX(order_in_list) else 0 end
			--	from dbo.fm_order ord
			--	where ord.account_id = @account_id
			--  )
			--, @id)
			(0, @id, @account_id)
	end

	--update dbo.fm_language
	--set
	--	[description] = @description
	--	,name = @name
	--where 
	--	fm_id = @id
	--	and language_id = @native_language_id
	--if (@@ROWCOUNT = 0)
	--begin
	--	insert into dbo.fm_language
	--		(fm_id, language_id, [description], name)
	--	values
	--		(@id, @native_language_id, @description, @name)
	--end

	select @id