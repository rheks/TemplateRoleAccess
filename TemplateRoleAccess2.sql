USE [master]
GO
/****** Object:  Database [TemplateRoleAccess2]    Script Date: 06/02/2023 15:21:13 ******/
CREATE DATABASE [TemplateRoleAccess2]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TemplateRoleAccess2', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\TemplateRoleAccess2.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TemplateRoleAccess2_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\TemplateRoleAccess2_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [TemplateRoleAccess2] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TemplateRoleAccess2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TemplateRoleAccess2] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TemplateRoleAccess2] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TemplateRoleAccess2] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TemplateRoleAccess2] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TemplateRoleAccess2] SET ARITHABORT OFF 
GO
ALTER DATABASE [TemplateRoleAccess2] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TemplateRoleAccess2] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TemplateRoleAccess2] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TemplateRoleAccess2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TemplateRoleAccess2] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TemplateRoleAccess2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TemplateRoleAccess2] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TemplateRoleAccess2] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TemplateRoleAccess2] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TemplateRoleAccess2] SET  ENABLE_BROKER 
GO
ALTER DATABASE [TemplateRoleAccess2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TemplateRoleAccess2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TemplateRoleAccess2] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TemplateRoleAccess2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TemplateRoleAccess2] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TemplateRoleAccess2] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [TemplateRoleAccess2] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TemplateRoleAccess2] SET RECOVERY FULL 
GO
ALTER DATABASE [TemplateRoleAccess2] SET  MULTI_USER 
GO
ALTER DATABASE [TemplateRoleAccess2] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TemplateRoleAccess2] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TemplateRoleAccess2] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TemplateRoleAccess2] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TemplateRoleAccess2] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TemplateRoleAccess2] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'TemplateRoleAccess2', N'ON'
GO
ALTER DATABASE [TemplateRoleAccess2] SET QUERY_STORE = OFF
GO
USE [TemplateRoleAccess2]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 06/02/2023 15:21:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountRoles]    Script Date: 06/02/2023 15:21:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountRoles](
	[RoleId] [int] NOT NULL,
	[AccountNIK] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AccountRoles] PRIMARY KEY CLUSTERED 
