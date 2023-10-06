-- ====================================================
-- Author:		Dave Brown
-- Create date: 05 Oct 2023
-- Description:	Get a contact record with address
-- ====================================================
CREATE PROCEDURE [general].[Contact_GetById]
	@Id					INT,
	@PortfolioId		INT
AS
BEGIN
	SET NOCOUNT ON;

    SELECT	c.Id, c.PortfolioId, c.ContactTypeId, ct.[Type] AS ContactType, c.[Name], c.AddressId, c.Notes, c.Active, c.Deleted, c.CreateUserId, c.CreateDate, c.AmendUserId, c.AmendDate
	FROM	[general].[Contact] c
	INNER JOIN	[general].[ContactType] ct on c.ContactTypeId = ct.id
	WHERE	c.PortfolioId = @PortfolioId AND c.Id = @Id

    SELECT	a.Id, StreetAddress, TownCity, CountyRegion, PostCode, a.Deleted, a.CreateUserId, a.CreateDate, a.AmendUserId, a.AmendDate
	FROM	[general].[Address] a
	INNER JOIN	[general].[Contact] c ON c.AddressId = a.Id
	WHERE	c.PortfolioId = @PortfolioId AND c.Id = @Id

END