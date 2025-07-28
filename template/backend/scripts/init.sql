-- ========================================================
-- CRIAÇÃO DAS TABELAS
-- ========================================================

-- Users
CREATE TABLE IF NOT EXISTS public."Users"
(
    "Id" uuid NOT NULL DEFAULT gen_random_uuid(),
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
    "Id" uuid NOT NULL DEFAULT gen_random_uuid(),
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
    "Id" uuid NOT NULL DEFAULT gen_random_uuid(),
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
    "Id" uuid NOT NULL DEFAULT gen_random_uuid(),
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
    "Id" uuid NOT NULL DEFAULT gen_random_uuid(),
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
    "Id" uuid NOT NULL DEFAULT gen_random_uuid(),
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
-- SEED DATA
-- ========================================================

-- Users
DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM "Users" WHERE "Email" = 'user1@email.com') THEN
        INSERT INTO "Users"("Id","Username","Email","Phone","Password","Role","Status","CreatedAt")
        VALUES (gen_random_uuid(),'User 1','user1@email.com','(11) 90000-0001','hashed_pass','Admin','Active',NOW());
    END IF;
    -- (demais usuários iguais ao original)
END$$;


-- Customers
DO $$
BEGIN
    FOR i IN 1..10 LOOP
        IF NOT EXISTS (SELECT 1 FROM "Customers" WHERE "Email" = CONCAT('customer', i, '@email.com')) THEN
            INSERT INTO "Customers"("Name","Email","Phone","Address")
            VALUES (
                CONCAT('Customer ', i),
                CONCAT('customer', i, '@email.com'),
                CONCAT('(11) 90000-00', LPAD(i::text, 2, '0')),
                CONCAT('Address ', i)
            );
        END IF;
    END LOOP;
END$$;


-- Products
DO $$
BEGIN
    FOR i IN 1..10 LOOP
        IF NOT EXISTS (SELECT 1 FROM "Products" WHERE "Name" = CONCAT('Product ', i)) THEN
            INSERT INTO "Products"("Name","Description","Price","StockQuantity")
            VALUES (CONCAT('Product ', i), CONCAT('Description for product ', i), (i * 10)::numeric, 100);
        END IF;
    END LOOP;
END$$;


-- Estoque inicial
DO $$
DECLARE
    prod_id uuid;
BEGIN
    FOR i IN 1..10 LOOP
        SELECT "Id" INTO prod_id FROM "Products" WHERE "Name" = CONCAT('Product ', i);
        IF prod_id IS NOT NULL AND NOT EXISTS (SELECT 1 FROM "StockControl" WHERE "ProductId" = prod_id AND "MovementType" = 'Entrada') THEN
            INSERT INTO "StockControl"("ProductId","MovementType","Quantity")
            VALUES (prod_id, 'Entrada', 100);
        END IF;
    END LOOP;
END$$;


-- Vendas e Itens
DO $$
DECLARE
    sale_id uuid;
    customer_id uuid;
    product_id uuid;
    unit_price numeric;
BEGIN
    FOR i IN 1..10 LOOP
        SELECT "Id" INTO customer_id FROM "Customers" ORDER BY random() LIMIT 1;
        INSERT INTO "Sales"("SaleNumber","CustomerId","Branch","TotalAmount","PaymentMethod","Status")
        VALUES (CONCAT('S', i), customer_id, 'Main Branch', 0, 'Credit Card', 'Paid')
        RETURNING "Id" INTO sale_id;

        SELECT "Id","Price" INTO product_id, unit_price FROM "Products" ORDER BY random() LIMIT 1;

        INSERT INTO "SaleItems"("SaleId","ProductId","ProductName","Quantity","UnitPrice","DiscountPercentage")
        VALUES (sale_id, product_id, (SELECT "Name" FROM "Products" WHERE "Id" = product_id), 5, unit_price, 10);

        UPDATE "Sales"
        SET "TotalAmount" = (SELECT SUM(("Quantity" * "UnitPrice") - (("Quantity" * "UnitPrice") * "DiscountPercentage"/100))
                             FROM "SaleItems" WHERE "SaleId" = sale_id)
        WHERE "Id" = sale_id;

        INSERT INTO "StockControl"("ProductId","MovementType","Quantity")
        VALUES (product_id, 'Saída', 5);

        UPDATE "Products" SET "StockQuantity" = "StockQuantity" - 5 WHERE "Id" = product_id;
    END LOOP;
END$$;
