-- аппрув изменений для курса
CREATE PROCEDURE [dbo].[pr_course_approve]
	@account_id int,
	@course_id int	
AS
	if (dbo.fn_is_course_allowed_approve(@account_id, @course_id) = 1)
	begin
		begin transaction;
		begin try

			update dbo.course
			set
				dbo.course.authors = child.authors,
				dbo.course.category_id = child.category_id,
				dbo.course.contacts = child.contacts,
				dbo.course.currency_id = child.currency_id,
				dbo.course.[description] = child.[description],
				dbo.course.detailed_description = child.[detailed_description],
				dbo.course.google_advertisement = child.google_advertisement,
				dbo.course.is_editable = child.is_editable,
				dbo.course.is_copied = child.is_copied,
				dbo.course.is_public = child.is_public,
				dbo.course.links = child.links,
				dbo.course.location_id = child.location_id,
				dbo.course.name = child.name,
				dbo.course.[password] = child.[password],
				dbo.course.picture_id = child.picture_id,
				dbo.course.price = child.price,
				dbo.course.tags = child.tags,
				dbo.course.ui_langauge_id = child.ui_langauge_id,
				dbo.course.url = child.url,
				dbo.course.is_deleted = child.is_deleted
			from dbo.course
			inner join dbo.course child
			on child.parent_id = dbo.course.id
			where 
				dbo.course.id = @course_id
				and dbo.course.parent_id is null
				and child.id is not null

			-- удаляем дочерние записи
			delete from [dbo].[course] 
			where parent_id = @course_id

			update dbo.course
			set
				is_approved = 1
			where 
				id = @course_id

			-- утверждаем модули
			declare module_cursor cursor
			for
			select m.id
			from [dbo].[module] m
			where m.course_id = @course_id
				and m.parent_id is null

			declare @module_approve_result int;
			declare @module_id int;

			open module_cursor

			fetch next from module_cursor
			into @module_id

			while (@@FETCH_STATUS = 0)
			begin
				
				exec dbo.pr_module_approve 
					@account_id = @account_id, 
					@module_id = @module_id,
					@result_id = @module_approve_result output

				if (@module_approve_result < 0)
				begin
					raiserror('Cannot approve module!', 16, 1);
				end

				fetch next from module_cursor
				into @module_id
			end

			commit
			return 0;
		end try
		begin catch
			rollback
			select ERROR_MESSAGE();
			return -2;
		end catch		
	end

	return -1;

	close module_cursor
	deallocate module_cursor