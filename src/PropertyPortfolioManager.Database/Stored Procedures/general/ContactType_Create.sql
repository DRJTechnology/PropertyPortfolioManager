-- ==========================================
-- Author:		Dave Brown
-- Create date: 03 Oct 2023
-- Description:	Creates a contact type record
-- ==========================================
CREATE PROCEDURE [general].[ContactType_Create]
	@Id					INT OUTPUT, 
	@PortfolioId		INT,
	@Type				NVARCHAR(255),
	@Active				BIT,
	@CurrentUserId		INT
AS
BEGIN
	SET NOCOUNT ON;

    INSERT INTO [general].[ContactType] (PortfolioId, [Type], Active, Deleted, CreateUserId, CreateDate, AmendUserId, AmendDate)
	VALUES (@PortfolioId, @Type, @Active, 0, @CurrentUserId, SYSDATETIME(), @CurrentUserId, SYSDATETIME())

	SET @Id = SCOPE_IDENTITY()

END