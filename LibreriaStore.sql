USE [master]
GO

/****** Object:  Database [storedb]    Script Date: 07/29/2010 10:51:09 ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'LibreriaBD')
DROP DATABASE [LibreriaBD]
GO

USE [master]
GO

/****** Object:  Database [storedb]    Script Date: 07/29/2010 10:51:09 ******/
CREATE DATABASE [LibreriaBD];
GO


USE [LibreriaBD]
GO

CREATE TYPE T_smallstring FROM varchar(50)
CREATE TYPE T_medstring FROM varchar(200)
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Editoriales]') AND type in (N'U'))
DROP TABLE [dbo].[Editoriales]
GO

CREATE TABLE [dbo].[Editoriales](
	[EditorialID] [int] IDENTITY(1,1) NOT NULL,
	[EditorialNombre] [T_smallstring] NOT NULL,
	[EditorialDireccion] [T_smallstring] NOT NULL,
 CONSTRAINT [PK_Editoriales] PRIMARY KEY CLUSTERED 
	([EditorialID] ASC)
)
GO

 CREATE TABLE [dbo].[Libros](
	[LibroID] [int] IDENTITY(1,1) NOT NULL,
	[EditorialID] [int] NOT NULL,
	[NombreAutor] [T_smallstring] NOT NULL UNIQUE,
	[Genero] [T_smallstring] NOT NULL UNIQUE,
	[PrecioUnitario] [money] NULL,
	[Descricion] [T_medstring] NULL,
 CONSTRAINT [PK_Libros] PRIMARY KEY CLUSTERED 
	([LibroID] ASC),
);

ALTER TABLE dbo.Libros
	ADD CONSTRAINT [FK_Libros_Editoriales]
	FOREIGN KEY(EditorialID)
	REFERENCES dbo.Editoriales(EditorialID);

GO

CREATE PROCEDURE [dbo].[GetEditoriales]
AS
BEGIN
    SELECT  E.EditorialNombre, E.EditorialDireccion
    From dbo.Editoriales AS E

END
GO

CREATE PROCEDURE [dbo].[GetLibros]
AS
BEGIN
    SELECT C.EditorialNombre, P.LibroID, P.NombreAutor, P.Genero, P.PrecioUnitario, P.Descricion
    From dbo.Libros AS P
	JOIN dbo.Editoriales AS C
	  ON P.EditorialID = C.EditorialID
END
GO

CREATE PROCEDURE [dbo].[UpdateEditorial]
(
  @EditorialID int,
  @EditorialNombre nvarchar(50),
  @EditorialDireccion nvarchar(50)
)
AS
BEGIN TRY
	BEGIN TRANSACTION
    IF NOT EXISTS (SELECT 1 FROM Editoriales WHERE EditorialNombre=@EditorialNombre)
    BEGIN
        INSERT INTO dbo.Editoriales (EditorialNombre) VALUES (@EditorialNombre)
    END
    UPDATE dbo.Editoriales
    SET EditorialNombre=@EditorialNombre,
		EditorialDireccion=@EditorialDireccion
	WHERE EditorialNombre=@EditorialNombre
	COMMIT TRANSACTION    
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
		ROLLBACK
END CATCH
GO

CREATE PROCEDURE [dbo].[UpdateLibro]
(
  @LibroID int,
  @EditorialNombre nvarchar(50),
  @NombreAutor nvarchar(50),
  @Genero nvarchar(50),
  @PrecioUnitario money,
  @Descricion nvarchar(50)
)
AS
BEGIN TRY
	BEGIN TRANSACTION
    UPDATE dbo.Libros
    SET NombreAutor=@NombreAutor,
		Genero=@Genero, PrecioUnitario=@PrecioUnitario, Descricion=@Descricion,
		EditorialID=(SELECT EditorialID FROM dbo.Editoriales WHERE EditorialNombre=@EditorialNombre)
	WHERE LibroID=@LibroID
	COMMIT TRANSACTION    
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
		ROLLBACK
END CATCH
GO

CREATE PROCEDURE [dbo].[DeleteEditorial]
(
  @EditorialID int
 )
AS
BEGIN TRY
    BEGIN TRANSACTION
	DECLARE @CatId int
	SET @CatId = (SELECT EditorialID FROM Editoriales WHERE @EditorialID=EditorialID);
	IF @CatId IS NOT NULL
	    BEGIN
			DELETE FROM dbo.Editoriales WHERE EditorialID = @EditorialID;
	    END
	COMMIT	    
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
    ROLLBACK
	DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int
	SELECT @ErrMsg = ERROR_MESSAGE(),
           @ErrSeverity = ERROR_SEVERITY()
	RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH
GO


CREATE PROCEDURE [dbo].[DeleteLibro]
(
  @LibroID int
 )
AS
BEGIN TRY
    BEGIN TRANSACTION
	DECLARE @CatId int
	SET @CatId = (SELECT LibroID FROM Libros WHERE @LibroID=LibroID);
	IF @CatId IS NOT NULL
	    BEGIN
			DELETE FROM dbo.Libros WHERE LibroID = @LibroID;
	    END
	COMMIT	    
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
    ROLLBACK
	DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int
	SELECT @ErrMsg = ERROR_MESSAGE(),
           @ErrSeverity = ERROR_SEVERITY()
	RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH
GO


CREATE PROCEDURE [dbo].[AddLibro]
(
  @EditorialID int(50),
  @NombreAutor nvarchar(50),
  @Genero nvarchar(50),
  @PrecioUnitario money,
  @Descricion nvarchar(200),
  @LibroID int OUTPUT
)
AS
BEGIN TRY
	BEGIN TRANSACTION

    INSERT dbo.Libros (EditorialID, NombreAutor, Genero, PrecioUnitario, Descricion) 
    VALUES (@EditorialID, @NombreAutor, @Genero, @PrecioUnitario, @Descricion );
    COMMIT TRANSACTION
    SET @LibroID = SCOPE_IDENTITY();
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
    ROLLBACK
    -- Raise an error with the details of the exception
	DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int
	SELECT @ErrMsg = ERROR_MESSAGE(),
           @ErrSeverity = ERROR_SEVERITY()
	RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH

GO


CREATE PROCEDURE [dbo].[AddEditorial]
(
  @EditorialNombre nvarchar(50),
  @EditorialDireccion nvarchar(50),
  @EditorialID int OUTPUT
)
AS
BEGIN TRY
	BEGIN TRANSACTION

    INSERT dbo.Editoriales (EditorialNombre, EditorialDireccion) 
    VALUES (@EditorialNombre, @EditorialDireccion );
    COMMIT TRANSACTION
    SET @EditorialID = SCOPE_IDENTITY();
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
    ROLLBACK
    -- Raise an error with the details of the exception
	DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int
	SELECT @ErrMsg = ERROR_MESSAGE(),
           @ErrSeverity = ERROR_SEVERITY()
	RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH

GO

