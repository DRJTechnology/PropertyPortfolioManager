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

    SELECT	c.Id, c.PortfolioId, [Name], StreetAddress, c.Active
	FROM	[general].[Contact] c
	INNER JOIN	[general].[Address] a ON c.AddressId = a.Id
	Where	c.PortfolioId = @PortfolioId
		AND c.Deleted = 0
		AND (@ActiveOnly != 1 OR c.Active = 1)

END