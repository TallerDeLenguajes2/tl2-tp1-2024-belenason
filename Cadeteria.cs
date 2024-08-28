public class Cadeteria
{
    private string nombre;
    private string telefono;
    private List<Cadete> cadetes;
    public string Nombre {get => nombre;}
    public string Telefono {get => telefono;}

    public List<Cadete> Cadetes {get => cadetes;}

    public Cadeteria(string Nombre, string Telefono, List<Cadete> Cadetes)
    {
        nombre = Nombre;
        telefono = Telefono;
        cadetes = Cadetes;
    }

    public Cadete AsignarPedidos(Pedido pedido)
    {
        int menosPedidos = 100;
        Cadete cadeteMenosPedidos = cadetes[0];
        foreach (var cadete in cadetes)
        {
            if (cadete.Pedidos.Count(p => p.Estado != Estados.Entregado) <= menosPedidos)
            {
                cadeteMenosPedidos = cadete;
                menosPedidos = cadete.Pedidos.Count;
            }
        }
        cadeteMenosPedidos.Pedidos.Add(pedido);
        return cadeteMenosPedidos;
    }

    public Cadete ReasignarPedidos(int nro)
    {
        var cadeteConPedido = cadetes.Where(c => c.Pedidos.Any(p => p.Nro == nro)).ToList();
        Cadete cadeteMenosPedidos = null;
        if (cadeteConPedido.Count != 0)
        {
            var pedidoAReasignar = cadeteConPedido[0].DarDeBajaPedido(nro);
            var cadetesDisponibles = cadetes.Where(c => c.Nombre != cadeteConPedido[0].Nombre).ToList();
            int menosPedidos = int.MaxValue;
            cadeteMenosPedidos = cadetes[0];
            foreach (var cadete in cadetesDisponibles)
            {
                if (cadete.Pedidos.Count(p => p.Estado == Estados.EnCamino) <= menosPedidos)
                {
                    cadeteMenosPedidos = cadete;
                    menosPedidos = cadete.Pedidos.Count;
                }
            }
            cadeteMenosPedidos.Pedidos.Add(pedidoAReasignar);
        } else
        {
            Console.WriteLine("El número ingresado no se corresponde con ningún pedido");
        }

        return cadeteMenosPedidos;
    }

    public void CambiarEstadoDelPedido(int nro)
    {
        var cadeteConPedido = cadetes.Where(c => c.Pedidos.Any(p => p.Nro == nro)).ToList();
        if (cadeteConPedido.Count != 0)
        {
            Menu menuDeSeleccion = new Menu("Seleccione el estado al que desea cambiar el pedido:", ["En camino", "Entregado"]);
            int seleccion = menuDeSeleccion.MenuDisplay();
            switch (seleccion)
            {
                case 0:
                    cadeteConPedido[0].RetirarPedido(nro);
                    break;
                case 1:
                    cadeteConPedido[0].EntregarPedido(nro);
                    break;
            }
        }else
        {
            Console.WriteLine("El número ingresado no se corresponde con ningún pedido asignado."); 
            Console.WriteLine("Presiona cualquier tecla para continuar.");
            Console.ReadKey();
        }

    }

    public void MostrarInforme()
    {
        float totalEnvios = 0;
        Console.WriteLine("Informe de los cadetes:");
        foreach (var cadete in cadetes)
        {
            Console.WriteLine($"Nombre: {cadete.Nombre} || Cantidad de pedidos realizados: {cadete.CantidadDePedidosCompletados()} || Monto ganado: ${cadete.JornalACobrar()}");
            totalEnvios = totalEnvios + cadete.CantidadDePedidosCompletados();
        }
        float promedioEnviosXCadete = totalEnvios/(float)cadetes.Count;
        Console.WriteLine("\nInforme general:");
        Console.WriteLine($"Cantidad total de envios: {totalEnvios}");
        Console.WriteLine($"Promedio de envios por cadete: {promedioEnviosXCadete}");
    }
}