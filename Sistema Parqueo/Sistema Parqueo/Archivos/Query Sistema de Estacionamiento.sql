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


--Creacion tabla detalle---

CREATE TABLE Estacionamiento.Detalle(
idDetalles INT IDENTITY(1,1) NOT NULL
			CONSTRAINT PK_idDetalles PRIMARY KEY CLUSTERED,
horaEntrada DATETIME NOT NULL,
horaSalida DATETIME NOT NULL,
placaVehiculo NVARCHAR(8) NOT NULL
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


ALTER TABLE Estacionamiento.Detalle
	ADD CONSTRAINT
		FK_Estacionamiento_Detalles$TieneUna$Estacionamiento_placaVehiculo
		FOREIGN KEY (placaVehiculo) REFERENCES Estacionamiento.Vehiculo(Placa)
		ON UPDATE NO ACTION
		ON DELETE NO ACTION
GO

ALTER TABLE Estacionamiento.Detalle
ADD UNIQUE (placaVehiculo);
go

---Creacion de trigger para llenar la tabla de reporte---
CREATE TRIGGER	TR_Reporte
ON Estacionamiento.Detalle FOR UPDATE
AS
begin
DECLARE @Placa NVarchar(8)
Select @Placa = PlacaVehiculo From deleted
DECLARE @TipoVehiculo Varchar(20)
Select @TipoVehiculo = TipoVehiculo From Estacionamiento.Vehiculo where Placa = @Placa 
DECLARE @HoraEntrada DateTime
Select @HoraEntrada = HoraEntrada From Estacionamiento.Detalle where PlacaVehiculo =@Placa
DECLARE @HoraSalida Datetime
Select @HoraSalida = HoraSalida from Estacionamiento.Detalle
Declare @TiempoTotal INT
SELECT @TiempoTotal=TiempoTotal From Estacionamiento.Reporte
set @TiempoTotal = DATEDIFF(hh,@HoraEntrada,@HoraSalida)
declare @costo int
set @costo = 0

if(@TiempoTotal) = 1 or (@TiempoTotal)=0 BEGIN
	set @costo=20
	set @TiempoTotal=1
END
if(@TiempoTotal) = 2  BEGIN
	set @costo=30
END
if(@TiempoTotal) = 3 or (@TiempoTotal) =4  BEGIN
	set @costo=70
END
if(@TiempoTotal) >= 4  BEGIN
	set @costo=15*@TiempoTotal
END
if(@TipoVehiculo) = 'Camion' or (@TipoVehiculo) = 'Bus' or (@TipoVehiculo) = 'Rastra' BEGIN
	UPDATE Estacionamiento.Reporte
	set @costo= @costo*2
	END
	if(@TipoVehiculo) = 'Motocicleta' or (@TipoVehiculo) = 'Otros' BEGIN
	set @costo=@costo*0.5
	END

INSERT INTO Estacionamiento.Reporte VALUES(@Placa,@TipoVehiculo,@HoraEntrada,@HoraSalida,@TiempoTotal,@costo)
end
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