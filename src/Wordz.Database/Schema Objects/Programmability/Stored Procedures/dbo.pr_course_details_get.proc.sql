-- Change Date: 2013.05.20
-- Description: хранимая процедура, которая возвращает
--  курс по его id

create procedure [dbo].[pr_course_details_get]
(
	@account_id int,
	@course_id int
)
as

declare @is_show_unapproved bit;
select @is_show_unapproved = [dbo].[fn_is_show_unapproved_course](@account_id, @course_id)

select @course_id 'id'
      ,c.[number]
      ,c.[name]
      ,c.[description]
      ,c.[detailed_description]
      ,c.[picture_id]
	  ,c.[currency_id]
      ,c.[price]
      ,c.[ui_langauge_id]
      ,c.[location_id]
      ,c.[category_id]
      ,c.[url]
      ,c.[authors]
      ,c.[contacts]
      ,c.[tags]
      ,c.[links]
      ,c.[is_editable]
      ,c.[is_copied]
      ,c.[is_public]
      ,c.[password]
      ,c.[google_advertisement]
      ,c.[is_approved]
	  ,c.owner_id
      ,convert(bit, (
					select case when isnull(SUM(m.price), 0) = 0 then 1 else 0 end
					from dbo.module m
					left join dbo.payment pt
					on pt.module_id = m.id
						and pt.account_id = @account_id
					where pt.id is null
						and m.course_id = @course_id
						and m.parent_id is null
						
					)
				) 'is_buyed_by_current_user'
      ,convert(float, (
					select top 1 (rs.value)
					from dbo.rating rs 
					where account_id is null
						and c.number = rs.target_element)
						) 'rate_by_current_user'
	  ,lang.name 'ui_langauge_name'
	  ,cat.name 'category_name'
	  ,cur.name 'currency_name'
	  ,dbo.fn_is_all_childs_approved(@course_id) 'is_all_childs_approved'
	  ,(select top 1 u.[password]
		from user_password_for_course u
		where u.[user_id] = @account_id
			and u.course_id = @course_id) 'stored_password_by_user'
from [dbo].[course] c

left join [dbo].[language] lang
	on lang.id = c.[ui_langauge_id]

left join [dbo].[article_category_language] cat
	on cat.category_id = c.category_id
	and cat.language_id = c.ui_langauge_id
left join [dbo].[currency] cur
	on cur.id = c.currency_id

where 
	(
		c.[id] = @course_id
		or c.[parent_id] = @course_id
	)
	and 
	(
		@is_show_unapproved = 0
		and is_approved = 1
		and parent_id is null
		or
		(
			@is_show_unapproved = 1
			and not exists (
					select innerCourse.id 
					from dbo.course innerCourse
					where innerCourse.parent_id = c.id
				)
		)
	)