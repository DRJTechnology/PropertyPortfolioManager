-- =========================================
-- Author:		Dave Brown
-- Create date: 03 Oct 2023
-- Description:	Get all contact type records
-- =========================================
CREATE PROCEDURE [general].[ContactType_GetAll]
	@PortfolioId	INT,
	@ActiveOnly		BIT = 1
AS
BEGIN
	SET NOCOUNT ON;

    SELECT	Id, PortfolioId, [Type], Active
	FROM	[general].[ContactType]
	Where	(PortfolioId = @PortfolioId OR PortfolioId = -1)
		AND Deleted = 0
		AND (@ActiveOnly != 1 OR Active = 1)
	ORDER BY [Type]

END