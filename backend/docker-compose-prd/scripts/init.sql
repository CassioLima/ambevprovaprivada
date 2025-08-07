-- ========================================================
-- CRIAÇÃO DAS TABELAS
-- ========================================================

-- Users
CREATE TABLE IF NOT EXISTS public."Users"
(
    "Id" uuid NOT NULL, -- sem DEFAULT gen_random_uuid()
    "Username" character varying(50) NOT NULL,
    "Email" character varying(100) NOT NULL,
    "Phone" character varying(20) NOT NULL,
    "Password" character varying(100) NOT NULL,
    "Role" character varying(20) NOT NULL,
    "Status" character varying(20) NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "UpdatedAt" timestamp with time zone,
    CONSTRAINT "PK_Users" PRIMARY KEY ("Id")
);

ALTER TABLE IF EXISTS public."Users" OWNER to developer;


-- Customers
CREATE TABLE IF NOT EXISTS public."Customers"
(
    "Id" uuid NOT NULL, -- sem DEFAULT
    "Name" character varying(100) NOT NULL,
    "Email" character varying(100) NOT NULL,
    "Phone" character varying(20),
    "Address" character varying(200),
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "UpdatedAt" timestamp with time zone NULL,
    CONSTRAINT "PK_Customers" PRIMARY KEY ("Id")
);

ALTER TABLE IF EXISTS public."Customers" OWNER TO developer;


-- Products
CREATE TABLE IF NOT EXISTS public."Products"
(
    "Id" uuid NOT NULL, -- sem DEFAULT
    "Name" character varying(100) NOT NULL,
    "Description" character varying(200),
    "Price" numeric(18,2) NOT NULL,
    "StockQuantity" integer NOT NULL DEFAULT 0,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_Products" PRIMARY KEY ("Id")
);

ALTER TABLE IF EXISTS public."Products" OWNER TO developer;


-- StockControl
CREATE TABLE IF NOT EXISTS public."StockControl"
(
    "Id" uuid NOT NULL, -- sem DEFAULT
    "ProductId" uuid NOT NULL,
    "MovementType" character varying(10) NOT NULL, -- 'Entrada' ou 'Saída'
    "Quantity" integer NOT NULL,
    "MovementDate" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_StockControl" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_StockControl_Products" FOREIGN KEY ("ProductId")
        REFERENCES public."Products" ("Id") ON DELETE RESTRICT
);

ALTER TABLE IF EXISTS public."StockControl" OWNER TO developer;


-- Sales
CREATE TABLE IF NOT EXISTS public."Sales"
(
    "Id" uuid NOT NULL, -- sem DEFAULT
    "SaleNumber" character varying(50) NOT NULL,
    "SaleDate" timestamp with time zone NOT NULL DEFAULT now(),
    "CustomerId" uuid NOT NULL,
    "Branch" character varying(100) NOT NULL,
    "PaymentMethod" character varying(50) NOT NULL,
    "Status" character varying(20) NOT NULL,
    "TotalAmount" numeric(18,2) NOT NULL DEFAULT 0,
    CONSTRAINT "PK_Sales" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Sales_Customers" FOREIGN KEY ("CustomerId")
        REFERENCES public."Customers" ("Id") ON DELETE RESTRICT
);

ALTER TABLE IF EXISTS public."Sales" OWNER TO developer;


-- SaleItems
CREATE TABLE IF NOT EXISTS public."SaleItems"
(
    "Id" uuid NOT NULL, -- sem DEFAULT
    "SaleId" uuid NOT NULL,
    "ProductId" uuid NOT NULL,
    "ProductName" character varying(200) NOT NULL,
    "Quantity" integer NOT NULL,
    "UnitPrice" numeric(18,2) NOT NULL,
    "DiscountPercentage" numeric(5,2) NOT NULL DEFAULT 0,
    CONSTRAINT "PK_SaleItems" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_SaleItems_Sales" FOREIGN KEY ("SaleId")
        REFERENCES public."Sales" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_SaleItems_Products" FOREIGN KEY ("ProductId")
        REFERENCES public."Products" ("Id") ON DELETE RESTRICT
);

ALTER TABLE IF EXISTS public."SaleItems" OWNER TO developer;

CREATE INDEX IF NOT EXISTS "IX_SaleItems_SaleId"
    ON public."SaleItems" USING btree ("SaleId");



