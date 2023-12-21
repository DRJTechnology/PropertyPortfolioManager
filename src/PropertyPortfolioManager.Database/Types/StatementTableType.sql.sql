-- ==========================================================
-- Author:		Dave Brown
-- Create date: 12 Dec 2023
-- Description:	Creates a Statement Table Type
-- ==========================================================
CREATE TYPE finance.StatementTableType AS TABLE (
  [Date]              DATETIME NULL, 
  [Amount]            MONEY NULL,
  [Description]       NVARCHAR(512) NULL,
  [TransactionType]   NVARCHAR(512) NULL
);
