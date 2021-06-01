CREATE PROCEDURE [dbo].[SearchArticles]
	@TotalItemsCount	INT OUT,
	@Tags AS [dbo].[SearchWords] READONLY,
	@SearchText			VARCHAR(100) = NULL,
	@OnlyVisible		BIT = 1,
	@AuthorId			INT = NULL,
	@OrderBy			INT = 1,
	@Page				INT = 1,
	@PageSize			INT = 9
AS
	DECLARE @FilteredData TABLE
	(
		Id			INT	NOT NULL,
		Visible		BIT	NOT NULL
	)

	-- Search in tags
	IF (SELECT COUNT(*) FROM @Tags) > 0
	BEGIN
		INSERT INTO @FilteredData
		SELECT
			ArticlesByTags.Id,
			MAX(CONVERT(int, ArticlesByTags.Visible))
		FROM (
			SELECT
				A.[Id],
				A.Visible
			FROM [dbo].[Article] AS A
			LEFT JOIN [dbo].[ArticleTag] AS ATag ON ATag.ArticleId = A.Id
			LEFT JOIN [dbo].[Tag] as T ON T.Id = ATag.TagId
			WHERE 
				T.[Code] IN (SELECT Word FROM @Tags)
				AND (A.Visible = 0 OR A.Visible = @OnlyVisible)
			) ArticlesByTags
			Group BY ArticlesByTags.Id
	END

	IF @AuthorId IS NOT NULL
	BEGIN
		DELETE FD
		FROM @FilteredData FD
		INNER JOIN Article A
			ON A.Id = FD.Id
		INNER JOIN [User] U
			ON U.Id = A.CreatedByUserId
		WHERE U.Id != @AuthorId
	END

	SELECT @TotalItemsCount = COUNT(*)
	FROM @FilteredData

	SELECT 
		A.[Id],
		A.[Url],
		A.[Title],
		A.[Description],
		A.Content,
		A.Visible,
		A.Created,
		U.UserName as Author
	FROM @FilteredData FD
	INNER JOIN Article A
	ON A.Id = FD.Id
	INNER JOIN [User] U
	ON U.Id = A.CreatedByUserId
	WHERE A.Visible = 1
	ORDER BY 
	CASE WHEN @OrderBy = 1 THEN A.Id END,
	CASE WHEN @OrderBy = 2 THEN A.Created END DESC
	OFFSET ((@Page -1) * @PageSize) ROWS
	FETCH NEXT @PageSize ROWS ONLY;
	 
