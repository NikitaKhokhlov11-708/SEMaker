USE [master]
GO
/****** Object:  Database [semaker]    Script Date: 25.03.2019 18:31:59 ******/
CREATE DATABASE [semaker]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'semaker', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\semaker.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'semaker_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\semaker_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [semaker] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [semaker].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [semaker] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [semaker] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [semaker] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [semaker] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [semaker] SET ARITHABORT OFF 
GO
ALTER DATABASE [semaker] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [semaker] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [semaker] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [semaker] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [semaker] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [semaker] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [semaker] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [semaker] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [semaker] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [semaker] SET  DISABLE_BROKER 
GO
ALTER DATABASE [semaker] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [semaker] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [semaker] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [semaker] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [semaker] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [semaker] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [semaker] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [semaker] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [semaker] SET  MULTI_USER 
GO
ALTER DATABASE [semaker] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [semaker] SET DB_CHAINING OFF 
GO
ALTER DATABASE [semaker] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [semaker] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [semaker] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [semaker] SET QUERY_STORE = OFF
GO
USE [semaker]
GO
/****** Object:  Table [dbo].[tblEvents]    Script Date: 25.03.2019 18:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblEvents](
	[EventId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[City] [varchar](20) NOT NULL,
	[Sport] [varchar](20) NOT NULL,
	[Author] [varchar](20) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[spAddEvent]    Script Date: 25.03.2019 18:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAddEvent]         
(        
    @Name VARCHAR(50),         
    @City VARCHAR(20),        
    @Sport VARCHAR(20),
	@Author VARCHAR(20)            
)        
as         
Begin         
    Insert into dbo.tblEvents (Name,City,Sport,Author)         
    Values (@Name,@City,@Sport,@Author)         
End    
GO
/****** Object:  StoredProcedure [dbo].[spDeleteEvent]    Script Date: 25.03.2019 18:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[spDeleteEvent]        
(          
   @EvntId int          
)          
as           
begin          
   Delete from tblEvents where EventId=@EvntId          
End
GO
/****** Object:  StoredProcedure [dbo].[spGetAllEvents]    Script Date: 25.03.2019 18:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[spGetAllEvents]      
as      
Begin      
    select *      
    from tblEvents      
End   
GO
/****** Object:  StoredProcedure [dbo].[spGetMyEvents]    Script Date: 25.03.2019 18:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spGetMyEvents]         
(
	@Author VARCHAR(20)            
) 
as     
Begin      
    select * 
    from tblEvents
	where @Author = Author          
End   
GO
/****** Object:  StoredProcedure [dbo].[spUpdateEvent]    Script Date: 25.03.2019 18:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[spUpdateEvent]          
(          
   @EvntId INTEGER ,        
   @Name VARCHAR(50),         
   @City VARCHAR(20),        
   @Sport VARCHAR(20)        
)          
as          
begin          
   Update tblEvents           
   set Name=@Name,          
   City=@City,      
   Sport=@Sport          
   where EventId=@EvntId          
End  
GO
USE [master]
GO
ALTER DATABASE [semaker] SET  READ_WRITE 
GO
