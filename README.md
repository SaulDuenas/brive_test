# brive_test
Repositorio del ejercico de web api de  la propuesta de empleo de Brivé | Soluciones de talento

Actualmente la empresa X, requiere un control de inventario para sus actuales sucursales, ya que vende los mismos productos en ambas. Necesita saber la existencia de productos que tiene en cada sucursal, podrá buscar un producto en especifico. Cabe mencionar que es muy probable que en los próximos días, se inaugure una nueva sucursal.												
												
Actualmente las sucursales tienen los siguientes productos:												
sucursal A							sucursal B					
												
												
	Nombre	Codigo de barras	Cantidad	Precio Unitario				Nombre	Codigo de barras	Cantidad	Precio Unitario	
	Café Legal	100010	5	$7.00				Café Legal	100010	8	$7.00	
	Chocolate Abuelita	100011	6	$15.00				Chocolate Abuelita	100011	4	$15.00	
	Bonafina	100012	1	$12.00				Bonafina	100012	2	$12.00	
												
												
												
Al agregar las siguientes existencias:												
	3 chocolates	sucursal A										
	2 cafes 	sucursal A										
	2 chocolates	sucursal B										
	1 Bonafina	sucursal B										
												
El resultado esperado, al consultar la informacion:												
sucursal A							sucursal B					
												
												
	Nombre	Codigo de barras	Cantidad	Precio Unitario				Nombre	Codigo de barras	Cantidad	Precio Unitario	
	Café Legal	100010	7	$7.00				Café Legal	100010	8	$7.00	
	Chocolate Abuelita	100011	9	$15.00				Chocolate Abuelita	100011	6	$15.00	
	Bonafina	100012	1	$12.00				Bonafina	100012	3	$12.00	
												
												
												
												
												
												
												
												
												
												
												
Al comprar												
15	chocolate	sucursal A										
2	cafes 	sucursal B										
												
El resultado esperado, al consultar la informacion:												
sucursal A							sucursal B					
												
												
	Nombre	Codigo de barras	Cantidad	Precio Unitario				Nombre	Codigo de barras	Cantidad	Precio Unitario	
	Café Legal	100010	7	$7.00				Café Legal	100010	6	$7.00	
	Chocolate Abuelita	100011	24	$15.00				Chocolate Abuelita	100011	6	$15.00	
	Bonafina	100012	1	$12.00				Bonafina	100012	3	$12.00	
												
												
Al agregar un nuevo producto												
	Coca Cola	100040										
	Pepsi	100020										
												
El resultado esperado, al consultar la informacion:												
sucursal A							sucursal B					
												
												
	Nombre	Codigo de barras	Cantidad	Precio Unitario				Nombre	Codigo de barras	Cantidad	Precio Unitario	
	Café Legal	100010	7	$7.00				Café Legal	100010	6	$7.00	
	Chocolate Abuelita	100011	8	$15.00				Chocolate Abuelita	100011	6	$15.00	
	Bonafina	100012	1	$12.00				Bonafina	100012	3	$12.00	
	Pepsi	100020	0	$11.00				Pepsi	100020	0	$11.00	
	Coca Cola	100040	0	$13.00				Coca Cola	100040	0	$13.00	
												
