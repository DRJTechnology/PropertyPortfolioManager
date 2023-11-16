-- ================================================
-- Author:		Dave Brown
-- Create date: 15 Nov 2023
-- Description:	Creates a tenancy record
-- ================================================
CREATE PROCEDURE [property].[Tenancy_Create]
	@Id					INT OUTPUT, 
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

    INSERT INTO [property].[Tenancy] (TenancyTypeId, UnitId, StartDate, EndDate, DurationQuantity, DurationUnitId, ExpireAfterEndDate, Active, Deleted, CreateUserId, CreateDate, AmendUserId, AmendDate)
	VALUES (@TenancyTypeId, @UnitId, @StartDate, @EndDate, @DurationQuantity, @DurationUnitId, @ExpireAfterEndDate, @Active, 0, @CurrentUserId, SYSDATETIME(), @CurrentUserId, SYSDATETIME())

	SET @Id = SCOPE_IDENTITY()

END