-- ===========================================================
-- Author:		Dave Brown
-- Create date: 20 Oct 2023
-- Description:	Updates a contact type record to deleted
-- ===========================================================
CREATE PROCEDURE [general].[ContactType_Delete]
	@Id					INT, 
	@CurrentUserId		INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @CurrentPortflioId INT

	SELECT	@CurrentPortflioId = SelectedPortfolioId
	FROM	profile.[User]
	Where	Id = @CurrentUserId

	UPDATE	[general].[ContactType]
	SET		Deleted = 1,
			AmendUserId = @CurrentUserId,
			AmendDate = SYSDATETIME()
	WHERE	PortfolioId = @CurrentPortflioId AND Id = @Id

END