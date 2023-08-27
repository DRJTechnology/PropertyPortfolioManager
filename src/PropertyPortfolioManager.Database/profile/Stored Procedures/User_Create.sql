-- =============================================
-- Author:		Dave Brown
-- Create date: 18 Aug 2023
-- Description:	Creates a user record
-- =============================================
CREATE PROCEDURE [profile].[User_Create]
	@Id					INT OUTPUT, 
	@ObjectIdentifier	UNIQUEIDENTIFIER,
	@Name				NVARCHAR(255)
AS
BEGIN
	SET NOCOUNT ON;

    INSERT INTO [profile].[User] (ObjectIdentifier, Name, Deleted, CreateDate, AmendDate)
	VALUES (@ObjectIdentifier, @Name, 0, SYSDATETIME(), SYSDATETIME())

	SET @Id = SCOPE_IDENTITY()
END