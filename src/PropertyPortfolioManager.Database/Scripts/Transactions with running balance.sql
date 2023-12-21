
DECLARE
	@PortfolioId		int,
	@FromDate			datetime = null,
	@ToDate				datetime = null,
	@AccountId			int = null,
	@TransactionTypeId	int = null

SET @PortfolioId		= 2
SET @FromDate			= '01 Sep 2023'
SET @ToDate				= '01 Oct 2023'
SET @AccountId			= 1020

DECLARE @StartingBalance MONEY
IF (@FromDate IS NOT NULL)
BEGIN
    SELECT @StartingBalance = SUM(amount * direction) 
        FROM	finance.TransactionDetail td
        INNER JOIN	finance.[Transaction] t ON td.TransactionId = t.Id
        INNER JOIN	finance.[TransactionType] tt ON t.TransactionTypeId = tt.Id
        INNER JOIN	finance.Account a on td.AccountId = a.Id
        WHERE	t.PortfolioId = @PortfolioId
            AND (td.[Date] < @FromDate)
            AND (ISNULL(@AccountId, 0) = 0 OR td.AccountId = @AccountId)
            AND (ISNULL(@TransactionTypeId, 0) = 0 OR t.TransactionTypeId = @TransactionTypeId)
END


SELECT @StartingBalance AS StartingBalance

	SELECT	td.Id,
			td.TransactionId,
			-- tt.[Type],
			td.[Date], 
			t.Reference,
			-- td.AccountId, 
			-- a.[Name] AS Account, 
			td.[Description],
			CASE WHEN direction = 1 THEN td.amount ELSE 0 END AS Debit, 
			CASE WHEN direction = -1 THEN td.amount ELSE 0 END AS Credit
             , (SUM(td.amount * td.direction) OVER (ORDER BY td.[Date]) + @StartingBalance) * -1 AS running_total
	FROM	finance.TransactionDetail td
	INNER JOIN	finance.[Transaction] t ON td.TransactionId = t.Id
	-- INNER JOIN	finance.[TransactionType] tt ON t.TransactionTypeId = tt.Id
	-- INNER JOIN	finance.Account a on td.AccountId = a.Id
	WHERE	t.PortfolioId = @PortfolioId
		AND	(@FromDate IS NULL OR td.[Date] >= @FromDate)
		AND (@ToDate IS NULL OR td.[Date] < DATEADD(DAY, 1, @ToDate))
		AND (ISNULL(@AccountId, 0) = 0 OR td.AccountId = @AccountId)
		AND (ISNULL(@TransactionTypeId, 0) = 0 OR t.TransactionTypeId = @TransactionTypeId)
	ORDER BY	td.[Date] --DESC--, td.TransactionId, a.[Name], Credit, Debit

