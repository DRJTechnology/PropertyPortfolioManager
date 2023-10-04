-- =========================================
-- Author:		Dave Brown
-- Create date: 03 Oct 2023
-- Description:	Get all contact type records
-- =========================================
CREATE PROCEDURE [general].[ContactType_GetAll]
	@PortfolioId	INT
AS
BEGIN
	SET NOCOUNT ON;

    SELECT	Id, PortfolioId, [Type]
	FROM	[general].[ContactType]
	Where	(PortfolioId = @PortfolioId OR PortfolioId = -1)
		AND Deleted = 0
	ORDER BY [Type]

END