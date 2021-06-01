CREATE PROCEDURE [dbo].[UpdTag]
	@OriginalCode	VARCHAR(50),
	@Code			VARCHAR(50),
	@Name			VARCHAR(50)
AS
	UPDATE [dbo].[Tag]
	SET
		[Code] = @Code,
		[Name] = @Name
	WHERE [Code] = @OriginalCode

