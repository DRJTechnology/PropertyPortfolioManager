-- =============================================
-- Author:		Dave Brown
-- Create date: 18 Aug 2023
-- Description:	Returns user record by ObjectIdentifier
-- =============================================
CREATE PROCEDURE profile.User_GetByObjectIdentifier 
	@ObjectIdentifier uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    SELECT	Id,
			ObjectIdentifier,
			Name,
			CreateDate,
			AmendDate
	FROM	[profile].[User]
	WHERE	ObjectIdentifier = @ObjectIdentifier
END