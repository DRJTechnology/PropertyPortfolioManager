-- ================================================
-- Author:		Dave Brown
-- Create date: 30 Sep 2023
-- Description:	Creates a property unit type record
-- ================================================
CREATE PROCEDURE [property].[UnitType_Create]
	@Id					INT OUTPUT, 
	@Type				NVARCHAR(255),
	@CurrentUserId		INT
AS
BEGIN
	SET NOCOUNT ON;

    INSERT INTO [property].[UnitType] ([Type], Deleted, CreateUserId, CreateDate, AmendUserId, AmendDate)
	VALUES (@Type, 0, @CurrentUserId, SYSDATETIME(), @CurrentUserId, SYSDATETIME())

	SET @Id = SCOPE_IDENTITY()

END