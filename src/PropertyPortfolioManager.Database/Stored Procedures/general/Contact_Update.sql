-- =============================================
-- Author:		Dave Brown
-- Create date: 01 Sep 2023
-- Description:	Updates a contact record
-- =============================================
CREATE PROCEDURE [general].[Contact_Update]
	@Id					INT, 
	@PortfolioId		INT,
	@ContactTypeId		INT,
	@Name				NVARCHAR(50),
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

	DECLARE @AddressID INT
	DECLARE @CurrentPortflioId INT

	SELECT	@AddressID = AddressId,
			@CurrentPortflioId = PortfolioId
	FROM	general.Contact
	Where	Id = @Id

	IF	@CurrentPortflioId != @PortfolioId
	BEGIN
	   RAISERROR ('Invalid Portfolio for user.' , 16, 1) WITH NOWAIT  
	   RETURN
	END

    UPDATE	[general].[Address]
	SET		StreetAddress = @StreetAddress,
			TownCity = @TownCity,
			CountyRegion = @CountyRegion,
			PostCode = @PostCode,
			AmendUserId = @CurrentUserId,
			AmendDate = SYSDATETIME()
	Where	Id = @AddressId

	UPDATE	[general].[Contact]
	SET		--ContactTypeId = @ContactTypeId,
			[Name] = @Name,
			Notes = @Notes,
			Active = @Active,
			AmendUserId = @CurrentUserId,
			AmendDate = SYSDATETIME()
	WHERE	Id = @Id

END