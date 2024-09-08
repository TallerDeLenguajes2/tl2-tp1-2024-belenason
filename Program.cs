string nombreArchivoCadetes = "Cadetes";
string nombreArchivoCadeteria = "Cadeteria";

var menuAccesoDatos = new Menu("Desea acceder a los datos mediante un archivo:", ["JSON", "CSV"]);
int formaAccesoDatos = menuAccesoDatos.MenuDisplay();

string tipoArchivo = null;
int existe = 0;
Cadeteria cadeteria = null;
AccesoADatos accesoDatos = null;

switch (formaAccesoDatos)
{
    case 0:
        tipoArchivo = "json";
        accesoDatos = new AccesoJSON();
        break;
    case 1:
        tipoArchivo = "csv";
        accesoDatos = new AccesoCSV();
        break;

}

if (AccesoADatos.Existe(tipoArchivo+"/"+nombreArchivoCadeteria+"."+tipoArchivo))
{
    existe = 1;
    cadeteria = accesoDatos.LeerCadeteria(nombreArchivoCadeteria);
    cadeteria.AsignarCadetes(accesoDatos.LeerCadetes(nombreArchivoCadetes));
}

if (existe == 1 && cadeteria != null && cadeteria.Cadetes != null)
{
    int seleccion;
    
    do
    {         
        Menu menu = new Menu($"Cadeteria {cadeteria.Nombre} - {cadeteria.Telefono}", ["Dar pedido de alta", "Asignar pedido", "Cambiar estado del pedido", "Reasignar pedido", "Cerrar"]);
        seleccion = menu.MenuDisplay();
        switch (seleccion)
        {
            
            case 0:
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
                string [] infoPedido = [observaciones, nombreCliente, direccionCliente, telefonoCliente, datosReferenciaDireccion];
                cadeteria.GuardarPedido(infoPedido);
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
                    var pedidoSeleccionado = pedidosSinAsignar.Find(p => p.Nro == numeroPedido);
                    if (pedidoSeleccionado == null)
                    {
                        Console.WriteLine("No hay ningún pedido sin asignar con ese número");
                    } else
                    {
                        Cadete cadeteAsignado = cadeteria.AsignarPedidos(pedidoSeleccionado.Nro);
                        Console.WriteLine("Listo!\n");
                        Console.WriteLine($"El pedido con la siguiente información ha sido asignado al cadete {cadeteAsignado.Nombre}:  ");
                        Console.WriteLine($"Nro de pedido: {pedidoSeleccionado.Nro}");
                        Console.WriteLine($"Observaciones: {pedidoSeleccionado.Obs}");
                        Console.WriteLine($"Estado: {pedidoSeleccionado.Estado}");
                        pedidoSeleccionado.VerDatosCliente();
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
                Menu menuDeSeleccion = new Menu("Seleccione el estado al que desea cambiar el pedido:", ["En camino", "Entregado"]);
                int selec = menuDeSeleccion.MenuDisplay();
                cadeteria.CambiarEstadoDelPedido(numIngresado, selec);
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
                float totalEnvios = 0;
                Console.WriteLine("Informe de los cadetes:");
                foreach (var cadete in cadeteria.Cadetes)
                {
                    Console.WriteLine($"Nombre: {cadete.Nombre} || Cantidad de pedidos realizados: {cadeteria.CantEntregasCadete(cadete.Id)} || Monto ganado: ${cadeteria.JornalACobrar(cadete.Id)}");
                    totalEnvios = totalEnvios + cadeteria.CantEntregasCadete(cadete.Id);
                }
                float promedioEnviosXCadete = totalEnvios/(float)cadeteria.Cadetes.Count;
                Console.WriteLine("\nInforme general:");
                Console.WriteLine($"Cantidad total de envios: {totalEnvios}");
                Console.WriteLine($"Promedio de envios por cadete: {promedioEnviosXCadete}");
                Console.ReadKey();
                break;
        }

    } while (seleccion != 4);
}else
{
    Console.WriteLine("No se encontró la información de la cadetería");
}
