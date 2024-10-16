DROP USER IF EXISTS 'Administrador'@'localhost';
CREATE USER 'Administrador'@'localhost' IDENTIFIED BY 'pass.5M00';

DROP USER IF EXISTS 'Lector'@'localhost';
CREATE USER 'Lector'@'localhost' IDENTIFIED BY '47Lpass.00';

GRANT SELECT, UPDATE, DELETE ON 5to_Domotica.* TO 'Administrador'@'localhost';

GRANT SELECT ON 5to_Domotica.Consumo TO 'Lector'@'localhost';