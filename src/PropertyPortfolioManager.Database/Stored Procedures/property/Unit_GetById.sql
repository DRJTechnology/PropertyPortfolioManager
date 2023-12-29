-- ====================================================
-- Author:		Dave Brown
-- Create date: 01 Sep 2023
-- Description:	Get a property unit record with address
-- ---------------------------------------------------
-- 09 Nov 2023	Dave Brown	Linking to Main Image
-- 10 Nov 2023	Dave Brown	Retuen full Main Image details
-- ====================================================
CREATE PROCEDURE [property].[Unit_GetById]
	@Id					INT,
	@PortfolioId		INT
AS
BEGIN
	SET NOCOUNT ON;

    SELECT	u.Id, u.PortfolioId, u.Code, u.AddressId, u.UnitTypeId, ut.[Type] AS UnitType, u.PurchaseDate, u.PurchasePrice, u.SaleDate, u.SalePrice, u.Active, u.Deleted, u.CreateUserId, u.CreateDate, u.AmendUserId, u.AmendDate
	FROM	[property].[Unit] u
	INNER JOIN	[property].[UnitType] ut on u.UnitTypeId = ut.Id
	WHERE	u.PortfolioId = @PortfolioId AND u.Id = @Id

    SELECT	a.Id, StreetAddress, TownCity, CountyRegion, PostCode, a.Deleted, a.CreateUserId, a.CreateDate, a.AmendUserId, a.AmendDate
	FROM	[general].[Address] a
	INNER JOIN	[property].[Unit] u ON u.AddressId = a.Id
	WHERE	u.PortfolioId = @PortfolioId AND u.Id = @Id

    SELECT	f.Id, f.ItemId, f.FileName, f.Size, f.Deleted, f.CreateUserId, f.CreateDate, f.AmendUserId, f.AmendDate
	FROM	[general].[File] f
	INNER JOIN [property].[Unit] u on f.Id = u.MainImageFileId
	WHERE	u.Id = @Id

END