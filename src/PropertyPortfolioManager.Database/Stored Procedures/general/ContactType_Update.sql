-- ================================================
-- Author:		Dave Brown
-- Create date: 03 Oct 2023
-- Description:	Updates a contact type record
-- ================================================
CREATE PROCEDURE [general].[ContactType_Update]
	@Id					INT, 
	@PortfolioId		INT,
	@Type				NVARCHAR(255),
	@Active				BIT,
	@CurrentUserId		INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE	[general].[ContactType]
	SET		[Type] = @Type,
			AmendUserId = @CurrentUserId,
			Active = @Active,
			AmendDate = SYSDATETIME()
	WHERE	PortfolioId = @PortfolioId AND Id = @Id

END