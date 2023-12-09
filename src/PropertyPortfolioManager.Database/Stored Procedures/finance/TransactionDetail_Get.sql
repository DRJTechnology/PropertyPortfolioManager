-- =========================================
-- Author:		Dave Brown
-- Create date: 07 Dec 2023
-- Description:	Get transaction records
-- =========================================
CREATE PROCEDURE [finance].[TransactionDetail_Get]
	@PortfolioId		int,
	@FromDate			datetime = null,
	@ToDate				datetime = null,
	@AccountId			int = null,
	@TransactionTypeId	int = null
AS

	SELECT	td.Id,
			td.TransactionId,
			tt.[Type],
			td.[Date], 
			t.Reference,
			td.AccountId, 
			a.[Name] AS Account, 
			td.[Description],
			CASE WHEN direction = 1 THEN amount ELSE 0 END AS Debit, 
			CASE WHEN direction = -1 THEN amount ELSE 0 END AS Credit
	FROM	finance.TransactionDetail td
	INNER JOIN	finance.[Transaction] t ON td.TransactionId = t.Id
	INNER JOIN	finance.[TransactionType] tt ON t.TransactionTypeId = tt.Id
	INNER JOIN	finance.Account a on td.AccountId = a.Id
	WHERE	t.PortfolioId = @PortfolioId
		AND	(@FromDate IS NULL OR td.[Date] >= @FromDate)
		AND (@ToDate IS NULL OR td.[Date] < DATEADD(DAY, 1, @ToDate))
		AND (ISNULL(@AccountId, 0) = 0 OR td.AccountId = @AccountId)
		AND (ISNULL(@TransactionTypeId, 0) = 0 OR t.TransactionTypeId = @TransactionTypeId)
	ORDER BY	td.[Date], td.TransactionId, a.[Name], Credit, Debit

RETURN 0
