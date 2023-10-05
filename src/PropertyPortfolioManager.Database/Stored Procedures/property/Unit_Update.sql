-- =============================================
-- Author:		Dave Brown
-- Create date: 01 Sep 2023
-- Description:	Updates a property unit record
-- =============================================
CREATE PROCEDURE [property].[Unit_Update]
	@Id					INT, 
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

	DECLARE @AddressID INT
	DECLARE @CurrentPortflioId INT

	SELECT	@AddressID = AddressId,
			@CurrentPortflioId = PortfolioId
	FROM	property.Unit
	Where	Id = @Id

	IF	@CurrentPortflioId != @PortfolioId
	BEGIN
	   RAISERROR ('Invalid Portfolio for user.' , 16, 1) WITH NOWAIT  
	   RETURN
	END

    UPDATE [general].[Address]
	SET		StreetAddress = @StreetAddress,
			TownCity = @TownCity,
			CountyRegion = @CountyRegion,
			PostCode = @PostCode,
			AmendUserId = @CurrentUserId,
			AmendDate = SYSDATETIME()
	Where	Id = @AddressId

	UPDATE	[property].[Unit]
	SET		Code = @Code,
			UnitTypeId = @UnitTypeId,
			PurchasePrice = @PurchasePrice,
			PurchaseDate = @PurchaseDate,
			SalePrice = @SalePrice,
			SaleDate = @SaleDate,
			Active = @Active,
			AmendUserId = @CurrentUserId,
			AmendDate = SYSDATETIME()
	WHERE	Id = @Id

END