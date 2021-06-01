CREATE PROCEDURE [dbo].[GetTag]
	@TagCode	VARCHAR(50)
AS
	SELECT 
		[Code],
		[Name]
	FROM [dbo].[Tag]
	WHERE [Code] = @TagCode
