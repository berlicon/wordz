
CREATE PROCEDURE dbo.pr_word_ins_use_exist
(
    @original nvarchar(254),
    @translation nvarchar(254),
    @native_language_id int,
    @learn_language_id int
)
AS

DECLARE @word_id int

SET @word_id = (
    SELECT TOP 1 word_id
    FROM word_language
    WHERE language_id = @learn_language_id
    AND text = @original)
    
IF (@word_id IS NOT NULL)
    BEGIN
        INSERT INTO word_language
        (language_id, word_id, [text])
        VALUES (@native_language_id, @word_id, @translation)
    END

