q33wUSE [master]
GO
/****** Object:  Database [TrackerDB]    Script Date: 13/12/2024 10:20:58 pm ******/
CREATE DATABASE [TrackerDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TrackerDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\TrackerDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TrackerDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\TrackerDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [TrackerDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TrackerDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TrackerDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TrackerDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TrackerDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TrackerDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TrackerDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [TrackerDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [TrackerDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TrackerDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TrackerDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TrackerDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TrackerDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TrackerDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TrackerDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TrackerDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TrackerDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [TrackerDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TrackerDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TrackerDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TrackerDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TrackerDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TrackerDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TrackerDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TrackerDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TrackerDB] SET  MULTI_USER 
GO
ALTER DATABASE [TrackerDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TrackerDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TrackerDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TrackerDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TrackerDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TrackerDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [TrackerDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [TrackerDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [TrackerDB]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 13/12/2024 10:20:58 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](255) NOT NULL,
	[Description] [text] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DateDeleted] [datetime] NULL,
	[DeleteApprover] [nvarchar](50) NULL,
 CONSTRAINT [PK__Categori__19093A2BCEF5D177] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 13/12/2024 10:20:58 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[CountryId] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Districts]    Script Date: 13/12/2024 10:20:58 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Districts](
	[DistrictId] [int] IDENTITY(1,1) NOT NULL,
	[DistrictName] [nvarchar](50) NOT NULL,
	[CountryId] [int] NOT NULL,
 CONSTRAINT [PK_Districts] PRIMARY KEY CLUSTERED 
(
	[DistrictId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InventoryTransactions]    Script Date: 13/12/2024 10:20:58 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventoryTransactions](
	[TransactionID] [int] IDENTITY(1,1) NOT NULL,
	[ShopProductID] [int] NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[TransactionType] [nvarchar](50) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Notes] [varchar](max) NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[Rev] [int] NOT NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DateDeleted] [datetime] NULL,
	[DeleteApprover] [nvarchar](50) NULL,
	[ReceivingShop] [nvarchar](50) NOT NULL,
	[SendingShop] [nvarchar](50) NOT NULL,
	[ProductExpiryDate] [datetime] NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
	[QuantityBeforeReorder] [int] NOT NULL,
	[UnitPriceOfPreviousStock] [decimal](10, 2) NOT NULL,
	[OrderPrice] [decimal](10, 2) NOT NULL,
	[RetailPrice] [decimal](10, 2) NOT NULL,
	[BarCode] [nvarchar](200) NOT NULL,
	[Sold] [int] NOT NULL,
 CONSTRAINT [PK__Inventor__55433A4B7619278F] PRIMARY KEY CLUSTERED 
(
	[TransactionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JournalEntries]    Script Date: 13/12/2024 10:20:58 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JournalEntries](
	[JournalEntryTransId] [bigint] IDENTITY(1,1) NOT NULL,
	[ReceiptNo] [nvarchar](50) NOT NULL,
	[ChequeNumber] [nvarchar](50) NULL,
	[AmountPaid] [float] NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[PayMode] [nvarchar](50) NOT NULL,
	[Rev] [char](2) NOT NULL,
	[DrCr] [nchar](10) NOT NULL,
	[ProcessedStatus] [nchar](10) NOT NULL,
	[ProcessDateTime] [datetime] NOT NULL,
	[Processedby] [nvarchar](max) NOT NULL,
	[TranscationDetails] [nvarchar](max) NULL,
	[Revreq] [char](2) NOT NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[DateModified] [datetime] NULL,
	[Barcode] [nvarchar](200) NULL,
 CONSTRAINT [PK_ReceiptNo] PRIMARY KEY CLUSTERED 
(
	[JournalEntryTransId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 13/12/2024 10:20:58 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderDetailID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NULL,
	[ProductID] [int] NULL,
	[Quantity] [int] NULL,
	[Price] [decimal](10, 2) NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DateDeleted] [datetime] NULL,
	[DeleteApprover] [nvarchar](50) NULL,
 CONSTRAINT [PK__OrderDet__D3B9D30C7A122BFF] PRIMARY KEY CLUSTERED 
(
	[OrderDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 13/12/2024 10:20:58 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[OrderDate] [datetime] NULL,
	[SupplierID] [int] NULL,
	[TotalAmount] [decimal](10, 2) NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DeletedApprover] [nvarchar](50) NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [PK__Orders__C3905BAF4A276831] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 13/12/2024 10:20:58 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [varchar](255) NOT NULL,
	[Description] [text] NULL,
	[CategoryID] [int] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DeletedApprover] [nvarchar](50) NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [PK__Products__B40CC6ED4D9A0596] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReorderAlerts]    Script Date: 13/12/2024 10:20:58 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReorderAlerts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductIdentifier] [int] NOT NULL,
	[ShopId] [int] NOT NULL,
 CONSTRAINT [PK_ReorderAlerts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShopProducts]    Script Date: 13/12/2024 10:20:58 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShopProducts](
	[ShopProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [varchar](255) NOT NULL,
	[Description] [text] NOT NULL,
	[QuantityInStock] [int] NOT NULL,
	[ReorderLevel] [int] NOT NULL,
	[Price] [decimal](10, 2) NOT NULL,
	[ShopId] [int] NULL,
	[LastOrderDate] [datetime] NULL,
	[ProductID] [int] NOT NULL,
	[QuantitySold] [int] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DeletedApprover] [nvarchar](50) NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [PK__ShopProducts__B40CC6ED4D9A0596] PRIMARY KEY CLUSTERED 
(
	[ShopProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shops]    Script Date: 13/12/2024 10:20:58 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shops](
	[ShopId] [int] IDENTITY(1,1) NOT NULL,
	[ShopName] [nvarchar](50) NOT NULL,
	[DistrictId] [int] NOT NULL,
	[ShopManagerName] [nvarchar](50) NOT NULL,
	[ShopManagerContact] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DeletedApprover] [nvarchar](50) NULL,
	[DateDeleted] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Shops] PRIMARY KEY CLUSTERED 
(
	[ShopId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Suppliers]    Script Date: 13/12/2024 10:20:58 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suppliers](
	[SupplierID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [varchar](255) NOT NULL,
	[ContactName] [varchar](255) NULL,
	[ContactEmail] [varchar](255) NULL,
	[Phone] [varchar](20) NULL,
	[Address] [text] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DeletedApprover] [nvarchar](50) NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [PK__Supplier__4BE66694FC62D27A] PRIMARY KEY CLUSTERED 
(
	[SupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactionTypes]    Script Date: 13/12/2024 10:20:58 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionTypes](
	[TransactionTypeId] [nvarchar](50) NOT NULL,
	[TransactionTypeName] [varchar](20) NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DeletedApprover] [nvarchar](50) NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [PK__Transact__20266D0B7AFB765E] PRIMARY KEY CLUSTERED 
(
	[TransactionTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Districts]  WITH CHECK ADD  CONSTRAINT [FK_Districts_Districts] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([CountryId])
GO
ALTER TABLE [dbo].[Districts] CHECK CONSTRAINT [FK_Districts_Districts]
GO
ALTER TABLE [dbo].[InventoryTransactions]  WITH CHECK ADD  CONSTRAINT [FK__Inventory__Produ__59FA5E80] FOREIGN KEY([ShopProductID])
REFERENCES [dbo].[ShopProducts] ([ShopProductID])
GO
ALTER TABLE [dbo].[InventoryTransactions] CHECK CONSTRAINT [FK__Inventory__Produ__59FA5E80]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK__OrderDeta__Order__5441852A] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK__OrderDeta__Order__5441852A]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK__OrderDeta__Produ__5535A963] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK__OrderDeta__Produ__5535A963]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK__Orders__Supplier__5165187F] FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Suppliers] ([SupplierID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK__Orders__Supplier__5165187F]
GO
ALTER TABLE [dbo].[Shops]  WITH CHECK ADD  CONSTRAINT [FK_Shops_Shops] FOREIGN KEY([ShopId])
REFERENCES [dbo].[Shops] ([ShopId])
GO
ALTER TABLE [dbo].[Shops] CHECK CONSTRAINT [FK_Shops_Shops]
GO
USE [master]
GO
ALTER DATABASE [TrackerDB] SET  READ_WRITE 
GO
