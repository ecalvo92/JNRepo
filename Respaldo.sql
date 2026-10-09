USE [master]
GO

CREATE DATABASE [JN_BD]
GO

USE [JN_BD]
GO

CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Identificacion] [nvarchar](20) NOT NULL,
	[NombreCompleto] [nvarchar](250) NOT NULL,
	[CorreoElectronico] [nvarchar](100) NOT NULL,
	[Contrasenna] [nvarchar](100) NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[Usuario] ON 
GO
INSERT [dbo].[Usuario] ([IdUsuario], [Identificacion], [NombreCompleto], [CorreoElectronico], [Contrasenna], [Estado]) VALUES (1, N'304590415', N'EDUARDO JOSE CALVO CASTILLO', N'ecalvo90415@ufide.ac.cr', N'90415', 1)
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

CREATE PROCEDURE [dbo].[sp_IniciarSesion]
    @CorreoElectronico nvarchar(100),
    @Contrasenna nvarchar(100)
AS
BEGIN

    DECLARE @EstadoActivo BIT = 1

    SELECT  IdUsuario, Identificacion, NombreCompleto, CorreoElectronico, Estado
    FROM    dbo.Usuario
    WHERE   CorreoElectronico = @CorreoElectronico
        AND Contrasenna = @Contrasenna
        AND Estado = @EstadoActivo

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

    IF NOT EXISTS(
        SELECT 1 FROM dbo.Usuario
        WHERE   Identificacion = @Identificacion
            OR  CorreoElectronico = @CorreoElectronico)
    BEGIN
	
        INSERT INTO dbo.Usuario(Identificacion,NombreCompleto,CorreoElectronico,Contrasenna,Estado)
        VALUES(@Identificacion,@NombreCompleto,@CorreoElectronico,@Contrasenna,@EstadoActivo)

    END
END
GO