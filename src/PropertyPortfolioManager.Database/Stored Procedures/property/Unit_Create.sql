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
	@MainPictureId		VARCHAR(4000) = NULL,
	@MainPictureFileName	nvarchar(500) = NULL,
	@MainPictureSize	BIGINT = NULL,
	@Active				BIT,
	@CurrentUserId		INT
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @MainImageFileId INT

    INSERT INTO [general].[Address] (StreetAddress, TownCity, CountyRegion, PostCode, Deleted, CreateUserId, CreateDate, AmendUserId, AmendDate)
	VALUES (@StreetAddress, @TownCity, @CountyRegion, @PostCode, 0, @CurrentUserId, SYSDATETIME(), @CurrentUserId, SYSDATETIME())

	DECLARE @AddressId INT
	SET @AddressId = SCOPE_IDENTITY()

	IF (@MainPictureId IS NOT NULL AND @MainPictureId != '')
	BEGIN
		EXEC [general].[GetFileIdFromItemId] @ItemId = @MainPictureId, @FileName = @MainPictureFileName, @Size = @MainPictureSize, @UserId = @CurrentUserId, @FileId = @MainImageFileId OUTPUT
	END


    INSERT INTO [property].[Unit] (PortfolioId, MainImageFileId, Code, UnitTypeId, AddressId, PurchasePrice, PurchaseDate, SalePrice, SaleDate, Active, Deleted, CreateUserId, CreateDate, AmendUserId, AmendDate)
	VALUES (@PortfolioId, @MainImageFileId, @Code, @UnitTypeId, @AddressId, @PurchasePrice, @PurchaseDate, @SalePrice, @SaleDate, @Active, 0, @CurrentUserId, SYSDATETIME(), @CurrentUserId, SYSDATETIME())

	SET @Id = SCOPE_IDENTITY()

END