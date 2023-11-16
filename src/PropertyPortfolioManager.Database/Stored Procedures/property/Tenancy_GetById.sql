-- ============================================
-- Author:		Dave Brown
-- Create date: 15 Nov 2023
-- Description:	Get a tenancy record
-- ============================================
CREATE PROCEDURE [property].[Tenancy_GetById]
	@Id				INT,
	@PortfolioId	INT
AS
BEGIN
	SET NOCOUNT ON;

    SELECT	t.TenancyTypeId, tt.[Type], t.UnitId, a.StreetAddress, t.StartDate, t.EndDate, t.DurationQuantity, t.DurationUnitId, t.ExpireAfterEndDate, t.Active
	FROM	[property].[Tenancy] t
	INNER JOIN [property].[TenancyType] tt ON t.TenancyTypeId = tt.Id
	INNER JOIN [property].[Unit] u on t.UnitId = u.Id
	INNER JOIN [general].[Address] a on u.AddressId = a.Id
	Where	u.PortfolioId = @PortfolioId
		AND t.Id = @Id
		AND t.Deleted = 0
		AND u.Deleted = 0

END