﻿-- =============================================
-- Author:		Dave Brown
-- Create date: 23 Aug 2023
-- Description:	Creates a property unit record
-- =============================================
CREATE PROCEDURE [profile].[Unit_Create]
	@Id					INT OUTPUT, 
	@Code				NVARCHAR(50),
	@UnitTypeId			INT,
	@StreetAddress		NVARCHAR(255) = NULL,
	@TownCity			NVARCHAR(128) = NULL,
	@CountyRegion		NVARCHAR(128) = NULL,
	@PostCode			NVARCHAR(50) = NULL,
	@PurchasePrice		MONEY = NULL,
	@PurchaseDate		DATETIME = NULL,
	@SalePrice			MONEY = NULL,
	@SaleDate			DATETIME = NULL,
	@CurrentUserId		INT
AS
BEGIN
	SET NOCOUNT ON;

	-- TODO Create a transaction?

    INSERT INTO [general].[Address] (StreetAddress, TownCity, CountyRegion, PostCode, Deleted, CreateUserId, CreateDate, AmendUserId, AmendDate)
	VALUES (@StreetAddress, @TownCity, @CountyRegion, @PostCode, 0, @CurrentUserId, SYSDATETIME(), @CurrentUserId, SYSDATETIME())

	DECLARE @AddressId INT
	SET @AddressId = SCOPE_IDENTITY()

    INSERT INTO [property].[Unit] (Code, UnitTypeId, AddressId, PurchasePrice, PurchaseDate, SalePrice, SaleDate, Deleted, CreateUserId, CreateDate, AmendUserId, AmendDate)
	VALUES (@Code, @UnitTypeId, @AddressId, @PurchasePrice, @PurchaseDate, @SalePrice, @SaleDate, 0, @CurrentUserId, SYSDATETIME(), @CurrentUserId, SYSDATETIME())

	DECLARE @Unit INT
	SET @Id = SCOPE_IDENTITY()

END