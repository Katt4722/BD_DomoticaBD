-- Active: 1752002174880@@127.0.0.1@3306@5to_domotica
DELIMITER $$
DROP TRIGGER IF EXISTS AftInsElectrodomestico$$
CREATE TRIGGER AftInsElectrodomestico AFTER INSERT ON Electrodomestico
FOR EACH ROW
BEGIN
	INSERT INTO HistorialRegistro (idElectrodomestico, fechaHoraRegistro)
    VALUES (NEW.idElectrodomestico, NOW());
END$$


DELIMITER $$
DROP TRIGGER IF EXISTS BefInsElectrodomestico$$
CREATE TRIGGER BefInsElectrodomestico 
BEFORE INSERT ON Electrodomestico
FOR EACH ROW
BEGIN
    IF NEW.Apagado = 1 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Para encender un electrodoméstico debe ser 1.';
    END IF;
END$$
DELIMITER ;


-- Trigger para actualizar el consumo total cuando se inserta un nuevo registro en la tabla consumo.
DELIMITER $$
DROP TRIGGER IF EXISTS AftInsConsumo$$
CREATE TRIGGER AftInsConsumo AFTER INSERT ON Consumo
FOR EACH ROW
BEGIN
	UPDATE Electrodomestico
    SET Apagado = 1
    WHERE idElectrodomestico = NEW.idElectrodomestico;
END $$

DELIMITER $$
DROP TRIGGER IF EXISTS Contrasenia $$
CREATE TRIGGER Contrasenia BEFORE INSERT ON Usuario 
FOR EACH ROW 
BEGIN 
	SET NEW.Contrasenia = sha2(NEW.Contrasenia, 256);
END $$

DELIMITER $$ 
DROP TRIGGER IF EXISTS ActualizarContrasenia$$ 
CREATE TRIGGER ActualizarContrasenia BEFORE UPDATE ON Usuario 
FOR EACH ROW 
BEGIN 
	IF (NEW.Contrasenia <> OLD.Contrasenia) THEN 
		SET NEW.Contrasenia = sha2(NEW.Contrasenia, 256);
	END IF;
END $$
