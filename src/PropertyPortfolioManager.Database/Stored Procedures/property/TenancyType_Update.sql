-- ================================================
-- Author:		Dave Brown
-- Create date: 14 Nov 2023
-- Description:	Updates a tenancy type record
-- ================================================
CREATE PROCEDURE [property].[TenancyType_Update]
	@Id					INT, 
	@PortfolioId		INT,
	@Type				NVARCHAR(255),
	@Active				BIT,
	@CurrentUserId		INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE	[property].[TenancyType]
	SET		[Type] = @Type,
			Active = @Active,
			AmendUserId = @CurrentUserId,
			AmendDate = SYSDATETIME()
	WHERE	PortfolioId = @PortfolioId AND Id = @Id

END