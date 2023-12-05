-- ====================================================
-- Author:		Dave Brown
-- Create date: 05 Dec 2023
-- Description:	Get an account record
-- ====================================================
CREATE PROCEDURE [finance].[Account_GetById]
	@Id					INT,
	@PortfolioId		INT
AS
BEGIN
	SET NOCOUNT ON;

    SELECT	a.Id, a.PortfolioId, AccountTypeId, [Name], act.[Type], a.Notes, a.Active, a.Deleted, a.CreateUserId, a.CreateDate, a.AmendUserId, a.AmendDate
	FROM	[finance].[Account] a
	INNER JOIN [finance].[AccountType] act ON a.AccountTypeId = act.Id
	WHERE	a.PortfolioId = @PortfolioId AND a.Id = @Id

END