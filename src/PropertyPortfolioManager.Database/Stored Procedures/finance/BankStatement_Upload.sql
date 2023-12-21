-- ==========================================================
-- Author:		Dave Brown
-- Create date: 12 Dec 2023
-- Description:	Creates an account record
-- ==========================================================
CREATE PROCEDURE [finance].[BankStatement_Upload]
	@AccountId      INT,
  @Statement finance.StatementTableType READONLY,
  @CurrentUserId	INT
AS
  INSERT INTO [finance].[BankAccountDetail] (AccountId, Date, Amount, Description, TransactionType, Deleted, CreateUserId, CreateDate, AmendUserId, AmendDate)
  SELECT  @AccountId, Date, Amount, Description, TransactionType, 0, @CurrentUserId, SYSDATETIME(), @CurrentUserId, SYSDATETIME()
  FROM  @Statement

RETURN 0

