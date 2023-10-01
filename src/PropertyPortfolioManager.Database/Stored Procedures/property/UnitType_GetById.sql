-- ============================================
-- Author:		Dave Brown
-- Create date: 01 Oct 2023
-- Description:	Get a property unit type record
-- ============================================
CREATE PROCEDURE [property].[UnitType_GetById]
	@Id				INT,
	@PortfolioId	INT
AS
BEGIN
	SET NOCOUNT ON;

    SELECT	Id, [Type], Deleted, CreateUserId, CreateDate, AmendUserId, AmendDate
	FROM	[property].[UnitType]
	WHERE	PortfolioId = @PortfolioId AND Id = @Id

END