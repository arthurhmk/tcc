-- -----------------------------------------------------
-- Table cliente
-- -----------------------------------------------------
CREATE TABLE cliente (
  cd_cliente INT NOT NULL IDENTITY(1,1),
  nm_cliente VARCHAR(60) NULL,
  ds_email VARCHAR(30) NULL,
  ds_endereco_cliente VARCHAR(100) NULL,
  ds_telefone VARCHAR(60) NULL,
  PRIMARY KEY (cd_cliente))
;


-- -----------------------------------------------------
-- Table pedido
-- -----------------------------------------------------
CREATE TABLE pedido (
  cd_pedido INT NOT NULL IDENTITY(1,1),
  cd_cliente INT NOT NULL,
  vl_pedido DECIMAL(10,2) NOT NULL,
  ic_status_pedido CHAR(1) NOT NULL,
  dt_pedido DATE NOT NULL,
  dt_entrega DATE NULL,
  ic_tipo_entrega CHAR(1) NOT NULL,
  ds_endereco_entrega VARCHAR(100) NULL,
  vl_entrega DECIMAL(10,2) NULL,
  PRIMARY KEY (cd_pedido),
  INDEX FK_PEDIDO_CLIENTE_idx (cd_cliente DESC),
  CONSTRAINT FK_PEDIDO_CLIENTE
    FOREIGN KEY (cd_cliente)
    REFERENCES cliente (cd_cliente)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
;


-- -----------------------------------------------------
-- Table pedido_extra
-- -----------------------------------------------------
CREATE TABLE pedido_extra (
  cd_extra INT NOT NULL IDENTITY(1,1),
  cd_pedido INT NOT NULL,
  nm_extra VARCHAR(60) NULL,
  vl_extra DECIMAL(10,2) NULL,
  PRIMARY KEY (cd_extra),
  INDEX FK_EXTRA_PEDIDO_idx (cd_pedido DESC),
  CONSTRAINT FK_EXTRA_PEDIDO
    FOREIGN KEY (cd_pedido)
    REFERENCES pedido (cd_pedido)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
;


-- -----------------------------------------------------
-- Table pagamento
-- -----------------------------------------------------
CREATE TABLE pagamento (
  cd_pagamento INT NOT NULL IDENTITY(1,1),
  cd_pedido INT NOT NULL,
  dt_pagamento DATE NOT NULL,
  vl_pagamento DECIMAL(10,2) NULL,
  PRIMARY KEY (cd_pagamento),
  INDEX FK_PAGAMENTO_PEDIDO_idx (cd_pedido DESC),
  CONSTRAINT FK_PAGAMENTO_PEDIDO
    FOREIGN KEY (cd_pedido)
    REFERENCES pedido (cd_pedido)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
;


-- -----------------------------------------------------
-- Table categoria
-- -----------------------------------------------------
CREATE TABLE categoria (
  cd_categoria INT NOT NULL IDENTITY(1,1),
  nm_categoria VARCHAR(30) NULL,
  PRIMARY KEY (cd_categoria))
;


-- -----------------------------------------------------
-- Table sabor
-- -----------------------------------------------------
CREATE TABLE sabor (
  cd_sabor INT NOT NULL IDENTITY(1,1),
  nm_sabor VARCHAR(30) NULL,
  PRIMARY KEY (cd_sabor))
;


-- -----------------------------------------------------
-- Table produto
-- -----------------------------------------------------
CREATE TABLE produto (
  cd_produto INT NOT NULL IDENTITY(1,1),
  cd_categoria INT NULL,
  cd_sabor INT NULL,
  nm_produto VARCHAR(60) NULL,
  vl_produto DECIMAL(10,2) NOT NULL,
  qt_produto INT NOT NULL,
  ic_ativo CHAR(1) NOT NULL,
  PRIMARY KEY (cd_produto),
  INDEX FK_PRODUTO_CATEGORIA_idx (cd_categoria DESC),
  INDEX FK_PRODUTO_SABOR_idx (cd_sabor DESC),
  CONSTRAINT FK_PRODUTO_CATEGORIA
    FOREIGN KEY (cd_categoria)
    REFERENCES categoria (cd_categoria)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT FK_PRODUTO_SABOR
    FOREIGN KEY (cd_sabor)
    REFERENCES sabor (cd_sabor)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
;


-- -----------------------------------------------------
-- Table pedido_produto
-- -----------------------------------------------------
CREATE TABLE pedido_produto (
  cd_produto INT NOT NULL,
  cd_pedido INT NOT NULL,
  INDEX FK_PRODUTO_PEDIDO_idx (cd_produto DESC),
  INDEX FK_PRODUTO_PEDIDO_idx1 (cd_pedido DESC),
  CONSTRAINT FK_PEDIDO_PRODUTO
    FOREIGN KEY (cd_produto)
    REFERENCES produto (cd_produto)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT FK_PRODUTO_PEDIDO
    FOREIGN KEY (cd_pedido)
    REFERENCES pedido (cd_pedido)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
;


-- -----------------------------------------------------
-- Table debito
-- -----------------------------------------------------
CREATE TABLE debito (
  cd_debito INT NOT NULL IDENTITY(1,1),
  dt_debito DATE NOT NULL,
  vl_debito DECIMAL(10,2) NOT NULL,
  PRIMARY KEY (cd_debito))
;


-- -----------------------------------------------------
-- Table categoria_sabor
-- -----------------------------------------------------
CREATE TABLE categoria_sabor (
  cd_categoria INT NOT NULL,
  cd_sabor INT NOT NULL,
  INDEX FK_CATEGORIA_idx (cd_categoria DESC),
  INDEX FK_SABOR_idx (cd_sabor DESC),
  CONSTRAINT FK_CATEGORIA
    FOREIGN KEY (cd_categoria)
    REFERENCES categoria (cd_categoria)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT FK_SABOR
    FOREIGN KEY (cd_sabor)
    REFERENCES sabor (cd_sabor)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
;