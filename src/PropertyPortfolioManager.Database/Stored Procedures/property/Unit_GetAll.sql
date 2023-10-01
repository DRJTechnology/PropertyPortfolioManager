-- =============================================
-- Author:		Dave Brown
-- Create date: 03 Sep 2023
-- Description:	Get all property unit records (Basic)
-- =============================================
CREATE PROCEDURE [property].[Unit_GetAll]
	@PortfolioId		INT
AS
BEGIN
	SET NOCOUNT ON;

    SELECT	u.Id, Code, StreetAddress, ut.[Type] AS UnitType
	FROM	[property].[Unit] u
	INNER JOIN	[general].[Address] a ON u.AddressId = a.Id
	INNER Join	[property].[UnitType] ut ON u.UnitTypeId = ut.Id
	Where	u.PortfolioId = @PortfolioId
		AND u.Deleted = 0

END