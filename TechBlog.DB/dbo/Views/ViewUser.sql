CREATE VIEW [dbo].[ViewUser]
AS 
	SELECT 
		[Id],
		[Email],
		[PasswordHash],
		[SecurityStamp],
		[AccessFailedCount],
		[UserName]
	FROM [dbo].[User]
