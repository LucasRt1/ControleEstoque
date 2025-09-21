CREATE DATABASE controle_estoque;

CREATE TABLE produto (
    id_produto SERIAL PRIMARY KEY,
    nome VARCHAR(45) NOT NULL,
    preco DECIMAL(10,2) NOT NULL,
    quantidade INT DEFAULT 0
);

INSERT INTO produto (nome, preco, quantidade) VALUES
('Notebook Dell i7', 3500.00, 15),
('Mouse Gamer RGB', 120.50, 30),
('Teclado Mecânico', 250.00, 20);
