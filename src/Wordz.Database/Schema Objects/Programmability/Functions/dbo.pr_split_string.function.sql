CREATE FUNCTION [dbo].[pr_split_string]
(   
 @String varchar(max)
,@Delimiter char
)
RETURNS @Results table
(
 Ordinal int
,StringValue varchar(max)
)
as
begin

    set @String = isnull(@String,'')
    set @Delimiter = isnull(@Delimiter,'')

    declare
     @TempString varchar(max) = @String
    ,@Ordinal int = 0
    ,@CharIndex int = 0

    set @CharIndex = charindex(@Delimiter, @TempString)
    while @CharIndex != 0 begin     
        set @Ordinal += 1       
        insert @Results values
        (
         @Ordinal
        ,substring(@TempString, 0, @CharIndex)
        )       
        set @TempString = substring(@TempString, @CharIndex + 1, len(@TempString) - @CharIndex)     
        set @CharIndex = charindex(@Delimiter, @TempString)
    end

    if @TempString != '' begin
        set @Ordinal += 1 
        insert @Results values
        (
         @Ordinal
        ,@TempString
        )
    end

    return
end