IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Product] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [BarCode] nvarchar(max) NULL,
    [Price] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Producto] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Codigobarras] nvarchar(max) NULL,
    [Precio] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_Producto] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Stock] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [BarCode] nvarchar(max) NULL,
    [Price] real NOT NULL,
    CONSTRAINT [PK_Stock] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Store] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Store] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Sucursal] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Sucursal] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Inventario] (
    [Id] int NOT NULL IDENTITY,
    [Idsucursal] int NOT NULL,
    [Idproducto] int NOT NULL,
    [Cantidad] int NOT NULL,
    [IdproductoNavigationId] int NULL,
    [IdsucursalNavigationId] int NULL,
    CONSTRAINT [PK_Inventario] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Inventario_Producto_IdproductoNavigationId] FOREIGN KEY ([IdproductoNavigationId]) REFERENCES [Producto] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Inventario_Sucursal_IdsucursalNavigationId] FOREIGN KEY ([IdsucursalNavigationId]) REFERENCES [Sucursal] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Inventario_IdproductoNavigationId] ON [Inventario] ([IdproductoNavigationId]);

GO

CREATE INDEX [IX_Inventario_IdsucursalNavigationId] ON [Inventario] ([IdsucursalNavigationId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200215235659_Initial', N'3.1.1');

GO

