-- ===========================================================
-- Author:		Dave Brown
-- Create date: 15 Nov 2023
-- Description:	Updates a tenancy record to deleted
-- ===========================================================
CREATE PROCEDURE [property].[Tenancy_Delete]
	@Id					INT, 
	@PortfolioId		INT,
	@CurrentUserId		INT
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

	UPDATE	t
	SET		Deleted = 1,
			AmendUserId = @CurrentUserId,
			AmendDate = SYSDATETIME()
	FROM	[property].[Tenancy] t
	INNER JOIN	[property].[Unit] u on t.UnitId = u.Id
	WHERE	u.PortfolioId = @PortfolioId AND t.Id = @Id

END