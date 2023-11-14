-- ================================================
-- Author:		Dave Brown
-- Create date: 14 Nov 2023
-- Description:	Creates a tenancy type record
-- ================================================
CREATE PROCEDURE [property].[TenancyType_Create]
	@Id					INT OUTPUT, 
	@PortfolioId		INT,
	@Type				NVARCHAR(255),
	@Active				BIT,
	@CurrentUserId		INT
AS
BEGIN
	SET NOCOUNT ON;

    INSERT INTO [property].[TenancyType] (PortfolioId, [Type], Active, Deleted, CreateUserId, CreateDate, AmendUserId, AmendDate)
	VALUES (@PortfolioId, @Type, @Active, 0, @CurrentUserId, SYSDATETIME(), @CurrentUserId, SYSDATETIME())

	SET @Id = SCOPE_IDENTITY()

END