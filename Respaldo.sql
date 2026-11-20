USE [master]
GO

CREATE DATABASE [JN_BD]
GO

USE [JN_BD]
GO

CREATE TABLE [dbo].[Error](
	[IdError] [bigint] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[Error] [nvarchar](max) NOT NULL,
	[FechaHora] [datetime] NOT NULL,
	[Origen] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Error] PRIMARY KEY CLUSTERED 
(
	[IdError] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[Rol](
	[IdRol] [smallint] IDENTITY(1,1) NOT NULL,
	[NombreRol] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Solicitud](
	[IdSolicitud] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [varchar](200) NOT NULL,
	[Descripcion] [varchar](5000) NOT NULL,
	[FechaRegistro] [datetime] NOT NULL,
	[FechaFinalizacion] [datetime] NULL,
	[IdUsuarioRegular] [int] NOT NULL,
	[IdUsuarioAdmin] [int] NOT NULL,
	[IdEstado] [int] NOT NULL,
	[Solucion] [varchar](5000) NULL,
 CONSTRAINT [PK_Solicitud] PRIMARY KEY CLUSTERED 
(
	[IdSolicitud] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SolicitudEstado](
	[IdEstado] [int] IDENTITY(1,1) NOT NULL,
	[NombreEstado] [varchar](200) NOT NULL,
 CONSTRAINT [PK_SolicitudEstado] PRIMARY KEY CLUSTERED 
(
	[IdEstado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Identificacion] [nvarchar](20) NOT NULL,
	[NombreCompleto] [nvarchar](250) NOT NULL,
	[CorreoElectronico] [nvarchar](100) NOT NULL,
	[Contrasenna] [nvarchar](100) NOT NULL,
	[Estado] [bit] NOT NULL,
	[IdRol] [smallint] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[Rol] ON 
GO
INSERT [dbo].[Rol] ([IdRol], [NombreRol]) VALUES (1, N'Cliente')
GO
INSERT [dbo].[Rol] ([IdRol], [NombreRol]) VALUES (2, N'Soporte')
GO
SET IDENTITY_INSERT [dbo].[Rol] OFF
GO

SET IDENTITY_INSERT [dbo].[SolicitudEstado] ON 
GO
INSERT [dbo].[SolicitudEstado] ([IdEstado], [NombreEstado]) VALUES (1, N'Abierto')
GO
INSERT [dbo].[SolicitudEstado] ([IdEstado], [NombreEstado]) VALUES (2, N'Atendido')
GO
INSERT [dbo].[SolicitudEstado] ([IdEstado], [NombreEstado]) VALUES (3, N'Cancelado')
GO
SET IDENTITY_INSERT [dbo].[SolicitudEstado] OFF
GO

SET IDENTITY_INSERT [dbo].[Usuario] ON 
GO
INSERT [dbo].[Usuario] ([IdUsuario], [Identificacion], [NombreCompleto], [CorreoElectronico], [Contrasenna], [Estado], [IdRol]) VALUES (1, N'304590415', N'EDUARDO JOSE CALVO CASTILLO', N'ecalvo90415@ufide.ac.cr', N'$2a$11$b3Kw62.6WMOmBgbqry2X1OWGxhLboVP//U75xPDO0m4wQOeyV2kcm', 1, 1)
GO
INSERT [dbo].[Usuario] ([IdUsuario], [Identificacion], [NombreCompleto], [CorreoElectronico], [Contrasenna], [Estado], [IdRol]) VALUES (2, N'207960874', N'BRANDON JOSUE CORELLA SANCHEZ', N'bcorella60874@ufide.ac.cr', N'$2a$11$mtKPR2VFHvzVAvqYwRsOa.K/JQ8G9tgeoGTFBrcPEbkrLHVvFjs.K', 1, 1)
GO
INSERT [dbo].[Usuario] ([IdUsuario], [Identificacion], [NombreCompleto], [CorreoElectronico], [Contrasenna], [Estado], [IdRol]) VALUES (3, N'115930362', N'JASSON RICARDO BEJARANO MUÑOZ', N'jbejarano30362@ufide.ac.cr', N'$2a$11$CXPj6WYJkK7Qxo4Xfs11KOShZwrfATXkKzf7wK3w8bZ1taqIVcYy6', 1, 2)
GO
INSERT [dbo].[Usuario] ([IdUsuario], [Identificacion], [NombreCompleto], [CorreoElectronico], [Contrasenna], [Estado], [IdRol]) VALUES (4, N'305100019', N'ALEJANDRO CERDAS JIMENEZ', N'ajimenez00019@ufide.ac.cr', N'$2a$11$wWU95Y0t3qGyCVzu4G4IZOtXTkULcT4V/dITOnHGBCRBD6BcRKZPS', 1, 2)
GO
INSERT [dbo].[Usuario] ([IdUsuario], [Identificacion], [NombreCompleto], [CorreoElectronico], [Contrasenna], [Estado], [IdRol]) VALUES (5, N'208730895', N'JOSHUA ANDREY RODRIGUEZ PEREZ', N'jrodriguez30895@ufide.ac.cr', N'$2a$11$R.nmIvflsn6Uw8DnrUKKD.Ys1O9WPBNcNJDhR6E2edfDA7ttiMD2e', 1, 2)
GO
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO

ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [UK_Usuario_CorreoElectronico] UNIQUE NONCLUSTERED 
(
	[CorreoElectronico] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [UK_Usuario_Identificacion] UNIQUE NONCLUSTERED 
(
	[Identificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Solicitud]  WITH CHECK ADD  CONSTRAINT [FK_Solicitud_SolicitudEstado] FOREIGN KEY([IdEstado])
REFERENCES [dbo].[SolicitudEstado] ([IdEstado])
GO
ALTER TABLE [dbo].[Solicitud] CHECK CONSTRAINT [FK_Solicitud_SolicitudEstado]
GO

ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Rol_IdRol] FOREIGN KEY([IdRol])
REFERENCES [dbo].[Rol] ([IdRol])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Rol_IdRol]
GO

CREATE PROCEDURE [dbo].[sp_ActualizarContrasenna]
	@UsuarioId   int,
    @Contrasenna nvarchar(100)
AS
BEGIN

	UPDATE	Usuario
	SET		Contrasenna = @Contrasenna
	WHERE	IdUsuario = @UsuarioId
   
END
GO

CREATE PROCEDURE [dbo].[sp_ActualizarDatosPersonales]
	@UsuarioId			int,
    @CorreoElectronico	nvarchar(100),
	@Identificacion		nvarchar(20),
	@NombreCompleto		nvarchar(250)
AS
BEGIN

	UPDATE	Usuario
	SET		CorreoElectronico = @CorreoElectronico,
			Identificacion = @Identificacion,
			NombreCompleto = @NombreCompleto
	WHERE	IdUsuario = @UsuarioId
   
END
GO

CREATE PROCEDURE [dbo].[sp_CancelarSolicitud]
	@IdSolicitud      int,
    @IdUsuarioRegular int
AS
BEGIN

   UPDATE Solicitud
   SET IdEstado = 3,
       FechaFinalizacion = GETDATE(),
       Solucion = 'Solicitud cancelada por el usuario'
   WHERE IdSolicitud = @IdSolicitud
     AND IdUsuarioRegular = @IdUsuarioRegular
     AND IdEstado = 1
    
END
GO

CREATE PROCEDURE [dbo].[sp_ConsultarCorreo]
    @CorreoElectronico nvarchar(100)
AS
BEGIN

    DECLARE @EstadoActivo BIT = 1

    SELECT  IdUsuario, Identificacion, NombreCompleto, CorreoElectronico, Estado, R.IdRol, NombreRol, Contrasenna
    FROM    dbo.Usuario U
    INNER JOIN dbo.Rol R ON U.IdRol = R.IdRol
    WHERE   CorreoElectronico = @CorreoElectronico
        AND Estado = @EstadoActivo

END
GO

CREATE PROCEDURE [dbo].[sp_ConsultarIdUsuario]
    @UsuarioId nvarchar(100)
AS
BEGIN

    DECLARE @EstadoActivo BIT = 1

    SELECT  IdUsuario, Identificacion, NombreCompleto, CorreoElectronico, Estado, R.IdRol, NombreRol, Contrasenna
    FROM    dbo.Usuario U
    INNER JOIN dbo.Rol R ON U.IdRol = R.IdRol
    WHERE   IdUsuario = @UsuarioId

END
GO

CREATE PROCEDURE [dbo].[sp_IniciarSesion]
    @CorreoElectronico nvarchar(100)
AS
BEGIN

    DECLARE @EstadoActivo BIT = 1

    SELECT  IdUsuario, Identificacion, NombreCompleto, CorreoElectronico, Estado, R.IdRol, NombreRol, Contrasenna
    FROM    dbo.Usuario U
    INNER JOIN dbo.Rol R ON U.IdRol = R.IdRol
    WHERE   CorreoElectronico = @CorreoElectronico
        AND Estado = @EstadoActivo

END
GO

CREATE PROCEDURE [dbo].[sp_RegistrarError]
	@IdUsuario int,
    @Error nvarchar(max),
    @Origen nvarchar(100)
AS
BEGIN

    INSERT INTO dbo.Error (IdUsuario,Error,FechaHora,Origen)
    VALUES (@IdUsuario, @Error, GETDATE(), @Origen)

END
GO

CREATE PROCEDURE [dbo].[sp_RegistrarSolicitud]
	@Titulo varchar(200),
    @Descripcion varchar(5000),
    @IdUsuarioRegular int
AS
BEGIN

    DECLARE @IdUsuarioAdmin INT

    SELECT  TOP 1 @IdUsuarioAdmin = U.IdUsuario
    FROM    Usuario U
    LEFT    JOIN Solicitud S ON  S.IdUsuarioAdmin = U.IdUsuario
                             AND S.IdEstado = 1
    WHERE   U.IdRol = 2
    GROUP BY U.IdUsuario
    ORDER BY COUNT(S.IdSolicitud), NEWID()


    INSERT INTO Solicitud (Titulo,Descripcion,FechaRegistro,FechaFinalizacion,
        IdUsuarioRegular,IdUsuarioAdmin,IdEstado,Solucion)
     VALUES (@Titulo,@Descripcion,GETDATE(),NULL,
        @IdUsuarioRegular,@IdUsuarioAdmin,1,NULL)
    
END
GO

CREATE PROCEDURE [dbo].[sp_RegistrarUsuario]
	@Identificacion nvarchar(20),
    @NombreCompleto nvarchar(250),
    @CorreoElectronico nvarchar(100),
    @Contrasenna nvarchar(100)
AS
BEGIN

    DECLARE @EstadoActivo BIT = 1
    DECLARE @RolUsuario INT = 1

    IF NOT EXISTS(
        SELECT 1 FROM dbo.Usuario
        WHERE   Identificacion = @Identificacion
            OR  CorreoElectronico = @CorreoElectronico)
    BEGIN
	
        INSERT INTO dbo.Usuario(Identificacion,NombreCompleto,CorreoElectronico,Contrasenna,Estado,IdRol)
        VALUES(@Identificacion,@NombreCompleto,@CorreoElectronico,@Contrasenna,@EstadoActivo,@RolUsuario)

    END
END
GO