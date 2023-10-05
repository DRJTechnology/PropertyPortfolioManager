-- =============================================
-- Author:		Dave Brown
-- Create date: 23 Aug 2023
-- Description:	Creates a property unit record
-- =============================================
CREATE PROCEDURE [property].[Unit_Create]
	@Id					INT OUTPUT, 
	@PortfolioId		INT,
	@Code				NVARCHAR(50),
	@UnitTypeId			INT,
	@StreetAddress		NVARCHAR(255) = NULL,
	@TownCity			NVARCHAR(128) = NULL,
	@CountyRegion		NVARCHAR(128) = NULL,
	@PostCode			NVARCHAR(50) = NULL,
	@PurchasePrice		MONEY = NULL,
	@PurchaseDate		DATE = NULL,
	@SalePrice			MONEY = NULL,
	@SaleDate			DATE = NULL,
	@Active				BIT,
	@CurrentUserId		INT
AS
BEGIN
	SET NOCOUNT ON;

    INSERT INTO [general].[Address] (StreetAddress, TownCity, CountyRegion, PostCode, Deleted, CreateUserId, CreateDate, AmendUserId, AmendDate)
	VALUES (@StreetAddress, @TownCity, @CountyRegion, @PostCode, 0, @CurrentUserId, SYSDATETIME(), @CurrentUserId, SYSDATETIME())

	DECLARE @AddressId INT
	SET @AddressId = SCOPE_IDENTITY()

    INSERT INTO [property].[Unit] (PortfolioId, Code, UnitTypeId, AddressId, PurchasePrice, PurchaseDate, SalePrice, SaleDate, Active, Deleted, CreateUserId, CreateDate, AmendUserId, AmendDate)
	VALUES (@PortfolioId, @Code, @UnitTypeId, @AddressId, @PurchasePrice, @PurchaseDate, @SalePrice, @SaleDate, @Active, 0, @CurrentUserId, SYSDATETIME(), @CurrentUserId, SYSDATETIME())

	SET @Id = SCOPE_IDENTITY()

END