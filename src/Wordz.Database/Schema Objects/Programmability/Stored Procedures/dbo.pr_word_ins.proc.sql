
CREATE PROCEDURE dbo.pr_word_ins
(
    @original nvarchar(254),
    @translation nvarchar(254),
    @native_language_id int,
    @learn_language_id int
)
AS

DECLARE @word_id int

INSERT INTO word DEFAULT VALUES
SET @word_id = SCOPE_IDENTITY()

INSERT INTO word_language
(language_id, word_id, [text])
VALUES (@learn_language_id, @word_id, @original)

IF (@native_language_id <> @learn_language_id)
    BEGIN
        INSERT INTO word_language
        (language_id, word_id, [text])
        VALUES (@native_language_id, @word_id, @translation)
    END

