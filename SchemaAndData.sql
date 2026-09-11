/****** Object:  Table [dbo].[APP_CurrentEmployees]    Script Date: 8/21/2026 7:25:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[APP_CurrentEmployees](
	[Emp_Id] [smallint] NOT NULL,
	[Emp_PreferredName] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[Emp_Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DART_Applications]    Script Date: 8/21/2026 7:25:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DART_Applications](
	[App_Id] [int] IDENTITY(1,1) NOT NULL,
	[App_Name] [varchar](max) NOT NULL,
	[App_CurrentVersion] [decimal](6, 4) NOT NULL,
	[App_Description] [varchar](max) NULL,
	[App_Type] [int] NOT NULL,
	[App_CriticalityId] [int] NOT NULL,
	[App_PrimDeveloper_EmpId] [smallint] NOT NULL,
	[App_SecondaryDeveloper_EmpId] [smallint] NULL,
	[App_Analyst_EmpId] [smallint] NULL,
	[App_SDLC_Phase_Id] [smallint] NOT NULL,
	[App_SDLC_CheckDate] [datetime] NOT NULL,
	[Create_Date] [datetime] NOT NULL,
	[Create_By] [smallint] NOT NULL,
	[Update_Date] [datetime] NULL,
	[Update_By] [smallint] NULL,
	[App_FriendlyName] [varchar](max) NOT NULL,
	[App_AllowFeedback] [bit] NOT NULL,
 CONSTRAINT [PK_DART_Applications] PRIMARY KEY CLUSTERED 
(
	[App_Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DART_AppTypes]    Script Date: 8/21/2026 7:25:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DART_AppTypes](
	[Type_Id] [int] IDENTITY(1,1) NOT NULL,
	[Type_Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_DART_AppTypes] PRIMARY KEY CLUSTERED 
(
	[Type_Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DART_Criticality]    Script Date: 8/21/2026 7:25:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DART_Criticality](
	[Crit_Id] [int] IDENTITY(1,1) NOT NULL,
	[Crit_Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_DART_Criticality] PRIMARY KEY CLUSTERED 
(
	[Crit_Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DART_SDLC_Phase]    Script Date: 8/21/2026 7:25:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DART_SDLC_Phase](
	[SDLC_Id] [smallint] IDENTITY(1,1) NOT NULL,
	[SDLC_Name] [varchar](150) NOT NULL,
 CONSTRAINT [PK_DART_SDLCphase] PRIMARY KEY CLUSTERED 
(
	[SDLC_Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_DART_ApplicationList]    Script Date: 8/21/2026 7:25:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_DART_ApplicationList]
AS
SELECT
    a.App_Id              AS AppId,
    a.App_Name            AS ApplicationName,
    c.Crit_Name           AS Criticality,
    t.Type_Name           AS AppType,
    pd.Emp_PreferredName  AS PrimaryDeveloper,
    sd.Emp_PreferredName  AS SecondaryDeveloper,
    an.Emp_PreferredName  AS Analyst,
    s.SDLC_Name           AS SdlcPhase,
    CAST(a.App_SDLC_CheckDate AS datetime2) AS SdlcCheckDate
FROM dbo.DART_Applications a
INNER JOIN dbo.DART_Criticality c
    ON a.App_CriticalityId = c.Crit_Id
INNER JOIN dbo.DART_AppTypes t
    ON a.App_Type = t.Type_Id
INNER JOIN dbo.DART_SDLC_Phase s
    ON a.App_SDLC_Phase_Id = s.SDLC_Id
INNER JOIN dbo.APP_CurrentEmployees pd
    ON a.App_PrimDeveloper_EmpId = pd.Emp_Id
LEFT JOIN dbo.APP_CurrentEmployees sd
    ON a.App_SecondaryDeveloper_EmpId = sd.Emp_Id
LEFT JOIN dbo.APP_CurrentEmployees an
    ON a.App_Analyst_EmpId = an.Emp_Id;
GO
/****** Object:  Table [dbo].[DART_Paths]    Script Date: 8/21/2026 7:25:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DART_Paths](
	[Path_Id] [int] IDENTITY(1,1) NOT NULL,
	[Path_App_Id] [int] NOT NULL,
	[Path_Type_Id] [int] NOT NULL,
	[Path_Location] [varchar](max) NOT NULL,
	[Create_Date] [datetime] NOT NULL,
	[Create_By] [smallint] NOT NULL,
	[Update_Date] [datetime] NULL,
	[Update_By] [smallint] NULL,
 CONSTRAINT [PK_DART_Paths] PRIMARY KEY CLUSTERED 
(
	[Path_Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DART_PathTypes]    Script Date: 8/21/2026 7:25:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DART_PathTypes](
	[PathType_Id] [int] IDENTITY(1,1) NOT NULL,
	[PathType_Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_DART_PathTypes] PRIMARY KEY CLUSTERED 
(
	[PathType_Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_DART_PathList]    Script Date: 8/21/2026 7:25:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_DART_PathList] AS
SELECT
    p.Path_Id       AS PathId,
    p.Path_App_Id   AS ApplicationId,
    p.Path_Type_Id  AS PathTypeId,
    pt.PathType_Name AS PathType_Name,
    p.Path_Location AS Path_Location
FROM DART_Paths p
INNER JOIN DART_Applications a ON a.App_Id = p.Path_App_Id
INNER JOIN DART_PathTypes pt ON pt.PathType_Id = p.Path_Type_Id
GO
/****** Object:  Table [dbo].[DART_Dependencies]    Script Date: 8/21/2026 7:25:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DART_Dependencies](
	[Depend_App_Id] [int] NOT NULL,
	[Depend_On_Id] [int] NOT NULL,
 CONSTRAINT [PK_DART_Dependencies] PRIMARY KEY CLUSTERED 
(
	[Depend_App_Id] ASC,
	[Depend_On_Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_DART_DependencyList]    Script Date: 8/21/2026 7:25:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_DART_DependencyList]
AS
SELECT
    d.Depend_App_Id    AS DependAppId,
    d.Depend_On_Id     AS DependOnAppId,
    a.App_Name         AS DependOnAppName
FROM dbo.DART_Dependencies d
INNER JOIN dbo.DART_Applications a
    ON d.Depend_On_Id = a.App_Id;
GO
/****** Object:  Table [dbo].[DART_Feedback]    Script Date: 8/21/2026 7:25:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DART_Feedback](
	[FB_Id] [int] IDENTITY(1,1) NOT NULL,
	[FB_App_Id] [int] NOT NULL,
	[FB_Description] [varchar](max) NOT NULL,
	[FB_isActive] [bit] NOT NULL,
	[FB_FollowupComplete] [bit] NULL,
	[FB_WasImplemented] [bit] NULL,
	[FB_ImplementedInVersion] [varchar](50) NULL,
	[Create_Date] [datetime] NOT NULL,
	[Create_By] [smallint] NOT NULL,
	[Update_Date] [datetime] NULL,
	[Update_By] [smallint] NULL,
	[FB_ImplementedComments] [varchar](max) NULL,
 CONSTRAINT [PK_DART_Feedback] PRIMARY KEY CLUSTERED 
(
	[FB_Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DART_FeedbackFiles]    Script Date: 8/21/2026 7:25:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DART_FeedbackFiles](
	[FF_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FF_DartFeedbackId] [int] NOT NULL,
	[FF_FilePathway] [nvarchar](4000) NOT NULL,
	[FF_CreateBy] [int] NOT NULL,
	[FF_CreateDate] [datetime] NOT NULL,
	[FF_UpdatedBy] [int] NULL,
	[FF_UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_DART_FeedbackFiles] PRIMARY KEY CLUSTERED 
(
	[FF_Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DART_Telemetry]    Script Date: 8/21/2026 7:25:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DART_Telemetry](
	[Tele_Id] [int] IDENTITY(1,1) NOT NULL,
	[Tele_App_Id] [int] NOT NULL,
	[Tele_MethodName] [varchar](150) NOT NULL,
	[Tele_Create_Date] [datetime] NOT NULL,
 CONSTRAINT [PK_DART_Telemetry] PRIMARY KEY CLUSTERED 
(
	[Tele_Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[APP_CurrentEmployees] ([Emp_Id], [Emp_PreferredName]) VALUES (1, N'CM')
GO
INSERT [dbo].[APP_CurrentEmployees] ([Emp_Id], [Emp_PreferredName]) VALUES (2, N'AL')
GO
INSERT [dbo].[APP_CurrentEmployees] ([Emp_Id], [Emp_PreferredName]) VALUES (3, N'LB')
GO
INSERT [dbo].[APP_CurrentEmployees] ([Emp_Id], [Emp_PreferredName]) VALUES (4, N'AA')
GO
INSERT [dbo].[APP_CurrentEmployees] ([Emp_Id], [Emp_PreferredName]) VALUES (5, N'GC')
GO
SET IDENTITY_INSERT [dbo].[DART_Applications] ON 
GO
INSERT [dbo].[DART_Applications] ([App_Id], [App_Name], [App_CurrentVersion], [App_Description], [App_Type], [App_CriticalityId], [App_PrimDeveloper_EmpId], [App_SecondaryDeveloper_EmpId], [App_Analyst_EmpId], [App_SDLC_Phase_Id], [App_SDLC_CheckDate], [Create_Date], [Create_By], [Update_Date], [Update_By], [App_FriendlyName], [App_AllowFeedback]) VALUES (1, N'Santa Tracker', CAST(3.6000 AS Decimal(6, 4)), N'Organizes elven interactions in the north pole and imports present deliveries made on SantaTracker.org', 5, 1, 1, 2, 3, 7, CAST(N'2023-12-08T00:00:00.000' AS DateTime), CAST(N'2021-06-14T00:00:00.000' AS DateTime), 1, CAST(N'2026-08-18T13:06:28.273' AS DateTime), 1, N'Santa Tracker', 1)
GO
SET IDENTITY_INSERT [dbo].[DART_Applications] OFF
GO
SET IDENTITY_INSERT [dbo].[DART_AppTypes] ON 
GO
INSERT [dbo].[DART_AppTypes] ([Type_Id], [Type_Name]) VALUES (1, N'Internal Application ')
GO
INSERT [dbo].[DART_AppTypes] ([Type_Id], [Type_Name]) VALUES (2, N'SDK Application ')
GO
INSERT [dbo].[DART_AppTypes] ([Type_Id], [Type_Name]) VALUES (3, N'Job or Process ')
GO
INSERT [dbo].[DART_AppTypes] ([Type_Id], [Type_Name]) VALUES (4, N'Pleiades')
GO
INSERT [dbo].[DART_AppTypes] ([Type_Id], [Type_Name]) VALUES (5, N'Library')
GO
INSERT [dbo].[DART_AppTypes] ([Type_Id], [Type_Name]) VALUES (6, N'Hosted API')
GO
SET IDENTITY_INSERT [dbo].[DART_AppTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[DART_Criticality] ON 
GO
INSERT [dbo].[DART_Criticality] ([Crit_Id], [Crit_Name]) VALUES (1, N'Customer Facing')
GO
INSERT [dbo].[DART_Criticality] ([Crit_Id], [Crit_Name]) VALUES (2, N'Internal, Mission Critical')
GO
INSERT [dbo].[DART_Criticality] ([Crit_Id], [Crit_Name]) VALUES (3, N'Internal, Impactful')
GO
INSERT [dbo].[DART_Criticality] ([Crit_Id], [Crit_Name]) VALUES (4, N'Internal, Minor')
GO
SET IDENTITY_INSERT [dbo].[DART_Criticality] OFF
GO
SET IDENTITY_INSERT [dbo].[DART_PathTypes] ON 
GO
INSERT [dbo].[DART_PathTypes] ([PathType_Id], [PathType_Name]) VALUES (1, N'Development')
GO
INSERT [dbo].[DART_PathTypes] ([PathType_Id], [PathType_Name]) VALUES (2, N'UAT')
GO
INSERT [dbo].[DART_PathTypes] ([PathType_Id], [PathType_Name]) VALUES (3, N'Production')
GO
INSERT [dbo].[DART_PathTypes] ([PathType_Id], [PathType_Name]) VALUES (4, N'Wiki')
GO
INSERT [dbo].[DART_PathTypes] ([PathType_Id], [PathType_Name]) VALUES (5, N'Network')
GO
INSERT [dbo].[DART_PathTypes] ([PathType_Id], [PathType_Name]) VALUES (6, N'Code Repo')
GO
INSERT [dbo].[DART_PathTypes] ([PathType_Id], [PathType_Name]) VALUES (7, N'Documentation')
GO
INSERT [dbo].[DART_PathTypes] ([PathType_Id], [PathType_Name]) VALUES (8, N'Mockup')
GO
SET IDENTITY_INSERT [dbo].[DART_PathTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[DART_SDLC_Phase] ON 
GO
INSERT [dbo].[DART_SDLC_Phase] ([SDLC_Id], [SDLC_Name]) VALUES (3, N'Design ')
GO
INSERT [dbo].[DART_SDLC_Phase] ([SDLC_Id], [SDLC_Name]) VALUES (4, N'Coding ')
GO
INSERT [dbo].[DART_SDLC_Phase] ([SDLC_Id], [SDLC_Name]) VALUES (5, N'Testing ')
GO
INSERT [dbo].[DART_SDLC_Phase] ([SDLC_Id], [SDLC_Name]) VALUES (7, N'Maintenance ')
GO
INSERT [dbo].[DART_SDLC_Phase] ([SDLC_Id], [SDLC_Name]) VALUES (8, N'Retired')
GO
SET IDENTITY_INSERT [dbo].[DART_SDLC_Phase] OFF
GO
SET IDENTITY_INSERT [dbo].[DART_Telemetry] ON 
GO
INSERT [dbo].[DART_Telemetry] ([Tele_Id], [Tele_App_Id], [Tele_MethodName], [Tele_Create_Date]) VALUES (1, 1, N'TrackedEvent', CAST(N'2021-06-11T11:32:47.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[DART_Telemetry] OFF
GO
ALTER TABLE [dbo].[DART_Applications]  WITH CHECK ADD  CONSTRAINT [FK_DART_Applications_APP_CurrentEmployees] FOREIGN KEY([App_PrimDeveloper_EmpId])
REFERENCES [dbo].[APP_CurrentEmployees] ([Emp_Id])
GO
ALTER TABLE [dbo].[DART_Applications] CHECK CONSTRAINT [FK_DART_Applications_APP_CurrentEmployees]
GO
ALTER TABLE [dbo].[DART_Applications]  WITH CHECK ADD  CONSTRAINT [FK_DART_Applications_APP_CurrentEmployees1] FOREIGN KEY([App_SecondaryDeveloper_EmpId])
REFERENCES [dbo].[APP_CurrentEmployees] ([Emp_Id])
GO
ALTER TABLE [dbo].[DART_Applications] CHECK CONSTRAINT [FK_DART_Applications_APP_CurrentEmployees1]
GO
ALTER TABLE [dbo].[DART_Applications]  WITH CHECK ADD  CONSTRAINT [FK_DART_Applications_APP_CurrentEmployees2] FOREIGN KEY([App_Analyst_EmpId])
REFERENCES [dbo].[APP_CurrentEmployees] ([Emp_Id])
GO
ALTER TABLE [dbo].[DART_Applications] CHECK CONSTRAINT [FK_DART_Applications_APP_CurrentEmployees2]
GO
ALTER TABLE [dbo].[DART_Applications]  WITH CHECK ADD  CONSTRAINT [FK_DART_Applications_DART_AppTypes] FOREIGN KEY([App_Type])
REFERENCES [dbo].[DART_AppTypes] ([Type_Id])
GO
ALTER TABLE [dbo].[DART_Applications] CHECK CONSTRAINT [FK_DART_Applications_DART_AppTypes]
GO
ALTER TABLE [dbo].[DART_Applications]  WITH CHECK ADD  CONSTRAINT [FK_DART_Applications_DART_Criticality] FOREIGN KEY([App_CriticalityId])
REFERENCES [dbo].[DART_Criticality] ([Crit_Id])
GO
ALTER TABLE [dbo].[DART_Applications] CHECK CONSTRAINT [FK_DART_Applications_DART_Criticality]
GO
ALTER TABLE [dbo].[DART_Applications]  WITH CHECK ADD  CONSTRAINT [FK_DART_Applications_DART_SDLCphase] FOREIGN KEY([App_SDLC_Phase_Id])
REFERENCES [dbo].[DART_SDLC_Phase] ([SDLC_Id])
GO
ALTER TABLE [dbo].[DART_Applications] CHECK CONSTRAINT [FK_DART_Applications_DART_SDLCphase]
GO
ALTER TABLE [dbo].[DART_Dependencies]  WITH CHECK ADD  CONSTRAINT [FK_DART_Dependencies_DART_Applications] FOREIGN KEY([Depend_On_Id])
REFERENCES [dbo].[DART_Applications] ([App_Id])
GO
ALTER TABLE [dbo].[DART_Dependencies] CHECK CONSTRAINT [FK_DART_Dependencies_DART_Applications]
GO
ALTER TABLE [dbo].[DART_Dependencies]  WITH CHECK ADD  CONSTRAINT [FK_DART_Dependencies_DART_Applications1] FOREIGN KEY([Depend_On_Id])
REFERENCES [dbo].[DART_Applications] ([App_Id])
GO
ALTER TABLE [dbo].[DART_Dependencies] CHECK CONSTRAINT [FK_DART_Dependencies_DART_Applications1]
GO
ALTER TABLE [dbo].[DART_Feedback]  WITH CHECK ADD  CONSTRAINT [FK_DART_Feedback_APP_CurrentEmployees] FOREIGN KEY([Create_By])
REFERENCES [dbo].[APP_CurrentEmployees] ([Emp_Id])
GO
ALTER TABLE [dbo].[DART_Feedback] CHECK CONSTRAINT [FK_DART_Feedback_APP_CurrentEmployees]
GO