-- ========================================================
-- USERS (10)
-- ========================================================
INSERT INTO "Users"("Id","Username","Email","Phone","Password","Role","Status","CreatedAt") VALUES
('a4c9b080-5b9c-44b3-a0fc-5c50c798f001','User 1','user1@email.com','(11) 90000-0001','hashed_pass','Admin','Active',NOW()),
('a4c9b080-5b9c-44b3-a0fc-5c50c798f002','User 2','user2@email.com','(11) 90000-0002','hashed_pass','User','Active',NOW()),
('a4c9b080-5b9c-44b3-a0fc-5c50c798f003','User 3','user3@email.com','(11) 90000-0003','hashed_pass','User','Active',NOW()),
('a4c9b080-5b9c-44b3-a0fc-5c50c798f004','User 4','user4@email.com','(11) 90000-0004','hashed_pass','User','Active',NOW()),
('a4c9b080-5b9c-44b3-a0fc-5c50c798f005','User 5','user5@email.com','(11) 90000-0005','hashed_pass','User','Active',NOW()),
('a4c9b080-5b9c-44b3-a0fc-5c50c798f006','User 6','user6@email.com','(11) 90000-0006','hashed_pass','User','Active',NOW()),
('a4c9b080-5b9c-44b3-a0fc-5c50c798f007','User 7','user7@email.com','(11) 90000-0007','hashed_pass','User','Active',NOW()),
('a4c9b080-5b9c-44b3-a0fc-5c50c798f008','User 8','user8@email.com','(11) 90000-0008','hashed_pass','User','Active',NOW()),
('a4c9b080-5b9c-44b3-a0fc-5c50c798f009','User 9','user9@email.com','(11) 90000-0009','hashed_pass','User','Active',NOW()),
('a4c9b080-5b9c-44b3-a0fc-5c50c798f010','User 10','user10@email.com','(11) 90000-0010','hashed_pass','User','Active',NOW())
ON CONFLICT DO NOTHING;

-- ========================================================
-- CUSTOMERS (10)
-- ========================================================
INSERT INTO "Customers"("Id","Name","Email","Phone","Address") VALUES
('e2b5cf7e-7f13-4cf6-a8b7-6a2e823b1001','Customer 1','customer1@email.com','(11) 90000-0001','Address 1'),
('e2b5cf7e-7f13-4cf6-a8b7-6a2e823b1002','Customer 2','customer2@email.com','(11) 90000-0002','Address 2'),
('e2b5cf7e-7f13-4cf6-a8b7-6a2e823b1003','Customer 3','customer3@email.com','(11) 90000-0003','Address 3'),
('e2b5cf7e-7f13-4cf6-a8b7-6a2e823b1004','Customer 4','customer4@email.com','(11) 90000-0004','Address 4'),
('e2b5cf7e-7f13-4cf6-a8b7-6a2e823b1005','Customer 5','customer5@email.com','(11) 90000-0005','Address 5'),
('e2b5cf7e-7f13-4cf6-a8b7-6a2e823b1006','Customer 6','customer6@email.com','(11) 90000-0006','Address 6'),
('e2b5cf7e-7f13-4cf6-a8b7-6a2e823b1007','Customer 7','customer7@email.com','(11) 90000-0007','Address 7'),
('e2b5cf7e-7f13-4cf6-a8b7-6a2e823b1008','Customer 8','customer8@email.com','(11) 90000-0008','Address 8'),
('e2b5cf7e-7f13-4cf6-a8b7-6a2e823b1009','Customer 9','customer9@email.com','(11) 90000-0009','Address 9'),
('e2b5cf7e-7f13-4cf6-a8b7-6a2e823b1010','Customer 10','customer10@email.com','(11) 90000-0010','Address 10')
ON CONFLICT DO NOTHING;

