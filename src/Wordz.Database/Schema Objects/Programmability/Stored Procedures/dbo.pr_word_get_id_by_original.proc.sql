
CREATE PROCEDURE dbo.pr_word_get_id_by_original
(
    @original nvarchar(254),
    @language_id int
)
AS

SELECT word_id AS id
FROM word_language
WHERE language_id = @language_id AND
[text] = @original

