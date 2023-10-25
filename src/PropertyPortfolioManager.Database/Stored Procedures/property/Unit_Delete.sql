-- ======================================================
-- Author:		Dave Brown
-- Create date: 23 Oct 2023
-- Description:	Updates a property unit record to deleted
-- ======================================================
CREATE PROCEDURE [property].[Unit_Delete]
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

	UPDATE	[property].[Unit]
	SET		Deleted = 1,
			AmendUserId = @CurrentUserId,
			AmendDate = SYSDATETIME()
	WHERE	PortfolioId = @PortfolioId AND Id = @Id

END