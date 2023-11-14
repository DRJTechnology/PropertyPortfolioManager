-- =============================================
-- Author:		Dave Brown
-- Create date: 14 Nov 2023
-- Description:	Get all tenancy type records
-- =============================================
CREATE PROCEDURE [property].[TenancyType_GetAll]
	@PortfolioId	INT,
	@ActiveOnly		BIT = 1
AS
BEGIN
	SET NOCOUNT ON;

    SELECT	Id, [Type], Active
	FROM	[property].[TenancyType]
	Where	PortfolioId = @PortfolioId
		AND Deleted = 0
		AND (@ActiveOnly != 1 OR Active = 1)
	ORDER BY [Type]

END