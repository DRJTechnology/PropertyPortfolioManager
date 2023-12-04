-- ============================================
-- Author:		Dave Brown
-- Create date: 04 Dec 2023
-- Description:	Remove a contact from a tenancy
-- ============================================
CREATE PROCEDURE [property].[Tenancy_RemoveContact]
	@TenancyId int,
	@ContactId int,
	@PortfolioId int,
	@CurrentUserId int
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @CurrentPortflioId INT

	SELECT	@CurrentPortflioId = SelectedPortfolioId
	FROM	profile.[User]
	Where	Id = @CurrentUserId

	IF	@CurrentPortflioId != @PortfolioId
	BEGIN
	   RAISERROR ('Invalid Portfolio for user.' , 16, 1) WITH NOWAIT  
	   RETURN
	END

	UPDATE	tc
	SET		Deleted = 1,
			AmendUserId = @CurrentUserId,
			AmendDate = SYSDATETIME()
	FROM	[property].[TenancyContact] tc
	INNER JOIN [general].[Contact] c on tc.ContactId = c.Id
	WHERE	c.PortfolioId = @PortfolioId 
		AND tc.TenancyId = @TenancyId
		AND tc.ContactId = @ContactId
		AND tc.Deleted = 0
END
