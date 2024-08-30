string nombreArchivoCadetes = "Cadetes.csv";
string nombreArchivoCadeteria = "Cadeteria.csv";
if (HelperDeCSV.Existe(nombreArchivoCadeteria) && HelperDeCSV.Existe(nombreArchivoCadetes))
{
    var cadeteria = HelperDeCSV.LeerCadeteria(nombreArchivoCadeteria);
    foreach (var cadete in HelperDeCSV.LeerCadetes(nombreArchivoCadetes))
    {
        cadeteria.Cadetes.Add(cadete);
    }

    int nroPedido = 0;
    int seleccion;
    
    do
    {         
        Menu menu = new Menu($"Cadeteria {cadeteria.Nombre} - {cadeteria.Telefono}", ["Dar pedido de alta", "Asignar pedido", "Cambiar estado del pedido", "Reasignar pedido", "Cerrar"]);
        seleccion = menu.MenuDisplay();
        switch (seleccion)
        {
            
            case 0:
                nroPedido++;
                string nombreCliente;
                string direccionCliente;
                string telefonoCliente;
                string datosReferenciaDireccion;
                string observaciones;
                do
                {
                    Console.Clear();
                    Console.WriteLine("A continuación complete la información del pedido");
                    Console.Write("Nombre del cliente: ");
                    nombreCliente = Console.ReadLine();
                    Console.Write("Dirección del cliente: ");
                    direccionCliente = Console.ReadLine();
                    Console.Write("Datos de referencia: ");
                    datosReferenciaDireccion = Console.ReadLine();
                    Console.Write("Teléfono del cliente: ");
                    telefonoCliente = Console.ReadLine();
                    Console.Write("Observaciones sobre el pedido: ");
                    observaciones = Console.ReadLine(); 
                    if(string.IsNullOrWhiteSpace(observaciones) || string.IsNullOrWhiteSpace(nombreCliente) || string.IsNullOrWhiteSpace(direccionCliente) || string.IsNullOrWhiteSpace(telefonoCliente))
                    {
                        Console.WriteLine("Debe rellenar correctamente los campos");
                        Console.WriteLine("\nPresione cualquier tecla para continuar.");
                        Console.ReadKey();

                    }
                } while (string.IsNullOrWhiteSpace(observaciones) || string.IsNullOrWhiteSpace(nombreCliente) || string.IsNullOrWhiteSpace(direccionCliente) || string.IsNullOrWhiteSpace(telefonoCliente));

                var pedidoNuevo = new Pedido(nroPedido, observaciones, nombreCliente, direccionCliente, telefonoCliente, datosReferenciaDireccion);
                cadeteria.Pedidos.Add(pedidoNuevo);
                break;

            case 1:
                Console.Clear();
                var pedidosSinAsignar = cadeteria.Pedidos.Where(p => p.Cadete == null).ToList();
                if(pedidosSinAsignar.Count != 0)
                {
                    string ingresa;
                    int numeroPedido;
                    Console.WriteLine("---PEDIDOS SIN ASIGNAR---");
                    foreach (var pedido in pedidosSinAsignar)
                    {
                        Console.WriteLine($"Pedido de número: {pedido.Nro}");
                    }
                    do{
                        Console.WriteLine("\nIngrese el número del pedido que desea asignar: ");
                        ingresa = Console.ReadLine();
                    } while (!int.TryParse(ingresa, out numeroPedido));
                    var pedidoSeleccionado = pedidosSinAsignar.Where(p => p.Nro == numeroPedido).ToList();
                    if (pedidoSeleccionado.Count == 0)
                    {
                        Console.WriteLine("No hay ningún pedido sin asignar con ese número");
                    } else
                    {
                        Cadete cadeteAsignado = cadeteria.AsignarPedidos(pedidoSeleccionado[0]);
                        Console.WriteLine("Listo!\n");
                        Console.WriteLine($"El pedido con la siguiente información ha sido asignado al cadete {cadeteAsignado.Nombre}:  ");
                        Console.WriteLine($"Nro de pedido: {pedidoSeleccionado[0].Nro}");
                        Console.WriteLine($"Observaciones: {pedidoSeleccionado[0].Obs}");
                        Console.WriteLine($"Estado: {pedidoSeleccionado[0].Estado}");
                        pedidoSeleccionado[0].VerDatosCliente();
                    }
                }else
                {
                    Console.WriteLine("No hay pedidos para asignar");
                }
                Console.WriteLine("\nPresione cualquier tecla para continuar.");
                Console.ReadKey();
                break;

            case 2:
                string num;
                int numIngresado;
                Console.Clear();
                do
                {
                    Console.WriteLine("Ingrese el numero de pedido cuyo estado desea modificar: ");
                    num = Console.ReadLine();
                } while (!int.TryParse(num, out numIngresado));
                cadeteria.CambiarEstadoDelPedido(numIngresado);
                break;

            case 3:
                Console.Clear();
                Console.WriteLine("--- PEDIDOS DISPONIBLES PARA REASIGNAR ---");
                string ingreso;
                int numPedido;
                int cantPedidosDispReasig = cadeteria.Pedidos.Count(p => p.Estado != Estados.Entregado && p.Cadete != null);
                if (cantPedidosDispReasig != 0)
                {
                    foreach (var pedido in cadeteria.Pedidos.Where(p => p.Estado != Estados.Entregado && p.Cadete != null).ToList())
                    {
                        Console.WriteLine($"Nro de pedido: {pedido.Nro}");
                        Console.WriteLine($"Cadete: {pedido.Cadete.Nombre}");
                        Console.WriteLine($"Observaciones: {pedido.Obs}");
                        Console.WriteLine($"Estado: {pedido.Estado}");
                        pedido.VerDatosCliente();
                        Console.WriteLine();
                    }
                    do
                    {
                        Console.Write("\nIngrese el numero del pedido que desea reasignar: ");
                        ingreso = Console.ReadLine();
                    } while (!int.TryParse(ingreso, out numPedido));
                    Cadete cadeteReasignado = cadeteria.ReasignarPedidos(numPedido);
                    if (cadeteReasignado != null)
                    {
                        Console.WriteLine($"El pedido ha sido correctamente asignado al cadete {cadeteReasignado.Nombre}");
                    }
                } else
                {
                    Console.WriteLine("No hay pedidos disponibles para reasignar");
                }
                Console.WriteLine("Presione cualquier tecla para continuar.");
                Console.ReadKey();
                break;
    
            case 4:
                Console.Clear();
                Console.WriteLine("Final de Jornada-Informe");
                cadeteria.MostrarInforme();
                Console.ReadKey();
                break;
        }

    } while (seleccion != 4);
}else
{
    Console.WriteLine("No se encontró la información de la cadetería");
}
