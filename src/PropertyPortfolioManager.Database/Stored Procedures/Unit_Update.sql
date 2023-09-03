-- =============================================
-- Author:		Dave Brown
-- Create date: 01 Sep 2023
-- Description:	Updates a property unit record
-- =============================================
CREATE PROCEDURE [profile].[Unit_Update]
	@Id					INT, 
	@Code				NVARCHAR(50),
	@StreetAddress		NVARCHAR(255) = NULL,
	@TownCity			NVARCHAR(128) = NULL,
	@CountyRegion		NVARCHAR(128) = NULL,
	@PostCode			NVARCHAR(50) = NULL,
	@CurrentUserId		INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @AddressID INT

	SELECT	@AddressID = AddressID
	FROM	property.Unit
	Where	Id = @Id

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
			AmendUserId = @CurrentUserId,
			AmendDate = SYSDATETIME()
	WHERE	Id =@Id

END