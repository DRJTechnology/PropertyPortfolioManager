-- ============================================
-- Author:		Dave Brown
-- Create date: 01 Oct 2023
-- Description:	Get a property portfolio record
-- ============================================
CREATE PROCEDURE [property].[Portfolio_GetById]
	@Id			INT,
	@UserId		INT
AS
BEGIN
	SET NOCOUNT ON;

    SELECT	p.Id, p.[Name], p.Active, p.Deleted, p.CreateUserId, p.CreateDate, p.AmendUserId, p.AmendDate
	FROM	property.Portfolio p
	INNER JOIN	property.PortfolioUser pu ON p.id = pu.PortFolioId AND pu.Deleted = 0
	WHERE	p.Id = @Id
		AND pu.UserId = @UserId
		AND p.Deleted = 0
END