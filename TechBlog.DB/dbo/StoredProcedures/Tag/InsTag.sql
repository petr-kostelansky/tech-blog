CREATE PROCEDURE [dbo].[InsTag]
	@Code		VARCHAR(50),
	@Name		VARCHAR(50)
AS
	INSERT INTO [dbo].[Tag] (Code, [Name])
	VALUES
	(
		@Code,
		@Name
	)

	RETURN SCOPE_IDENTITY()
