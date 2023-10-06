-- =============================================
-- Author:		Dave Brown
-- Create date: 05 Oct 2023
-- Description:	Get all contact records
-- =============================================
CREATE PROCEDURE [general].[Contact_GetAll]
	@PortfolioId	INT,
	@ActiveOnly		BIT
AS
BEGIN
	SET NOCOUNT ON;

    SELECT	c.Id, c.PortfolioId, ct.[Type] AS ContactType, [Name], StreetAddress, c.Active
	FROM	[general].[Contact] c
	INNER JOIN	[general].[Address] a ON c.AddressId = a.Id
	INNER Join	[general].[ContactType] ct ON c.ContactTypeId = ct.Id
	Where	c.PortfolioId = @PortfolioId
		AND c.Deleted = 0
		AND (@ActiveOnly != 1 OR c.Active = 1)

END