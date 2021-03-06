USE [master]
GO
/****** Object:  Database [test]    Script Date: 4/18/2021 5:48:57 PM ******/
CREATE DATABASE [test]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'test', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\test.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'test_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\test_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [test] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [test].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [test] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [test] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [test] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [test] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [test] SET ARITHABORT OFF 
GO
ALTER DATABASE [test] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [test] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [test] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [test] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [test] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [test] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [test] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [test] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [test] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [test] SET  DISABLE_BROKER 
GO
ALTER DATABASE [test] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [test] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [test] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [test] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [test] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [test] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [test] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [test] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [test] SET  MULTI_USER 
GO
ALTER DATABASE [test] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [test] SET DB_CHAINING OFF 
GO
ALTER DATABASE [test] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [test] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [test] SET DELAYED_DURABILITY = DISABLED 
GO
USE [test]
GO
/****** Object:  Table [dbo].[account]    Script Date: 4/18/2021 5:48:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[account](
	[acc_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[username] [nvarchar](50) NULL,
	[password] [nvarchar](50) NULL,
	[phone] [varchar](50) NULL,
	[email] [nvarchar](50) NULL,
	[address] [nvarchar](50) NULL,
	[birth] [datetime] NULL,
 CONSTRAINT [PK_account] PRIMARY KEY CLUSTERED 
(
	[acc_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[admin_account]    Script Date: 4/18/2021 5:48:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[admin_account](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NULL,
	[password] [nvarchar](50) NULL,
	[name] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[bill]    Script Date: 4/18/2021 5:48:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bill](
	[bill_id] [int] IDENTITY(1,1) NOT NULL,
	[acc_id] [int] NOT NULL,
	[payment] [bit] NULL,
	[delivery_status] [int] NULL,
	[order_date] [datetime] NULL,
	[delivery_date] [datetime] NULL,
	[total] [decimal](18, 0) NULL,
	[status] [bit] NULL,
 CONSTRAINT [PK_bill_1] PRIMARY KEY CLUSTERED 
(
	[bill_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cart]    Script Date: 4/18/2021 5:48:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cart](
	[cart_id] [int] IDENTITY(1,1) NOT NULL,
	[acc_id] [int] NOT NULL,
	[shoe_id] [int] NOT NULL,
	[quantity] [int] NULL,
 CONSTRAINT [PK_cart] PRIMARY KEY CLUSTERED 
(
	[cart_id] ASC,
	[acc_id] ASC,
	[shoe_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[category]    Script Date: 4/18/2021 5:48:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[category_id] [int] IDENTITY(1,1) NOT NULL,
	[category_name] [nvarchar](50) NULL,
 CONSTRAINT [PK_category] PRIMARY KEY CLUSTERED 
(
	[category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[order_detail]    Script Date: 4/18/2021 5:48:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_detail](
	[bill_id] [int] NOT NULL,
	[shoe_id] [int] NOT NULL,
	[size] [int] NOT NULL,
	[price] [decimal](18, 0) NOT NULL,
	[quantity] [int] NULL,
 CONSTRAINT [PK_order_detail_1] PRIMARY KEY CLUSTERED 
(
	[bill_id] ASC,
	[shoe_id] ASC,
	[size] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[shoe_info]    Script Date: 4/18/2021 5:48:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[shoe_info](
	[shoe_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[image] [nvarchar](50) NULL,
	[detail] [nvarchar](max) NULL,
	[category_id] [int] NULL,
	[sex] [int] NULL,
	[date] [datetime] NULL,
	[price] [decimal](18, 0) NULL,
 CONSTRAINT [PK_shoe_info] PRIMARY KEY CLUSTERED 
(
	[shoe_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[shoes]    Script Date: 4/18/2021 5:48:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[shoes](
	[shoe_id] [int] NOT NULL,
	[size] [int] NOT NULL,
	[stock] [bigint] NULL,
	[sold] [bigint] NULL,
 CONSTRAINT [PK_shoes] PRIMARY KEY CLUSTERED 
(
	[shoe_id] ASC,
	[size] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[account] ON 

INSERT [dbo].[account] ([acc_id], [name], [username], [password], [phone], [email], [address], [birth]) VALUES (2, N'Hi?p Ð?i Bàng', N'1', N'1', N'0916367574', N'tranlochiep1111@gmail.com', N'Eastside City (Thu Duc City)', CAST(N'2000-09-10 00:00:00.000' AS DateTime))
INSERT [dbo].[account] ([acc_id], [name], [username], [password], [phone], [email], [address], [birth]) VALUES (1025, N'testtt', N'test', N'test', N'+84916367574', N'tranlochiep1111@gmail.com', N'Eastside City (Thu Duc City)', CAST(N'2021-04-14 00:00:00.000' AS DateTime))
INSERT [dbo].[account] ([acc_id], [name], [username], [password], [phone], [email], [address], [birth]) VALUES (1026, N'hiep', N'hiep', N'hiep', N'0916367574', N'hiep@gmail.com', N'HCM', CAST(N'2005-12-12 00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[account] OFF
SET IDENTITY_INSERT [dbo].[admin_account] ON 

INSERT [dbo].[admin_account] ([id], [username], [password], [name]) VALUES (1, N'admin', N'admin', N'Hiệp')
SET IDENTITY_INSERT [dbo].[admin_account] OFF
SET IDENTITY_INSERT [dbo].[bill] ON 

INSERT [dbo].[bill] ([bill_id], [acc_id], [payment], [delivery_status], [order_date], [delivery_date], [total], [status]) VALUES (9, 1025, 0, 3, CAST(N'2021-04-17 19:20:33.810' AS DateTime), CAST(N'2021-04-23 00:00:00.000' AS DateTime), CAST(3100000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[bill] ([bill_id], [acc_id], [payment], [delivery_status], [order_date], [delivery_date], [total], [status]) VALUES (14, 1025, NULL, 3, CAST(N'2021-04-18 00:57:59.620' AS DateTime), CAST(N'2021-04-29 00:00:00.000' AS DateTime), CAST(9300000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[bill] ([bill_id], [acc_id], [payment], [delivery_status], [order_date], [delivery_date], [total], [status]) VALUES (15, 1025, NULL, -1, CAST(N'2021-04-18 11:19:05.510' AS DateTime), CAST(N'2021-04-20 00:00:00.000' AS DateTime), CAST(12200000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[bill] ([bill_id], [acc_id], [payment], [delivery_status], [order_date], [delivery_date], [total], [status]) VALUES (16, 1025, NULL, -1, CAST(N'2021-04-18 11:32:22.480' AS DateTime), CAST(N'2021-04-27 00:00:00.000' AS DateTime), CAST(12300000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[bill] ([bill_id], [acc_id], [payment], [delivery_status], [order_date], [delivery_date], [total], [status]) VALUES (17, 1026, NULL, -1, CAST(N'2021-04-18 16:13:59.460' AS DateTime), CAST(N'2021-04-23 00:00:00.000' AS DateTime), CAST(20400000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[bill] ([bill_id], [acc_id], [payment], [delivery_status], [order_date], [delivery_date], [total], [status]) VALUES (18, 1025, NULL, -2, CAST(N'2021-04-18 17:15:05.710' AS DateTime), CAST(N'2021-04-21 00:00:00.000' AS DateTime), CAST(27000000 AS Decimal(18, 0)), NULL)
SET IDENTITY_INSERT [dbo].[bill] OFF
SET IDENTITY_INSERT [dbo].[category] ON 

INSERT [dbo].[category] ([category_id], [category_name]) VALUES (2, N'Giày chạy')
INSERT [dbo].[category] ([category_id], [category_name]) VALUES (4, N'Bóng rổ')
INSERT [dbo].[category] ([category_id], [category_name]) VALUES (5, N'Original')
INSERT [dbo].[category] ([category_id], [category_name]) VALUES (8, N'test')
INSERT [dbo].[category] ([category_id], [category_name]) VALUES (12, N'test')
SET IDENTITY_INSERT [dbo].[category] OFF
INSERT [dbo].[order_detail] ([bill_id], [shoe_id], [size], [price], [quantity]) VALUES (9, 1, 39, CAST(3100000 AS Decimal(18, 0)), 1)
INSERT [dbo].[order_detail] ([bill_id], [shoe_id], [size], [price], [quantity]) VALUES (14, 1, 40, CAST(3100000 AS Decimal(18, 0)), 1)
INSERT [dbo].[order_detail] ([bill_id], [shoe_id], [size], [price], [quantity]) VALUES (14, 1, 41, CAST(3100000 AS Decimal(18, 0)), 2)
INSERT [dbo].[order_detail] ([bill_id], [shoe_id], [size], [price], [quantity]) VALUES (15, 4, 41, CAST(0 AS Decimal(18, 0)), 1)
INSERT [dbo].[order_detail] ([bill_id], [shoe_id], [size], [price], [quantity]) VALUES (15, 16, 15, CAST(0 AS Decimal(18, 0)), 1)
INSERT [dbo].[order_detail] ([bill_id], [shoe_id], [size], [price], [quantity]) VALUES (15, 21, 1, CAST(0 AS Decimal(18, 0)), 2)
INSERT [dbo].[order_detail] ([bill_id], [shoe_id], [size], [price], [quantity]) VALUES (16, 5, 39, CAST(4100000 AS Decimal(18, 0)), 1)
INSERT [dbo].[order_detail] ([bill_id], [shoe_id], [size], [price], [quantity]) VALUES (16, 5, 40, CAST(4100000 AS Decimal(18, 0)), 1)
INSERT [dbo].[order_detail] ([bill_id], [shoe_id], [size], [price], [quantity]) VALUES (16, 5, 41, CAST(4100000 AS Decimal(18, 0)), 1)
INSERT [dbo].[order_detail] ([bill_id], [shoe_id], [size], [price], [quantity]) VALUES (17, 3, 40, CAST(2400000 AS Decimal(18, 0)), 1)
INSERT [dbo].[order_detail] ([bill_id], [shoe_id], [size], [price], [quantity]) VALUES (17, 4, 39, CAST(4500000 AS Decimal(18, 0)), 1)
INSERT [dbo].[order_detail] ([bill_id], [shoe_id], [size], [price], [quantity]) VALUES (17, 4, 40, CAST(4500000 AS Decimal(18, 0)), 2)
INSERT [dbo].[order_detail] ([bill_id], [shoe_id], [size], [price], [quantity]) VALUES (17, 4, 41, CAST(4500000 AS Decimal(18, 0)), 1)
INSERT [dbo].[order_detail] ([bill_id], [shoe_id], [size], [price], [quantity]) VALUES (18, 4, 39, CAST(4500000 AS Decimal(18, 0)), 3)
INSERT [dbo].[order_detail] ([bill_id], [shoe_id], [size], [price], [quantity]) VALUES (18, 4, 40, CAST(4500000 AS Decimal(18, 0)), 1)
INSERT [dbo].[order_detail] ([bill_id], [shoe_id], [size], [price], [quantity]) VALUES (18, 4, 41, CAST(4500000 AS Decimal(18, 0)), 2)
SET IDENTITY_INSERT [dbo].[shoe_info] ON 

INSERT [dbo].[shoe_info] ([shoe_id], [name], [image], [detail], [category_id], [sex], [date], [price]) VALUES (1, N'X9000L4 CYBERPUNK 2077', N'/Content/img/1.jpg', N'cyberpunk 2077 collection', 2, 1, CAST(N'2020-01-01 00:00:00.000' AS DateTime), CAST(3100000 AS Decimal(18, 0)))
INSERT [dbo].[shoe_info] ([shoe_id], [name], [image], [detail], [category_id], [sex], [date], [price]) VALUES (2, N'Giày Alphatorsion', N'/Content/img/2.jpg', N'đây là 1 đôi giày :))', 2, 1, CAST(N'2019-01-01 00:00:00.000' AS DateTime), CAST(2500000 AS Decimal(18, 0)))
INSERT [dbo].[shoe_info] ([shoe_id], [name], [image], [detail], [category_id], [sex], [date], [price]) VALUES (3, N'X9000L3 M', N'/Content/img/3.jpg', N'đôi giày này như 1 đôi giày', 2, 1, CAST(N'2021-01-01 00:00:00.000' AS DateTime), CAST(2400000 AS Decimal(18, 0)))
INSERT [dbo].[shoe_info] ([shoe_id], [name], [image], [detail], [category_id], [sex], [date], [price]) VALUES (4, N'Giày Ultraboost PB', N'/Content/img/4.jpg', N'đây cũng là 1 đôi giày', 2, 1, CAST(N'2019-01-01 00:00:00.000' AS DateTime), CAST(4500000 AS Decimal(18, 0)))
INSERT [dbo].[shoe_info] ([shoe_id], [name], [image], [detail], [category_id], [sex], [date], [price]) VALUES (5, N'Giày Stan Smith', N'/Content/img/5.jpg', N'giày này tên stan smith', 5, 1, CAST(N'2018-01-01 00:00:00.000' AS DateTime), CAST(4100000 AS Decimal(18, 0)))
INSERT [dbo].[shoe_info] ([shoe_id], [name], [image], [detail], [category_id], [sex], [date], [price]) VALUES (6, N'Giày Continental 80', N'/Content/img/6.jpg', N'không có thông tin gì cả', 5, 1, CAST(N'2019-01-01 00:00:00.000' AS DateTime), CAST(3200000 AS Decimal(18, 0)))
INSERT [dbo].[shoe_info] ([shoe_id], [name], [image], [detail], [category_id], [sex], [date], [price]) VALUES (7, N'Giày Day Jogger', N'/Content/img/7.jpg', N'aaaaaaaaaaaaaaaaaaa -.-', 5, 1, CAST(N'2018-01-01 00:00:00.000' AS DateTime), CAST(2500000 AS Decimal(18, 0)))
INSERT [dbo].[shoe_info] ([shoe_id], [name], [image], [detail], [category_id], [sex], [date], [price]) VALUES (8, N'Giày Ultraboost DNA CC_1', N'/Content/img/8.jpg', N'bbbbbbbbbbbb', 2, 2, CAST(N'2018-01-01 00:00:00.000' AS DateTime), CAST(1800000 AS Decimal(18, 0)))
INSERT [dbo].[shoe_info] ([shoe_id], [name], [image], [detail], [category_id], [sex], [date], [price]) VALUES (9, N'Giày UltraBoost 21', N'/Content/img/9.jpg', N'ccccccccccccccccccc', 2, 2, CAST(N'2020-01-01 00:00:00.000' AS DateTime), CAST(3400000 AS Decimal(18, 0)))
INSERT [dbo].[shoe_info] ([shoe_id], [name], [image], [detail], [category_id], [sex], [date], [price]) VALUES (10, N'X9000L3 W', N'/Content/img/10.jpg', N'ddddddddddddddddd', 2, 2, CAST(N'2021-01-01 00:00:00.000' AS DateTime), CAST(4200000 AS Decimal(18, 0)))
INSERT [dbo].[shoe_info] ([shoe_id], [name], [image], [detail], [category_id], [sex], [date], [price]) VALUES (11, N'Giày Superstar Ellure', N'/Content/img/11.jpg', N'eeeeeeeeeee', 5, 2, CAST(N'2018-01-01 00:00:00.000' AS DateTime), CAST(1700000 AS Decimal(18, 0)))
INSERT [dbo].[shoe_info] ([shoe_id], [name], [image], [detail], [category_id], [sex], [date], [price]) VALUES (12, N'Giày Superstar Up', N'/Content/img/12.jpg', N'fffffffffffffffffffffffff', 5, 2, CAST(N'2021-01-01 00:00:00.000' AS DateTime), CAST(3400000 AS Decimal(18, 0)))
INSERT [dbo].[shoe_info] ([shoe_id], [name], [image], [detail], [category_id], [sex], [date], [price]) VALUES (13, N'Giày Stan Smith', N'/Content/img/13.jpg', N'ggggggggggggg', 5, 2, CAST(N'2020-01-01 00:00:00.000' AS DateTime), CAST(2500000 AS Decimal(18, 0)))
INSERT [dbo].[shoe_info] ([shoe_id], [name], [image], [detail], [category_id], [sex], [date], [price]) VALUES (14, N'X9000L2 W', N'/Content/img/14.jpg', N'hhhhhhhhhhhhhhhhhhh', 2, 2, CAST(N'2021-01-01 00:00:00.000' AS DateTime), CAST(2800000 AS Decimal(18, 0)))
INSERT [dbo].[shoe_info] ([shoe_id], [name], [image], [detail], [category_id], [sex], [date], [price]) VALUES (15, N'Giày Originals Flex', N'/Content/img/15.jpg', N'iiiiiiiiiiiiiiiiiiiiiiiiii', 5, 3, CAST(N'2018-01-01 00:00:00.000' AS DateTime), CAST(2700000 AS Decimal(18, 0)))
INSERT [dbo].[shoe_info] ([shoe_id], [name], [image], [detail], [category_id], [sex], [date], [price]) VALUES (16, N'Giày adidas RapidaZen x LEGO®', N'/Content/img/16.jpg', N'jjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjj', 2, 3, CAST(N'2020-01-01 00:00:00.000' AS DateTime), CAST(3100000 AS Decimal(18, 0)))
INSERT [dbo].[shoe_info] ([shoe_id], [name], [image], [detail], [category_id], [sex], [date], [price]) VALUES (19, N'Dép quai ngang Adilette Comfort', N'/Content/img/19.jpg', N'kkkkkkkkkkkkkkkkkkkkk', NULL, 1, CAST(N'2018-01-01 00:00:00.000' AS DateTime), CAST(2900000 AS Decimal(18, 0)))
INSERT [dbo].[shoe_info] ([shoe_id], [name], [image], [detail], [category_id], [sex], [date], [price]) VALUES (21, N'test', N'/Content/img/empty_cart.png', N'test', 8, 1, CAST(N'2021-01-01 00:00:00.000' AS DateTime), CAST(2300000 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[shoe_info] OFF
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (1, 39, 87, 11)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (1, 40, 15, 11)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (1, 41, 45, 2)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (1, 42, 1, 1)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (2, 39, 24, 6)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (2, 40, 87, 5)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (2, 41, 10, 2)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (3, 39, 65, 5)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (3, 40, 10, 5)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (3, 41, 48, 5)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (4, 39, 45, 8)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (4, 40, 48, 6)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (4, 41, 86, 5)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (5, 39, 87, 7)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (5, 40, 83, 9)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (5, 41, 45, 8)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (6, 39, 25, 2)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (6, 40, 45, 1)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (6, 41, 9, 3)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (7, 39, 65, 4)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (7, 40, 34, 8)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (7, 41, 98, 5)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (8, 39, 72, 7)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (8, 40, 28, 6)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (8, 41, 87, 5)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (9, 39, 86, 2)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (9, 40, 15, 8)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (9, 41, 48, 4)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (10, 39, 5, 2)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (10, 40, 65, 5)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (10, 41, 75, 8)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (11, 37, 12, 4)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (12, 37, 21, 6)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (13, 37, 45, 2)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (14, 37, 21, 5)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (15, 15, 82, 7)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (16, 15, 45, 6)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (19, 40, 12, 5)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (21, 1, 56, 6)
INSERT [dbo].[shoes] ([shoe_id], [size], [stock], [sold]) VALUES (21, 2, 34, 4)
ALTER TABLE [dbo].[bill]  WITH CHECK ADD  CONSTRAINT [FK_bill_account] FOREIGN KEY([acc_id])
REFERENCES [dbo].[account] ([acc_id])
GO
ALTER TABLE [dbo].[bill] CHECK CONSTRAINT [FK_bill_account]
GO
ALTER TABLE [dbo].[cart]  WITH CHECK ADD  CONSTRAINT [FK_cart_account] FOREIGN KEY([acc_id])
REFERENCES [dbo].[account] ([acc_id])
GO
ALTER TABLE [dbo].[cart] CHECK CONSTRAINT [FK_cart_account]
GO
ALTER TABLE [dbo].[order_detail]  WITH CHECK ADD  CONSTRAINT [FK_order_detail_bill] FOREIGN KEY([bill_id])
REFERENCES [dbo].[bill] ([bill_id])
GO
ALTER TABLE [dbo].[order_detail] CHECK CONSTRAINT [FK_order_detail_bill]
GO
ALTER TABLE [dbo].[order_detail]  WITH CHECK ADD  CONSTRAINT [FK_order_detail_shoes1] FOREIGN KEY([shoe_id], [size])
REFERENCES [dbo].[shoes] ([shoe_id], [size])
GO
ALTER TABLE [dbo].[order_detail] CHECK CONSTRAINT [FK_order_detail_shoes1]
GO
ALTER TABLE [dbo].[shoe_info]  WITH CHECK ADD  CONSTRAINT [FK_shoe_info_category] FOREIGN KEY([category_id])
REFERENCES [dbo].[category] ([category_id])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[shoe_info] CHECK CONSTRAINT [FK_shoe_info_category]
GO
ALTER TABLE [dbo].[shoes]  WITH CHECK ADD  CONSTRAINT [FK_shoes_shoe_info] FOREIGN KEY([shoe_id])
REFERENCES [dbo].[shoe_info] ([shoe_id])
GO
ALTER TABLE [dbo].[shoes] CHECK CONSTRAINT [FK_shoes_shoe_info]
GO
USE [master]
GO
ALTER DATABASE [test] SET  READ_WRITE 
GO
