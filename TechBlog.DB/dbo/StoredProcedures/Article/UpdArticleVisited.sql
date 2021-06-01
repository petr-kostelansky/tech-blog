CREATE PROCEDURE [dbo].[UpdArticleVisited]
	@ArticleId	INT
AS
	UPDATE [dbo].[Article] 
	SET 
		[Visited] = [Visited] + 1
	WHERE [Id] = @ArticleId

		
