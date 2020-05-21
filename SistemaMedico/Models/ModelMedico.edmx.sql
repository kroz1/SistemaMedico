
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/21/2020 15:39:44
-- Generated from EDMX file: C:\Users\edgar\OneDrive\Documentos\Visual Studio 2019\Projects\SistemaMedico\SistemaMedico\Models\ModelMedico.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [citas_medicas];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Citas_Consultorios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Citas] DROP CONSTRAINT [FK_Citas_Consultorios];
GO
IF OBJECT_ID(N'[dbo].[FK_Citas_Medicos]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Citas] DROP CONSTRAINT [FK_Citas_Medicos];
GO
IF OBJECT_ID(N'[dbo].[FK_Citas_Pacientes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Citas] DROP CONSTRAINT [FK_Citas_Pacientes];
GO
IF OBJECT_ID(N'[dbo].[FK_Medicos_Especialidades]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Medicos] DROP CONSTRAINT [FK_Medicos_Especialidades];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Citas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Citas];
GO
IF OBJECT_ID(N'[dbo].[Consultorios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Consultorios];
GO
IF OBJECT_ID(N'[dbo].[Especialidades]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Especialidades];
GO
IF OBJECT_ID(N'[dbo].[Medicos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Medicos];
GO
IF OBJECT_ID(N'[dbo].[Pacientes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pacientes];
GO
IF OBJECT_ID(N'[dbo].[PerfilEmpresa]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PerfilEmpresa];
GO
IF OBJECT_ID(N'[dbo].[Usuarios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Usuarios];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Citas'
CREATE TABLE [dbo].[Citas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Fecha] datetime  NOT NULL,
    [Hora] varchar(10)  NOT NULL,
    [Id_paciente] int  NOT NULL,
    [Id_medico] int  NOT NULL,
    [Id_consultorio] int  NOT NULL,
    [Estado] varchar(50)  NOT NULL,
    [Observacion] varchar(250)  NOT NULL
);
GO

-- Creating table 'Consultorios'
CREATE TABLE [dbo].[Consultorios] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] varchar(50)  NOT NULL,
    [Estado] varchar(50)  NOT NULL,
    [Agregado] datetime  NOT NULL
);
GO

-- Creating table 'Especialidades'
CREATE TABLE [dbo].[Especialidades] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] varchar(100)  NOT NULL,
    [Estado] varchar(50)  NOT NULL,
    [Agregado] datetime  NOT NULL
);
GO

-- Creating table 'Medicos'
CREATE TABLE [dbo].[Medicos] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] varchar(100)  NOT NULL,
    [Apellido] varchar(100)  NOT NULL,
    [Telefono] varchar(20)  NOT NULL,
    [Direccion] varchar(250)  NOT NULL,
    [correo] varchar(250)  NOT NULL,
    [Especialidad] varchar(50)  NULL,
    [Id_especialidad] int  NOT NULL
);
GO

-- Creating table 'Pacientes'
CREATE TABLE [dbo].[Pacientes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] varchar(100)  NOT NULL,
    [Apellido] varchar(100)  NOT NULL,
    [Edad] varchar(10)  NOT NULL,
    [Telefono] varchar(20)  NOT NULL,
    [Direccion] varchar(250)  NOT NULL,
    [Correo] varchar(100)  NULL,
    [Genero] varchar(50)  NOT NULL,
    [FechaNacimiento] datetime  NOT NULL
);
GO

-- Creating table 'PerfilEmpresa'
CREATE TABLE [dbo].[PerfilEmpresa] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] varchar(50)  NOT NULL,
    [NumeroRegistro] varchar(50)  NOT NULL,
    [Correo] varchar(100)  NOT NULL,
    [Telefono] varchar(50)  NOT NULL,
    [Moneda] varchar(50)  NULL,
    [ZonaHoraria] varchar(50)  NULL,
    [Logo] varbinary(max)  NULL,
    [Calle] varchar(100)  NOT NULL,
    [Ciudad] varchar(50)  NOT NULL,
    [CodigoPostal] varchar(10)  NOT NULL,
    [Pais] varchar(50)  NOT NULL
);
GO

-- Creating table 'Usuarios'
CREATE TABLE [dbo].[Usuarios] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] varchar(100)  NOT NULL,
    [Apellido] varchar(100)  NOT NULL,
    [Usuario] varchar(100)  NOT NULL,
    [Correo] varchar(150)  NOT NULL,
    [Grupo] varchar(50)  NOT NULL,
    [Agregado] datetime  NOT NULL,
    [Estado] varchar(50)  NOT NULL,
    [Contrasenia] varchar(255)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Citas'
ALTER TABLE [dbo].[Citas]
ADD CONSTRAINT [PK_Citas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Consultorios'
ALTER TABLE [dbo].[Consultorios]
ADD CONSTRAINT [PK_Consultorios]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Especialidades'
ALTER TABLE [dbo].[Especialidades]
ADD CONSTRAINT [PK_Especialidades]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Medicos'
ALTER TABLE [dbo].[Medicos]
ADD CONSTRAINT [PK_Medicos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Pacientes'
ALTER TABLE [dbo].[Pacientes]
ADD CONSTRAINT [PK_Pacientes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PerfilEmpresa'
ALTER TABLE [dbo].[PerfilEmpresa]
ADD CONSTRAINT [PK_PerfilEmpresa]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Usuarios'
ALTER TABLE [dbo].[Usuarios]
ADD CONSTRAINT [PK_Usuarios]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Id_consultorio] in table 'Citas'
ALTER TABLE [dbo].[Citas]
ADD CONSTRAINT [FK_Citas_Consultorios]
    FOREIGN KEY ([Id_consultorio])
    REFERENCES [dbo].[Consultorios]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Citas_Consultorios'
CREATE INDEX [IX_FK_Citas_Consultorios]
ON [dbo].[Citas]
    ([Id_consultorio]);
GO

-- Creating foreign key on [Id_medico] in table 'Citas'
ALTER TABLE [dbo].[Citas]
ADD CONSTRAINT [FK_Citas_Medicos]
    FOREIGN KEY ([Id_medico])
    REFERENCES [dbo].[Medicos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Citas_Medicos'
CREATE INDEX [IX_FK_Citas_Medicos]
ON [dbo].[Citas]
    ([Id_medico]);
GO

-- Creating foreign key on [Id_paciente] in table 'Citas'
ALTER TABLE [dbo].[Citas]
ADD CONSTRAINT [FK_Citas_Pacientes]
    FOREIGN KEY ([Id_paciente])
    REFERENCES [dbo].[Pacientes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Citas_Pacientes'
CREATE INDEX [IX_FK_Citas_Pacientes]
ON [dbo].[Citas]
    ([Id_paciente]);
GO

-- Creating foreign key on [Id_especialidad] in table 'Medicos'
ALTER TABLE [dbo].[Medicos]
ADD CONSTRAINT [FK_Medicos_Especialidades]
    FOREIGN KEY ([Id_especialidad])
    REFERENCES [dbo].[Especialidades]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Medicos_Especialidades'
CREATE INDEX [IX_FK_Medicos_Especialidades]
ON [dbo].[Medicos]
    ([Id_especialidad]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------