-- ================================================
-- Author:		Dave Brown
-- Create date: 30 Oct 2023
-- Description:	Creates a property portfolio record
-- ================================================
CREATE PROCEDURE [property].[Portfolio_Create]
	@Id					INT OUTPUT, 
	@Name				NVARCHAR(255),
	@Active				BIT,
	@CurrentUserId		INT
AS
BEGIN
	SET NOCOUNT ON;

    INSERT INTO [property].[Portfolio] ([Name], Active, Deleted, CreateUserId, CreateDate, AmendUserId, AmendDate)
	VALUES (@Name, @Active, 0, @CurrentUserId, SYSDATETIME(), @CurrentUserId, SYSDATETIME())

	SET @Id = SCOPE_IDENTITY()

	INSERT INTO property.PortfolioUser (PortfolioId, UserId, Deleted, CreateUserId, CreateDate, AmendUserId, AmendDate)
	VALUES (@Id, @CurrentUserId, 0, @CurrentUserId, SYSDATETIME(), @CurrentUserId, SYSDATETIME())

	EXEC [property].[Portfolio_Initialise] @PortfolioId = @Id, @CurrentUserId = @CurrentUserId
END