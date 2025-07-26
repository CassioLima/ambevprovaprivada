-- DROP TABLE IF EXISTS public."Users";
-- DROP TABLE IF EXISTS public."Sales";
-- DROP TABLE IF EXISTS public."SaleItems";
-- DROP INDEX IF EXISTS public."IX_SaleItems_SaleId";

CREATE TABLE IF NOT EXISTS public."Users"
(
    "Id" uuid NOT NULL DEFAULT gen_random_uuid(),
    "Username" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "Email" character varying(100) COLLATE pg_catalog."default" NOT NULL,
    "Phone" character varying(20) COLLATE pg_catalog."default" NOT NULL,
    "Password" character varying(100) COLLATE pg_catalog."default" NOT NULL,
    "Role" character varying(20) COLLATE pg_catalog."default" NOT NULL,
    "Status" character varying(20) COLLATE pg_catalog."default" NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "UpdatedAt" timestamp with time zone,
    CONSTRAINT "PK_Users" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Users"
    OWNER to developer;


CREATE TABLE IF NOT EXISTS public."Sales"
(
    "Id" uuid NOT NULL,
    "SaleNumber" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "SaleDate" timestamp with time zone NOT NULL,
    "Customer" character varying(100) COLLATE pg_catalog."default" NOT NULL,
    "Branch" character varying(100) COLLATE pg_catalog."default" NOT NULL,
    "IsCancelled" boolean NOT NULL,
    CONSTRAINT "PK_Sales" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Sales"
    OWNER to developer;

CREATE TABLE IF NOT EXISTS public."SaleItems"
(
    "Id" uuid NOT NULL,
    "SaleId" uuid NOT NULL,
    "ProductId" uuid NOT NULL,
    "ProductDescription" character varying(200) COLLATE pg_catalog."default" NOT NULL,
    "Quantity" integer NOT NULL,
    "UnitPrice" numeric(18,2) NOT NULL,
    "DiscountPercentage" numeric(5,2) NOT NULL,
    CONSTRAINT "PK_SaleItems" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_SaleItems_Sales_SaleId" FOREIGN KEY ("SaleId")
        REFERENCES public."Sales" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."SaleItems"
    OWNER to developer;


CREATE INDEX IF NOT EXISTS "IX_SaleItems_SaleId"
    ON public."SaleItems" USING btree
    ("SaleId" ASC NULLS LAST)
    TABLESPACE pg_default;    


-- ========== SEED USERS ==========
DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM "Users" WHERE "Email" = 'user1@email.com') THEN
        INSERT INTO "Users"("Id","Username","Email","Phone","Password","Role","Status","CreatedAt")
        VALUES (gen_random_uuid(),'User 1','user1@email.com','(11) 90000-0001','hashed_pass','Admin','Active',NOW());
    END IF;

    IF NOT EXISTS (SELECT 1 FROM "Users" WHERE "Email" = 'user2@email.com') THEN
        INSERT INTO "Users"("Id","Username","Email","Phone","Password","Role","Status","CreatedAt")
        VALUES (gen_random_uuid(),'User 2','user2@email.com','(11) 90000-0002','hashed_pass','User','Active',NOW());
    END IF;

    IF NOT EXISTS (SELECT 1 FROM "Users" WHERE "Email" = 'user3@email.com') THEN
        INSERT INTO "Users"("Id","Username","Email","Phone","Password","Role","Status","CreatedAt")
        VALUES (gen_random_uuid(),'User 3','user3@email.com','(11) 90000-0003','hashed_pass','User','Active',NOW());
    END IF;

    IF NOT EXISTS (SELECT 1 FROM "Users" WHERE "Email" = 'user4@email.com') THEN
        INSERT INTO "Users"("Id","Username","Email","Phone","Password","Role","Status","CreatedAt")
        VALUES (gen_random_uuid(),'User 4','user4@email.com','(11) 90000-0004','hashed_pass','User','Active',NOW());
    END IF;

    IF NOT EXISTS (SELECT 1 FROM "Users" WHERE "Email" = 'user5@email.com') THEN
        INSERT INTO "Users"("Id","Username","Email","Phone","Password","Role","Status","CreatedAt")
        VALUES (gen_random_uuid(),'User 5','user5@email.com','(11) 90000-0005','hashed_pass','User','Active',NOW());
    END IF;

    IF NOT EXISTS (SELECT 1 FROM "Users" WHERE "Email" = 'user6@email.com') THEN
        INSERT INTO "Users"("Id","Username","Email","Phone","Password","Role","Status","CreatedAt")
        VALUES (gen_random_uuid(),'User 6','user6@email.com','(11) 90000-0006','hashed_pass','User','Active',NOW());
    END IF;

    IF NOT EXISTS (SELECT 1 FROM "Users" WHERE "Email" = 'user7@email.com') THEN
        INSERT INTO "Users"("Id","Username","Email","Phone","Password","Role","Status","CreatedAt")
        VALUES (gen_random_uuid(),'User 7','user7@email.com','(11) 90000-0007','hashed_pass','User','Active',NOW());
    END IF;

    IF NOT EXISTS (SELECT 1 FROM "Users" WHERE "Email" = 'user8@email.com') THEN
        INSERT INTO "Users"("Id","Username","Email","Phone","Password","Role","Status","CreatedAt")
        VALUES (gen_random_uuid(),'User 8','user8@email.com','(11) 90000-0008','hashed_pass','User','Active',NOW());
    END IF;

    IF NOT EXISTS (SELECT 1 FROM "Users" WHERE "Email" = 'user9@email.com') THEN
        INSERT INTO "Users"("Id","Username","Email","Phone","Password","Role","Status","CreatedAt")
        VALUES (gen_random_uuid(),'User 9','user9@email.com','(11) 90000-0009','hashed_pass','User','Active',NOW());
    END IF;

    IF NOT EXISTS (SELECT 1 FROM "Users" WHERE "Email" = 'user10@email.com') THEN
        INSERT INTO "Users"("Id","Username","Email","Phone","Password","Role","Status","CreatedAt")
        VALUES (gen_random_uuid(),'User 10','user10@email.com','(11) 90000-0010','hashed_pass','User','Active',NOW());
    END IF;
END$$;

-- ========== SEED SALES ==========
DO $$
BEGIN
    FOR i IN 1..10 LOOP
        IF NOT EXISTS (SELECT 1 FROM "Sales" WHERE "SaleNumber" = CONCAT('S', i)) THEN
            INSERT INTO "Sales"("Id","SaleNumber","SaleDate","Customer","Branch","IsCancelled")
            VALUES (gen_random_uuid(), CONCAT('S', i), NOW(), CONCAT('Customer ', i), 'Main Branch', FALSE);
        END IF;
    END LOOP;
END$$;

-- ========== SEED SALE ITEMS ==========
DO $$
DECLARE
    sale_id uuid;
BEGIN
    FOR i IN 1..10 LOOP
        SELECT "Id" INTO sale_id FROM "Sales" WHERE "SaleNumber" = CONCAT('S', i);

        IF sale_id IS NOT NULL AND NOT EXISTS (SELECT 1 FROM "SaleItems" WHERE "SaleId" = sale_id) THEN
            INSERT INTO "SaleItems"("Id","SaleId","ProductId","ProductDescription","Quantity","UnitPrice","DiscountPercentage")
            VALUES (gen_random_uuid(), sale_id, gen_random_uuid(), CONCAT('Product ', i), 5, 100.00, 10.0);
        END IF;
    END LOOP;
END$$;

