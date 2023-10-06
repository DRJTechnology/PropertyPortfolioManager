-- =============================================
-- Author:		Dave Brown
-- Create date: 03 Sep 2023
-- Description:	Get all property unit records (Basic)
-- =============================================
CREATE PROCEDURE [property].[Unit_GetAll]
	@PortfolioId	INT,
	@ActiveOnly		BIT
AS
BEGIN
	SET NOCOUNT ON;

    SELECT	u.Id, u.PortfolioId, Code, StreetAddress, ut.[Type] AS UnitType, u.Active
	FROM	[property].[Unit] u
	INNER JOIN	[general].[Address] a ON u.AddressId = a.Id
	INNER Join	[property].[UnitType] ut ON u.UnitTypeId = ut.Id
	Where	u.PortfolioId = @PortfolioId
		AND u.Deleted = 0
		AND (@ActiveOnly != 1 OR u.Active = 1)

END