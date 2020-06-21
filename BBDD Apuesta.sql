CREATE DATABASE apuestas CHARACTER SET UTF8;
SET storage_engine = INNODB;

USE apuestas;

CREATE TABLE Evento
(Id INTEGER NOT null,
NombreLocal char(45),
NombreVisitante char(45),
PRIMARY KEY (Id));
	
CREATE TABLE Mercado
(Id INTEGER NOT NULL,
InfoCuotaOver DOUBLE,
InfoCuotaUnder DOUBLE,
DineroApostadoOver DOUBLE,
DineroApostadoUnder DOUBLE,
TipoMercado DOUBLE,
idEvento INTEGER,
PRIMARY KEY (Id),
CONSTRAINT evento_mercado FOREIGN KEY (idEvento) REFERENCES evento(Id));
	
CREATE TABLE Usuario
(idUsuario INTEGER NOT NULL,
Nombre char(45),
Apellido char(45),
Email char(45),
Edad INTEGER,
PRIMARY KEY (idUsuario));
	
CREATE TABLE Apuesta
(Id_Apuesta INTEGER NOT NULL AUTO_INCREMENT,
TipoApuesta char(45),
Cuota DOUBLE,
DineroApostado DOUBLE,
Id_Mercado INTEGER,
Id_Usuario INTEGER,
PRIMARY KEY (Id_Apuesta),
CONSTRAINT Id_Mercado FOREIGN KEY (Id_Mercado) REFERENCES mercado(Id),
CONSTRAINT Id_Usuario FOREIGN KEY (Id_Usuario) REFERENCES Usuario(idUsuario));
	
CREATE TABLE Cuentabancaria
(NumTarjeta INTEGER NOT NULL,
Saldo DOUBLE,
NombreBanco CHAR(45),
idUsuario INTEGER,
PRIMARY KEY(NumTarjeta),
CONSTRAINT Tarjeta_Usuario FOREIGN KEY (idUsuario) REFERENCES Usuario(idUsuario));