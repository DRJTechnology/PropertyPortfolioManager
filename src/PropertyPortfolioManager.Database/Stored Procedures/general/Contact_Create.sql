-- =====================================
-- Author:		Dave Brown
-- Create date: 05 Oct 2023
-- Description:	Creates a contact record
-- =====================================
CREATE PROCEDURE [general].[Contact_Create]
	@Id					INT OUTPUT, 
	@PortfolioId		INT,
	@ContactTypeId		INT,
	@Name				NVARCHAR(255),
	@StreetAddress		NVARCHAR(255) = NULL,
	@TownCity			NVARCHAR(128) = NULL,
	@CountyRegion		NVARCHAR(128) = NULL,
	@PostCode			NVARCHAR(50) = NULL,
	@Notes				NVARCHAR(4000) = NULL,
	@Active				BIT,
	@CurrentUserId		INT
AS
BEGIN
	SET NOCOUNT ON;

    INSERT INTO [general].[Address] (StreetAddress, TownCity, CountyRegion, PostCode, Deleted, CreateUserId, CreateDate, AmendUserId, AmendDate)
	VALUES (@StreetAddress, @TownCity, @CountyRegion, @PostCode, 0, @CurrentUserId, SYSDATETIME(), @CurrentUserId, SYSDATETIME())

	DECLARE @AddressId INT
	SET @AddressId = SCOPE_IDENTITY()

    INSERT INTO [general].[Contact] (PortfolioId, ContactTypeId, [Name], AddressId, Notes, Active, Deleted, CreateUserId, CreateDate, AmendUserId, AmendDate)
	VALUES (@PortfolioId, @ContactTypeId, @Name, @AddressId, @Notes, @Active, 0, @CurrentUserId, SYSDATETIME(), @CurrentUserId, SYSDATETIME())

	SET @Id = SCOPE_IDENTITY()

END
