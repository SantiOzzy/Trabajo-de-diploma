-- Inline SQL script
USE [master]
GO
/** Object:  Database [IngenieriaSoftware]    Script Date: 27/10/2024 21:29:09 **/
IF EXISTS(SELECT 1 FROM SYS.databases WHERE name = 'IngenieriaSoftware')
BEGIN
    ALTER DATABASE IngenieriaSoftware SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE IngenieriaSoftware
END
CREATE DATABASE IngenieriaSoftware
GO
USE IngenieriaSoftware
GO
/** Object:  Table [dbo].[Cliente]    Script Date: 27/10/2024 21:29:10 **/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

USE [IngenieriaSoftware]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 05/11/2024 05:48:31 p.m. ******/
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
/****** Object:  Table [dbo].[DV]    Script Date: 20/11/2024 01:36:24 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DV](
	[Tabla] [nvarchar](20) NOT NULL,
	[DVH] [varchar](64) NULL,
	[DVV] [varchar](64) NULL,
 CONSTRAINT [PK_DV] PRIMARY KEY CLUSTERED 
(
	[Tabla] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Evento]    Script Date: 20/11/2024 01:36:24 a.m. ******/
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
/****** Object:  Table [dbo].[Factura]    Script Date: 20/11/2024 01:36:24 a.m. ******/
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
/****** Object:  Table [dbo].[Familia]    Script Date: 20/11/2024 01:36:24 a.m. ******/
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
/****** Object:  Table [dbo].[Item]    Script Date: 20/11/2024 01:36:24 a.m. ******/
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
/****** Object:  Table [dbo].[ItemOrden]    Script Date: 20/11/2024 01:36:24 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemOrden](
	[ISBN] [nvarchar](17) NOT NULL,
	[CodOrdenCompra] [int] NOT NULL,
	[Cotizacion] [float] NOT NULL,
	[StockCompra] [int] NOT NULL,
	[StockRecepcion] [int] NULL,
	[FechaEntrega] [datetime] NULL,
	[CodFactura] [nvarchar](30) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemSolicitud]    Script Date: 20/11/2024 01:36:24 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemSolicitud](
	[ISBN] [nvarchar](17) NOT NULL,
	[CodSolicitud] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Libro]    Script Date: 20/11/2024 01:36:24 a.m. ******/
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
/****** Object:  Table [dbo].[Libro_C]    Script Date: 20/11/2024 01:36:24 a.m. ******/
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
	[EstadoActual] [bit] NOT NULL,
	[MaxStock] [int] NOT NULL,
	[MinStock] [int] NOT NULL,
	[Activo] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrdenCompra]    Script Date: 20/11/2024 01:36:24 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrdenCompra](
	[CodOrdenCompra] [int] IDENTITY(1,1) NOT NULL,
	[CUIT] [nvarchar](11) NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[PrecioTotal] [float] NOT NULL,
	[NumTransaccion] [nvarchar](30) NOT NULL,
	[CodFactura] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_OrdenCompra] PRIMARY KEY CLUSTERED 
