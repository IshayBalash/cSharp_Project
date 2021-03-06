USE [master]
GO
/****** Object:  Database [SearchForFilesProjectDB]    Script Date: 23/07/2018 23:47:51 ******/
CREATE DATABASE [SearchForFilesProjectDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SearchForFilesProjectDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQL_SERVER\MSSQL\DATA\SearchForFilesProjectDB.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SearchForFilesProjectDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQL_SERVER\MSSQL\DATA\SearchForFilesProjectDB_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SearchForFilesProjectDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SearchForFilesProjectDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SearchForFilesProjectDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SearchForFilesProjectDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SearchForFilesProjectDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SearchForFilesProjectDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SearchForFilesProjectDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [SearchForFilesProjectDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SearchForFilesProjectDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SearchForFilesProjectDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SearchForFilesProjectDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SearchForFilesProjectDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SearchForFilesProjectDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SearchForFilesProjectDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SearchForFilesProjectDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SearchForFilesProjectDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SearchForFilesProjectDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SearchForFilesProjectDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SearchForFilesProjectDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SearchForFilesProjectDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SearchForFilesProjectDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SearchForFilesProjectDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SearchForFilesProjectDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SearchForFilesProjectDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SearchForFilesProjectDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SearchForFilesProjectDB] SET  MULTI_USER 
GO
ALTER DATABASE [SearchForFilesProjectDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SearchForFilesProjectDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SearchForFilesProjectDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SearchForFilesProjectDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [SearchForFilesProjectDB] SET DELAYED_DURABILITY = DISABLED 
GO
USE [SearchForFilesProjectDB]
GO
/****** Object:  Table [dbo].[SearchResultConnections]    Script Date: 23/07/2018 23:47:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SearchResultConnections](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SearchId] [int] NOT NULL,
	[ResultId] [int] NOT NULL,
 CONSTRAINT [PK_Search-Result-connections] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SearchResults]    Script Date: 23/07/2018 23:47:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SearchResults](
	[SearchResultId] [int] IDENTITY(1,1) NOT NULL,
	[FilePath] [nvarchar](300) NOT NULL,
	[FileName] [nvarchar](300) NOT NULL,
 CONSTRAINT [PK_SearchResults] PRIMARY KEY CLUSTERED 
(
	[SearchResultId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserSearches]    Script Date: 23/07/2018 23:47:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSearches](
	[SearchId] [int] IDENTITY(1,1) NOT NULL,
	[SearchName] [nvarchar](100) NOT NULL,
	[SearchFolder] [nvarchar](200) NOT NULL,
	[SearchDate] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_UserSearches] PRIMARY KEY CLUSTERED 
(
	[SearchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[SearchResultConnections]  WITH CHECK ADD  CONSTRAINT [FK_Search-Result-connections_SearchResults] FOREIGN KEY([ResultId])
REFERENCES [dbo].[SearchResults] ([SearchResultId])
GO
ALTER TABLE [dbo].[SearchResultConnections] CHECK CONSTRAINT [FK_Search-Result-connections_SearchResults]
GO
ALTER TABLE [dbo].[SearchResultConnections]  WITH CHECK ADD  CONSTRAINT [FK_Search-Result-connections_UserSearches] FOREIGN KEY([SearchId])
REFERENCES [dbo].[UserSearches] ([SearchId])
GO
ALTER TABLE [dbo].[SearchResultConnections] CHECK CONSTRAINT [FK_Search-Result-connections_UserSearches]
GO
/****** Object:  StoredProcedure [dbo].[InsertConnection]    Script Date: 23/07/2018 23:47:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[InsertConnection](@SearchID int,@ResultId int)
as
insert into SearchResultConnections(SearchId,ResultId)values(@SearchID,@ResultId)
 



GO
/****** Object:  StoredProcedure [dbo].[InsertResultValue]    Script Date: 23/07/2018 23:47:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[InsertResultValue](@FilePath nvarchar(299),@FileName nvarchar(299))
as
insert into SearchResults(FilePath,FileName)values(@FilePath,@FileName) 

GO
/****** Object:  StoredProcedure [dbo].[InsertSearchValue]    Script Date: 23/07/2018 23:47:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[InsertSearchValue](@SearchName nvarchar(99),@SearchFolder nvarchar(199),@SearchDate nvarchar(199))
as
insert into UserSearches(SearchName,SearchFolder,SearchDate) values(@SearchName,@SearchFolder,@SearchDate)

GO
/****** Object:  StoredProcedure [dbo].[UpdateSearchDate]    Script Date: 23/07/2018 23:47:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create Procedure [dbo].[UpdateSearchDate](@SearchID int ,@SearchDate nvarchar(199))
as
update UserSearches set SearchDate=@SearchDate where SearchId=@SearchID

GO
USE [master]
GO
ALTER DATABASE [SearchForFilesProjectDB] SET  READ_WRITE 
GO
