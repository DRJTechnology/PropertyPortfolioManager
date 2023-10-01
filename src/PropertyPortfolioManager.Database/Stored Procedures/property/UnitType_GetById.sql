-- ============================================
-- Author:		Dave Brown
-- Create date: 01 Oct 2023
-- Description:	Get a property unit type record
-- ============================================
CREATE PROCEDURE [property].[UnitType_GetById]
	@Id					INT
AS
BEGIN
	SET NOCOUNT ON;

    SELECT	Id, [Type], Deleted, CreateUserId, CreateDate, AmendUserId, AmendDate
	FROM	[property].[UnitType]
	WHERE	Id = @Id

END