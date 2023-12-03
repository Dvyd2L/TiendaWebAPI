/****** Object:  Table [dbo].[Categorias]    Script Date: 29/11/2023 13:18:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorias](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dispositivos]    Script Date: 29/11/2023 13:18:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dispositivos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](150) NOT NULL,
	[Precio] [decimal](9, 2) NOT NULL,
	[Descatalogado] [bit] NOT NULL,
	[MarcaId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Marcas]    Script Date: 29/11/2023 13:18:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [Email]            NVARCHAR (100)  NOT NULL,
    [Password]         NVARCHAR (500)  NOT NULL,
    [Salt]             VARBINARY (MAX) NULL,
    [EnlaceCambioPass] NVARCHAR (50)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
)
CREATE TABLE [dbo].[Marcas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[CategoriaId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categorias] ON 
GO
INSERT [dbo].[Categorias] ([Id], [Nombre]) VALUES (1, N'Impresora')
GO
INSERT [dbo].[Categorias] ([Id], [Nombre]) VALUES (2, N'Ordenador')
GO
INSERT [dbo].[Categorias] ([Id], [Nombre]) VALUES (3, N'Otros')
GO
SET IDENTITY_INSERT [dbo].[Categorias] OFF
GO
SET IDENTITY_INSERT [dbo].[Dispositivos] ON 
GO
INSERT [dbo].[Dispositivos] ([Id], [Nombre], [Precio], [Descatalogado], [MarcaId]) VALUES (2, N'Ordenador', CAST(1000.00 AS Decimal(9, 2)), 0, 2)
GO
INSERT [dbo].[Dispositivos] ([Id], [Nombre], [Precio], [Descatalogado], [MarcaId]) VALUES (3, N'Portátil', CAST(800.00 AS Decimal(9, 2)), 0, 2)
GO
INSERT [dbo].[Dispositivos] ([Id], [Nombre], [Precio], [Descatalogado], [MarcaId]) VALUES (4, N'Impresora 1', CAST(200.00 AS Decimal(9, 2)), 0, 1)
GO
INSERT [dbo].[Dispositivos] ([Id], [Nombre], [Precio], [Descatalogado], [MarcaId]) VALUES (5, N'Impresora 2', CAST(250.00 AS Decimal(9, 2)), 0, 1)
GO
INSERT [dbo].[Dispositivos] ([Id], [Nombre], [Precio], [Descatalogado], [MarcaId]) VALUES (6, N'Ratón', CAST(15.00 AS Decimal(9, 2)), 1, 3)
GO
INSERT [dbo].[Dispositivos] ([Id], [Nombre], [Precio], [Descatalogado], [MarcaId]) VALUES (8, N'Teclado', CAST(80.00 AS Decimal(9, 2)), 0, 3)
GO
INSERT [dbo].[Dispositivos] ([Id], [Nombre], [Precio], [Descatalogado], [MarcaId]) VALUES (9, N'Auriculares', CAST(50.00 AS Decimal(9, 2)), 0, 3)
GO
INSERT [dbo].[Dispositivos] ([Id], [Nombre], [Precio], [Descatalogado], [MarcaId]) VALUES (10, N'Micrófono', CAST(10.00 AS Decimal(9, 2)), 1, 3)
GO
SET IDENTITY_INSERT [dbo].[Dispositivos] OFF
GO
SET IDENTITY_INSERT [dbo].[Marcas] ON 
GO
INSERT [dbo].[Marcas] ([Id], [Nombre], [CategoriaId]) VALUES (1, N'HP', 1)
GO
INSERT [dbo].[Marcas] ([Id], [Nombre], [CategoriaId]) VALUES (2, N'Dell', 2)
GO
INSERT [dbo].[Marcas] ([Id], [Nombre], [CategoriaId]) VALUES (3, N'Logitech', 3)
GO
SET IDENTITY_INSERT [dbo].[Marcas] OFF
GO
ALTER TABLE [dbo].[Dispositivos]  WITH CHECK ADD  CONSTRAINT [FK_Marcas_Dispositivos] FOREIGN KEY([MarcaId])
REFERENCES [dbo].[Marcas] ([Id])
GO
ALTER TABLE [dbo].[Dispositivos] CHECK CONSTRAINT [FK_Marcas_Dispositivos]
GO
ALTER TABLE [dbo].[Marcas]  WITH CHECK ADD  CONSTRAINT [FK_Categorias_Marcas] FOREIGN KEY([CategoriaId])
REFERENCES [dbo].[Categorias] ([Id])
GO
ALTER TABLE [dbo].[Marcas] CHECK CONSTRAINT [FK_Categorias_Marcas]
GO
USE [master]
GO
ALTER DATABASE [MiTienda] SET  READ_WRITE 
GO
