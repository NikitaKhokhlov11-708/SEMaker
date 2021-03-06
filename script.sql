USE [master]
GO
/****** Object:  Database [semaker]    Script Date: 14.05.2019 21:12:05 ******/
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
/****** Object:  Table [dbo].[tblApplications]    Script Date: 14.05.2019 21:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblApplications](
	[AppAuthor] [varchar](50) NOT NULL,
	[EventID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblEvents]    Script Date: 14.05.2019 21:12:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblEvents](
	[EventId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[City] [varchar](20) NOT NULL,
	[Sport] [varchar](20) NOT NULL,
	[Author] [varchar](20) NOT NULL,
	[Places] [int] NOT NULL,
	[Date] [date] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblRoles]    Script Date: 14.05.2019 21:12:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblRoles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblUsers]    Script Date: 14.05.2019 21:12:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUsers](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Surname] [varchar](50) NOT NULL,
	[SecondName] [varchar](50) NULL,
	[BirthDate] [date] NOT NULL,
	[Login] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[PhoneNum] [varchar](50) NOT NULL,
	[Role] [int] NOT NULL,
	[Premium] [int] NULL,
	[EndDate] [datetime] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[spAddEvent]    Script Date: 14.05.2019 21:12:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAddEvent]         
(        
    @Name VARCHAR(50),         
    @City VARCHAR(20),        
    @Sport VARCHAR(20),
	@Author VARCHAR(20),
	@Date DATE,
	@Places int            
)        
as         
Begin         
    Insert into dbo.tblEvents (Name,City,Sport,Author, Date, Places)         
    Values (@Name,@City,@Sport,@Author,@Date,@Places)         
End    
GO
/****** Object:  StoredProcedure [dbo].[spAddRole]    Script Date: 14.05.2019 21:12:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAddRole]         
(        
    @RoleId INT,         
    @Name VARCHAR(50)      
)        
as         
Begin    
	Insert into dbo.tblRoles (RoleId, RoleName)         
    Values (@RoleId, @Name)         
End    
GO
/****** Object:  StoredProcedure [dbo].[spAddUser]    Script Date: 14.05.2019 21:12:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAddUser]         
(        
    @Name VARCHAR(50),         
    @Surname VARCHAR(50),        
    @SecondName VARCHAR(50),
	@BirthDate DATE,
	@Login VARCHAR(50),
	@Password VARCHAR (50),
	@PhoneNum VARCHAR(50),
	@Role INT,
	@Premium INT,
	@EndDate DATETIME           
)        
as         
Begin    
	Insert into dbo.tblUsers (Name,Surname,SecondName,BirthDate, Login, Password, PhoneNum, Role, Premium, EndDate)         
    Values (@Name,@Surname,@SecondName,@BirthDate,@Login,@Password,@PhoneNum, @Role, @Premium, @EndDate)         
End    
GO
/****** Object:  StoredProcedure [dbo].[spApply]    Script Date: 14.05.2019 21:12:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spApply]   
(  
	@Name VARCHAR(50),      
	@EvntId int          
)   
as      
Begin      
     Insert into dbo.tblApplications ([AppAuthor],EventID) Values (@Name,@EvntId)
	 UPDATE tblEvents SET Places=Places-1 WHERE EventId=@EvntId
End   
GO
/****** Object:  StoredProcedure [dbo].[spCheckApplication]    Script Date: 14.05.2019 21:12:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spCheckApplication]   
(  
	@Name VARCHAR(50),      
	@EvntId int          
)   
as      
Begin      
     SELECT * FROM tblApplications 
	 WHERE AppAuthor = @Name AND EventID = @EvntId
End   
GO
/****** Object:  StoredProcedure [dbo].[spDeleteEvent]    Script Date: 14.05.2019 21:12:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spDeleteEvent]        
(          
   @EvntId int          
)          
as           
begin          
   Delete from tblEvents where EventId=@EvntId
   Delete from tblApplications where EventId=@EvntId           
End
GO
/****** Object:  StoredProcedure [dbo].[spDeleteUser]    Script Date: 14.05.2019 21:12:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spDeleteUser]        
(          
   @UserLogin [varchar](50)      
)          
as           
begin          
   Delete from tblUsers where Login=@UserLogin
   Delete from tblApplications where AppAuthor=@UserLogin
   Delete from tblEvents where Author=@UserLogin      
End
GO
/****** Object:  StoredProcedure [dbo].[spGetAllEvents]    Script Date: 14.05.2019 21:12:06 ******/
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
/****** Object:  StoredProcedure [dbo].[spGetApplications]    Script Date: 14.05.2019 21:12:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spGetApplications]   
(          
   @EvntID int          
)   
as      
Begin
	select * from dbo.tblUsers
	 where dbo.tblUsers.Name in 
	 (select dbo.tblApplications.[AppAuthor] from tblApplications
	 where tblApplications.EventID = @EvntID)  
End   
GO
/****** Object:  StoredProcedure [dbo].[spGetMyApplications]    Script Date: 14.05.2019 21:12:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spGetMyApplications]   
(  
	@Name VARCHAR(50)          
)   
as      
Begin      
     select * from dbo.tblEvents
	 where dbo.tblEvents.EventId in 
	 (select dbo.tblApplications.EventID from tblApplications
	 where tblApplications.[AppAuthor] = @Name)
End   
GO
/****** Object:  StoredProcedure [dbo].[spGetMyEvents]    Script Date: 14.05.2019 21:12:06 ******/
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
/****** Object:  StoredProcedure [dbo].[spRemoveApplication]    Script Date: 14.05.2019 21:12:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spRemoveApplication]   
(  
	@Name VARCHAR(50),      
	@EvntId int          
)   
as      
Begin      
     DELETE FROM tblApplications 
	 WHERE [AppAuthor] = @Name AND EventID = @EvntId
	 UPDATE tblEvents set Places = Places + 1 where EventId=@EvntId
End   
GO
/****** Object:  StoredProcedure [dbo].[spUpdateEvent]    Script Date: 14.05.2019 21:12:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spUpdateEvent]          
(          
   @EvntId INTEGER ,        
   @Name VARCHAR(50),  
   @Sport VARCHAR(20),       
   @City VARCHAR(20),           
   @Places INTEGER,
   @Date date        
)          
as          
begin          
   Update tblEvents           
   set Name=@Name,          
   City=@City,      
   Sport=@Sport,
   Places=@Places,
   Date=@Date          
   where EventId=@EvntId          
End  
GO
/****** Object:  StoredProcedure [dbo].[spUpdateUser]    Script Date: 14.05.2019 21:12:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spUpdateUser]         
(        
	@UserId INTEGER ,
    @Name VARCHAR(50),         
    @Surname VARCHAR(50),        
    @SecondName VARCHAR(50),
	@BirthDate DATE,
	@Login VARCHAR(50),
	@Password VARCHAR (50),
	@PhoneNum VARCHAR(20),
	@Role INT,
	@Premium INT,
	@EndDate DATETIME            
)        
as         
Begin    
   Update tblUsers           
   set Name=@Name,          
   Surname=@Surname,   
   SecondName=@SecondName,   
   BirthDate=@BirthDate,
   Login=@Login,
   Password=@Password,
   PhoneNum=@PhoneNum,
   Role=@Role,
   Premium=@Premium,
   EndDate=@EndDate
   where UserId=@UserId       
   End    
GO

USE [semaker]
GO
INSERT INTO [dbo].[tblUsers]
           ([Name]
           ,[Surname]
           ,[SecondName]
           ,[BirthDate]
           ,[Login]
           ,[Password]
           ,[PhoneNum]
           ,[Role]
		   ,[Premium]
		   ,[EndDate])
     VALUES
           ('Admin'
           ,'Admin'
           ,'Admin'
           ,'19970101 00:00:00 AM'
           ,'Admin'
           ,'Admin'
           ,1234567890
           ,2
		   ,1
		   ,'20190901 00:00:00 AM')   
GO

USE [master]
GO
INSERT INTO [dbo].[tblRoles]
           ([RoleName])
     VALUES
           ('user'),
		   ('admin')
GO

USE [master]
GO
ALTER DATABASE [semaker] SET  READ_WRITE 
GO