(
	[AccountNIK] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 06/02/2023 15:21:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[NIK] [nvarchar](450) NOT NULL,
	[Password] [nvarchar](max) NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[NIK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Departements]    Script Date: 06/02/2023 15:21:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departements](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Manager_Id] [nvarchar](450) NULL,
 CONSTRAINT [PK_Departements] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 06/02/2023 15:21:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[NIK] [nvarchar](450) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NOT NULL,
	[BirthDate] [date] NOT NULL,
	[Salary] [int] NOT NULL,
	[Email] [nvarchar](max) NULL,
	[Gender] [int] NOT NULL,
	[Manager_Id] [nvarchar](450) NULL,
	[Departement_Id] [int] NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[NIK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 06/02/2023 15:21:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230206034144_InitialMigration', N'6.0.0')
GO
INSERT [dbo].[AccountRoles] ([RoleId], [AccountNIK]) VALUES (1, N'202306020001')
INSERT [dbo].[AccountRoles] ([RoleId], [AccountNIK]) VALUES (3, N'202306020002')
INSERT [dbo].[AccountRoles] ([RoleId], [AccountNIK]) VALUES (3, N'202306020003')
GO
INSERT [dbo].[Accounts] ([NIK], [Password]) VALUES (N'202306020001', N'$2a$11$8TRW1KDjC4alrtxW4vq1/eCQCGuLczh0IyeHNbT6qYn6JOPVsjkBu')
INSERT [dbo].[Accounts] ([NIK], [Password]) VALUES (N'202306020002', N'$2a$11$McJrsDA.PBewaopNMwl/suW2EyU0iZUyewwppgMWUcf.h.N3tY13q')
INSERT [dbo].[Accounts] ([NIK], [Password]) VALUES (N'202306020003', N'$2a$11$R6ddtNh5.k1ELJPjUyzKgOObHSzWhg6ugceahZnVZNswLTFe5LhA.')
GO
SET IDENTITY_INSERT [dbo].[Departements] ON 

INSERT [dbo].[Departements] ([Id], [Name], [Manager_Id]) VALUES (1, N'Resource Application Service', NULL)
INSERT [dbo].[Departements] ([Id], [Name], [Manager_Id]) VALUES (2, N'Human Resources', NULL)
SET IDENTITY_INSERT [dbo].[Departements] OFF
GO
INSERT [dbo].[Employees] ([NIK], [FirstName], [LastName], [Phone], [BirthDate], [Salary], [Email], [Gender], [Manager_Id], [Departement_Id]) VALUES (N'202306020001', N'Andre', N'Kautsar', N'081382868570', CAST(N'2000-06-09' AS Date), 0, N'andre.ktsr9@gmail.com', 0, NULL, 1)
INSERT [dbo].[Employees] ([NIK], [FirstName], [LastName], [Phone], [BirthDate], [Salary], [Email], [Gender], [Manager_Id], [Departement_Id]) VALUES (N'202306020002', N'Areks', N'Ryuno', N'081382868571', CAST(N'2000-06-17' AS Date), 5000000, N'areks.ryuno@gmail.com', 0, N'202306020001', 1)
INSERT [dbo].[Employees] ([NIK], [FirstName], [LastName], [Phone], [BirthDate], [Salary], [Email], [Gender], [Manager_Id], [Departement_Id]) VALUES (N'202306020003', N'Abdul', N'Halim', N'08124214434', CAST(N'1946-02-06' AS Date), 100000000, N'abdul.halim@gmail.com', 0, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [Name]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (2, N'Manager')
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (3, N'Employee')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
/****** Object:  Index [IX_AccountRoles_RoleId]    Script Date: 06/02/2023 15:21:13 ******/
CREATE NONCLUSTERED INDEX [IX_AccountRoles_RoleId] ON [dbo].[AccountRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Departements_Manager_Id]    Script Date: 06/02/2023 15:21:13 ******/
CREATE NONCLUSTERED INDEX [IX_Departements_Manager_Id] ON [dbo].[Departements]
(
	[Manager_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Employees_Departement_Id]    Script Date: 06/02/2023 15:21:13 ******/
CREATE NONCLUSTERED INDEX [IX_Employees_Departement_Id] ON [dbo].[Employees]
(
	[Departement_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Employees_Manager_Id]    Script Date: 06/02/2023 15:21:13 ******/
CREATE NONCLUSTERED INDEX [IX_Employees_Manager_Id] ON [dbo].[Employees]
(
	[Manager_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AccountRoles]  WITH CHECK ADD  CONSTRAINT [FK_AccountRoles_Accounts_AccountNIK] FOREIGN KEY([AccountNIK])
REFERENCES [dbo].[Accounts] ([NIK])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AccountRoles] CHECK CONSTRAINT [FK_AccountRoles_Accounts_AccountNIK]
GO
ALTER TABLE [dbo].[AccountRoles]  WITH CHECK ADD  CONSTRAINT [FK_AccountRoles_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AccountRoles] CHECK CONSTRAINT [FK_AccountRoles_Roles_RoleId]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Employees_NIK] FOREIGN KEY([NIK])
REFERENCES [dbo].[Employees] ([NIK])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Employees_NIK]
GO
ALTER TABLE [dbo].[Departements]  WITH CHECK ADD  CONSTRAINT [FK_Departements_Employees_Manager_Id] FOREIGN KEY([Manager_Id])
REFERENCES [dbo].[Employees] ([NIK])
GO
ALTER TABLE [dbo].[Departements] CHECK CONSTRAINT [FK_Departements_Employees_Manager_Id]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Departements_Departement_Id] FOREIGN KEY([Departement_Id])
REFERENCES [dbo].[Departements] ([Id])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Departements_Departement_Id]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Employees_Manager_Id] FOREIGN KEY([Manager_Id])
REFERENCES [dbo].[Employees] ([NIK])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Employees_Manager_Id]
GO
USE [master]
GO
ALTER DATABASE [TemplateRoleAccess2] SET  READ_WRITE 
GO
