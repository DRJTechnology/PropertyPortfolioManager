-- ================================================
-- Author:		Dave Brown
-- Create date: 30 Oct 2023
-- Description:	Creates a property portfolio record
-- ================================================
CREATE PROCEDURE [property].[Portfolio_Create]
	@Id					INT OUTPUT, 
	@Name				NVARCHAR(255),
	@CurrentUserId		INT
AS
BEGIN
	SET NOCOUNT ON;

    INSERT INTO [property].[Portfolio] ([Name], Deleted, CreateUserId, CreateDate, AmendUserId, AmendDate)
	VALUES (@Name, 0, @CurrentUserId, SYSDATETIME(), @CurrentUserId, SYSDATETIME())

	SET @Id = SCOPE_IDENTITY()

	INSERT INTO property.PortfolioUser (PortFolioId, UserId, Deleted, CreateUserId, CreateDate, AmendUserId, AmendDate)
	VALUES (@Id, @CurrentUserId, 0, @CurrentUserId, SYSDATETIME(), @CurrentUserId, SYSDATETIME())

	EXEC [property].[Portfolio_Initialise] @PortfolioId = @Id, @CurrentUserId = @CurrentUserId
END