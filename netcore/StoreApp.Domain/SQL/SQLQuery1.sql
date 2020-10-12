
/*==============================================================*/
/* Table: INVENTARIO                                            */
/*==============================================================*/
create table INVENTARIO (
   ID                   integer              identity,
   IDSUCURSAL           integer              not null,
   IDPRODUCTO           integer              not null,
   CANTIDAD             integer              not null,
   constraint PK_INVENTARIO primary key (ID, IDSUCURSAL, IDPRODUCTO)
)
go

create table INVENTARIO (
   ID                   integer              identity,
   IDSUCURSAL           integer              not null,
   IDPRODUCTO           integer              not null,
   CANTIDAD             money                not null,
   constraint PK_INVENTARIO primary key (ID, IDSUCURSAL, IDPRODUCTO)
)
go

/*==============================================================*/
/* Table: PRODUCTO                                              */
/*==============================================================*/
create table PRODUCTO (
   ID                   integer              identity,
   NAME                 varchar(100)         null,
   CODIGOBARRAS         varchar(200)         null,
   PRECIO               money                null,
   constraint PK_PRODUCTO primary key (ID)
)
go

/*==============================================================*/
/* Table: SUCURSAL                                              */
/*==============================================================*/
create table SUCURSAL (
   ID                   integer              identity,
   NAME                 varchar(100)         null,
   constraint PK_SUCURSAL primary key (ID)
)
go

alter table INVENTARIO
   add constraint FK_INVENTAR_REFERENCE_SUCURSAL foreign key (IDSUCURSAL)
      references SUCURSAL (ID)
go

alter table INVENTARIO
   add constraint FK_INVENTAR_REFERENCE_PRODUCTO foreign key (IDPRODUCTO)
      references PRODUCTO (ID)
go