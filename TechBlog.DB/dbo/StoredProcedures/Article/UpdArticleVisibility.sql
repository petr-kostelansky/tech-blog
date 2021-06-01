CREATE PROCEDURE [dbo].[UpdArticleVisibility]
	@Id int,
	@Visible bit
AS

	UPDATE dbo.Article
	SET 
		[Visible] = @Visible,
		[LastChange] = GETDATE()
	WHERE Id = @Id

	IF(@@ERROR = 0 OR @@ROWCOUNT = 1)
		RETURN 1
	ELSE
		RETURN 0

GO