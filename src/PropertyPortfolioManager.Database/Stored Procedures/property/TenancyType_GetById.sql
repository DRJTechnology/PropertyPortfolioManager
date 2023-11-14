-- ============================================
-- Author:		Dave Brown
-- Create date: 14 Nov 2023
-- Description:	Get a tenancy type record
-- ============================================
CREATE PROCEDURE [property].[TenancyType_GetById]
	@Id				INT,
	@PortfolioId	INT
AS
BEGIN
	SET NOCOUNT ON;

    SELECT	Id, [Type], Active, Deleted, CreateUserId, CreateDate, AmendUserId, AmendDate
	FROM	[property].[TenancyType]
	WHERE	PortfolioId = @PortfolioId AND Id = @Id

END