if exists (select 1
          from sysobjects
          where id = object_id('TGR_PRODUCTOAGREGAREXISTENCIASCERO')
          and type = 'TR')
   drop trigger TGR_PRODUCTOAGREGAREXISTENCIASCERO
go

if exists (select 1
          from sysobjects
          where id = object_id('TGR_SUCURSALAGREGAREXISTENCIASCERO')
          and type = 'TR')
   drop trigger TGR_SUCURSALAGREGAREXISTENCIASCERO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('EXISTENCIA') and o.name = 'FK_EXISTENC_REFERENCE_SUCURSAL')
alter table EXISTENCIA
   drop constraint FK_EXISTENC_REFERENCE_SUCURSAL
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('EXISTENCIA') and o.name = 'FK_EXISTENC_REFERENCE_PRODUCTO')
alter table EXISTENCIA
   drop constraint FK_EXISTENC_REFERENCE_PRODUCTO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('MOVIMIENTO') and o.name = 'FK_MOVIMIEN_REFERENCE_PRODUCTO')
alter table MOVIMIENTO
   drop constraint FK_MOVIMIEN_REFERENCE_PRODUCTO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('MOVIMIENTO') and o.name = 'FK_MOVIMIEN_REFERENCE_SUCURSAL')
alter table MOVIMIENTO
   drop constraint FK_MOVIMIEN_REFERENCE_SUCURSAL
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('MOVIMIENTO') and o.name = 'FK_MOVIMIEN_REFERENCE_TIPOTRAN')
alter table MOVIMIENTO
   drop constraint FK_MOVIMIEN_REFERENCE_TIPOTRAN
go

if exists (select 1
            from  sysobjects
           where  id = object_id('EXISTENCIA')
            and   type = 'U')
   drop table EXISTENCIA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('MOVIMIENTO')
            and   type = 'U')
   drop table MOVIMIENTO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PRODUCTO')
            and   type = 'U')
   drop table PRODUCTO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SUCURSAL')
            and   type = 'U')
   drop table SUCURSAL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TIPOTRANSACCION')
            and   type = 'U')
   drop table TIPOTRANSACCION
go

/*==============================================================*/
/* Table: EXISTENCIA                                            */
/*==============================================================*/
create table EXISTENCIA (
   IDSUCURSAL           uniqueidentifier     not null,
   IDPRODUCTO           uniqueidentifier     not null,
   CANTIDAD             integer              not null
      constraint CKC_CANTIDAD_EXISTENC check (CANTIDAD >= 0),
   constraint PK_EXISTENCIA primary key nonclustered (IDSUCURSAL, IDPRODUCTO)
)
go

/*==============================================================*/
/* Table: MOVIMIENTO                                            */
/*==============================================================*/
create table MOVIMIENTO (
   ID                   uniqueidentifier     not null default newid(),
   IDSUCURSAL           uniqueidentifier     not null,
   IDPRODUCTO           uniqueidentifier     not null,
   IDTIPOTRANS          integer              not null,
   CANTIDAD             integer              null,
   PRECIOOPERACION      decimal(10,2)        null,
   FECHAOPERACION       datetime             null default getdate(),
   constraint PK_MOVIMIENTO primary key nonclustered (ID, IDSUCURSAL, IDPRODUCTO)
)
go

/*==============================================================*/
/* Table: PRODUCTO                                              */
/*==============================================================*/
create table PRODUCTO (
   ID                   uniqueidentifier     not null default newid(),
   NOMBRE               varchar(100)         not null,
   CODIGOBARRAS         varchar(200)         null,
   PRECIO               decimal(10,2)        null,
   constraint PK_PRODUCTO primary key (ID)
)
go

/*==============================================================*/
/* Table: SUCURSAL                                              */
/*==============================================================*/
create table SUCURSAL (
   ID                   uniqueidentifier     not null default newid(),
   NOMBRE               varchar(100)         not null,
   constraint PK_SUCURSAL primary key nonclustered (ID)
)
go

/*==============================================================*/
/* Table: TIPOTRANSACCION                                       */
/*==============================================================*/
create table TIPOTRANSACCION (
   ID                   integer              not null,
   NOMBRE               varchar(50)          null,
   constraint PK_TIPOTRANSACCION primary key nonclustered (ID)
)
go

alter table EXISTENCIA
   add constraint FK_EXISTENC_REFERENCE_SUCURSAL foreign key (IDSUCURSAL)
      references SUCURSAL (ID)
         on update cascade on delete cascade
go

alter table EXISTENCIA
   add constraint FK_EXISTENC_REFERENCE_PRODUCTO foreign key (IDPRODUCTO)
      references PRODUCTO (ID)
         on update cascade on delete cascade
go

alter table MOVIMIENTO
   add constraint FK_MOVIMIEN_REFERENCE_PRODUCTO foreign key (IDPRODUCTO)
      references PRODUCTO (ID)
go

alter table MOVIMIENTO
   add constraint FK_MOVIMIEN_REFERENCE_SUCURSAL foreign key (IDSUCURSAL)
      references SUCURSAL (ID)
go

alter table MOVIMIENTO
   add constraint FK_MOVIMIEN_REFERENCE_TIPOTRAN foreign key (IDTIPOTRANS)
      references TIPOTRANSACCION (ID)
go


CREATE TRIGGER [tgr_ProductoAgregarExistenciasCero]
	ON [dbo].[PRODUCTO]
	FOR INSERT
	AS
	BEGIN
				SET NOCOUNT ON
	
		BEGIN TRY
			BEGIN TRANSACTION inserted

			INSERT INTO EXISTENCIA (IDSUCURSAL,IDPRODUCTO,CANTIDAD )
			SELECT DISTINCT s.ID, p.ID, 0
			FROM SUCURSAL as s CROSS JOIN inserted as p
			WHERE s.ID NOT IN (
								SELECT DISTINCT e.IDSUCURSAL
								FROM inserted AS p
								INNER JOIN EXISTENCIA as e ON (p.ID = e.IDPRODUCTO)	
							  )

			COMMIT 
	
		END TRY

		BEGIN CATCH
			ROLLBACK
		END CATCH
	END
go


CREATE TRIGGER [tgr_SucursalAgregarExistenciasCero]
    ON [dbo].[SUCURSAL]
    FOR  INSERT
    AS
    BEGIN
    
    SET NOCOUNT ON
	
		BEGIN TRY
			BEGIN TRANSACTION inserted

			INSERT INTO EXISTENCIA (IDSUCURSAL,IDPRODUCTO,CANTIDAD )
			SELECT DISTINCT s.ID, p.ID, 0
			FROM inserted as s CROSS JOIN PRODUCTO as p
			WHERE p.ID NOT IN (
								SELECT DISTINCT e.IDPRODUCTO
								FROM inserted AS s
								INNER JOIN EXISTENCIA as e ON (s.ID = e.IDSUCURSAL)	
							  )

			COMMIT 
	
		END TRY

		BEGIN CATCH
			ROLLBACK
		END CATCH
        
    END
go
