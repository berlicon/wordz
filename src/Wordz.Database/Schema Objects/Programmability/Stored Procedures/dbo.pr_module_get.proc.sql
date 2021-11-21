-- Description: хранимая процедура, которая 
-- выводит модуль по id

create procedure [dbo].[pr_module_get]
(
	@account_id int
	,@module_id int
)
as

declare @is_show_unapproved bit;
select @is_show_unapproved = dbo.fn_is_show_unapproved_module(@account_id, @module_id);

select @module_id 'id'
      ,m.[number]
      ,m.[course_id]
      ,m.[name]
      ,m.[description]
      ,m.[detailed_description]
      ,m.[picture_id]
	  ,m.[currency_id]
      ,m.[price]
      ,m.[url]
      ,m.[tags]
      ,m.[links]
      ,m.[order_in_course]
	  ,m.[exercise_max_number]
	  ,convert(bit, ( select top 1 
			case when pt.id is not null or m.price = 0 then 1 
				else 0 
			end 
		 from dbo.payment pt 
		 where pt.module_id = ISNULL(m.parent_id, m.id) and pt.account_id = @account_id)) as 'is_payd'
	  ,cur.name 'currency_name'
	  ,convert(float, (
					select top 1 (rs.value)
					from dbo.rating rs 
					where account_id is null
						and m.number = rs.target_element)
						) 'rate_by_current_user'
	 ,cr.owner_id 'owner_id'
from [dbo].[module] m
left join dbo.currency cur
on cur.id = m.currency_id
left join dbo.course cr
on cr.id = m.course_id
where 
	(
		m.[id] = @module_id
		or m.[parent_id] = @module_id
	)
	and 
	(
		@is_show_unapproved = 0
		and m.is_approved = 1
		and m.parent_id is null
		or
		(
			@is_show_unapproved = 1
			and not exists (
					select innerModule.id 
					from dbo.module innerModule
					where m.id = innerModule.parent_id
				)
		)
	)
	and isnull(m.is_deleted, 0) = 0