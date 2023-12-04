-- ============================================
-- Author:		Dave Brown
-- Create date: 04 Dec 2023
-- Description:	Add a contact to a tenancy
-- ============================================
CREATE PROCEDURE [property].[Tenancy_AddContact]
	@TenancyId int,
	@ContactId int,
	@PortfolioId int,
	@CurrentUserId int,
	@Id	int OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @TenantContactTypeId INT
	SET @TenantContactTypeId = 1

	DECLARE @CurrentPortflioId INT

	SELECT	@CurrentPortflioId = SelectedPortfolioId
	FROM	profile.[User]
	Where	Id = @CurrentUserId

	IF	@CurrentPortflioId != @PortfolioId
	BEGIN
	   RAISERROR ('Invalid Portfolio for user.' , 16, 1) WITH NOWAIT  
	   RETURN
	END
	
	DECLARE @ContactPortflioId INT

	SELECT	@ContactPortflioId = PortfolioId
	FROM	general.Contact
	Where	Id = @ContactId

	IF	@ContactPortflioId != @PortfolioId
	BEGIN
	   RAISERROR ('Invalid Portfolio for contact.' , 16, 1) WITH NOWAIT  
	   RETURN
	END

	INSERT INTO [property].[TenancyContact] (TenancyId, ContactId, Deleted, CreateUserId, CreateDate, AmendUserId, AmendDate)
	VALUES (@TenancyId, @ContactId, 0, @CurrentUserId, SYSDATETIME(), @CurrentUserId, SYSDATETIME())
	
	SET @Id = SCOPE_IDENTITY()

	IF NOT EXISTS (SELECT 1 FROM [general].[ContactContactType] WHERE ContactTypeId = @TenantContactTypeId AND ContactId = @ContactId AND Deleted = 0)
	BEGIN
		INSERT INTO [general].[ContactContactType] (ContactId, ContactTypeId, Deleted, CreateUserId, CreateDate, AmendUserId, AmendDate)
		VALUES (@ContactId, @TenantContactTypeId, 0, @CurrentUserId, SYSDATETIME(), @CurrentUserId, SYSDATETIME())
	END
END
