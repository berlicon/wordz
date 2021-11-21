
CREATE PROCEDURE pr_word_get_count
(
    @learn_language_id int,
    @native_language_id int
)
AS

declare @count1 int
set @count1 = (
	SELECT COUNT(*)
	FROM word_language
	WHERE language_id = @learn_language_id)

declare @count2 int
set @count2 = (
	SELECT COUNT(*)
	FROM word_language
	WHERE language_id = @native_language_id)

IF @count1 < @count2
SELECT @count1
ELSE 
SELECT @count2

