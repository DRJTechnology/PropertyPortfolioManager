-- ==========================================================
-- Author:		Dave Brown
-- Create date: 12 Dec 2023
-- Description:	Creates an account record
-- ==========================================================
CREATE PROCEDURE [finance].[BankStatement_Upload]
	@AccountId      INT,
	@PortfolioId    INT,
  @Statement finance.StatementTableType READONLY,
  @CurrentUserId	INT
AS
	SET NOCOUNT ON;

	DECLARE @CurrentPortflioId INT

	SELECT	@CurrentPortflioId = SelectedPortfolioId
	FROM	profile.[User]
	Where	Id = @CurrentUserId

	IF	@CurrentPortflioId != @PortfolioId
	BEGIN
	   RAISERROR ('Invalid Portfolio for user.' , 16, 1) WITH NOWAIT  
	   RETURN
	END

  DECLARE @TotalRowCount  INT
  DECLARE @InsertedRowCount  INT

  SELECT @TotalRowCount = COUNT(1) FROM @Statement

  DECLARE @UploadId UNIQUEIDENTIFIER
  SET @UploadId = NEWID()

  INSERT INTO [finance].[BankAccountDetail] (AccountId, UploadId, Date, Amount, [Description], TransactionType, Deleted, CreateUserId, CreateDate, AmendUserId, AmendDate)
  SELECT  @AccountId, @UploadId, s.Date, s.Amount, REPLACE(s.Description, CHAR(9), ''), s.TransactionType, 0, @CurrentUserId, SYSDATETIME(), @CurrentUserId, SYSDATETIME()
  FROM        @Statement s
  LEFT OUTER JOIN  [finance].[BankAccountDetail] bad ON bad.AccountId = @AccountId AND s.Amount = bad.Amount AND s.[Date] = bad.[Date] AND REPLACE(s.Description, CHAR(9), '') = REPLACE(bad.Description, CHAR(9), '') AND s.TransactionType = bad.TransactionType
  WHERE bad.Id IS NULL

  SELECT @InsertedRowCount = @@ROWCOUNT

  SELECT  @TotalRowCount AS TotalRowCount, @InsertedRowCount AS InsertedRowCount

RETURN 0

