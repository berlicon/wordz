-- Author: Trenogina E.
-- Create time: 12.06.2013
-- Default database
-- ======================================================================

begin try
begin transaction Default_database_script

declare @courseA_id int;
declare @courseB_id int;
declare @currencyR_id int;
declare @currencyU_id int;

-- currencies
if exists (select * from sysobjects where name='currency' and xtype='U')
begin
	insert into currency ([name], [letter_code], [digit_code])
		values ('Российский рубль', 'RUR', 810);
	set @currencyR_id = @@identity;
	insert into currency ([name], [letter_code], [digit_code])
		values ('Доллар США', 'USD', 840);
	set @currencyU_id = @@identity;
end

-- currency_rate
if exists (select * from sysobjects where name='currency_rate' and xtype='U')
begin
	insert into currency_rate ([source_currency_id], [target_currency_id], [multiplier], [change_date])
		values (@currencyR_id, @currencyU_id, 31.2, getdate());
end

-- 2 courses
if exists (select * from sysobjects where name='course' and xtype='U')
begin
	insert into course ([number], [name], [description], 
						[detailed_description], [picture_id],
						[price], [currency_id], [ui_langauge_id],
						[location_id], [category_id], [url],
						[authors], [contacts], [tags], [links],
						[is_editable], [is_copied], [is_public],
						[password], [google_advertisement], [is_approved])
		values (NEWID(), 'Курс английского языка', 'Beginary', '', 
				null, 200.00, @currencyR_id, 2,	null, 1, '', 'Cambrige', 
				'', '', '', 1, 1, 1, '', '', 1);
		set @courseA_id = @@identity;
	insert into course ([number], [name], [description], 
						[detailed_description], [picture_id],
						[price], [currency_id], [ui_langauge_id],
						[location_id], [category_id], [url],
						[authors], [contacts], [tags], [links],
						[is_editable], [is_copied], [is_public],
						[password], [google_advertisement], [is_approved])
		values (NEWID(), 'Курс немецкого языка', 'Beginary', '', 
				null, 300.00, @currencyU_id, 4, null, 1, '',	'Govard', 
				'', '', '',	1, 1, 1, '', '', 1);
		set @courseB_id = @@identity;
end

-- 2 modules for first cource, courseA_id
if exists (select * from sysobjects where name='module' and xtype='U')
begin
	insert into module ([number],[course_id],[name],[description],
						[detailed_description],[picture_id],[price],
						[currency_id],[url],[tags],[links],[order_in_course])
		values (NEWID(),@courseA_id,'Первый модуль курса',
						'Первый модуль курса по английскому языку',
						'',null,100.00,@currencyR_id,'','','',1);
	insert into module ([number],[course_id],[name],[description],
						[detailed_description],[picture_id],[price],
						[currency_id],[url],[tags],[links],[order_in_course])
		values (NEWID(),@courseA_id,'Второй модуль курса',
						'Второй модуль курса по английскому языку',
						'',null,100.00,@currencyR_id,'','','',2);
end

select @currencyR_id as 'Id рубля', @currencyU_id as 'Id доллара';

commit transaction Default_database_script
end try
begin catch
rollback transaction Default_database_script
end catch
