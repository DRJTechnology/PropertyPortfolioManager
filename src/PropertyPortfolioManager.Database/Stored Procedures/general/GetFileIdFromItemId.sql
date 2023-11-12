-- =================================================
-- Author:		Dave Brown
-- Create date: 09 Nov 2023
-- Description:	Gets the file ID from the SharePoint
--				driveItemId. The File record is created if it does not exist
-- =================================================
CREATE PROCEDURE [general].[GetFileIdFromItemId]
	@ItemId	VARCHAR(4000),
	@FileName NVARCHAR(500),
	@Size BIGINT,
	@UserId INT,
	@FileID INT OUTPUT
AS
	DECLARE @OriginalFileName NVARCHAR(500)
	DECLARE @OriginalSize BIGINT

	SET @FileId = NULL

	SELECT	@FileId =  Id,
			@OriginalFileName = [FileName],
			@OriginalSize = Size
	FROM	general.[File]
	WHERE	ItemId = @ItemId
		AND Deleted = 0

	IF (@FileId IS NULL)
	BEGIN
		INSERT INTO general.[File] (ItemId, [FileName], Size, [Deleted], [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
		VALUES (@ItemId, @FileName, @Size, 0, @UserId, SYSDATETIME(), @UserId, SYSDATETIME())
		SET @FileId = SCOPE_IDENTITY()
	END
	ELSE
	BEGIN
		IF @FileName != @OriginalFileName OR @Size != @OriginalSize
		BEGIN
			UPDATE general.[File]
			SET	[FileName] = @FileName,
				Size = @Size,
				AmendUserId = @UserId,
				AmendDate = SYSDATETIME()
			WHERE Id = @FileId
		END
	END

RETURN 0
