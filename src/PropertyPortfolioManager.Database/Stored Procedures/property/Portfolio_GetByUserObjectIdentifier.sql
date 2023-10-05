-- ===========================================================
-- Author:		Dave Brown
-- Create date: 01 Oct 2023
-- Description:	Get current property portfolio record for user
-- ===========================================================
CREATE PROCEDURE [property].[Portfolio_GetByUserObjectIdentifier]
	@ObjectIdentifier uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    SELECT	p.Id, p.[Name], p.Active, p.Deleted, p.CreateUserId, p.CreateDate, p.AmendUserId, p.AmendDate
	FROM	property.Portfolio p
	INNER JOIN	[profile].[User] u ON p.Id = u.SelectedPortfolioId
	WHERE	u.ObjectIdentifier = @ObjectIdentifier
		AND p.Deleted = 0
END