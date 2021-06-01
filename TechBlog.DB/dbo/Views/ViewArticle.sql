CREATE VIEW [dbo].[ViewArticle]
AS 
	SELECT 
		A.[Id] 
		,A.[Url] 
		,A.[Title] 
		,A.[Description]
		,A.[Content] 
		,A.[Visible] 
		,A.[Created] 
		,A.[LastChange] 
		,A.Visited
		,A.MetaDescription
		,A.MetaKeywords
		,U.UserName as Author
		,U.Id as AuthorId
	FROM [dbo].[Article] A
	INNER JOIN dbo.[User] U
	ON U.Id = A.CreatedByUserId