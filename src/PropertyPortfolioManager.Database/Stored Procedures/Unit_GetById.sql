-- =============================================
-- Author:		Dave Brown
-- Create date: 01 Sep 2023
-- Description:	Get a property unit record with address
-- =============================================
CREATE PROCEDURE [profile].[Unit_GetById]
	@Id					INT
AS
BEGIN
	SET NOCOUNT ON;

    SELECT	Id, Code, AddressId, Deleted, CreateUserId, CreateDate, AmendUserId, AmendDate
	FROM	[property].[Unit]
	WHERE	Id = @Id

    SELECT	a.Id, StreetAddress, TownCity, CountyRegion, PostCode, a.Deleted, a.CreateUserId, a.CreateDate, a.AmendUserId, a.AmendDate
	FROM	[general].[Address] a
	INNER JOIN	[property].[Unit] u ON u.AddressId = a.Id
	WHERE	u.Id = @Id

END