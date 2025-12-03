-- Active: 1764783930796@@127.0.0.1@3306@5to_domotica

DELIMITER $$
DROP PROCEDURE IF EXISTS altaUsuario $$
CREATE PROCEDURE altaUsuario(OUT unidUsuario INT,
							IN unNombre VARCHAR(100),
							IN unCorreo VARCHAR(100),
							IN uncontrasenia CHAR(64),
							IN unTelefono VARCHAR(20),
							IN esAdmin BOOLEAN)
BEGIN 
	INSERT INTO Usuario (Nombre, Correo, contrasenia, Telefono, EsAdmin)
				VALUES (unNombre, unCorreo, uncontrasenia, unTelefono, esAdmin);
	SET unidUsuario = LAST_INSERT_ID();
END $$

DELIMITER;
CALL altaUsuario (@idUsuarioCori, 'Corina', 'Cori@gmail.com', 22131,1123562, FALSE);
CALL altaUsuario (@idUsuarioDani, 'Daniel', 'Dani@gmail.com', 78231,1569823, FALSE);
CALL altaUsuario(@idUsuarioZhami, 'Zhami', 'Zhami@gmail.com', 92667,1155478, FALSE);
CALL altaUsuario (@idUsuarioAdmin, 'Admin', 'admin@gmail.com', 22157, 1234567890, TRUE);


DELIMITER $$
DROP PROCEDURE IF EXISTS altaCasa $$
CREATE PROCEDURE altaCasa (OUT unidCasa INT,
						IN unDireccion VARCHAR(100))
BEGIN 
	INSERT INTO Casa (Direccion)
			VALUES (unDireccion);
	SET unidCasa = LAST_INSERT_ID();
END $$

DELIMITER ;
CALL AltaCasa (@idCasaCervino, 'Cerviño 336');
CALL AltaCasa (@idCasaLibertador, 'Libertador 284');
CALL AltaCasa (@idCasaLavalle, 'Lavalle 425');
CALL AltaCasa (@idCasaEsmeralda, 'Esmeralda 527');
CALL AltaCasa (@idCasaFlorida, 'Florida 327');


DELIMITER $$
DROP PROCEDURE IF EXISTS altaCasaUsuario $$
CREATE PROCEDURE altaCasaUsuario (IN unidUsuario INT,
								IN unidCasa INT)
BEGIN 
	INSERT INTO casaUsuario (idUsuario, idCasa)
					VALUES (unidUsuario, unidCasa);
END $$
DELIMITER;
CALL altaCasaUsuario(@idUsuarioCori, 2);
CALL altaCasaUsuario(@idUsuarioDani, 3);
CALL altaCasaUsuario(3, 5);
CALL altaCasaUsuario(4, 1);
CALL altaCasaUsuario(4, 2);
CALL altaCasaUsuario(4, 3);
CALL altaCasaUsuario(4, 4);
CALL altaCasaUsuario(4, 5);
DELIMITER $$
DROP PROCEDURE IF EXISTS altaElectrodomestico $$
CREATE PROCEDURE altaElectrodomestico (OUT unidElectrodomestico INT, 
									IN unidCasa INT,
                                    IN unNombre VARCHAR(100),
                                    IN unTipo VARCHAR(50),
                                    IN unUbicacion VARCHAR(50),
                                    IN unApagado BOOL)
BEGIN 
	INSERT INTO Electrodomestico (idCasa, Nombre, Tipo, Ubicacion, Apagado)
						VALUES (unidCasa, unNombre,unTipo, unUbicacion, unApagado);
	SET unidElectrodomestico = LAST_INSERT_ID();
END $$

DELIMITER ;
CALL AltaElectrodomestico (@idElectrodomestico, @idCasaFlorida, 'Lavarropa', 'Lavarropa', 'Lavadero', TRUE);
CALL AltaElectrodomestico (@idElectrodomestico, 2, 'Heladera', 'Heladera', 'Cocina', TRUE);
CALL AltaElectrodomestico (@idElectrodomestico, 3, 'Cafetera Full', 'Cafetera', 'Mesada', TRUE);

DELIMITER $$
DROP PROCEDURE IF EXISTS altaHistorialRegistro $$
CREATE PROCEDURE altaHistorialRegistro (IN unidElectrodomestico INT,
                                        IN unFechaHoraRegistro DATETIME)
BEGIN 
	INSERT INTO HistorialRegistro (idElectrodomestico, FechaHoraRegistro)
						VALUES (unidElectrodomestico, unFechaHoraRegistro);
END $$

DELIMITER ;
CALL altaHistorialRegistro(@idElectrodomestico, '2024-08-19 14:30:00');
CALL altaHistorialRegistro(@idElectrodomestico, '2022-06-02 17:28:23');

DELIMITER $$
DROP PROCEDURE IF EXISTS altaConsumo $$
CREATE PROCEDURE altaConsumo (OUT unidConsumo INT,
							IN unidElectrodomestico INT,
                            IN uninicio DATETIME,
                            IN unDuracion TIME,
                            IN unConsumoTotal FLOAT)
BEGIN 
	INSERT INTO Consumo (idElectrodomestico, inicio, Duracion, ConsumoTotal)
				VALUES (unidElectrodomestico, uninicio, unDuracion, unConsumoTotal);
	SET unidConsumo = LAST_INSERT_ID();
END $$

DELIMITER ;
CALL altaConsumo(@idConsumo, @idElectrodomestico, '2024-08-20 15:30:05', '1:03:27', 103.27);
CALL altaConsumo(@idConsumo, @idElectrodomestico, '2022-06-02 18:33:49', '2:17:02', 200.02);


-- Función para contar cuantos electrodomesticos hay en una casa.
DELIMITER $$
DROP FUNCTION IF EXISTS DispositivosEncontados $$
CREATE FUNCTION DispositivosEncontados(unIdElectrodomestico INT)
RETURNS INT UNSIGNED READS SQL DATA
BEGIN
	DECLARE cantidad INT;
	SELECT COUNT(idCasa) INTO cantidad
	FROM Casa
	WHERE idElectrodomestico = unIdElectrodomestico;
	RETURN cantidad;
END $$
