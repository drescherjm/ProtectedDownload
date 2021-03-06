/****** Object:  StoredProcedure [dbo].[ExpireOldRequests]    Script Date: 09/26/2012 00:11:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExpireOldRequests]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ExpireOldRequests]
GO
/****** Object:  StoredProcedure [dbo].[GetValidRequest]    Script Date: 09/26/2012 00:11:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetValidRequest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetValidRequest]
GO
/****** Object:  StoredProcedure [dbo].[InsertRequest]    Script Date: 09/26/2012 00:11:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertRequest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[InsertRequest]
GO
/****** Object:  StoredProcedure [dbo].[InsertUser]    Script Date: 09/26/2012 00:11:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[InsertUser]
GO
/****** Object:  Table [dbo].[Request]    Script Date: 09/26/2012 00:11:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Request]') AND type in (N'U'))
DROP TABLE [dbo].[Request]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 09/26/2012 00:11:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
DROP TABLE [dbo].[Users]
GO
/****** Object:  Default [DF_Request_Expired]    Script Date: 09/26/2012 00:11:36 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Request_Expired]') AND parent_object_id = OBJECT_ID(N'[dbo].[Request]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Request_Expired]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Request] DROP CONSTRAINT [DF_Request_Expired]
END


End
GO
/****** Object:  Table [dbo].[Users]    Script Date: 09/26/2012 00:11:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Users](
	[First Name] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Last Name] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Email] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DateAdded] [datetime] NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND name = N'IX_Users')
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users] ON [dbo].[Users] 
(
	[Email] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET IDENTITY_INSERT [dbo].[Users] ON
INSERT [dbo].[Users] ([First Name], [Last Name], [Email], [DateAdded], [ID]) VALUES (N'John', N'Drescher', N'drescherjm@gmail.com', CAST(0x0000A0D7011D26FD AS DateTime), 4)
INSERT [dbo].[Users] ([First Name], [Last Name], [Email], [DateAdded], [ID]) VALUES (N'John', N'Drescher', N'drescherjm@upmc.edu', CAST(0x0000A0D3011F8F25 AS DateTime), 6)
INSERT [dbo].[Users] ([First Name], [Last Name], [Email], [DateAdded], [ID]) VALUES (N'John', N'Drescher', N'drescherjm@yahoo.com', CAST(0x0000A0D301214BFA AS DateTime), 7)
SET IDENTITY_INSERT [dbo].[Users] OFF
/****** Object:  Table [dbo].[Request]    Script Date: 09/26/2012 00:11:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Request]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Request](
	[UserID] [int] NOT NULL,
	[Token] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Expired] [bit] NOT NULL
)
END
GO
INSERT [dbo].[Request] ([UserID], [Token], [Date], [Expired]) VALUES (7, N'e75120b2-8a3b-4dab-8042-81bbed6f3544', CAST(0x0000A0D3012A7694 AS DateTime), 1)
INSERT [dbo].[Request] ([UserID], [Token], [Date], [Expired]) VALUES (4, N'58447c35-43e1-4bb1-be5f-995ca9b0954f', CAST(0x0000A0D301308D01 AS DateTime), 1)
INSERT [dbo].[Request] ([UserID], [Token], [Date], [Expired]) VALUES (4, N'aeaa3b8a-bf6f-4f31-a6d1-a458512cd542', CAST(0x0000A0D600E14021 AS DateTime), 1)
INSERT [dbo].[Request] ([UserID], [Token], [Date], [Expired]) VALUES (4, N'1e181630-2ae0-4886-8a3d-0796c08387a0', CAST(0x0000A0D600E531D8 AS DateTime), 1)
INSERT [dbo].[Request] ([UserID], [Token], [Date], [Expired]) VALUES (4, N'c8467ca4-fd8c-4fc2-903e-b6c3dbb62c64', CAST(0x0000A0D600FD8CC2 AS DateTime), 1)
INSERT [dbo].[Request] ([UserID], [Token], [Date], [Expired]) VALUES (4, N'6e16901e-282b-48d7-8820-7c930c1b9bce', CAST(0x0000A0D60100E7FF AS DateTime), 1)
INSERT [dbo].[Request] ([UserID], [Token], [Date], [Expired]) VALUES (4, N'84829557-424e-4a1e-afe7-5f01bcdb4770', CAST(0x0000A0D601018776 AS DateTime), 1)
INSERT [dbo].[Request] ([UserID], [Token], [Date], [Expired]) VALUES (4, N'567732ae-3894-4b0b-897d-a4622b50a196', CAST(0x0000A0D60101B299 AS DateTime), 1)
INSERT [dbo].[Request] ([UserID], [Token], [Date], [Expired]) VALUES (4, N'78c6ac25-ca33-44b2-924e-242f5df4acec', CAST(0x0000A0D601047EA9 AS DateTime), 1)
INSERT [dbo].[Request] ([UserID], [Token], [Date], [Expired]) VALUES (4, N'1c640f02-fa11-4c4c-ab62-e312785d7ca5', CAST(0x0000A0D60104E470 AS DateTime), 1)
INSERT [dbo].[Request] ([UserID], [Token], [Date], [Expired]) VALUES (4, N'95e16428-3f9f-4520-8994-98ec692f7ab9', CAST(0x0000A0D601054AD3 AS DateTime), 1)
INSERT [dbo].[Request] ([UserID], [Token], [Date], [Expired]) VALUES (4, N'c493f9b2-8d15-4b78-afc0-92adc5e60d71', CAST(0x0000A0D601284B07 AS DateTime), 0)
INSERT [dbo].[Request] ([UserID], [Token], [Date], [Expired]) VALUES (4, N'd726db7b-4ad5-4b71-b338-a50bd084f75f', CAST(0x0000A0D60128B8B0 AS DateTime), 0)
INSERT [dbo].[Request] ([UserID], [Token], [Date], [Expired]) VALUES (4, N'3647c19b-f9fa-43d1-a92c-3926028632fc', CAST(0x0000A0D700C7DD95 AS DateTime), 0)
INSERT [dbo].[Request] ([UserID], [Token], [Date], [Expired]) VALUES (4, N'5da1983a-570b-422d-8f02-54b187aeae01', CAST(0x0000A0D7011D2708 AS DateTime), 0)
/****** Object:  StoredProcedure [dbo].[InsertUser]    Script Date: 09/26/2012 00:11:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[InsertUser]
	(
	@FirstName		nvarchar(50),
	@LastName		nvarchar(50),
	@EmailAddress	nvarchar(50),
	@ID int OUTPUT
	)
AS
    begin tran
	--SET DATEFORMAT YYYYMMDD;
	if exists (select * from Users where Email = @EmailAddress) 
	begin
		update Users
		set [First Name] = @FirstName,[Last Name] = @LastName,
			DateAdded = CURRENT_TIMESTAMP
		where Email = @EmailAddress;
		SET @ID = (select ID from Users where Email = @EmailAddress);
	end
	else
	begin
		INSERT INTO Users
		                  ([First Name], [Last Name], Email, DateAdded)
		VALUES (@FirstName,@LastName,@EmailAddress,CURRENT_TIMESTAMP);
		SET @ID = @@IDENTITY
	end
	commit tran
	

' 
END
GO
/****** Object:  StoredProcedure [dbo].[InsertRequest]    Script Date: 09/26/2012 00:11:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertRequest]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[InsertRequest]
	(
	@UserID			int,
	@Token			uniqueidentifier
	)
AS
    begin tran
		INSERT INTO Request
		                  (UserID, Token, [Date])
		VALUES (@UserID,@Token,CURRENT_TIMESTAMP);
	commit tran' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetValidRequest]    Script Date: 09/26/2012 00:11:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetValidRequest]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetValidRequest]
	(
	@Token uniqueidentifier,
	@UserID int OUTPUT
	)
AS
	SET NOCOUNT ON;

	SET @UserID = -1

	SELECT @UserID = UserID 
	FROM Request 
	WHERE Expired  = 0 AND Token = @Token;
	RETURN
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ExpireOldRequests]    Script Date: 09/26/2012 00:11:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExpireOldRequests]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ExpireOldRequests]
	(
	@hours int = 24
	)
AS
    Declare @CurrDate DATETIME
	SET @CurrDate = GetDate()

	UPDATE Request
	SET Expired = 1
	WHERE  [Date] < DATEADD(hh,-@hours,@CurrDate); 
	 
	RETURN
' 
END
GO
/****** Object:  Default [DF_Request_Expired]    Script Date: 09/26/2012 00:11:36 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Request_Expired]') AND parent_object_id = OBJECT_ID(N'[dbo].[Request]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Request_Expired]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Request] ADD  CONSTRAINT [DF_Request_Expired]  DEFAULT ((0)) FOR [Expired]
END


End
GO
