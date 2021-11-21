-- Description: 
-- Change Date: 01.06.2013

CREATE FUNCTION [dbo].[fn_get_currency_rate]
(
	@source_currency int,
	@target_currency int,
	@onDate datetime
)
RETURNS decimal(18, 8)
AS
BEGIN
	-- Получаем множитель для преобразования валют
	declare @currencyRateMultiplier decimal(18, 8)
	if (@source_currency <> @target_currency)
	begin
		select top 1 @currencyRateMultiplier = cr.multiplier
		from dbo.currency_rate cr
		where cr.source_currency_id = @source_currency
			and cr.target_currency_id = @target_currency
			and cr.change_date <= @onDate
		order by cr.change_date desc
					
		-- Если не нашли напрямую, то ищем в обратную сторону
		if (@currencyRateMultiplier is null)
		begin
			select top 1 @currencyRateMultiplier = 1.0 / cr.multiplier
			from dbo.currency_rate cr
			where cr.source_currency_id = @target_currency
				and cr.target_currency_id = @source_currency
				and cr.change_date <= @onDate
			order by cr.change_date desc
		end
	end
	else
	begin
		select @currencyRateMultiplier = 1.0;
	end
	
	RETURN @currencyRateMultiplier;
END