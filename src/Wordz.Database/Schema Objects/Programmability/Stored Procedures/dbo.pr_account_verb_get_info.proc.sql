
CREATE PROCEDURE pr_account_verb_get_info
(
    @account_id int
)
AS
DECLARE @iKnow int
DECLARE @percentOfPopular int
DECLARE @percentOfTotal int

SET @iKnow = (SELECT COUNT(*) FROM account_verb
WHERE account_id = @account_id)

SET @percentOfPopular = (SELECT CEILING(100 * @iKnow / COUNT(*)) FROM verb
WHERE popular = 1)

IF (@percentOfPopular > 100) SET @percentOfPopular = 100

SET @percentOfTotal = (SELECT CEILING(100 * @iKnow / COUNT(*)) FROM verb)

SELECT
    @iKnow AS iKnow,
    @percentOfPopular AS percentOfPopular,
    @percentOfTotal AS percentOfTotal