-- ========================================================
-- PRODUCTS (10)
-- ========================================================
INSERT INTO "Products"("Id","Name","Description","Price","StockQuantity") VALUES
('93abde52-b29f-47a9-a3e3-7f58c2e70001','Product 1','Description for product 1',10.00,100),
('93abde52-b29f-47a9-a3e3-7f58c2e70002','Product 2','Description for product 2',20.00,100),
('93abde52-b29f-47a9-a3e3-7f58c2e70003','Product 3','Description for product 3',30.00,100),
('93abde52-b29f-47a9-a3e3-7f58c2e70004','Product 4','Description for product 4',40.00,100),
('93abde52-b29f-47a9-a3e3-7f58c2e70005','Product 5','Description for product 5',50.00,100),
('93abde52-b29f-47a9-a3e3-7f58c2e70006','Product 6','Description for product 6',60.00,100),
('93abde52-b29f-47a9-a3e3-7f58c2e70007','Product 7','Description for product 7',70.00,100),
('93abde52-b29f-47a9-a3e3-7f58c2e70008','Product 8','Description for product 8',80.00,100),
('93abde52-b29f-47a9-a3e3-7f58c2e70009','Product 9','Description for product 9',90.00,100),
('93abde52-b29f-47a9-a3e3-7f58c2e70010','Product 10','Description for product 10',100.00,100)
ON CONFLICT DO NOTHING;

-- ========================================================
-- STOCK CONTROL (10)
-- ========================================================
INSERT INTO "StockControl"("Id","ProductId","MovementType","Quantity") VALUES
('1fb2ffb4-0b57-4d1d-9b72-2b7843cb0001','93abde52-b29f-47a9-a3e3-7f58c2e70001','Entrada',100),
('1fb2ffb4-0b57-4d1d-9b72-2b7843cb0002','93abde52-b29f-47a9-a3e3-7f58c2e70002','Entrada',100),
('1fb2ffb4-0b57-4d1d-9b72-2b7843cb0003','93abde52-b29f-47a9-a3e3-7f58c2e70003','Entrada',100),
('1fb2ffb4-0b57-4d1d-9b72-2b7843cb0004','93abde52-b29f-47a9-a3e3-7f58c2e70004','Entrada',100),
('1fb2ffb4-0b57-4d1d-9b72-2b7843cb0005','93abde52-b29f-47a9-a3e3-7f58c2e70005','Entrada',100),
('1fb2ffb4-0b57-4d1d-9b72-2b7843cb0006','93abde52-b29f-47a9-a3e3-7f58c2e70006','Entrada',100),
('1fb2ffb4-0b57-4d1d-9b72-2b7843cb0007','93abde52-b29f-47a9-a3e3-7f58c2e70007','Entrada',100),
('1fb2ffb4-0b57-4d1d-9b72-2b7843cb0008','93abde52-b29f-47a9-a3e3-7f58c2e70008','Entrada',100),
('1fb2ffb4-0b57-4d1d-9b72-2b7843cb0009','93abde52-b29f-47a9-a3e3-7f58c2e70009','Entrada',100),
('1fb2ffb4-0b57-4d1d-9b72-2b7843cb0010','93abde52-b29f-47a9-a3e3-7f58c2e70010','Entrada',100)
ON CONFLICT DO NOTHING;

-- ========================================================
-- SALES (10)
-- ========================================================
INSERT INTO "Sales"("Id","SaleNumber","CustomerId","Branch","PaymentMethod","Status","TotalAmount") VALUES
('11111111-aaaa-4aaa-aaaa-111111111111','S1','e2b5cf7e-7f13-4cf6-a8b7-6a2e823b1001','Main Branch','Credit Card','Paid',90.00),
('22222222-aaaa-4aaa-aaaa-222222222222','S2','e2b5cf7e-7f13-4cf6-a8b7-6a2e823b1002','Main Branch','Credit Card','Paid',100.00),
('33333333-aaaa-4aaa-aaaa-333333333333','S3','e2b5cf7e-7f13-4cf6-a8b7-6a2e823b1003','Main Branch','Credit Card','Paid',110.00),
('44444444-aaaa-4aaa-aaaa-444444444444','S4','e2b5cf7e-7f13-4cf6-a8b7-6a2e823b1004','Main Branch','Credit Card','Paid',120.00),
('55555555-aaaa-4aaa-aaaa-555555555555','S5','e2b5cf7e-7f13-4cf6-a8b7-6a2e823b1005','Main Branch','Credit Card','Paid',130.00),
('66666666-aaaa-4aaa-aaaa-666666666666','S6','e2b5cf7e-7f13-4cf6-a8b7-6a2e823b1006','Main Branch','Credit Card','Paid',140.00),
('77777777-aaaa-4aaa-aaaa-777777777777','S7','e2b5cf7e-7f13-4cf6-a8b7-6a2e823b1007','Main Branch','Credit Card','Paid',150.00),
('88888888-aaaa-4aaa-aaaa-888888888888','S8','e2b5cf7e-7f13-4cf6-a8b7-6a2e823b1008','Main Branch','Credit Card','Paid',160.00),
('99999999-aaaa-4aaa-aaaa-999999999999','S9','e2b5cf7e-7f13-4cf6-a8b7-6a2e823b1009','Main Branch','Credit Card','Paid',170.00),
('aaaaaaaa-aaaa-4aaa-aaaa-aaaaaaaaaaaa','S10','e2b5cf7e-7f13-4cf6-a8b7-6a2e823b1010','Main Branch','Credit Card','Paid',180.00)
ON CONFLICT DO NOTHING;

