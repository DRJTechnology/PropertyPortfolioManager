-- ================================================
-- Author:		Dave Brown
-- Create date: 15 Nov 2023
-- Description:	Updates a tenancy record
-- ================================================
CREATE PROCEDURE [property].[Tenancy_Update]
	@Id					INT OUTPUT, 
	@PortfolioId		INT,
	@TenancyTypeId		INT,
	@UnitId				INT,
	@StartDate			DATETIME,
	@EndDate			DATETIME,
	@DurationQuantity	SMALLINT,
	@DurationUnitId		SMALLINT,
	@ExpireAfterEndDate	BIT,
	@Active				BIT,
	@CurrentUserId		INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE	t
	SET		TenancyTypeId = @TenancyTypeId,
			UnitId = @UnitId,
			StartDate = @StartDate,
			EndDate = @EndDate,
			DurationQuantity = @DurationQuantity,
			DurationUnitId = @DurationUnitId,
			ExpireAfterEndDate = @ExpireAfterEndDate,
			Active = @Active,
			AmendUserId = @CurrentUserId,
			AmendDate = SYSDATETIME()
	FROM	[property].[Tenancy] t
	INNER JOIN [property].[Unit] u on t.UnitId = u.Id
	WHERE	u.PortfolioId = @PortfolioId
		AND t.Id = @Id

END