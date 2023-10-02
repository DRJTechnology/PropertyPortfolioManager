-- ===========================================================
-- Author:		Dave Brown
-- Create date: 02 Oct 2023
-- Description:	Sets the current selected portfolio for a user
-- ===========================================================
CREATE PROCEDURE [property].[Portfolio_SelectForUser]
	@PortfolioId	INT,
	@UserId			INT
AS
	UPDATE	u
	SET		SelectedPortfolioId = @PortfolioId
	FROM	[profile].[User] u
	INNER JOIN	[property].[PortfolioUser] pu ON u.Id = pu.UserId AND pu.PortFolioId = @PortfolioId AND pu.Deleted = 0
	WHERE	u.Id = @UserId
RETURN 0
