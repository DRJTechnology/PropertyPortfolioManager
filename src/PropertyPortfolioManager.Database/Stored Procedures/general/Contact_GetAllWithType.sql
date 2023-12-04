-- =============================================
-- Author:		Dave Brown
-- Create date: 30 Nov 2023
-- Description:	Get all contact records
--				with a specific type prioritised
-- =============================================
CREATE PROCEDURE [general].[Contact_GetAllWithType]
	@PortfolioId	INT,
	@ContactTypeId	INT
AS
BEGIN
	SET NOCOUNT ON;

    SELECT	c.Id, c.PortfolioId, [Name], StreetAddress, CASE WHEN cct.Id IS NULL THEN 0 ELSE 1 END AS MatchingType
	FROM	[general].[Contact] c
	INNER JOIN	[general].[Address] a ON c.AddressId = a.Id
	LEFT OUTER JOIN	[general].[ContactContactType] cct ON c.Id = cct.ContactId AND cct.ContactTypeId = @ContactTypeId AND cct.Deleted = 0
	Where	c.PortfolioId = @PortfolioId
		AND c.Active = 1
		AND c.Deleted = 0
	ORDER By MatchingType DESC, [Name]

END