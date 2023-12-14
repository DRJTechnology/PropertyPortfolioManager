CREATE procedure [dbo].[InsertAppLog] 
(
	@Addeddate datetime,
	@Level nvarchar(50),
	@Message nvarchar(max),
	@StackTrace nvarchar(max),
	@Logger nvarchar(255),
	@Exception nvarchar(max),
	@RequestUrl nvarchar(255),
	@RequestType nvarchar(10)
)
as

INSERT INTO [dbo].[AppLog]
           ([AddedDate]
           ,[Level]
           ,[Message]
           ,[StackTrace]
           ,[Logger]
           ,[Exception]
           ,[RequestUrl]
		   ,[RequestType])
VALUES
(	
	@Addeddate,
	@Level,
	@Message,
	@StackTrace,
	@Logger,
	@Exception,
	@RequestUrl,
	@RequestType
)
GO

