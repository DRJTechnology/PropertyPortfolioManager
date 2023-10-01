-- =============================================
-- Author:		Dave Brown
-- Create date: 03 Sep 2023
-- Description:	Get all portfolios for user
-- =============================================
CREATE PROCEDURE [property].[Portfolio_GetAll]
	@UserId		INT
AS
BEGIN
	SET NOCOUNT ON;

    SELECT	p.Id, p.[Name]
	FROM	[property].[Portfolio] p
	INNER Join	[property].[PortfolioUser] pu ON p.Id = pu.PortFolioId AND pu.Deleted = 0
	Where	p.Deleted = 0
		AND pu.UserId = @UserId

END