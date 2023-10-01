-- ====================================================
-- Author:		Dave Brown
-- Create date: 01 Sep 2023
-- Description:	Get a property unit record with address
-- ====================================================
CREATE PROCEDURE [property].[Unit_GetById]
	@Id					INT
AS
BEGIN
	SET NOCOUNT ON;

    SELECT	u.Id, u.Code, u.AddressId, u.UnitTypeId, ut.[Type] AS UnitType, u.PurchaseDate, u.PurchasePrice, u.SaleDate, u.SalePrice, u.Deleted, u.CreateUserId, u.CreateDate, u.AmendUserId, u.AmendDate
	FROM	[property].[Unit] u
	INNER JOIN	[property].[UnitType] ut on u.UnitTypeId = ut.id
	WHERE	u.Id = @Id

    SELECT	a.Id, StreetAddress, TownCity, CountyRegion, PostCode, a.Deleted, a.CreateUserId, a.CreateDate, a.AmendUserId, a.AmendDate
	FROM	[general].[Address] a
	INNER JOIN	[property].[Unit] u ON u.AddressId = a.Id
	WHERE	u.Id = @Id

END