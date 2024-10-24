USE [master]
GO
/****** Object:  Database [IngenieriaSoftware]    Script Date: 22/10/2024 10:32:53 p.m. ******/
CREATE DATABASE [IngenieriaSoftware]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'IngenieriaSoftware', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.PCSANTI\MSSQL\DATA\IngenieriaSoftware.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'IngenieriaSoftware_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.PCSANTI\MSSQL\DATA\IngenieriaSoftware_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [IngenieriaSoftware] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [IngenieriaSoftware].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [IngenieriaSoftware] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [IngenieriaSoftware] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [IngenieriaSoftware] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [IngenieriaSoftware] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [IngenieriaSoftware] SET ARITHABORT OFF 
GO
ALTER DATABASE [IngenieriaSoftware] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [IngenieriaSoftware] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [IngenieriaSoftware] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [IngenieriaSoftware] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [IngenieriaSoftware] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [IngenieriaSoftware] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [IngenieriaSoftware] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [IngenieriaSoftware] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [IngenieriaSoftware] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [IngenieriaSoftware] SET  DISABLE_BROKER 
GO
ALTER DATABASE [IngenieriaSoftware] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [IngenieriaSoftware] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [IngenieriaSoftware] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [IngenieriaSoftware] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [IngenieriaSoftware] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [IngenieriaSoftware] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [IngenieriaSoftware] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [IngenieriaSoftware] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [IngenieriaSoftware] SET  MULTI_USER 
GO
ALTER DATABASE [IngenieriaSoftware] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [IngenieriaSoftware] SET DB_CHAINING OFF 
GO
ALTER DATABASE [IngenieriaSoftware] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [IngenieriaSoftware] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [IngenieriaSoftware] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [IngenieriaSoftware] SET QUERY_STORE = OFF
GO
USE [IngenieriaSoftware]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 22/10/2024 10:32:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[DNI] [int] NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Apellido] [nvarchar](50) NOT NULL,
	[Direccion] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](320) NOT NULL,
	[NumTelefono] [nvarchar](50) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[DNI] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Evento]    Script Date: 22/10/2024 10:32:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Evento](
	[CodEvento] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](20) NOT NULL,
	[Fecha] [nvarchar](10) NOT NULL,
	[Hora] [nvarchar](8) NOT NULL,
	[Modulo] [nvarchar](50) NOT NULL,
	[Evento] [nvarchar](100) NOT NULL,
	[Criticidad] [int] NOT NULL,
 CONSTRAINT [PK_Evento] PRIMARY KEY CLUSTERED 
(
	[CodEvento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Factura]    Script Date: 22/10/2024 10:32:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Factura](
	[CodFactura] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [smalldatetime] NOT NULL,
	[PrecioTotal] [int] NOT NULL,
	[MetodoPago] [nvarchar](20) NOT NULL,
	[Banco] [nvarchar](50) NULL,
	[MarcaTarjeta] [nvarchar](30) NULL,
	[TipoTarjeta] [nvarchar](20) NULL,
	[DNI] [int] NOT NULL,
 CONSTRAINT [PK_Factura] PRIMARY KEY CLUSTERED 
(
	[CodFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Familia]    Script Date: 22/10/2024 10:32:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Familia](
	[CodFamilia] [int] NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Tipo] [bit] NOT NULL,
	[CodPermiso] [int] NULL,
	[CodComp] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Item]    Script Date: 22/10/2024 10:32:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Item](
	[ISBN] [nvarchar](17) NOT NULL,
	[CodFactura] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
 CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED 
(
	[ISBN] ASC,
	[CodFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemOrden]    Script Date: 22/10/2024 10:32:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemOrden](
	[ISBN] [nvarchar](17) NOT NULL,
	[CodOrdenCompra] [int] NOT NULL,
	[Cotizacion] [int] NOT NULL,
	[StockCompra] [int] NOT NULL,
	[StockRecepcion] [int] NULL,
	[FechaEntrega] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemSolicitud]    Script Date: 22/10/2024 10:32:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemSolicitud](
	[ISBN] [nvarchar](17) NOT NULL,
	[CodSolicitud] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Libro]    Script Date: 22/10/2024 10:32:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Libro](
	[ISBN] [nvarchar](17) NOT NULL,
	[Autor] [nvarchar](50) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Precio] [int] NOT NULL,
	[Stock] [int] NOT NULL,
	[Activo] [bit] NOT NULL,
	[MaxStock] [int] NOT NULL,
	[MinStock] [int] NOT NULL,
 CONSTRAINT [PK_Libro] PRIMARY KEY CLUSTERED 
(
	[ISBN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Libro_C]    Script Date: 22/10/2024 10:32:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Libro_C](
	[ISBN] [nvarchar](17) NOT NULL,
	[Fecha] [nvarchar](10) NOT NULL,
	[Hora] [nvarchar](8) NOT NULL,
	[Autor] [nvarchar](50) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Precio] [int] NOT NULL,
	[Stock] [int] NOT NULL,
	[Activo] [bit] NOT NULL,
	[MaxStock] [int] NOT NULL,
	[MinStock] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrdenCompra]    Script Date: 22/10/2024 10:32:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrdenCompra](
	[CodOrdenCompra] [int] IDENTITY(1,1) NOT NULL,
	[CUIT] [nvarchar](11) NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[PrecioTotal] [int] NOT NULL,
	[CodFactura] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_OrdenCompra] PRIMARY KEY CLUSTERED 
(
	[CodOrdenCompra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permiso]    Script Date: 22/10/2024 10:32:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permiso](
	[CodPermiso] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Permiso] PRIMARY KEY CLUSTERED 
(
	[CodPermiso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proveedor]    Script Date: 22/10/2024 10:32:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proveedor](
	[CUIT] [nvarchar](11) NOT NULL,
	[RazonSocial] [nvarchar](100) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](320) NOT NULL,
	[NumTelefono] [nvarchar](50) NOT NULL,
	[Direccion] [nvarchar](100) NULL,
	[CuentaBancaria] [nvarchar](34) NULL,
 CONSTRAINT [PK_Proveedor] PRIMARY KEY CLUSTERED 
(
	[CUIT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProveedorSolicitud]    Script Date: 22/10/2024 10:32:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProveedorSolicitud](
	[CUIT] [nvarchar](11) NOT NULL,
	[CodSolicitud] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SolicitudCotizacion]    Script Date: 22/10/2024 10:32:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SolicitudCotizacion](
	[CodSolicitud] [int] IDENTITY(1,1) NOT NULL,
	[FechaEmision] [datetime] NOT NULL,
 CONSTRAINT [PK_SolicitudCotizacion] PRIMARY KEY CLUSTERED 
(
	[CodSolicitud] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 22/10/2024 10:32:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[DNI] [int] NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Apellido] [nvarchar](50) NOT NULL,
	[FechaNac] [date] NOT NULL,
	[Email] [nvarchar](320) NOT NULL,
	[NumTelefono] [nvarchar](50) NOT NULL,
	[Username] [nvarchar](20) NOT NULL,
	[Contraseña] [nvarchar](128) NOT NULL,
	[Rol] [nvarchar](20) NOT NULL,
	[Bloqueado] [bit] NOT NULL,
	[Desactivado] [bit] NOT NULL,
	[IntentosFallidos] [int] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[DNI] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Cliente] ([DNI], [Nombre], [Apellido], [Direccion], [Email], [NumTelefono], [Activo]) VALUES (10000000, N'Thomas', N'Shelby', N'OD1v101YadvlZAawty8Jig==', N'peaky@blinders.com', N'35423524632', 1)
INSERT [dbo].[Cliente] ([DNI], [Nombre], [Apellido], [Direccion], [Email], [NumTelefono], [Activo]) VALUES (10000001, N'sda', N'sa', N'HtulEckGkUVAlLbGNSKXVQ==', N'sad@fasw', N'35223626436', 1)
INSERT [dbo].[Cliente] ([DNI], [Nombre], [Apellido], [Direccion], [Email], [NumTelefono], [Activo]) VALUES (10000002, N'aaa', N'sdaffd', N'AyEx11iByAWDZw/qieSpSg==', N'rhgyyh@ed', N'2464267752', 0)
GO
SET IDENTITY_INSERT [dbo].[Evento] ON 

INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (33, N'a', N'2024-08-18', N'14:51:17', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (34, N'a', N'2024-08-18', N'14:51:21', N'Sesiones', N'Cierre de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (35, N'a', N'2024-08-18', N'14:51:27', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (36, N'a', N'2024-08-18', N'14:51:58', N'Sesiones', N'Cambio de contraseña', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (37, N'a', N'2024-08-18', N'14:52:03', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (38, N'a', N'2024-08-18', N'14:53:31', N'Usuarios', N'Registro de usuario', 2)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (39, N'a', N'2024-08-18', N'14:53:45', N'Usuarios', N'Modificación de usuario', 2)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (40, N'a', N'2024-08-18', N'14:53:51', N'Usuarios', N'Borrado lógico de usuario', 2)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (41, N'a', N'2024-08-18', N'14:53:57', N'Usuarios', N'Restauración de usuario', 2)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (42, N'a', N'2024-08-18', N'14:54:01', N'Usuarios', N'Bloqueo manual de usuario', 2)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (43, N'a', N'2024-08-18', N'14:54:04', N'Usuarios', N'Desbloqueo de usuario', 2)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (44, N'a', N'2024-08-18', N'14:54:09', N'Usuarios', N'Consulta de usuarios', 6)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (45, N'a', N'2024-08-18', N'14:54:56', N'Perfiles', N'Creación de perfil', 2)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (46, N'a', N'2024-08-18', N'14:55:03', N'Perfiles', N'Modificación de perfil', 2)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (47, N'a', N'2024-08-18', N'14:55:05', N'Perfiles', N'Borrado de perfil', 2)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (48, N'a', N'2024-08-18', N'14:55:15', N'Perfiles', N'Creación de familia', 2)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (49, N'a', N'2024-08-18', N'14:55:22', N'Perfiles', N'Modificación de familia', 2)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (50, N'a', N'2024-08-18', N'14:55:24', N'Perfiles', N'Borrado de familia', 2)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (51, N'a', N'2024-08-18', N'14:56:34', N'Libros', N'Registro de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (52, N'a', N'2024-08-18', N'14:56:49', N'Libros', N'Modificación de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (53, N'a', N'2024-08-18', N'14:56:54', N'Libros', N'Borrado de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (54, N'a', N'2024-08-18', N'14:56:57', N'Libros', N'Consulta de libros', 6)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (55, N'a', N'2024-08-18', N'14:57:29', N'Clientes', N'Registro de cliente', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (56, N'a', N'2024-08-18', N'14:57:34', N'Clientes', N'Modificación de cliente', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (57, N'a', N'2024-08-18', N'14:57:37', N'Clientes', N'Eliminación de cliente', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (58, N'a', N'2024-08-18', N'14:57:39', N'Clientes', N'Consulta de clientes', 6)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (59, N'a', N'2024-08-18', N'14:58:03', N'Ventas', N'Cobro de venta', 3)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (60, N'a', N'2024-08-18', N'14:58:55', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (61, N'a', N'2024-08-18', N'14:59:23', N'Ventas', N'Generación de reporte de factura de venta', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (62, N'a', N'2024-08-18', N'15:05:06', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (63, N'a', N'2024-08-18', N'15:05:23', N'Ventas', N'Cobro de venta', 3)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (64, N'a', N'2024-08-18', N'15:07:27', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (65, N'a', N'2024-08-18', N'15:07:40', N'Ventas', N'Cobro de venta', 3)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (66, N'a', N'2024-08-18', N'15:19:09', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (67, N'a', N'2024-08-18', N'15:19:32', N'Ventas', N'Cobro de venta', 3)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (68, N'a', N'2024-08-18', N'15:40:20', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (69, N'a', N'2024-08-18', N'15:41:09', N'Ventas', N'Cobro de venta', 3)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (70, N'a', N'2024-08-18', N'15:41:11', N'Ventas', N'Generación de factura', 3)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (71, N'a', N'2024-08-18', N'16:00:53', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (72, N'a', N'2024-08-18', N'16:23:45', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (73, N'a', N'2024-08-18', N'16:37:32', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (74, N'a', N'2024-08-18', N'16:40:03', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (75, N'a', N'2024-08-18', N'16:40:41', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (76, N'a', N'2024-08-18', N'16:42:08', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (77, N'a', N'2024-08-18', N'16:44:43', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (78, N'a', N'2024-08-18', N'16:45:00', N'Ventas', N'Generación de reporte de factura de venta', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (79, N'a', N'2024-08-18', N'16:45:06', N'Ventas', N'Generación de reporte de factura de venta', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (80, N'a', N'2024-08-18', N'16:45:14', N'Ventas', N'Generación de reporte de factura de venta', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (81, N'a', N'2024-08-18', N'16:45:21', N'Ventas', N'Generación de reporte de factura de venta', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (82, N'a', N'2024-08-18', N'16:45:25', N'Ventas', N'Generación de reporte de factura de venta', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (83, N'a', N'2024-08-18', N'16:45:30', N'Ventas', N'Generación de reporte de factura de venta', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (84, N'a', N'2024-08-18', N'16:46:25', N'Ventas', N'Generación de reporte de factura de venta', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (85, N'a', N'2024-08-18', N'16:46:29', N'Ventas', N'Generación de reporte de factura de venta', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (86, N'a', N'2024-08-18', N'16:46:33', N'Ventas', N'Generación de reporte de factura de venta', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (87, N'a', N'2024-08-18', N'16:46:38', N'Ventas', N'Generación de reporte de factura de venta', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (88, N'a', N'2024-08-18', N'17:03:35', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (89, N'a', N'2024-08-18', N'17:12:35', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (90, N'a', N'2024-08-18', N'17:13:37', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (91, N'a', N'2024-08-18', N'17:16:46', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (92, N'a', N'2024-08-18', N'17:29:58', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (93, N'a', N'2024-08-18', N'17:37:07', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (94, N'a', N'2024-08-18', N'17:41:07', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (95, N'a', N'2024-08-18', N'17:55:57', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (96, N'a', N'2024-08-18', N'17:56:28', N'Perfiles', N'Creación de perfil', 2)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (97, N'a', N'2024-08-18', N'17:56:30', N'Perfiles', N'Borrado de perfil', 2)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (98, N'a', N'2024-08-18', N'19:20:02', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (99, N'a', N'2024-08-18', N'19:20:38', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (100, N'a', N'2024-08-18', N'19:21:44', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (101, N'a', N'2024-08-18', N'19:21:55', N'Ventas', N'Generación de reporte de factura de venta', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (102, N'a', N'2024-08-18', N'19:23:20', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (103, N'a', N'2024-08-18', N'19:24:35', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (104, N'a', N'2024-08-18', N'19:25:26', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (105, N'a', N'2024-08-18', N'19:27:28', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (106, N'a', N'2024-08-18', N'19:28:08', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (107, N'a', N'2024-08-18', N'19:28:46', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (108, N'a', N'2024-08-18', N'19:29:54', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (109, N'a', N'2024-08-18', N'19:33:01', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (110, N'a', N'2024-08-18', N'19:33:36', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (111, N'a', N'2024-08-18', N'19:33:44', N'Usuarios', N'Generación de reporte de evento', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (112, N'a', N'2024-08-18', N'19:33:53', N'Usuarios', N'Generación de reporte de evento', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (113, N'a', N'2024-08-19', N'09:15:36', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (114, N'a', N'2024-08-19', N'09:15:49', N'Perfiles', N'Modificación de perfil', 2)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (115, N'a', N'2024-08-19', N'09:36:42', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (116, N'a', N'2024-08-19', N'09:37:01', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (117, N'a', N'2024-08-19', N'09:38:25', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (118, N'a', N'2024-08-19', N'09:40:07', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (119, N'a', N'2024-08-19', N'09:40:49', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (120, N'a', N'2024-08-20', N'16:03:25', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (121, N'a', N'2024-08-20', N'16:04:10', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (122, N'a', N'2024-08-20', N'19:48:48', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (123, N'a', N'2024-08-20', N'20:47:41', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (124, N'a', N'2024-08-20', N'20:48:32', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (125, N'a', N'2024-08-20', N'20:49:11', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (126, N'a', N'2024-08-20', N'20:50:11', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (127, N'a', N'2024-08-20', N'20:52:19', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (128, N'a', N'2024-08-20', N'20:52:53', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (129, N'a', N'2024-08-20', N'20:58:06', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (130, N'a', N'2024-08-20', N'21:05:22', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (131, N'a', N'2024-08-20', N'21:05:46', N'Usuarios', N'Generación de reporte de evento', 5)
GO
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (132, N'a', N'2024-08-20', N'21:08:32', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (133, N'a', N'2024-08-20', N'21:08:53', N'Usuarios', N'Generación de reporte de evento', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (134, N'a', N'2024-08-20', N'21:09:56', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (135, N'a', N'2024-08-20', N'21:10:08', N'Usuarios', N'Generación de reporte de evento', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (136, N'a', N'2024-08-20', N'21:15:00', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (137, N'a', N'2024-08-20', N'21:15:11', N'Usuarios', N'Generación de reporte de evento', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (138, N'a', N'2024-08-23', N'15:33:35', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (139, N'a', N'2024-08-23', N'15:34:45', N'Ventas', N'Cobro de venta', 3)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (140, N'a', N'2024-08-23', N'16:51:27', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (141, N'a', N'2024-08-26', N'11:27:34', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (142, N'a', N'2024-08-26', N'11:28:03', N'Libros', N'Modificación de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (143, N'a', N'2024-08-26', N'11:29:01', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (144, N'a', N'2024-08-26', N'11:30:10', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (145, N'a', N'2024-08-26', N'11:31:31', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (146, N'a', N'2024-08-26', N'11:33:38', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (147, N'a', N'2024-08-26', N'11:33:51', N'Libros', N'Modificación de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (148, N'a', N'2024-08-26', N'11:34:05', N'Libros', N'Modificación de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (149, N'a', N'2024-08-26', N'11:34:21', N'Ventas', N'Cobro de venta', 3)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (150, N'a', N'2024-08-26', N'11:34:23', N'Ventas', N'Generación de factura', 3)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (151, N'a', N'2024-08-26', N'11:34:33', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (152, N'a', N'2024-08-26', N'11:35:01', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (153, N'a', N'2024-08-26', N'11:35:59', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (154, N'a', N'2024-08-26', N'11:40:06', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (155, N'a', N'2024-08-26', N'11:40:38', N'Libros', N'Registro de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (156, N'a', N'2024-08-26', N'11:43:12', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (157, N'a', N'2024-08-26', N'13:00:52', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (158, N'a', N'2024-08-26', N'13:01:06', N'Libros', N'Restauración de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (159, N'a', N'2024-08-26', N'13:01:12', N'Libros', N'Consulta de libros', 6)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (160, N'a', N'2024-08-26', N'13:01:18', N'Libros', N'Restauración de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (161, N'a', N'2024-08-26', N'13:02:09', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (162, N'a', N'2024-08-26', N'13:02:17', N'Libros', N'Borrado lógico de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (163, N'a', N'2024-08-26', N'13:06:53', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (164, N'a', N'2024-08-26', N'13:07:46', N'Libros', N'Borrado lógico de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (165, N'a', N'2024-08-26', N'13:13:36', N'Libros', N'Borrado lógico de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (166, N'a', N'2024-08-26', N'13:15:41', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (167, N'a', N'2024-08-26', N'13:15:51', N'Libros', N'Borrado lógico de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (168, N'a', N'2024-08-26', N'13:22:22', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (169, N'a', N'2024-08-26', N'13:22:29', N'Libros', N'Restauración de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (170, N'a', N'2024-08-26', N'13:22:45', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (171, N'a', N'2024-08-26', N'13:22:50', N'Libros', N'Restauración de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (172, N'a', N'2024-08-26', N'13:35:39', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (173, N'a', N'2024-08-26', N'13:37:46', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (174, N'a', N'2024-08-26', N'13:37:52', N'Libros', N'Borrado lógico de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (175, N'a', N'2024-08-26', N'13:37:55', N'Libros', N'Consulta de libros', 6)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (176, N'a', N'2024-08-26', N'13:38:00', N'Libros', N'Restauración de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (177, N'a', N'2024-08-26', N'13:43:41', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (178, N'a', N'2024-08-26', N'13:45:32', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (179, N'a', N'2024-08-26', N'13:47:18', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (180, N'a', N'2024-08-26', N'13:48:42', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (181, N'a', N'2024-08-26', N'13:51:32', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (182, N'a', N'2024-08-26', N'16:34:59', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (183, N'a', N'2024-08-26', N'16:39:47', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (184, N'a', N'2024-08-26', N'16:39:52', N'Libros', N'Actualización de estado de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (185, N'a', N'2024-08-26', N'16:40:23', N'Libros', N'Actualización de estado de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (186, N'a', N'2024-08-26', N'16:43:23', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (187, N'a', N'2024-08-26', N'16:43:42', N'Libros', N'Actualización de estado de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (188, N'a', N'2024-08-27', N'17:48:00', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (189, N'a', N'2024-08-27', N'17:48:36', N'Libros', N'Actualización de estado de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (190, N'a', N'2024-08-27', N'17:54:56', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (191, N'a', N'2024-08-27', N'17:56:00', N'Libros', N'Actualización de estado de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (192, N'a', N'2024-08-27', N'19:16:04', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (193, N'a', N'2024-08-28', N'00:06:36', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (194, N'a', N'2024-08-28', N'15:36:32', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (196, N'a', N'2024-08-28', N'15:50:07', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (197, N'a', N'2024-08-28', N'15:57:31', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (198, N'a', N'2024-08-28', N'15:57:40', N'Libros', N'Actualización de estado de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (199, N'a', N'2024-08-28', N'15:59:32', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (200, N'a', N'2024-08-28', N'16:26:16', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (201, N'a', N'2024-08-28', N'16:26:43', N'Perfiles', N'Modificación de perfil', 2)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (202, N'a', N'2024-08-28', N'16:26:49', N'Perfiles', N'Modificación de perfil', 2)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (203, N'a', N'2024-08-28', N'16:27:46', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (204, N'a', N'2024-08-28', N'16:29:56', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (205, N'a', N'2024-08-28', N'16:31:47', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (206, N'a', N'2024-08-28', N'16:33:26', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (207, N'a', N'2024-08-28', N'16:41:27', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (208, N'a', N'2024-08-28', N'16:41:58', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (209, N'a', N'2024-08-28', N'16:43:14', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (210, N'a', N'2024-08-28', N'16:45:06', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (211, N'a', N'2024-08-31', N'16:50:05', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (212, N'a', N'2024-08-31', N'16:56:34', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (213, N'a', N'2024-08-31', N'16:56:53', N'Clientes', N'Borrado lógico de cliente', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (214, N'a', N'2024-08-31', N'16:57:11', N'Clientes', N'Restauración de cliente', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (215, N'a', N'2024-08-31', N'16:57:19', N'Clientes', N'Borrado lógico de cliente', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (216, N'a', N'2024-08-31', N'16:59:22', N'Libros', N'Restauración de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (217, N'a', N'2024-08-31', N'17:16:09', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (218, N'a', N'2024-08-31', N'17:16:36', N'Usuarios', N'Generación de reporte de evento', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (219, N'a', N'2024-08-31', N'17:33:19', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (220, N'a', N'2024-08-31', N'17:33:41', N'Libros', N'Modificación de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (221, N'a', N'2024-08-31', N'17:34:37', N'Ventas', N'Cobro de venta', 3)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (222, N'a', N'2024-08-31', N'17:34:38', N'Ventas', N'Generación de factura', 3)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (223, N'a', N'2024-08-31', N'17:35:10', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (224, N'a', N'2024-08-31', N'17:35:25', N'Perfiles', N'Modificación de perfil', 2)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (225, N'a', N'2024-08-31', N'17:35:46', N'Perfiles', N'Modificación de perfil', 2)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (226, N'a', N'2024-08-31', N'17:36:28', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (227, N'a', N'2024-08-31', N'19:43:01', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (228, N'a', N'2024-08-31', N'19:43:48', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (229, N'a', N'2024-08-31', N'19:45:40', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (230, N'a', N'2024-08-31', N'19:45:54', N'Base de datos', N'Respaldo', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (231, N'a', N'2024-08-31', N'19:47:56', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (232, N'a', N'2024-08-31', N'19:51:25', N'Sesiones', N'Inicio de sesión', 1)
GO
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (233, N'a', N'2024-08-31', N'19:52:23', N'Base de datos', N'Restauración', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (234, N'a', N'2024-08-31', N'20:52:14', N'Base de datos', N'Restauración', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (235, N'a', N'2024-08-31', N'20:54:00', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (236, N'a', N'2024-08-31', N'20:56:52', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (237, N'a', N'2024-08-31', N'20:59:04', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (238, N'a', N'2024-08-31', N'21:00:48', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (239, N'a', N'2024-08-31', N'21:01:13', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (240, N'a', N'2024-08-31', N'23:16:12', N'Base de datos', N'Restauración', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (241, N'a', N'2024-09-03', N'18:23:41', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (242, N'a', N'2024-09-08', N'17:26:57', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (243, N'a', N'2024-09-09', N'10:35:03', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (244, N'a', N'2024-09-09', N'10:42:12', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (245, N'a', N'2024-09-09', N'10:45:50', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (246, N'a', N'2024-09-09', N'10:50:40', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (247, N'a', N'2024-09-09', N'10:51:32', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (248, N'a', N'2024-09-09', N'10:57:08', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (249, N'a', N'2024-09-09', N'11:10:40', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (250, N'a', N'2024-09-09', N'11:11:59', N'Usuarios', N'Generación de reporte de evento', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (251, N'a', N'2024-09-09', N'11:13:49', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (252, N'a', N'2024-09-09', N'11:15:38', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (253, N'a', N'2024-09-09', N'11:17:14', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (254, N'a', N'2024-09-09', N'12:03:25', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (255, N'a', N'2024-09-09', N'12:06:52', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (256, N'a', N'2024-09-09', N'12:14:29', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (257, N'a', N'2024-09-09', N'12:18:22', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (258, N'a', N'2024-09-09', N'12:21:15', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (259, N'a', N'2024-09-09', N'12:26:10', N'Clientes', N'Consulta de clientes', 6)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (260, N'a', N'2024-09-09', N'12:26:21', N'Clientes', N'Modificación de cliente', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (261, N'a', N'2024-09-09', N'12:26:31', N'Clientes', N'Consulta de clientes', 6)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (262, N'a', N'2024-09-09', N'12:27:13', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (263, N'a', N'2024-09-09', N'12:27:38', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (264, N'a', N'2024-09-09', N'12:30:51', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (265, N'a', N'2024-09-09', N'15:25:55', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (266, N'a', N'2024-09-16', N'18:36:45', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (267, N'a', N'2024-09-17', N'21:59:58', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (268, N'a', N'2024-09-17', N'22:00:13', N'Clientes', N'Serialización XML de clientes', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (269, N'a', N'2024-09-17', N'22:00:22', N'Clientes', N'Modificación de cliente', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (270, N'a', N'2024-09-17', N'22:00:37', N'Clientes', N'Des-serialización XML de clientes', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (271, N'a', N'2024-09-17', N'22:02:27', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (272, N'a', N'2024-09-17', N'22:03:20', N'Ventas', N'Generación de reporte de factura de venta', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (273, N'a', N'2024-09-17', N'22:06:34', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (274, N'a', N'2024-09-17', N'22:07:04', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (275, N'a', N'2024-09-17', N'22:11:48', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (276, N'a', N'2024-09-17', N'22:11:57', N'Clientes', N'Des-serialización XML de clientes', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (277, N'a', N'2024-09-17', N'22:12:36', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (278, N'a', N'2024-09-17', N'22:16:18', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (279, N'a', N'2024-09-17', N'22:18:47', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (280, N'a', N'2024-09-17', N'22:19:47', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (281, N'a', N'2024-09-17', N'22:28:17', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (282, N'a', N'2024-09-17', N'22:28:23', N'Clientes', N'Des-serialización XML de clientes', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (283, N'a', N'2024-09-17', N'22:29:07', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (284, N'a', N'2024-09-17', N'22:29:33', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (285, N'a', N'2024-09-17', N'22:30:03', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (286, N'a', N'2024-09-17', N'22:30:18', N'Clientes', N'Des-serialización XML de clientes', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (287, N'a', N'2024-09-20', N'14:42:47', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (288, N'a', N'2024-09-20', N'16:30:19', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (289, N'a', N'2024-09-20', N'16:30:29', N'Clientes', N'Serialización XML de clientes', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (290, N'a', N'2024-09-20', N'16:30:38', N'Clientes', N'Des-serialización XML de clientes', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (291, N'a', N'2024-09-20', N'16:30:50', N'Clientes', N'Des-serialización XML de clientes', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (292, N'a', N'2024-09-20', N'16:30:56', N'Clientes', N'Serialización XML de clientes', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (293, N'a', N'2024-09-20', N'16:31:09', N'Clientes', N'Des-serialización XML de clientes', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (294, N'a', N'2024-09-21', N'14:51:26', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (295, N'a', N'2024-09-21', N'15:15:40', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (296, N'a', N'2024-09-21', N'15:16:47', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (297, N'a', N'2024-09-21', N'15:17:50', N'Libros', N'Registro de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (298, N'a', N'2024-09-21', N'15:18:19', N'Libros', N'Modificación de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (299, N'a', N'2024-09-21', N'15:18:27', N'Libros', N'Borrado lógico de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (300, N'a', N'2024-09-21', N'15:18:34', N'Libros', N'Restauración de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (301, N'a', N'2024-09-21', N'15:19:39', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (302, N'a', N'2024-09-21', N'15:22:21', N'Libros', N'Borrado lógico de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (303, N'a', N'2024-09-21', N'15:22:56', N'Libros', N'Restauración de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (304, N'a', N'2024-09-21', N'15:33:20', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (305, N'a', N'2024-09-26', N'10:29:54', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (306, N'a', N'2024-09-26', N'10:30:27', N'Clientes', N'Serialización XML de clientes', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (307, N'a', N'2024-09-26', N'10:32:02', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (308, N'a', N'2024-09-26', N'10:32:51', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (309, N'a', N'2024-09-26', N'10:33:10', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (310, N'a', N'2024-09-26', N'10:33:52', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (311, N'a', N'2024-09-26', N'10:35:28', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (312, N'a', N'2024-09-26', N'10:54:18', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (313, N'a', N'2024-09-26', N'10:54:56', N'Perfiles', N'Modificación de perfil', 2)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (314, N'a', N'2024-09-26', N'10:59:14', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (315, N'a', N'2024-09-26', N'11:07:51', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (316, N'a', N'2024-09-26', N'11:17:04', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (317, N'a', N'2024-09-26', N'11:20:07', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (318, N'a', N'2024-09-26', N'11:20:47', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (319, N'a', N'2024-09-26', N'11:22:26', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (320, N'a', N'2024-09-26', N'11:24:15', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (321, N'a', N'2024-09-26', N'11:40:14', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (322, N'a', N'2024-09-26', N'11:58:29', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (323, N'a', N'2024-09-27', N'14:19:00', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (324, N'a', N'2024-09-27', N'14:43:18', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (325, N'a', N'2024-09-27', N'14:47:48', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (326, N'a', N'2024-09-27', N'14:48:40', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (327, N'a', N'2024-09-27', N'14:48:47', N'Perfiles', N'Modificación de familia', 2)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (328, N'a', N'2024-09-27', N'14:49:41', N'Perfiles', N'Modificación de familia', 2)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (329, N'a', N'2024-09-27', N'15:02:15', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (330, N'a', N'2024-09-27', N'15:08:57', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (331, N'a', N'2024-09-27', N'15:09:11', N'Perfiles', N'Modificación de familia', 2)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (332, N'a', N'2024-09-27', N'15:09:19', N'Perfiles', N'Modificación de familia', 2)
GO
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (333, N'a', N'2024-09-27', N'15:09:31', N'Perfiles', N'Modificación de familia', 2)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (334, N'a', N'2024-09-27', N'15:09:37', N'Perfiles', N'Modificación de familia', 2)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (335, N'a', N'2024-09-27', N'15:32:18', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (336, N'a', N'2024-09-27', N'15:32:29', N'Perfiles', N'Modificación de familia', 2)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (337, N'a', N'2024-10-01', N'15:21:10', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (338, N'a', N'2024-10-01', N'15:21:42', N'Clientes', N'Serialización XML de clientes', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (339, N'a', N'2024-10-01', N'15:21:46', N'Clientes', N'Des-serialización XML de clientes', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (340, N'a', N'2024-10-01', N'16:29:23', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (341, N'a', N'2024-10-01', N'16:32:08', N'Usuarios', N'Registro de usuario', 2)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (342, N'a', N'2024-10-01', N'16:33:17', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (343, N'a', N'2024-10-01', N'16:33:53', N'Perfiles', N'Creación de familia', 2)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (344, N'a', N'2024-10-01', N'16:34:09', N'Perfiles', N'Modificación de familia', 2)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (345, N'a', N'2024-10-01', N'16:34:45', N'Perfiles', N'Modificación de familia', 2)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (346, N'a', N'2024-10-01', N'16:47:53', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (347, N'a', N'2024-10-01', N'16:54:01', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (348, N'a', N'2024-10-01', N'16:55:56', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (349, N'a', N'2024-10-01', N'16:58:23', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (350, N'a', N'2024-10-01', N'17:00:35', N'Perfiles', N'Modificación de familia', 2)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (351, N'a', N'2024-10-01', N'17:01:58', N'Sesiones', N'Cierre de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (352, N'BUU', N'2024-10-01', N'17:02:10', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (353, N'BUU', N'2024-10-01', N'17:04:50', N'Sesiones', N'Cierre de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (354, N'a', N'2024-10-01', N'17:05:01', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (355, N'a', N'2024-10-01', N'17:26:45', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (356, N'a', N'2024-10-01', N'17:27:04', N'Sesiones', N'Cierre de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (357, N'BUU', N'2024-10-01', N'17:27:17', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (358, N'BUU', N'2024-10-01', N'17:27:34', N'Sesiones', N'Cierre de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (359, N'a', N'2024-10-01', N'17:27:40', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (360, N'BUU', N'2024-10-01', N'17:30:53', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (361, N'a', N'2024-10-01', N'18:48:19', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (362, N'a', N'2024-10-01', N'19:21:50', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (363, N'a', N'2024-10-01', N'19:22:04', N'Clientes', N'Serialización XML de clientes', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (364, N'a', N'2024-10-01', N'19:22:17', N'Clientes', N'Modificación de cliente', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (365, N'a', N'2024-10-01', N'19:22:21', N'Clientes', N'Des-serialización XML de clientes', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (366, N'a', N'2024-10-01', N'19:23:57', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (367, N'a', N'2024-10-01', N'19:24:04', N'Clientes', N'Des-serialización XML de clientes', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (368, N'a', N'2024-10-01', N'19:24:38', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (369, N'a', N'2024-10-01', N'19:24:44', N'Clientes', N'Des-serialización XML de clientes', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (370, N'a', N'2024-10-01', N'19:25:21', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (371, N'a', N'2024-10-01', N'19:25:26', N'Clientes', N'Des-serialización XML de clientes', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (372, N'a', N'2024-10-01', N'19:35:49', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (373, N'a', N'2024-10-01', N'19:36:41', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (374, N'a', N'2024-10-01', N'19:40:21', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (375, N'a', N'2024-10-01', N'21:24:16', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (376, N'a', N'2024-10-01', N'21:27:54', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (377, N'a', N'2024-10-01', N'21:29:09', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (378, N'a', N'2024-10-01', N'21:29:54', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (379, N'a', N'2024-10-01', N'21:31:28', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (380, N'a', N'2024-10-01', N'21:32:33', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (381, N'a', N'2024-10-01', N'21:33:38', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (382, N'a', N'2024-10-01', N'21:48:09', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (383, N'a', N'2024-10-01', N'21:49:28', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (384, N'a', N'2024-10-01', N'21:51:19', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (385, N'a', N'2024-10-01', N'22:40:40', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (386, N'a', N'2024-10-01', N'22:44:38', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (387, N'a', N'2024-10-01', N'23:45:54', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (388, N'a', N'2024-10-01', N'23:46:40', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (389, N'a', N'2024-10-01', N'23:47:13', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (390, N'a', N'2024-10-01', N'23:48:42', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (391, N'a', N'2024-10-01', N'23:48:53', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (392, N'a', N'2024-10-01', N'23:51:32', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (393, N'a', N'2024-10-15', N'14:41:31', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (394, N'a', N'2024-10-15', N'18:15:16', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (395, N'a', N'2024-10-15', N'18:15:35', N'Clientes', N'Serialización XML de clientes', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (396, N'a', N'2024-10-15', N'18:15:38', N'Clientes', N'Des-serialización XML de clientes', 5)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (397, N'a', N'2024-10-17', N'14:45:52', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (398, N'a', N'2024-10-17', N'14:52:58', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (399, N'a', N'2024-10-17', N'14:53:17', N'Libros', N'Borrado lógico de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (400, N'a', N'2024-10-17', N'14:53:29', N'Libros', N'Restauración de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (401, N'a', N'2024-10-17', N'14:53:53', N'Libros', N'Modificación de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (402, N'a', N'2024-10-17', N'14:56:38', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (403, N'a', N'2024-10-17', N'14:57:21', N'Libros', N'Modificación de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (404, N'a', N'2024-10-17', N'15:38:13', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (405, N'a', N'2024-10-17', N'15:38:38', N'Libros', N'Modificación de libro', 4)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (406, N'a', N'2024-10-17', N'16:18:03', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (407, N'a', N'2024-10-17', N'16:36:52', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (408, N'a', N'2024-10-17', N'16:39:16', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (409, N'a', N'2024-10-17', N'16:41:45', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (410, N'a', N'2024-10-17', N'16:45:23', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (411, N'a', N'2024-10-17', N'17:30:24', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (412, N'a', N'2024-10-17', N'17:31:21', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (413, N'a', N'2024-10-17', N'17:31:49', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (414, N'a', N'2024-10-17', N'17:35:38', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (415, N'a', N'2024-10-22', N'14:55:37', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (416, N'a', N'2024-10-22', N'14:58:24', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (417, N'a', N'2024-10-22', N'15:01:39', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (418, N'a', N'2024-10-22', N'15:14:53', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (419, N'a', N'2024-10-22', N'15:15:48', N'Ventas', N'Generación de factura', 3)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (420, N'a', N'2024-10-22', N'15:40:42', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (421, N'a', N'2024-10-22', N'15:44:07', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (422, N'a', N'2024-10-22', N'17:02:30', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (423, N'a', N'2024-10-22', N'17:03:58', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (424, N'a', N'2024-10-22', N'17:05:10', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (425, N'a', N'2024-10-22', N'17:05:39', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (426, N'a', N'2024-10-22', N'17:06:43', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (427, N'a', N'2024-10-22', N'17:09:26', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (428, N'a', N'2024-10-22', N'17:14:18', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (429, N'a', N'2024-10-22', N'17:18:49', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (430, N'a', N'2024-10-22', N'17:19:54', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (431, N'a', N'2024-10-22', N'19:47:32', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (432, N'a', N'2024-10-22', N'20:13:20', N'Sesiones', N'Inicio de sesión', 1)
GO
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (433, N'a', N'2024-10-22', N'20:13:51', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (434, N'a', N'2024-10-22', N'20:15:16', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (435, N'a', N'2024-10-22', N'20:15:49', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (436, N'a', N'2024-10-22', N'20:16:47', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (437, N'a', N'2024-10-22', N'20:17:38', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (438, N'a', N'2024-10-22', N'20:19:15', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (439, N'a', N'2024-10-22', N'20:19:57', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (440, N'a', N'2024-10-22', N'20:32:13', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (441, N'a', N'2024-10-22', N'20:33:07', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (442, N'a', N'2024-10-22', N'20:33:55', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (443, N'a', N'2024-10-22', N'20:34:14', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (444, N'a', N'2024-10-22', N'20:34:54', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (445, N'a', N'2024-10-22', N'20:39:58', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (446, N'a', N'2024-10-22', N'20:41:02', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (447, N'a', N'2024-10-22', N'20:41:38', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (448, N'a', N'2024-10-22', N'20:47:11', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (449, N'a', N'2024-10-22', N'20:47:56', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (450, N'a', N'2024-10-22', N'21:15:52', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (451, N'a', N'2024-10-22', N'21:16:32', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (452, N'a', N'2024-10-22', N'21:17:12', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (453, N'a', N'2024-10-22', N'21:17:55', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (454, N'a', N'2024-10-22', N'21:25:02', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (455, N'a', N'2024-10-22', N'22:02:44', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (456, N'a', N'2024-10-22', N'22:04:54', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (457, N'a', N'2024-10-22', N'22:10:34', N'Sesiones', N'Inicio de sesión', 1)
INSERT [dbo].[Evento] ([CodEvento], [Login], [Fecha], [Hora], [Modulo], [Evento], [Criticidad]) VALUES (458, N'a', N'2024-10-22', N'22:21:54', N'Sesiones', N'Inicio de sesión', 1)
SET IDENTITY_INSERT [dbo].[Evento] OFF
GO
SET IDENTITY_INSERT [dbo].[Factura] ON 

INSERT [dbo].[Factura] ([CodFactura], [Fecha], [PrecioTotal], [MetodoPago], [Banco], [MarcaTarjeta], [TipoTarjeta], [DNI]) VALUES (4, CAST(N'2024-05-06T23:57:00' AS SmallDateTime), 11000, N'Efectivo', NULL, NULL, NULL, 10000000)
INSERT [dbo].[Factura] ([CodFactura], [Fecha], [PrecioTotal], [MetodoPago], [Banco], [MarcaTarjeta], [TipoTarjeta], [DNI]) VALUES (6, CAST(N'2024-06-06T00:20:00' AS SmallDateTime), 5000, N'Efectivo', NULL, NULL, NULL, 10000000)
INSERT [dbo].[Factura] ([CodFactura], [Fecha], [PrecioTotal], [MetodoPago], [Banco], [MarcaTarjeta], [TipoTarjeta], [DNI]) VALUES (13, CAST(N'2024-06-19T17:17:00' AS SmallDateTime), 15000, N'Efectivo', NULL, NULL, NULL, 10000000)
INSERT [dbo].[Factura] ([CodFactura], [Fecha], [PrecioTotal], [MetodoPago], [Banco], [MarcaTarjeta], [TipoTarjeta], [DNI]) VALUES (14, CAST(N'2024-06-19T17:18:00' AS SmallDateTime), 34000, N'Crédito', N'423', N'432', N'324', 10000000)
INSERT [dbo].[Factura] ([CodFactura], [Fecha], [PrecioTotal], [MetodoPago], [Banco], [MarcaTarjeta], [TipoTarjeta], [DNI]) VALUES (15, CAST(N'2024-06-19T17:22:00' AS SmallDateTime), 16500, N'Débito', N'545', N'54645', N'46456', 10000000)
INSERT [dbo].[Factura] ([CodFactura], [Fecha], [PrecioTotal], [MetodoPago], [Banco], [MarcaTarjeta], [TipoTarjeta], [DNI]) VALUES (1007, CAST(N'2024-06-22T19:15:00' AS SmallDateTime), 12000, N'Efectivo', NULL, NULL, NULL, 10000000)
INSERT [dbo].[Factura] ([CodFactura], [Fecha], [PrecioTotal], [MetodoPago], [Banco], [MarcaTarjeta], [TipoTarjeta], [DNI]) VALUES (1008, CAST(N'2024-06-26T18:05:00' AS SmallDateTime), 5500, N'Efectivo', NULL, NULL, NULL, 10000000)
INSERT [dbo].[Factura] ([CodFactura], [Fecha], [PrecioTotal], [MetodoPago], [Banco], [MarcaTarjeta], [TipoTarjeta], [DNI]) VALUES (1009, CAST(N'2024-06-26T21:00:00' AS SmallDateTime), 56000, N'Crédito', N'asdfasw', N'dsqfw', N'ghgi', 10000000)
INSERT [dbo].[Factura] ([CodFactura], [Fecha], [PrecioTotal], [MetodoPago], [Banco], [MarcaTarjeta], [TipoTarjeta], [DNI]) VALUES (1010, CAST(N'2024-05-07T00:00:00' AS SmallDateTime), 5500, N'Crédito', N'11', N'22', N'212', 10000000)
INSERT [dbo].[Factura] ([CodFactura], [Fecha], [PrecioTotal], [MetodoPago], [Banco], [MarcaTarjeta], [TipoTarjeta], [DNI]) VALUES (1011, CAST(N'2024-05-07T00:00:00' AS SmallDateTime), 6500, N'Efectivo', NULL, NULL, NULL, 10000000)
INSERT [dbo].[Factura] ([CodFactura], [Fecha], [PrecioTotal], [MetodoPago], [Banco], [MarcaTarjeta], [TipoTarjeta], [DNI]) VALUES (1012, CAST(N'2024-09-07T00:00:00' AS SmallDateTime), 6500, N'Crédito', N'3', N'3', N'3', 10000000)
INSERT [dbo].[Factura] ([CodFactura], [Fecha], [PrecioTotal], [MetodoPago], [Banco], [MarcaTarjeta], [TipoTarjeta], [DNI]) VALUES (1013, CAST(N'2024-10-07T00:00:00' AS SmallDateTime), 6500, N'Débito', N'21321', N'3213', N'213', 10000001)
INSERT [dbo].[Factura] ([CodFactura], [Fecha], [PrecioTotal], [MetodoPago], [Banco], [MarcaTarjeta], [TipoTarjeta], [DNI]) VALUES (1016, CAST(N'2024-08-18T15:41:00' AS SmallDateTime), 13000, N'Efectivo', NULL, NULL, NULL, 10000000)
INSERT [dbo].[Factura] ([CodFactura], [Fecha], [PrecioTotal], [MetodoPago], [Banco], [MarcaTarjeta], [TipoTarjeta], [DNI]) VALUES (1017, CAST(N'2024-08-26T11:34:00' AS SmallDateTime), 3500, N'Efectivo', NULL, NULL, NULL, 10000000)
INSERT [dbo].[Factura] ([CodFactura], [Fecha], [PrecioTotal], [MetodoPago], [Banco], [MarcaTarjeta], [TipoTarjeta], [DNI]) VALUES (1018, CAST(N'2024-08-31T17:35:00' AS SmallDateTime), 16500, N'Efectivo', NULL, NULL, NULL, 10000000)
INSERT [dbo].[Factura] ([CodFactura], [Fecha], [PrecioTotal], [MetodoPago], [Banco], [MarcaTarjeta], [TipoTarjeta], [DNI]) VALUES (1019, CAST(N'2024-10-22T15:16:00' AS SmallDateTime), 39000, N'Crédito', N'Patagonia', N'VISA', N'Oro', 10000000)
SET IDENTITY_INSERT [dbo].[Factura] OFF
GO
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (101, N'Admin', 1, 1, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (101, N'Admin', 1, 2, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (101, N'Admin', 1, 3, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (101, N'Admin', 1, 4, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (101, N'Admin', 1, 5, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (101, N'Admin', 1, 6, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (101, N'Admin', 1, 7, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (101, N'Admin', 1, 8, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (100, N'Vendedor1', 0, NULL, 104)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (103, N'Administrador', 0, 1, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (103, N'Administrador', 0, 2, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (103, N'Administrador', 0, 3, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (103, N'Administrador', 0, 4, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (103, N'Administrador', 0, 5, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (103, N'Administrador', 0, 6, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (103, N'Administrador', 0, 7, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (101, N'Admin', 1, 9, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (101, N'Admin', 1, 10, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (101, N'Admin', 1, 11, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (101, N'Admin', 1, 12, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (101, N'Admin', 1, 13, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (102, N'Vendedor', 1, NULL, 100)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (105, N'Vendedor2', 0, 5, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (104, N'Comprador1', 0, 6, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (104, N'Comprador1', 0, 3, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (104, N'Comprador1', 0, NULL, 105)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (105, N'Vendedor2', 0, 10, NULL)
GO
INSERT [dbo].[Item] ([ISBN], [CodFactura], [Cantidad]) VALUES (N'09788417292768', 4, 2)
INSERT [dbo].[Item] ([ISBN], [CodFactura], [Cantidad]) VALUES (N'09788417292768', 6, 1)
INSERT [dbo].[Item] ([ISBN], [CodFactura], [Cantidad]) VALUES (N'09788417292768', 13, 3)
INSERT [dbo].[Item] ([ISBN], [CodFactura], [Cantidad]) VALUES (N'09788417292768', 14, 1)
INSERT [dbo].[Item] ([ISBN], [CodFactura], [Cantidad]) VALUES (N'09788417356330', 14, 2)
INSERT [dbo].[Item] ([ISBN], [CodFactura], [Cantidad]) VALUES (N'09788417356330', 15, 3)
INSERT [dbo].[Item] ([ISBN], [CodFactura], [Cantidad]) VALUES (N'09788417356330', 1008, 1)
INSERT [dbo].[Item] ([ISBN], [CodFactura], [Cantidad]) VALUES (N'09788417356330', 1009, 1)
INSERT [dbo].[Item] ([ISBN], [CodFactura], [Cantidad]) VALUES (N'09788417356330', 1010, 1)
INSERT [dbo].[Item] ([ISBN], [CodFactura], [Cantidad]) VALUES (N'09788417356934', 14, 3)
INSERT [dbo].[Item] ([ISBN], [CodFactura], [Cantidad]) VALUES (N'09788417356934', 1007, 2)
INSERT [dbo].[Item] ([ISBN], [CodFactura], [Cantidad]) VALUES (N'09788417356934', 1009, 4)
INSERT [dbo].[Item] ([ISBN], [CodFactura], [Cantidad]) VALUES (N'09788417356934', 1018, 1)
INSERT [dbo].[Item] ([ISBN], [CodFactura], [Cantidad]) VALUES (N'09788417356934', 1019, 2)
INSERT [dbo].[Item] ([ISBN], [CodFactura], [Cantidad]) VALUES (N'09788417490751', 1009, 3)
INSERT [dbo].[Item] ([ISBN], [CodFactura], [Cantidad]) VALUES (N'09788417490751', 1019, 6)
INSERT [dbo].[Item] ([ISBN], [CodFactura], [Cantidad]) VALUES (N'09788417537401', 1009, 2)
INSERT [dbo].[Item] ([ISBN], [CodFactura], [Cantidad]) VALUES (N'09788417537401', 1011, 1)
INSERT [dbo].[Item] ([ISBN], [CodFactura], [Cantidad]) VALUES (N'09788417537401', 1012, 1)
INSERT [dbo].[Item] ([ISBN], [CodFactura], [Cantidad]) VALUES (N'09788417537401', 1013, 1)
INSERT [dbo].[Item] ([ISBN], [CodFactura], [Cantidad]) VALUES (N'09788417537401', 1016, 2)
INSERT [dbo].[Item] ([ISBN], [CodFactura], [Cantidad]) VALUES (N'09788417777241', 1017, 1)
INSERT [dbo].[Item] ([ISBN], [CodFactura], [Cantidad]) VALUES (N'09788417777241', 1018, 3)
GO
INSERT [dbo].[ItemOrden] ([ISBN], [CodOrdenCompra], [Cotizacion], [StockCompra], [StockRecepcion], [FechaEntrega]) VALUES (N'09788417356330', 1, 2, 2, 2, CAST(N'2024-10-22T22:24:08.000' AS DateTime))
INSERT [dbo].[ItemOrden] ([ISBN], [CodOrdenCompra], [Cotizacion], [StockCompra], [StockRecepcion], [FechaEntrega]) VALUES (N'09788417537401', 1, 3, 3, NULL, NULL)
INSERT [dbo].[ItemOrden] ([ISBN], [CodOrdenCompra], [Cotizacion], [StockCompra], [StockRecepcion], [FechaEntrega]) VALUES (N'09788417490751', 1, 2, 1, NULL, NULL)
GO
INSERT [dbo].[ItemSolicitud] ([ISBN], [CodSolicitud]) VALUES (N'09788417292768', 3)
GO
INSERT [dbo].[Libro] ([ISBN], [Autor], [Nombre], [Precio], [Stock], [Activo], [MaxStock], [MinStock]) VALUES (N'09788417292768', N'Akira Toriyama', N'Dragon Ball Super 01', 5000, 999, 1, 5000, 1000)
INSERT [dbo].[Libro] ([ISBN], [Autor], [Nombre], [Precio], [Stock], [Activo], [MaxStock], [MinStock]) VALUES (N'09788417356330', N'Akira Toriyama', N'Dragon Ball Super 02', 5500, 2197, 1, 5000, 1000)
INSERT [dbo].[Libro] ([ISBN], [Autor], [Nombre], [Precio], [Stock], [Activo], [MaxStock], [MinStock]) VALUES (N'09788417356934', N'Akira Toriyama', N'Dragon Ball Super 03', 6000, 1793, 1, 5000, 1000)
INSERT [dbo].[Libro] ([ISBN], [Autor], [Nombre], [Precio], [Stock], [Activo], [MaxStock], [MinStock]) VALUES (N'09788417490751', N'Akira Toriyama', N'Dragon Ball Super 04', 4500, 2491, 1, 5000, 1000)
INSERT [dbo].[Libro] ([ISBN], [Autor], [Nombre], [Precio], [Stock], [Activo], [MaxStock], [MinStock]) VALUES (N'09788417537401', N'Akira Toriyama', N'Dragon Ball Super 05', 6500, 1593, 1, 5000, 1000)
INSERT [dbo].[Libro] ([ISBN], [Autor], [Nombre], [Precio], [Stock], [Activo], [MaxStock], [MinStock]) VALUES (N'09788417699123', N'Akira Toriyama', N'Dragon Ball Super 06', 2000, 4000, 1, 5000, 1000)
INSERT [dbo].[Libro] ([ISBN], [Autor], [Nombre], [Precio], [Stock], [Activo], [MaxStock], [MinStock]) VALUES (N'09788417777241', N'Akira Toriyama', N'Dragon Ball Super 07', 3500, 4997, 1, 5000, 1000)
INSERT [dbo].[Libro] ([ISBN], [Autor], [Nombre], [Precio], [Stock], [Activo], [MaxStock], [MinStock]) VALUES (N'09788417777500', N'Akira Toriyama', N'Dragon Ball Super 08', 5000, 3000, 1, 5000, 1000)
GO
INSERT [dbo].[Libro_C] ([ISBN], [Fecha], [Hora], [Autor], [Nombre], [Precio], [Stock], [Activo], [MaxStock], [MinStock]) VALUES (N'09788417356330', N'2024-10-17', N'14:57:21', N'Akira Toriyama', N'Dragon Ball Super 02', 5500, 2197, 1, 5000, 1000)
INSERT [dbo].[Libro_C] ([ISBN], [Fecha], [Hora], [Autor], [Nombre], [Precio], [Stock], [Activo], [MaxStock], [MinStock]) VALUES (N'09788417292768', N'2024-10-17', N'15:38:38', N'Akira Toriyama', N'Dragon Ball Super 01', 5000, 999, 1, 5000, 1000)
INSERT [dbo].[Libro_C] ([ISBN], [Fecha], [Hora], [Autor], [Nombre], [Precio], [Stock], [Activo], [MaxStock], [MinStock]) VALUES (N'09788417356934', N'2024-10-22', N'15:15:48', N'Akira Toriyama', N'Dragon Ball Super 03', 6000, 1793, 1, 5000, 1000)
INSERT [dbo].[Libro_C] ([ISBN], [Fecha], [Hora], [Autor], [Nombre], [Precio], [Stock], [Activo], [MaxStock], [MinStock]) VALUES (N'09788417490751', N'2024-10-22', N'15:15:48', N'Akira Toriyama', N'Dragon Ball Super 04', 4500, 2491, 1, 5000, 1000)
GO
SET IDENTITY_INSERT [dbo].[OrdenCompra] ON 

INSERT [dbo].[OrdenCompra] ([CodOrdenCompra], [CUIT], [FechaCreacion], [PrecioTotal], [CodFactura]) VALUES (1, N'11111111111', CAST(N'2024-10-22T17:20:22.907' AS DateTime), 15, N'082208220822082208220822082208')
SET IDENTITY_INSERT [dbo].[OrdenCompra] OFF
GO
SET IDENTITY_INSERT [dbo].[Permiso] ON 

INSERT [dbo].[Permiso] ([CodPermiso], [Nombre]) VALUES (1, N'gestionDeUsuarios')
INSERT [dbo].[Permiso] ([CodPermiso], [Nombre]) VALUES (2, N'gestionDePerfiles')
INSERT [dbo].[Permiso] ([CodPermiso], [Nombre]) VALUES (3, N'gestionDeLibros')
INSERT [dbo].[Permiso] ([CodPermiso], [Nombre]) VALUES (4, N'gestionDeClientes')
INSERT [dbo].[Permiso] ([CodPermiso], [Nombre]) VALUES (5, N'facturar')
INSERT [dbo].[Permiso] ([CodPermiso], [Nombre]) VALUES (6, N'registrarCompra')
INSERT [dbo].[Permiso] ([CodPermiso], [Nombre]) VALUES (7, N'generarReporte')
INSERT [dbo].[Permiso] ([CodPermiso], [Nombre]) VALUES (8, N'bitacoraDeEventos')
INSERT [dbo].[Permiso] ([CodPermiso], [Nombre]) VALUES (9, N'bitacoraDeCambios')
INSERT [dbo].[Permiso] ([CodPermiso], [Nombre]) VALUES (10, N'respaldos')
INSERT [dbo].[Permiso] ([CodPermiso], [Nombre]) VALUES (11, N'generarSolicitudDeCotizacion')
INSERT [dbo].[Permiso] ([CodPermiso], [Nombre]) VALUES (12, N'generarOrdenDeCompra')
INSERT [dbo].[Permiso] ([CodPermiso], [Nombre]) VALUES (13, N'verificarRecepciónDeProductos')
SET IDENTITY_INSERT [dbo].[Permiso] OFF
GO
INSERT [dbo].[Proveedor] ([CUIT], [RazonSocial], [Nombre], [Email], [NumTelefono], [Direccion], [CuentaBancaria]) VALUES (N'11111111111', N'Ivrea', N'Ivrea', N'ivrea@gmail.com', N'65469846984', N'Laprida 2001', N'cuenta1234')
GO
INSERT [dbo].[ProveedorSolicitud] ([CUIT], [CodSolicitud]) VALUES (N'11111111111', 3)
GO
SET IDENTITY_INSERT [dbo].[SolicitudCotizacion] ON 

INSERT [dbo].[SolicitudCotizacion] ([CodSolicitud], [FechaEmision]) VALUES (3, CAST(N'2024-10-22T15:01:47.353' AS DateTime))
SET IDENTITY_INSERT [dbo].[SolicitudCotizacion] OFF
GO
INSERT [dbo].[Usuario] ([DNI], [Nombre], [Apellido], [FechaNac], [Email], [NumTelefono], [Username], [Contraseña], [Rol], [Bloqueado], [Desactivado], [IntentosFallidos]) VALUES (10000000, N'Goku', N'Son', CAST(N'1984-12-03' AS Date), N'goku@DB.com', N'15263625145', N'Kakaroto', N'5e26dc5a4c5f50b0234f55c6cd49f31dfd00845f1b63e3b3b28cc468df17aed7', N'Vendedor', 0, 0, 0)
INSERT [dbo].[Usuario] ([DNI], [Nombre], [Apellido], [FechaNac], [Email], [NumTelefono], [Username], [Contraseña], [Rol], [Bloqueado], [Desactivado], [IntentosFallidos]) VALUES (10000001, N'Goten', N'Son', CAST(N'1994-05-22' AS Date), N'GOTEN@DBZ.com', N'469478463549', N'SaiyamanX2', N'b26b2b03dcd92d9c3646a203d8f5d25cbfa8c117ea3ea07faa2549164541dfcf', N'Vendedor', 0, 0, 1)
INSERT [dbo].[Usuario] ([DNI], [Nombre], [Apellido], [FechaNac], [Email], [NumTelefono], [Username], [Contraseña], [Rol], [Bloqueado], [Desactivado], [IntentosFallidos]) VALUES (10000055, N'Luffy', N'Monkey D.', CAST(N'1999-05-22' AS Date), N'luffy@gmail.com', N'14564645463', N'luffy123', N'c849095a0da9be5f9a884928705674bc410e85fd1f285bc61edbbddf428e3c4a', N'Vendedor', 0, 0, 0)
INSERT [dbo].[Usuario] ([DNI], [Nombre], [Apellido], [FechaNac], [Email], [NumTelefono], [Username], [Contraseña], [Rol], [Bloqueado], [Desactivado], [IntentosFallidos]) VALUES (10000087, N'José', N'San Martín', CAST(N'1977-06-26' AS Date), N'jose@SM.com', N'456454546454', N'JoseSM_0822', N'bc91de96dbcf4366ed9e676dd9d05b876cabe75e6677a4db1e21039f8d6d5098', N'Vendedor', 0, 0, 0)
INSERT [dbo].[Usuario] ([DNI], [Nombre], [Apellido], [FechaNac], [Email], [NumTelefono], [Username], [Contraseña], [Rol], [Bloqueado], [Desactivado], [IntentosFallidos]) VALUES (10000555, N'Vegeta', N'IV', CAST(N'1989-05-09' AS Date), N'vegeta@4.com', N'2436432733', N'UltraEgo', N'0e0cca4a64d9b1eb01043ec7e85dff708e3c1cbdcc0f36e0471a1613318788a3', N'Vendedor', 0, 0, 0)
INSERT [dbo].[Usuario] ([DNI], [Nombre], [Apellido], [FechaNac], [Email], [NumTelefono], [Username], [Contraseña], [Rol], [Bloqueado], [Desactivado], [IntentosFallidos]) VALUES (12345678, N'Dexter', N'Morgan', CAST(N'2000-05-21' AS Date), N'ramon@ramon.com', N'128655688554', N'Asesino', N'6a73fd4b4db138c8c4b8ae2295e080d20f3b753f224c74b0186b2c78cbbe0ced', N'Vendedor', 0, 1, 0)
INSERT [dbo].[Usuario] ([DNI], [Nombre], [Apellido], [FechaNac], [Email], [NumTelefono], [Username], [Contraseña], [Rol], [Bloqueado], [Desactivado], [IntentosFallidos]) VALUES (22222222, N'Gohan', N'Son', CAST(N'1989-05-22' AS Date), N'gohan@beast.com', N'456467895625', N'GohanBeast', N'0057c17ab21780dc3e5c682e5e93613f6de6c45402bf16cea41f539d0807ce5d', N'Vendedor', 0, 0, 0)
INSERT [dbo].[Usuario] ([DNI], [Nombre], [Apellido], [FechaNac], [Email], [NumTelefono], [Username], [Contraseña], [Rol], [Bloqueado], [Desactivado], [IntentosFallidos]) VALUES (35456598, N'Majin', N'Buu', CAST(N'1994-12-15' AS Date), N'chocolate@gmail.com', N'44658479846', N'BUU', N'166ae138fe78af16cd76eb42f518cceb85f1f6fecc81f4287e2d018a310f6ab3', N'Vendedor', 0, 0, 0)
INSERT [dbo].[Usuario] ([DNI], [Nombre], [Apellido], [FechaNac], [Email], [NumTelefono], [Username], [Contraseña], [Rol], [Bloqueado], [Desactivado], [IntentosFallidos]) VALUES (45304036, N'Santiago Martín', N'Gonzalez', CAST(N'2003-11-17' AS Date), N'SantiagoMartin.Gonzalez@alumnos.uai.edu.ar', N'1111111111', N'a', N'754068f93ca0903e1db7f0ad3ec5a616179c738f462959dd2380b6e2743680db', N'Admin', 0, 0, 0)
INSERT [dbo].[Usuario] ([DNI], [Nombre], [Apellido], [FechaNac], [Email], [NumTelefono], [Username], [Contraseña], [Rol], [Bloqueado], [Desactivado], [IntentosFallidos]) VALUES (51967832, N'Tony', N'Redgrave', CAST(N'2001-08-23' AS Date), N'DANTE@yahoo.com', N'1233213255', N'SonOfSparda', N'c3ca8ac557ce50777e8eca0076f9a61eaa71bfab4387e257cacb4f3862137096', N'Vendedor', 0, 0, 1)
INSERT [dbo].[Usuario] ([DNI], [Nombre], [Apellido], [FechaNac], [Email], [NumTelefono], [Username], [Contraseña], [Rol], [Bloqueado], [Desactivado], [IntentosFallidos]) VALUES (54665321, N'Franklin', N'Clinton', CAST(N'1988-11-09' AS Date), N'Frankie@eyefind.info', N'3285550156', N'F', N'd94b6094573d986efbeb7c093539a16d67d542c338d55ce85398d60aa418568b', N'Vendedor', 0, 0, 0)
INSERT [dbo].[Usuario] ([DNI], [Nombre], [Apellido], [FechaNac], [Email], [NumTelefono], [Username], [Contraseña], [Rol], [Bloqueado], [Desactivado], [IntentosFallidos]) VALUES (55555555, N'nombre', N'apellido', CAST(N'1960-05-21' AS Date), N'gmail@gmail.com', N'15646897852', N'nombredeusuario', N'f7dad307a3537e0aea28a3873f845053db9835ac617256c3b71ac206407c7bf4', N'Vendedor', 0, 1, 0)
INSERT [dbo].[Usuario] ([DNI], [Nombre], [Apellido], [FechaNac], [Email], [NumTelefono], [Username], [Contraseña], [Rol], [Bloqueado], [Desactivado], [IntentosFallidos]) VALUES (55663322, N'Zoro', N'Roronoa', CAST(N'1999-06-29' AS Date), N'zoro@zoro.ar', N'4564654654', N'ZORO', N'aede4725183acaec2aaecdf209919a04ad075f7369af1fce52a54141a0c7b317', N'Vendedor', 0, 0, 0)
INSERT [dbo].[Usuario] ([DNI], [Nombre], [Apellido], [FechaNac], [Email], [NumTelefono], [Username], [Contraseña], [Rol], [Bloqueado], [Desactivado], [IntentosFallidos]) VALUES (65656565, N'Connor', N'Kenway', CAST(N'1986-05-22' AS Date), N'connor@hotmail.com', N'6565656565', N'Ratonhnhaketon', N'8ce702cba52e7780bb085ae747b9b8545d8f60c1f30f59025d6fb546a681c3ad', N'Vendedor', 1, 0, 0)
INSERT [dbo].[Usuario] ([DNI], [Nombre], [Apellido], [FechaNac], [Email], [NumTelefono], [Username], [Contraseña], [Rol], [Bloqueado], [Desactivado], [IntentosFallidos]) VALUES (66666666, N'Mario', N'Mario', CAST(N'1981-07-09' AS Date), N'Mario@Luigi.com', N'6556464656', N'SuperMario', N'c8a726e08b6f13af27f8753d88231740c86aa0359f6653e42322ab40812b6570', N'Vendedor', 0, 0, 0)
INSERT [dbo].[Usuario] ([DNI], [Nombre], [Apellido], [FechaNac], [Email], [NumTelefono], [Username], [Contraseña], [Rol], [Bloqueado], [Desactivado], [IntentosFallidos]) VALUES (87654321, N'Ezio', N'Auditore', CAST(N'1930-05-21' AS Date), N'ezio@gmail.com', N'1565454565', N'EZIO', N'369dae66bead17297e736607b965ac0817bb49f1bb5481a5c20589a68cb91660', N'Vendedor', 1, 1, 0)
INSERT [dbo].[Usuario] ([DNI], [Nombre], [Apellido], [FechaNac], [Email], [NumTelefono], [Username], [Contraseña], [Rol], [Bloqueado], [Desactivado], [IntentosFallidos]) VALUES (99999998, N'Arthur', N'Morgan', CAST(N'2000-02-02' AS Date), N'morgan@morgan.com', N'131541646455', N'arthurM', N'f15ada700702b2ca219339ad5b7213e5f68a9a4377a369d8aada6615a635ab60', N'Vendedor', 0, 0, 0)
INSERT [dbo].[Usuario] ([DNI], [Nombre], [Apellido], [FechaNac], [Email], [NumTelefono], [Username], [Contraseña], [Rol], [Bloqueado], [Desactivado], [IntentosFallidos]) VALUES (99999999, N'wqertyuio', N'asdfghjk', CAST(N'1999-07-15' AS Date), N'zxvb@geds', N'12345671234', N'ñññññññ', N'b0299e53ba5568854214f471498ad434533ae1185c97608d2a9d997926e98b7f', N'Vendedor', 0, 0, 0)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Usuario]    Script Date: 22/10/2024 10:32:54 p.m. ******/
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [IX_Usuario] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Evento]  WITH CHECK ADD  CONSTRAINT [FK_Evento_Usuario] FOREIGN KEY([Login])
REFERENCES [dbo].[Usuario] ([Username])
GO
ALTER TABLE [dbo].[Evento] CHECK CONSTRAINT [FK_Evento_Usuario]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [FK_Factura_Cliente] FOREIGN KEY([DNI])
REFERENCES [dbo].[Cliente] ([DNI])
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [FK_Factura_Cliente]
GO
ALTER TABLE [dbo].[Familia]  WITH CHECK ADD  CONSTRAINT [FK_Familia_Permiso] FOREIGN KEY([CodPermiso])
REFERENCES [dbo].[Permiso] ([CodPermiso])
GO
ALTER TABLE [dbo].[Familia] CHECK CONSTRAINT [FK_Familia_Permiso]
GO
ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_Item_Factura] FOREIGN KEY([CodFactura])
REFERENCES [dbo].[Factura] ([CodFactura])
GO
ALTER TABLE [dbo].[Item] CHECK CONSTRAINT [FK_Item_Factura]
GO
ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_Item_Libro] FOREIGN KEY([ISBN])
REFERENCES [dbo].[Libro] ([ISBN])
GO
ALTER TABLE [dbo].[Item] CHECK CONSTRAINT [FK_Item_Libro]
GO
ALTER TABLE [dbo].[ItemOrden]  WITH CHECK ADD  CONSTRAINT [FK_ItemOrden_Libro] FOREIGN KEY([ISBN])
REFERENCES [dbo].[Libro] ([ISBN])
GO
ALTER TABLE [dbo].[ItemOrden] CHECK CONSTRAINT [FK_ItemOrden_Libro]
GO
ALTER TABLE [dbo].[ItemOrden]  WITH CHECK ADD  CONSTRAINT [FK_ItemOrden_OrdenCompra] FOREIGN KEY([CodOrdenCompra])
REFERENCES [dbo].[OrdenCompra] ([CodOrdenCompra])
GO
ALTER TABLE [dbo].[ItemOrden] CHECK CONSTRAINT [FK_ItemOrden_OrdenCompra]
GO
ALTER TABLE [dbo].[ItemSolicitud]  WITH CHECK ADD  CONSTRAINT [FK_ItemSolicitud_Libro] FOREIGN KEY([ISBN])
REFERENCES [dbo].[Libro] ([ISBN])
GO
ALTER TABLE [dbo].[ItemSolicitud] CHECK CONSTRAINT [FK_ItemSolicitud_Libro]
GO
ALTER TABLE [dbo].[ItemSolicitud]  WITH CHECK ADD  CONSTRAINT [FK_ItemSolicitud_SolicitudCotizacion] FOREIGN KEY([CodSolicitud])
REFERENCES [dbo].[SolicitudCotizacion] ([CodSolicitud])
GO
ALTER TABLE [dbo].[ItemSolicitud] CHECK CONSTRAINT [FK_ItemSolicitud_SolicitudCotizacion]
GO
ALTER TABLE [dbo].[Libro_C]  WITH CHECK ADD  CONSTRAINT [FK_Libro_C_Libro] FOREIGN KEY([ISBN])
REFERENCES [dbo].[Libro] ([ISBN])
GO
ALTER TABLE [dbo].[Libro_C] CHECK CONSTRAINT [FK_Libro_C_Libro]
GO
ALTER TABLE [dbo].[OrdenCompra]  WITH CHECK ADD  CONSTRAINT [FK_OrdenCompra_Proveedor] FOREIGN KEY([CUIT])
REFERENCES [dbo].[Proveedor] ([CUIT])
GO
ALTER TABLE [dbo].[OrdenCompra] CHECK CONSTRAINT [FK_OrdenCompra_Proveedor]
GO
ALTER TABLE [dbo].[ProveedorSolicitud]  WITH CHECK ADD  CONSTRAINT [FK_ProveedorSolicitud_Proveedor] FOREIGN KEY([CUIT])
REFERENCES [dbo].[Proveedor] ([CUIT])
GO
ALTER TABLE [dbo].[ProveedorSolicitud] CHECK CONSTRAINT [FK_ProveedorSolicitud_Proveedor]
GO
ALTER TABLE [dbo].[ProveedorSolicitud]  WITH CHECK ADD  CONSTRAINT [FK_ProveedorSolicitud_SolicitudCotizacion] FOREIGN KEY([CodSolicitud])
REFERENCES [dbo].[SolicitudCotizacion] ([CodSolicitud])
GO
ALTER TABLE [dbo].[ProveedorSolicitud] CHECK CONSTRAINT [FK_ProveedorSolicitud_SolicitudCotizacion]
GO
/****** Object:  StoredProcedure [dbo].[BloqueoUsuarioDNI]    Script Date: 22/10/2024 10:32:54 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[BloqueoUsuarioDNI]
	-- Add the parameters for the stored procedure here
	@DNI int,
	@Bloqueado bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Usuario SET Bloqueado = @Bloqueado, IntentosFallidos = 0 WHERE DNI = @DNI
END
GO
/****** Object:  StoredProcedure [dbo].[BloqueoUsuarioUsername]    Script Date: 22/10/2024 10:32:54 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[BloqueoUsuarioUsername]
	-- Add the parameters for the stored procedure here
	@Username nvarchar(20),
	@Bloqueado bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Usuario SET Bloqueado = @Bloqueado WHERE Username = @Username
END
GO
/****** Object:  StoredProcedure [dbo].[DesactivacionCliente]    Script Date: 22/10/2024 10:32:54 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DesactivacionCliente]
	-- Add the parameters for the stored procedure here
	@DNI int,
	@Activo bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Cliente SET Activo = @Activo WHERE DNI = @DNI
END
GO
/****** Object:  StoredProcedure [dbo].[DesactivacionLibro]    Script Date: 22/10/2024 10:32:54 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DesactivacionLibro]
	-- Add the parameters for the stored procedure here
	@ISBN nvarchar(17),
	@Activo bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Libro SET Activo = @Activo WHERE ISBN = @ISBN
END
GO
/****** Object:  StoredProcedure [dbo].[DesactivacionUsuario]    Script Date: 22/10/2024 10:32:54 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DesactivacionUsuario]
	-- Add the parameters for the stored procedure here
	@DNI int,
	@Desactivado bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Usuario SET Desactivado = @Desactivado WHERE DNI = @DNI
END
GO
/****** Object:  StoredProcedure [dbo].[DesactivarUsuario]    Script Date: 22/10/2024 10:32:54 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DesactivarUsuario]
	-- Add the parameters for the stored procedure here
	@DNI int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Usuario SET Desactivado = 1 WHERE DNI = @DNI
END
GO
/****** Object:  StoredProcedure [dbo].[InsertarCliente]    Script Date: 22/10/2024 10:32:54 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertarCliente]
	-- Add the parameters for the stored procedure here
	@DNI int,
	@Nombre nvarchar(50),
	@Apellido nvarchar(50),
	@Direccion nvarchar(100),
	@Email nvarchar(320),
	@NumTelefono nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Cliente(DNI, Nombre, Apellido, Direccion, Email, NumTelefono) VALUES (@DNI, @Nombre, @Apellido, @Direccion, @Email, @NumTelefono)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertarEvento]    Script Date: 22/10/2024 10:32:54 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertarEvento]
	-- Add the parameters for the stored procedure here
	@Login nvarchar(20),
	@Fecha nvarchar(50),
	@Hora nvarchar(50),
	@Modulo nvarchar(50),
	@Evento nvarchar(100),
	@Criticidad int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Evento(Login, Fecha, Hora, Modulo, Evento, Criticidad) VALUES (@Login, @Fecha, @Hora, @Modulo, @Evento, @Criticidad)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertarFacturaEfectivo]    Script Date: 22/10/2024 10:32:54 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertarFacturaEfectivo]
	-- Add the parameters for the stored procedure here
	@Fecha smalldatetime,
	@PrecioTotal int,
	@MetodoPago nvarchar(20),
	@DNI int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Factura(Fecha, PrecioTotal, MetodoPago, DNI) VALUES (@Fecha, @PrecioTotal, @MetodoPago, @DNI)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertarFacturaTarjeta]    Script Date: 22/10/2024 10:32:54 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertarFacturaTarjeta]
	-- Add the parameters for the stored procedure here
	@Fecha smalldatetime,
	@PrecioTotal int,
	@MetodoPago nvarchar(20),
	@Banco nvarchar(50),
	@MarcaTarjeta nvarchar(30),
	@TipoTarjeta nvarchar(20),
	@DNI int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Factura(Fecha, PrecioTotal, MetodoPago, Banco, MarcaTarjeta, TipoTarjeta, DNI) VALUES (@Fecha, @PrecioTotal, @MetodoPago, @Banco, @MarcaTarjeta, @TipoTarjeta, @DNI)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertarLibro]    Script Date: 22/10/2024 10:32:54 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertarLibro]
	-- Add the parameters for the stored procedure here
	@ISBN nvarchar(17),
	@Autor nvarchar(50),
	@Nombre nvarchar(50),
	@Precio int,
	@Stock int,
	@MaxStock int,
	@MinStock int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Libro(ISBN, Autor, Nombre, Precio, Stock, Activo, MaxStock, MinStock) VALUES (@ISBN, @Autor, @Nombre, @Precio, @Stock, 1, @MaxStock, @MinStock)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertarLibroC]    Script Date: 22/10/2024 10:32:54 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertarLibroC]
	-- Add the parameters for the stored procedure here
	@ISBN nvarchar(17),
	@Fecha nvarchar(10),
	@Hora nvarchar(8),
	@Autor nvarchar(50),
	@Nombre nvarchar(50),
	@Precio int,
	@Stock int,
	@Activo bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Libro_C(ISBN, Fecha, Hora, Autor, Nombre, Precio, Stock, Activo) VALUES(@ISBN, @Fecha, @Hora, @Autor, @Nombre, @Precio, @Stock, @Activo)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertarOrdenCompra]    Script Date: 22/10/2024 10:32:54 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertarOrdenCompra]
	-- Add the parameters for the stored procedure here
	@CUIT nvarchar(11),
	@FechaCreacion datetime,
	@PrecioTotal int,
	@CodFactura nvarchar(30)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO OrdenCompra(CUIT, FechaCreacion, PrecioTotal, CodFactura) VALUES (@CUIT, @FechaCreacion, @PrecioTotal, @CodFactura)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertarSolicitudCotizacion]    Script Date: 22/10/2024 10:32:54 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertarSolicitudCotizacion]
	-- Add the parameters for the stored procedure here
	@Fecha datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO SolicitudCotizacion(FechaEmision) VALUES (@Fecha)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertarUsuario]    Script Date: 22/10/2024 10:32:54 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertarUsuario]
	-- Add the parameters for the stored procedure here
	@DNI int,
	@Nombre nvarchar(50),
	@Apellido nvarchar(50),
	@FechaNac date,
	@Email nvarchar(320),
	@NumTelefono nvarchar(50),
	@Username nvarchar(20),
	@Contraseña nvarchar(128),
	@Rol nvarchar(20)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Usuario(DNI, Nombre, Apellido, FechaNac, Email, NumTelefono, Username, Contraseña, Rol, Bloqueado, Desactivado, IntentosFallidos) VALUES (@DNI, @Nombre, @Apellido, @FechaNac, @Email, @NumTelefono, @Username, @Contraseña, @Rol, 0, 0, 0)
END
GO
/****** Object:  StoredProcedure [dbo].[IntentoFallido]    Script Date: 22/10/2024 10:32:54 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[IntentoFallido]
	-- Add the parameters for the stored procedure here
	@Username nvarchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Usuario SET IntentosFallidos = IntentosFallidos+1 WHERE Username = @Username
END
GO
/****** Object:  StoredProcedure [dbo].[ModificarCliente]    Script Date: 22/10/2024 10:32:54 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ModificarCliente]
	-- Add the parameters for the stored procedure here
	@DNI int,
	@Nombre nvarchar(50),
	@Apellido nvarchar(50),
	@Direccion nvarchar(100),
	@Email nvarchar(320),
	@NumTelefono nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Cliente SET Nombre = @Nombre, Apellido = @Apellido, Direccion = @Direccion, Email = @Email, NumTelefono = @NumTelefono WHERE DNI = @DNI
END
GO
/****** Object:  StoredProcedure [dbo].[ModificarLibro]    Script Date: 22/10/2024 10:32:54 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ModificarLibro]
	-- Add the parameters for the stored procedure here
	@ISBN nvarchar(17),
	@Autor nvarchar(50),
	@Nombre nvarchar(50),
	@Precio int,
	@Stock int,
	@MaxStock int,
	@MinStock int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Libro SET Autor = @Autor, Nombre = @Nombre, Precio = @Precio, Stock = @Stock, MaxStock = @MaxStock, MinStock = @MinStock WHERE ISBN = @ISBN
END
GO
/****** Object:  StoredProcedure [dbo].[ModificarPassword]    Script Date: 22/10/2024 10:32:54 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ModificarPassword]
	-- Add the parameters for the stored procedure here
	@DNI int,
	@Contraseña nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Usuario SET Contraseña = @Contraseña WHERE DNI = @DNI
END
GO
/****** Object:  StoredProcedure [dbo].[ModificarUsuario]    Script Date: 22/10/2024 10:32:54 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ModificarUsuario]
	-- Add the parameters for the stored procedure here
	@DNI int,
	@Nombre nvarchar(50),
	@Apellido nvarchar(50),
	@FechaNac date,
	@Email nvarchar(320),
	@NumTelefono nvarchar(50),
	@Username nvarchar(20),
	@Rol nvarchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Usuario SET Nombre = @Nombre, Apellido = @Apellido, FechaNac = @FechaNac, Email = @Email, NumTelefono = @NumTelefono, Username = @Username, Rol = @Rol WHERE DNI = @DNI
END
GO
/****** Object:  StoredProcedure [dbo].[PreRegistrarProveedor]    Script Date: 22/10/2024 10:32:54 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[PreRegistrarProveedor]
	-- Add the parameters for the stored procedure here
	@CUIT nvarchar(11),
	@RazonSocial nvarchar(100),
	@Nombre nvarchar(50),
	@Email nvarchar(320),
	@NumTelefono nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Proveedor(CUIT, RazonSocial, Nombre, Email, NumTelefono) VALUES (@CUIT, @RazonSocial, @Nombre, @Email, @NumTelefono)
END
GO
/****** Object:  StoredProcedure [dbo].[RecibirProducto]    Script Date: 22/10/2024 10:32:54 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RecibirProducto]
	-- Add the parameters for the stored procedure here
	@ISBN nvarchar(17),
	@StockRecepcion int,
	@FechaEntrega datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE ItemOrden SET StockRecepcion = @StockRecepcion, FechaEntrega = @FechaEntrega WHERE ISBN = @ISBN
END
GO
/****** Object:  StoredProcedure [dbo].[RegistrarFamilia]    Script Date: 22/10/2024 10:32:54 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RegistrarFamilia]
	-- Add the parameters for the stored procedure here
	@CodFamilia int,
	@Nombre nvarchar(50),
	@Tipo bit,
	@CodPermiso int,
	@CodComp int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Familia(CodFamilia, Nombre, Tipo, CodPermiso, CodComp) VALUES (@CodFamilia, @Nombre, @Tipo, @CodPermiso, @CodComp)
END
GO
/****** Object:  StoredProcedure [dbo].[RegistrarItem]    Script Date: 22/10/2024 10:32:54 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RegistrarItem] 
	-- Add the parameters for the stored procedure here
	@CodFact int,
	@ISBN nvarchar(17),
	@Cantidad int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Item (CodFactura, ISBN, Cantidad) VALUES (@CodFact, @ISBN, @Cantidad)
	UPDATE Libro SET Stock = Stock-@Cantidad WHERE ISBN = @ISBN
END
GO
/****** Object:  StoredProcedure [dbo].[RegistrarItemOrden]    Script Date: 22/10/2024 10:32:54 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RegistrarItemOrden]
	-- Add the parameters for the stored procedure here
	@ISBN nvarchar(17),
	@CodOrdenCompra int,
	@Cotizacion int,
	@StockCompra int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO ItemOrden (ISBN, CodOrdenCompra, Cotizacion, StockCompra) VALUES (@ISBN, @CodOrdenCompra, @Cotizacion, @StockCompra)
END
GO
/****** Object:  StoredProcedure [dbo].[RegistrarItemSolicitud]    Script Date: 22/10/2024 10:32:54 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RegistrarItemSolicitud]
	-- Add the parameters for the stored procedure here
	@CodSolicitud int,
	@ISBN nvarchar(17)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO ItemSolicitud (CodSolicitud, ISBN) VALUES (@CodSolicitud, @ISBN)
END
GO
/****** Object:  StoredProcedure [dbo].[RegistrarProveedor]    Script Date: 22/10/2024 10:32:54 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RegistrarProveedor]
	-- Add the parameters for the stored procedure here
	@CUIT nvarchar(11),
	@Direccion nvarchar(100),
	@CuentaBancaria nvarchar(34)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Proveedor SET Direccion = @Direccion, CuentaBancaria = @CuentaBancaria WHERE CUIT = @CUIT

END
GO
/****** Object:  StoredProcedure [dbo].[RegistrarProveedorSolicitud]    Script Date: 22/10/2024 10:32:54 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RegistrarProveedorSolicitud]
	-- Add the parameters for the stored procedure here
	@CodSolicitud int,
	@CUIT nvarchar(11)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO ProveedorSolicitud(CodSolicitud, CUIT) VALUES (@CodSolicitud, @CUIT)
END
GO
/****** Object:  StoredProcedure [dbo].[ReiniciarIntentosFallidos]    Script Date: 22/10/2024 10:32:54 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ReiniciarIntentosFallidos]
	-- Add the parameters for the stored procedure here
	@Username varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Usuario SET IntentosFallidos = 0 WHERE Username = @Username
END
GO
USE [master]
GO
ALTER DATABASE [IngenieriaSoftware] SET  READ_WRITE 
GO
