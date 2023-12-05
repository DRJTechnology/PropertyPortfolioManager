-- ==========================================================
-- Author:		Dave Brown
-- Create date: 05 Dec 2023
-- Description:	Creates an account record
-- ==========================================================
CREATE PROCEDURE [finance].[Account_Create]
	@Id					INT OUTPUT, 
	@PortfolioId		INT,
	@AccountTypeId		INT,
	@Name				NVARCHAR(255),
	@Notes				NVARCHAR(4000) = NULL,
	@Active				BIT,
	@CurrentUserId		INT
AS
BEGIN
	SET NOCOUNT ON;

    INSERT INTO [finance].[Account] (AccountTypeId, PortfolioId, [Name], Notes, Active, Deleted, CreateUserId, CreateDate, AmendUserId, AmendDate)
	VALUES (@AccountTypeId, @PortfolioId, @Name, @Notes, @Active, 0, @CurrentUserId, SYSDATETIME(), @CurrentUserId, SYSDATETIME())

	SET @Id = SCOPE_IDENTITY()

END