(
	[CodOrdenCompra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permiso]    Script Date: 20/11/2024 01:36:24 a.m. ******/
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
/****** Object:  Table [dbo].[Proveedor]    Script Date: 20/11/2024 01:36:24 a.m. ******/
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
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Proveedor] PRIMARY KEY CLUSTERED 
(
	[CUIT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProveedorSolicitud]    Script Date: 20/11/2024 01:36:24 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProveedorSolicitud](
	[CUIT] [nvarchar](11) NOT NULL,
	[CodSolicitud] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SolicitudCotizacion]    Script Date: 20/11/2024 01:36:24 a.m. ******/
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
/****** Object:  Table [dbo].[Usuario]    Script Date: 20/11/2024 01:36:24 a.m. ******/
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
INSERT [dbo].[DV] ([Tabla], [DVH], [DVV]) VALUES (N'Cliente', N'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855', N'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855')
INSERT [dbo].[DV] ([Tabla], [DVH], [DVV]) VALUES (N'Factura', N'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855', N'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855')
INSERT [dbo].[DV] ([Tabla], [DVH], [DVV]) VALUES (N'Item', N'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855', N'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855')
INSERT [dbo].[DV] ([Tabla], [DVH], [DVV]) VALUES (N'ItemOrden', N'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855', N'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855')
INSERT [dbo].[DV] ([Tabla], [DVH], [DVV]) VALUES (N'ItemSolicitud', N'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855', N'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855')
INSERT [dbo].[DV] ([Tabla], [DVH], [DVV]) VALUES (N'Libro', N'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855', N'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855')
INSERT [dbo].[DV] ([Tabla], [DVH], [DVV]) VALUES (N'Libro_C', N'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855', N'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855')
INSERT [dbo].[DV] ([Tabla], [DVH], [DVV]) VALUES (N'OrdenCompra', N'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855', N'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855')
INSERT [dbo].[DV] ([Tabla], [DVH], [DVV]) VALUES (N'Proveedor', N'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855', N'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855')
INSERT [dbo].[DV] ([Tabla], [DVH], [DVV]) VALUES (N'ProveedorSolicitud', N'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855', N'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855')
INSERT [dbo].[DV] ([Tabla], [DVH], [DVV]) VALUES (N'SolicitudCotizacion', N'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855', N'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855')
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
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (101, N'Admin', 1, 9, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (103, N'Administrador', 0, 1, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (103, N'Administrador', 0, 2, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (103, N'Administrador', 0, 3, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (103, N'Administrador', 0, 4, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (103, N'Administrador', 0, 5, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (103, N'Administrador', 0, 6, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (103, N'Administrador', 0, 7, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (101, N'Admin', 1, 10, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (101, N'Admin', 1, 11, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (101, N'Admin', 1, 12, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (101, N'Admin', 1, 13, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (101, N'Admin', 1, 14, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (102, N'Vendedor', 1, NULL, 100)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (105, N'Vendedor2', 0, 5, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (104, N'Comprador1', 0, 6, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (104, N'Comprador1', 0, 3, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (104, N'Comprador1', 0, NULL, 105)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (105, N'Vendedor2', 0, 10, NULL)
INSERT [dbo].[Familia] ([CodFamilia], [Nombre], [Tipo], [CodPermiso], [CodComp]) VALUES (101, N'Admin', 1, 15, NULL)
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
INSERT [dbo].[Permiso] ([CodPermiso], [Nombre]) VALUES (14, N'gestionDeProveedores')
INSERT [dbo].[Permiso] ([CodPermiso], [Nombre]) VALUES (15, N'generarReporteInteligente')
SET IDENTITY_INSERT [dbo].[Permiso] OFF
GO
INSERT [dbo].[Usuario] ([DNI], [Nombre], [Apellido], [FechaNac], [Email], [NumTelefono], [Username], [Contraseña], [Rol], [Bloqueado], [Desactivado], [IntentosFallidos]) VALUES (45304036, N'Santiago Martín', N'Gonzalez', CAST(N'2003-11-17' AS Date), N'SantiagoMartin.Gonzalez@alumnos.uai.edu.ar', N'1111111111', N'a', N'754068f93ca0903e1db7f0ad3ec5a616179c738f462959dd2380b6e2743680db', N'Admin', 0, 0, 0)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Usuario]    Script Date: 20/11/2024 01:36:25 a.m. ******/
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
/****** Object:  StoredProcedure [dbo].[ActualizarEstadoLibro]    Script Date: 20/11/2024 01:36:25 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ActualizarEstadoLibro]
	-- Add the parameters for the stored procedure here
	@ISBN nvarchar(17),
	@Autor nvarchar(50),
	@Nombre nvarchar(50),
	@Precio int,
	@Stock int,
	@MaxStock int,
	@MinStock int,
	@Activo bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Libro SET Autor = @Autor, Nombre = @Nombre, Precio = @Precio, Stock = @Stock, MaxStock = @MaxStock, MinStock = @MinStock, Activo = @Activo WHERE ISBN = @ISBN

END
GO
/****** Object:  StoredProcedure [dbo].[BloqueoUsuarioDNI]    Script Date: 20/11/2024 01:36:25 a.m. ******/
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
/****** Object:  StoredProcedure [dbo].[BloqueoUsuarioUsername]    Script Date: 20/11/2024 01:36:25 a.m. ******/
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
/****** Object:  StoredProcedure [dbo].[DesactivacionCliente]    Script Date: 20/11/2024 01:36:25 a.m. ******/
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
/****** Object:  StoredProcedure [dbo].[DesactivacionLibro]    Script Date: 20/11/2024 01:36:25 a.m. ******/
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
/****** Object:  StoredProcedure [dbo].[DesactivacionUsuario]    Script Date: 20/11/2024 01:36:25 a.m. ******/
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
/****** Object:  StoredProcedure [dbo].[DesactivarUsuario]    Script Date: 20/11/2024 01:36:25 a.m. ******/
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
/****** Object:  StoredProcedure [dbo].[InsertarCliente]    Script Date: 20/11/2024 01:36:25 a.m. ******/
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
	INSERT INTO Cliente(DNI, Nombre, Apellido, Direccion, Email, NumTelefono, Activo) VALUES (@DNI, @Nombre, @Apellido, @Direccion, @Email, @NumTelefono, 1)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertarEvento]    Script Date: 20/11/2024 01:36:25 a.m. ******/
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
/****** Object:  StoredProcedure [dbo].[InsertarFacturaEfectivo]    Script Date: 20/11/2024 01:36:25 a.m. ******/
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
/****** Object:  StoredProcedure [dbo].[InsertarFacturaTarjeta]    Script Date: 20/11/2024 01:36:25 a.m. ******/
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
/****** Object:  StoredProcedure [dbo].[InsertarLibro]    Script Date: 20/11/2024 01:36:25 a.m. ******/
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
/****** Object:  StoredProcedure [dbo].[InsertarOrdenCompra]    Script Date: 20/11/2024 01:36:25 a.m. ******/
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
	@PrecioTotal float,
	@NumTransaccion nvarchar(30),
	@CodFactura nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO OrdenCompra(CUIT, FechaCreacion, PrecioTotal, NumTransaccion, CodFactura) VALUES (@CUIT, @FechaCreacion, @PrecioTotal, @NumTransaccion, @CodFactura)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertarSolicitudCotizacion]    Script Date: 20/11/2024 01:36:25 a.m. ******/
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
/****** Object:  StoredProcedure [dbo].[InsertarUsuario]    Script Date: 20/11/2024 01:36:25 a.m. ******/
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
/****** Object:  StoredProcedure [dbo].[IntentoFallido]    Script Date: 20/11/2024 01:36:25 a.m. ******/
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
/****** Object:  StoredProcedure [dbo].[ModificarCliente]    Script Date: 20/11/2024 01:36:25 a.m. ******/
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
/****** Object:  StoredProcedure [dbo].[ModificarLibro]    Script Date: 20/11/2024 01:36:25 a.m. ******/
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
/****** Object:  StoredProcedure [dbo].[ModificarPassword]    Script Date: 20/11/2024 01:36:25 a.m. ******/
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
/****** Object:  StoredProcedure [dbo].[ModificarProveedor]    Script Date: 20/11/2024 01:36:25 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ModificarProveedor]
	-- Add the parameters for the stored procedure here
	@CUIT nvarchar(11),
	@RazonSocial nvarchar(100),
	@Nombre nvarchar(50),
	@Email nvarchar(320),
	@NumTelefono nvarchar(50),
	@Direccion nvarchar(100),
	@CuentaBancaria nvarchar(34)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Proveedor SET RazonSocial = @RazonSocial, Nombre = @Nombre, Email = @Email, NumTelefono = @NumTelefono, Direccion = @Direccion, CuentaBancaria = @CuentaBancaria WHERE CUIT = @CUIT

END
GO
/****** Object:  StoredProcedure [dbo].[ModificarUsuario]    Script Date: 20/11/2024 01:36:25 a.m. ******/
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
/****** Object:  StoredProcedure [dbo].[PreRegistrarProveedor]    Script Date: 20/11/2024 01:36:25 a.m. ******/
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
	INSERT INTO Proveedor(CUIT, RazonSocial, Nombre, Email, NumTelefono, Activo) VALUES (@CUIT, @RazonSocial, @Nombre, @Email, @NumTelefono, 1)
END
GO
/****** Object:  StoredProcedure [dbo].[RecibirProducto]    Script Date: 20/11/2024 01:36:25 a.m. ******/
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
	@FechaEntrega datetime,
	@CodFactura nvarchar(30)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE ItemOrden SET StockRecepcion = @StockRecepcion, FechaEntrega = @FechaEntrega, CodFactura = @CodFactura WHERE ISBN = @ISBN
	UPDATE Libro SET Stock += @StockRecepcion WHERE ISBN = @ISBN
END
GO
/****** Object:  StoredProcedure [dbo].[RegistrarFamilia]    Script Date: 20/11/2024 01:36:25 a.m. ******/
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
/****** Object:  StoredProcedure [dbo].[RegistrarItem]    Script Date: 20/11/2024 01:36:25 a.m. ******/
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
/****** Object:  StoredProcedure [dbo].[RegistrarItemOrden]    Script Date: 20/11/2024 01:36:25 a.m. ******/
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
/****** Object:  StoredProcedure [dbo].[RegistrarItemSolicitud]    Script Date: 20/11/2024 01:36:25 a.m. ******/
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
/****** Object:  StoredProcedure [dbo].[RegistrarProveedor]    Script Date: 20/11/2024 01:36:25 a.m. ******/
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
/****** Object:  StoredProcedure [dbo].[RegistrarProveedorCompleto]    Script Date: 20/11/2024 01:36:25 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RegistrarProveedorCompleto]
	-- Add the parameters for the stored procedure here
	@CUIT nvarchar(11),
	@RazonSocial nvarchar(100),
	@Nombre nvarchar(50),
	@Email nvarchar(320),
	@NumTelefono nvarchar(50),
	@Direccion nvarchar(100),
	@CuentaBancaria nvarchar(34)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Proveedor(CUIT, RazonSocial, Nombre, Email, NumTelefono, Direccion, CuentaBancaria, Activo) VALUES (@CUIT, @RazonSocial, @Nombre, @Email, @NumTelefono, @Direccion, @CuentaBancaria, 1)

END
GO
/****** Object:  StoredProcedure [dbo].[RegistrarProveedorSolicitud]    Script Date: 20/11/2024 01:36:25 a.m. ******/
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
/****** Object:  StoredProcedure [dbo].[ReiniciarIntentosFallidos]    Script Date: 20/11/2024 01:36:25 a.m. ******/
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
/****** Object:  Trigger [dbo].[RegistrarCambiosLibro]    Script Date: 20/11/2024 01:36:25 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [dbo].[RegistrarCambiosLibro]
   ON  [dbo].[Libro]
   AFTER INSERT, UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	UPDATE Libro_C SET Libro_C.EstadoActual = 0 FROM inserted WHERE Libro_C.ISBN = inserted.ISBN
	INSERT INTO Libro_C(ISBN, Fecha, Hora, Autor, Nombre, Precio, Stock, EstadoActual, MaxStock, MinStock, Activo) SELECT ISBN, CONVERT(VARCHAR(10),GETDATE(),23), CONVERT(VARCHAR(8),GETDATE(),108), Autor, Nombre, Precio, Stock, 1, MaxStock, MinStock, Activo FROM inserted
END
GO
ALTER TABLE [dbo].[Libro] ENABLE TRIGGER [RegistrarCambiosLibro]
GO
USE [master]
GO
ALTER DATABASE [IngenieriaSoftware] SET  READ_WRITE 
GO
