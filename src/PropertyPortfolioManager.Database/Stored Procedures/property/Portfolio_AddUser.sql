-- ============================================
-- Author:		Dave Brown
-- Create date: 01 Oct 2023
-- Description:	Creates a portfolio user record
-- ============================================
CREATE PROCEDURE [property].[Portfolio_AddUser]
	@PortfolioId	INT,
	@UserId			INT,
	@CurrentUserId	INT
AS

	IF NOT EXISTS (SELECT 1 FROM property.PortfolioUser WHERE PortFolioId = @PortfolioId AND UserId = @UserId AND Deleted = 0)
	BEGIN
		INSERT INTO property.PortfolioUser (PortFolioId, UserId, Deleted, CreateUserId, CreateDate, AmendUserId, AmendDate)
		VALUES		(@PortfolioId, @UserId, 0, @CurrentUserId, SYSDATETIME(), @CurrentUserId, SYSDATETIME())
	END

RETURN 0
