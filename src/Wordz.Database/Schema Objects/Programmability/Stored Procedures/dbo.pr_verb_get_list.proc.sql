
CREATE PROCEDURE pr_verb_get_list
(
    @account_id int,
    @load_popular bit,
    @not_use_well_known_verbs bit,
    @word_count int
)
AS

SET RowCount @word_count
SELECT v.id, v.form1, v.form2, v.form3, v.translation, v.verb_type_id
FROM verb v
LEFT JOIN account_verb av
ON av.verb_id = v.id AND av.account_id = @account_id
WHERE (@load_popular = 0 OR v.popular = 1) AND
(@not_use_well_known_verbs = 0 OR av.account_id IS NULL)
ORDER BY v.form1

