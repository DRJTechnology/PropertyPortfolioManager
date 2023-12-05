-- =========================================
-- Author:		Dave Brown
-- Create date: 05 Dec 2023
-- Description:	Get all account type records
-- =========================================
CREATE PROCEDURE [finance].[AccountType_GetAll]
AS
BEGIN
	SET NOCOUNT ON;

    SELECT	Id, [Type]
	FROM	[finance].[AccountType]
	Where	Deleted = 0
	ORDER BY [Type]

END