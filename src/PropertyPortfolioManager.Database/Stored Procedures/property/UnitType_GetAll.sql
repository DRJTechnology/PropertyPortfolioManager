-- =============================================
-- Author:		Dave Brown
-- Create date: 30 Sep 2023
-- Description:	Get all property unit type records
-- =============================================
CREATE PROCEDURE [property].[UnitType_GetAll]
	@PortfolioId	INT,
	@ActiveOnly		BIT = 1
AS
BEGIN
	SET NOCOUNT ON;

    SELECT	Id, [Type], Active
	FROM	[property].[UnitType]
	Where	PortfolioId = @PortfolioId
		AND Deleted = 0
		AND (@ActiveOnly != 1 OR Active = 1)
	ORDER BY [Type]

END