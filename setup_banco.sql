-- Cria o banco de dados
CREATE DATABASE projeto_esig;
GO

USE projeto_esig;
GO

-- Cria a tabela de Cargos
CREATE TABLE cargo (
    id INT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    salario_base DECIMAL(18,2) NOT NULL
);
GO

-- Cria a tabela de Pessoas (com ID auto-incremental, conforme ajustamos)
CREATE TABLE pessoa (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    cidade VARCHAR(100),
    email VARCHAR(100),
    cep VARCHAR(20),
    endereco VARCHAR(200),
    pais VARCHAR(50),
    usuario VARCHAR(50),
    telefone VARCHAR(20),
    data_nascimento DATE,
    cargo_id INT NOT NULL,
    FOREIGN KEY (cargo_id) REFERENCES cargo(id)
);
GO

-- Cria a tabela de Resultados
CREATE TABLE pessoa_salario (
    pessoa_id INT,
    pessoa_nome VARCHAR(100),
    cargo_nome VARCHAR(100),
    salario DECIMAL(18,2)
);
GO

-- Insere os cargos base exigidos
INSERT INTO cargo (id, nome, salario_base) VALUES 
(1, 'Estagiário', 500.00),
(2, 'Técnico', 1000.00),
(3, 'Analista', 2000.00),
(4, 'Coordenador', 3000.00),
(5, 'Gerente', 5000.00);
GO

-- Cria a Stored Procedure
CREATE PROCEDURE sp_CalcularSalarios
AS
BEGIN
    TRUNCATE TABLE pessoa_salario;

    INSERT INTO pessoa_salario (pessoa_id, pessoa_nome, cargo_nome, salario)
    SELECT 
        p.id,
        p.nome,
        c.nome,
        c.salario_base
    FROM 
        pessoa p
    INNER JOIN 
        cargo c ON p.cargo_id = c.id;
END;
GO