
CREATE PROCEDURE dbo.pr_word_get_by_original
(
    @original nvarchar(254),
    @native_language_id int,
    @learn_language_id int
)
AS

DECLARE @word_id int

SET @word_id = (SELECT TOP 1 word_id
                FROM word_language
                WHERE language_id = @learn_language_id AND
                [text] = @original)

SELECT @word_id AS id, [text] AS [translation], 1 AS sounded
FROM word_language
WHERE language_id = @native_language_id AND word_id = @word_id

