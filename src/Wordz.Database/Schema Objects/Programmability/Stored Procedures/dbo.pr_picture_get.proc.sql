CREATE PROCEDURE [dbo].[pr_picture_get]
	@picture_id int
AS
SELECT picture.data FROM picture WHERE id = @picture_id
