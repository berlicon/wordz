
CREATE PROCEDURE dbo.pr_word_get_sound_by_id
(
    @id int
)
AS

SELECT NULL AS original_sound, NULL AS translation_sound
FROM word WHERE id = @id

