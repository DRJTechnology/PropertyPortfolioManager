-- ====================================================
-- Author:		Dave Brown
-- Create date: 01 Sep 2023
-- Description:	Get a property unit record with address
-- ---------------------------------------------------
-- 09 Nov 2023	Dave Brown	Linking to Main Image
-- ====================================================
CREATE PROCEDURE [property].[Unit_GetById]
	@Id					INT,
	@PortfolioId		INT
AS
BEGIN
	SET NOCOUNT ON;

    SELECT	u.Id, f.ItemId AS MainPictureId, u.PortfolioId, u.Code, u.AddressId, u.UnitTypeId, ut.[Type] AS UnitType, u.PurchaseDate, u.PurchasePrice, u.SaleDate, u.SalePrice, u.Active, u.Deleted, u.CreateUserId, u.CreateDate, u.AmendUserId, u.AmendDate
	FROM	[property].[Unit] u
	INNER JOIN	[property].[UnitType] ut on u.UnitTypeId = ut.id
	LEFT OUTER JOIN [general].[File] f ON u.MainImageFileId = f.Id
	WHERE	u.PortfolioId = @PortfolioId AND u.Id = @Id

    SELECT	a.Id, StreetAddress, TownCity, CountyRegion, PostCode, a.Deleted, a.CreateUserId, a.CreateDate, a.AmendUserId, a.AmendDate
	FROM	[general].[Address] a
	INNER JOIN	[property].[Unit] u ON u.AddressId = a.Id
	WHERE	u.PortfolioId = @PortfolioId AND u.Id = @Id

END