-- =============================================
-- Author:		Dave Brown
-- Create date: 05 Dec 2023
-- Description:	Get all account records
-- =============================================
CREATE PROCEDURE [finance].[Account_GetAll]
	@PortfolioId	INT,
	@ActiveOnly		BIT
AS
BEGIN
	SET NOCOUNT ON;

    SELECT	a.Id, a.PortfolioId, AccountTypeId, [Name], act.[Type], a.Notes, a.Active, a.Deleted, a.CreateUserId, a.CreateDate, a.AmendUserId, a.AmendDate
	FROM	[finance].[Account] a
	INNER JOIN [finance].[AccountType] act ON a.AccountTypeId = act.Id
	Where	(a.PortfolioId = @PortfolioId OR a.PortfolioId = -1)
		AND a.Deleted = 0
		AND (@ActiveOnly != 1 OR a.Active = 1)

END