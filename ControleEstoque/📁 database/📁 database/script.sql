CREATE TABLE produto (
    id_produto SERIAL PRIMARY KEY,
    nome VARCHAR(45) NOT NULL,
    preco DECIMAL(10,2) NOT NULL,
    quantidade INT DEFAULT 0
);
