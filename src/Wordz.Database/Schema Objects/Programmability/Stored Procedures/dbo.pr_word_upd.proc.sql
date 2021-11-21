
CREATE PROCEDURE dbo.pr_word_upd
(
    @id int,
    @original varchar(254),
    @translation varchar(254),
    @native_language_id int,
    @learn_language_id int
)
AS

UPDATE word_language
SET [text] = @original
WHERE word_id = @id AND language_id = @learn_language_id

IF (@native_language_id <> @learn_language_id)
    BEGIN
        UPDATE word_language
        SET [text] = @translation
        WHERE word_id = @id AND language_id = @native_language_id
    END

