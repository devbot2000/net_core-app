CREATE DATABASE DB_CORE;
GO

CREATE TABLE Alumnos(
IDAlum INT IDENTITY(1,1) PRIMARY KEY,
Nombres VARCHAR(200),
Apellidos VARCHAR(200),
IDEstado INT
);
GO

CREATE TABLE Estados(
IDEst INT IDENTITY(1,1) PRIMARY KEY,
Estado VARCHAR(50)
);
GO

INSERT INTO Estados(Estado) VALUES('Aguascalientes'); 
GO
INSERT INTO Estados(Estado) VALUES('Baja California');
GO
INSERT INTO Estados(Estado) VALUES('Baja California Sur');
GO
INSERT INTO Estados(Estado) VALUES('Campeche');
GO
INSERT INTO Estados(Estado) VALUES('Coahuila');
GO
INSERT INTO Estados(Estado) VALUES('Colima');
GO
INSERT INTO Estados(Estado) VALUES('Chiapas');
GO
INSERT INTO Estados(Estado) VALUES('Chihuahua');
GO
INSERT INTO Estados(Estado) VALUES('Durango');
GO
INSERT INTO Estados(Estado) VALUES('Distrito Federal');
GO
INSERT INTO Estados(Estado) VALUES('Guanajuato');
GO
INSERT INTO Estados(Estado) VALUES('Guerrero');
GO
INSERT INTO Estados(Estado) VALUES('Hidalgo');
GO
INSERT INTO Estados(Estado) VALUES('Jalisco');
GO
INSERT INTO Estados(Estado) VALUES('México');
GO
INSERT INTO Estados(Estado) VALUES('Michoacán');
GO
INSERT INTO Estados(Estado) VALUES('Morelos');
GO
INSERT INTO Estados(Estado) VALUES('Nayarit');
GO
INSERT INTO Estados(Estado) VALUES('Nuevo León');
GO
INSERT INTO Estados(Estado) VALUES('Oaxaca');
GO
INSERT INTO Estados(Estado) VALUES('Puebla');
GO
INSERT INTO Estados(Estado) VALUES('Querétaro');
GO
INSERT INTO Estados(Estado) VALUES('Quintana Roo');
GO
INSERT INTO Estados(Estado) VALUES('San Luis Potosí');
GO
INSERT INTO Estados(Estado) VALUES('Sinaloa');
GO
INSERT INTO Estados(Estado) VALUES('Sonora');
GO
INSERT INTO Estados(Estado) VALUES('Tabasco');
GO
INSERT INTO Estados(Estado) VALUES('Tamaulipas'); 
GO
INSERT INTO Estados(Estado) VALUES('Tlaxcala');
GO
INSERT INTO Estados(Estado) VALUES('Veracruz');
GO
INSERT INTO Estados(Estado) VALUES('Yucatán');
GO
INSERT INTO Estados(Estado) VALUES('Zacatecas');
GO

CREATE PROCEDURE SPVER_ALUMNOS @ID INT AS
SELECT * FROM Alumnos
GO

CREATE PROCEDURE SP_INS_ALUMNO(@Nombres VARCHAR(250), @Apellidos VARCHAR(250), @IDEst INT) AS
INSERT INTO Alumnos(Nombres, Apellidos, IDEstado) VALUES(@Nombres, @Apellidos, @IDEst)
GO

CREATE PROCEDURE SP_CONS_ALUMNO(@IDAlum INT) AS
SELECT * FROM Alumnos WHERE IDAlum= @IDAlum
GO

CREATE PROCEDURE SP_UPD_ALUMNO(@IDAlum INT, @Nombres VARCHAR(250), @APE VARCHAR(250), @IDEst INT) AS
UPDATE Alumnos SET Nombres = @Nombres, Apellidos = @APE, IDEstado = @IDEst WHERE IDAlum = @IDAlum
GO

CREATE PROCEDURE SP_DEL_ALUMNO(@ID INT) AS
DELETE FROM Alumnos WHERE IDAlum= @ID
GO

CREATE PROCEDURE SP_CONS_ALUMNOXEdo @IDEdo INT AS
SELECT Nombres [Col1], Apellidos [Col2], Estado [Col3]  FROM Alumnos A INNER JOIN Estados B ON A.IDEstado = B.IDEst WHERE A.IDEstado = @IDEdo
GO

CREATE PROCEDURE SPEstados(@ID INT) AS
SELECT IDEst, Estado FROM Estados
GO

CREATE PROCEDURE SP_REP_ALUMNOS @ID INT AS
SELECT 
Nombres [Col1], Apellidos [Col2], IDEstado [Col3] FROM Alumnos
GO
