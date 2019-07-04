-- Creación de la tabla Vehículo-- BASE DE DATOS ESTACIONAMIENTO --
-- Creado por: Abel Consuegra y Luis Rivera --

USE tempdb
GO

CREATE DATABASE SistemaDeEstacionamiento
GO

USE SistemaDeEstacionamiento
GO

CREATE SCHEMA Estacionamiento
GO

-- Creación de la tabla Vehículo
CREATE TABLE Estacionamiento.Vehiculo (
	Placa NVARCHAR(8) NOT NULL
		CONSTRAINT PK_Estacionamiento_Vehiculo_Placa PRIMARY KEY CLUSTERED,
	TipoVehiculo VARCHAR(20) NOT NULL,
)

-- Creación de la tabla Hora de Entrada
CREATE TABLE Estacionamiento.HoraEntrada (
	HoraEntrada DATETIME NOT NULL
		CONSTRAINT PK_Estacionamiento_HoraEntrada_HoraEntrada PRIMARY KEY CLUSTERED,
	PlacaVehiculo NVARCHAR(8) NOT NULL
)
GO

-- Creación de la tabla Hora Salida
CREATE TABLE Estacionamiento.HoraSalida (
	HoraSalida DATETIME NOT NULL
		CONSTRAINT PK_Estacionamiento_HoraSalida_HoraSalida PRIMARY KEY CLUSTERED,
	PlacaVehiculo NVARCHAR(8) NOT NULL
)
GO
-- Creamos la tabla reportes
CREATE TABLE Estacionamiento.Reporte (
	Id INT IDENTITY(1,1) NOT NULL
	CONSTRAINT PK_Estacionamiento_Reporte_Id PRIMARY KEY CLUSTERED,
	Placa NVARCHAR(8) NOT NULL,
	TipoVehiculo VARCHAR(20) NOT NULL,
	HoraEntrada DATETIME NOT NULL,
	HoraSalida DATETIME NOT NULL,
	TiempoTotal INT NOT NULL,
	Costo DECIMAL NOT NULL,
)

-- Creamos las llaves foráneas
ALTER TABLE Estacionamiento.HoraEntrada
	ADD CONSTRAINT
		FK_Estacionamiento_Vehiculo$TieneUnaOMas$Estacionamiento_HoraEntrada
		FOREIGN KEY (PlacaVehiculo) REFERENCES Estacionamiento.Vehiculo(Placa)
		ON UPDATE NO ACTION
		ON DELETE NO ACTION
GO

ALTER TABLE Estacionamiento.HoraSalida
	ADD CONSTRAINT
		FK_Estacionamiento_Vehiculo$TieneUnaOMas$Estacionamiento_HoraSalida
		FOREIGN KEY (PlacaVehiculo) REFERENCES Estacionamiento.Vehiculo(Placa)
		ON UPDATE NO ACTION
		ON DELETE NO ACTION
GO

/*
INSERT INTO Estacionamiento.Vehiculo
VALUES	('HDP1205', 'Turismo')
GO

Select * from Estacionamiento.Vehiculo
GO

INSERT INTO Estacionamiento.HoraEntrada
VALUES	(GETDATE(), 'HDP1205')
GO
Select * from Estacionamiento.HoraEntrada
GO

INSERT INTO Estacionamiento.HoraSalida
VALUES	(GETDATE(), 'HDP1205')
GO
Select * from Estacionamiento.HoraSalida
GO

DELETE FROM Estacionamiento.Vehiculo
WHERE Placa = 'HDP1205'

INSERT INTO Estacionamiento.Reporte (Placa, TipoVehiculo, HoraEntrada, HoraSalida, TiempoTotal, Costo)
VALUES	('HDP1205', 'Turismo', '2019-07-04 17:02:14.573', '2019-07-04 17:03:40.953', DATEDIFF(HH, '2019-07-04 17:02:14.573', '2019-07-04 17:03:40.953'), 20*2)
GO

SELECT * FROM Estacionamiento.Reporte
GO

DELETE FROM Estacionamiento.Vehiculo
WHERE Placa = 'HDP1205'
*/