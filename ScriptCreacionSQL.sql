USE Salones;


SET IDENTITY_INSERT clientes ON
GO


CREATE TABLE clientes
(
	id INT PRIMARY KEY IDENTITY,
        cedula VARCHAR(50),
	nombres VARCHAR(50),
	apellidos VARCHAR(50),
	telefono VARCHAR(50),
	correo VARCHAR(50),
	departamento VARCHAR(50),
	ciudad VARCHAR(50),
	edad VARCHAR(50)
);


INSERT INTO clientes(id, cedula, nombres, apellidos, telefono, correo, departamento, ciudad, edad) VALUES
(1, '98765', 'Homer Jay', 'Simpson', '5554444', 'chunkylover53@aol.com', 'Somewhere', 'Springfield', '38'),
(2, '43219', 'Justin Peter', 'Griffin', '5554444', 'pe.griffin@pawtucketbrewery.com', 'Quahog', 'Rhode Island', '43'),
(3, '87654', 'Stan', 'Smith', '5554444', 'smith@cia.com', 'Washington D.C', 'Langley Falls ', '42');




SET IDENTITY_INSERT reservas ON
GO


CREATE TABLE reservas
(
	id INT PRIMARY KEY IDENTITY,
	cliente VARCHAR(50),
	fecha DATE,
	cantidad VARCHAR(100),
	motivo VARCHAR(100),
	observaciones VARCHAR(50),
	estado VARCHAR(50)
);


INSERT INTO reservas(id, cliente, fecha, cantidad, motivo, observaciones, estado) VALUES
(1, '987654', '2022-05-29', '200', 'Cumpleanos', 'Musica', 'Confirmado'),
(2, '432198', '2022-05-29', '150', 'Fin de Ano', 'Cerveza', 'Confirmado'),
(3, '876543', '2022-05-29', '100', 'Celebracion', 'Limpio', 'Confirmado');