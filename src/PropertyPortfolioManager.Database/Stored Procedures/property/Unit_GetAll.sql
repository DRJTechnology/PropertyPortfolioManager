-- ==================================================
-- Author:		Dave Brown
-- Create date: 03 Sep 2023
-- Description:	Get all property unit records (Basic)
-- ---------------------------------------------------
-- 08 Nov 2023	Dave Brown	Addition of TownCity field
--							Linking to Main Image
-- ===================================================
CREATE PROCEDURE [property].[Unit_GetAll]
	@PortfolioId	INT,
	@ActiveOnly		BIT
AS
BEGIN
	SET NOCOUNT ON;

    SELECT	u.Id, f.ItemId AS MainPictureId, u.PortfolioId, Code, StreetAddress, TownCity, ut.[Type] AS UnitType, u.Active
	FROM	[property].[Unit] u
	INNER JOIN	[general].[Address] a ON u.AddressId = a.Id
	INNER Join	[property].[UnitType] ut ON u.UnitTypeId = ut.Id
	LEFT OUTER JOIN [general].[File] f ON u.MainImageFileId = f.Id
	Where	u.PortfolioId = @PortfolioId
		AND u.Deleted = 0
		AND (@ActiveOnly != 1 OR u.Active = 1)

END