-- ========================================================
-- SALE ITEMS (10)
-- ========================================================
INSERT INTO "SaleItems"("Id","SaleId","ProductId","ProductName","Quantity","UnitPrice","DiscountPercentage") VALUES
('11111111-bbbb-4bbb-bbbb-111111111111','11111111-aaaa-4aaa-aaaa-111111111111','93abde52-b29f-47a9-a3e3-7f58c2e70002','Product 2',5,20.00,10),
('22222222-bbbb-4bbb-bbbb-222222222222','22222222-aaaa-4aaa-aaaa-222222222222','93abde52-b29f-47a9-a3e3-7f58c2e70003','Product 3',5,30.00,10),
('33333333-bbbb-4bbb-bbbb-333333333333','33333333-aaaa-4aaa-aaaa-333333333333','93abde52-b29f-47a9-a3e3-7f58c2e70004','Product 4',5,40.00,10),
('44444444-bbbb-4bbb-bbbb-444444444444','44444444-aaaa-4aaa-aaaa-444444444444','93abde52-b29f-47a9-a3e3-7f58c2e70005','Product 5',5,50.00,10),
('55555555-bbbb-4bbb-bbbb-555555555555','55555555-aaaa-4aaa-aaaa-555555555555','93abde52-b29f-47a9-a3e3-7f58c2e70006','Product 6',5,60.00,10),
('66666666-bbbb-4bbb-bbbb-666666666666','66666666-aaaa-4aaa-aaaa-666666666666','93abde52-b29f-47a9-a3e3-7f58c2e70007','Product 7',5,70.00,10),
('77777777-bbbb-4bbb-bbbb-777777777777','77777777-aaaa-4aaa-aaaa-777777777777','93abde52-b29f-47a9-a3e3-7f58c2e70008','Product 8',5,80.00,10),
('88888888-bbbb-4bbb-bbbb-888888888888','88888888-aaaa-4aaa-aaaa-888888888888','93abde52-b29f-47a9-a3e3-7f58c2e70009','Product 9',5,90.00,10),
('99999999-bbbb-4bbb-bbbb-999999999999','99999999-aaaa-4aaa-aaaa-999999999999','93abde52-b29f-47a9-a3e3-7f58c2e70010','Product 10',5,100.00,10),
('aaaaaaaa-bbbb-4bbb-bbbb-aaaaaaaaaaaa','aaaaaaaa-aaaa-4aaa-aaaa-aaaaaaaaaaaa','93abde52-b29f-47a9-a3e3-7f58c2e70001','Product 1',5,10.00,10)
ON CONFLICT DO NOTHING;

-- ========================================================
-- AJUSTE DE ESTOQUE PÓS-VENDA
-- ========================================================
UPDATE "Products" 
SET "StockQuantity" = "StockQuantity" - 5
WHERE "Id" IN (
    '93abde52-b29f-47a9-a3e3-7f58c2e70001',
    '93abde52-b29f-47a9-a3e3-7f58c2e70002',
    '93abde52-b29f-47a9-a3e3-7f58c2e70003',
    '93abde52-b29f-47a9-a3e3-7f58c2e70004',
    '93abde52-b29f-47a9-a3e3-7f58c2e70005',
    '93abde52-b29f-47a9-a3e3-7f58c2e70006',
    '93abde52-b29f-47a9-a3e3-7f58c2e70007',
    '93abde52-b29f-47a9-a3e3-7f58c2e70008',
    '93abde52-b29f-47a9-a3e3-7f58c2e70009',
    '93abde52-b29f-47a9-a3e3-7f58c2e70010'
);