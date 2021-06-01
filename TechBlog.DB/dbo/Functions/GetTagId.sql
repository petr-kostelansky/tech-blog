CREATE FUNCTION [dbo].[GetTagId]
(
	@TagCode	varchar(20)
)
RETURNS INT
AS
BEGIN
	DECLARE @TagId INT

	SELECT @TagId = Id 
	FROM [dbo].[Tag]
	WHERE [Code] = @TagCode

	RETURN @TagId
END
