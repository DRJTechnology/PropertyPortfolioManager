-- ===========================================================
-- Author:		Dave Brown
-- Create date: 20 Oct 2023
-- Description:	Updates a property portfolio record to deleted
-- ===========================================================
CREATE PROCEDURE [property].[Portfolio_Delete]
	@Id					INT, 
	@CurrentUserId		INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE	p
	SET		Deleted = 1,
			AmendUserId = @CurrentUserId,
			AmendDate = SYSDATETIME()
	FROM	[property].[Portfolio] p
	INNER JOIN property.PortfolioUser pu on p.Id = pu.PortfolioId AND pu.UserId = @CurrentUserId AND pu.Deleted = 0
	WHERE	p.Id = @Id
		AND p.Deleted = 0

END