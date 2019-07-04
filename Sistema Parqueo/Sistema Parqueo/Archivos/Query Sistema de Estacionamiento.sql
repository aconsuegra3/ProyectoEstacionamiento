-- Creaci�n de la tabla Veh�culo-- BASE DE DATOS ESTACIONAMIENTO --
-- Creado por: Abel Consuegra y Luis Rivera --

USE tempdb
GO

CREATE DATABASE SistemaDeEstacionamiento
GO

USE SistemaDeEstacionamiento
GO

CREATE SCHEMA Estacionamiento
GO

-- Creaci�n de la tabla Veh�culo
CREATE TABLE Estacionamiento.Vehiculo (
	Placa NVARCHAR(8) NOT NULL
		CONSTRAINT PK_Estacionamiento_Vehiculo_Placa PRIMARY KEY CLUSTERED,
	TipoVehiculo VARCHAR(20) NOT NULL,
)

-- Creaci�n de la tabla Hora de Entrada
CREATE TABLE Estacionamiento.HoraEntrada (
	HoraEntrada DATETIME NOT NULL
		CONSTRAINT PK_Estacionamiento_HoraEntrada_HoraEntrada PRIMARY KEY CLUSTERED,
	PlacaVehiculo NVARCHAR(8) NOT NULL
)
GO

-- Creaci�n de la tabla Hora Salida
CREATE TABLE Estacionamiento.HoraSalida (
	HoraSalida DATETIME NOT NULL
		CONSTRAINT PK_Estacionamiento_HoraSalida_HoraSalida PRIMARY KEY CLUSTERED,
	PlacaVehiculo NVARCHAR(8) NOT NULL
)
GO

-- Creamos las llaves for�neas
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