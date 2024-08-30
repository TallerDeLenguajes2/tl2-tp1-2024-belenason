public class Cadeteria
{
    private string nombre;
    private string telefono;
    private List<Cadete> cadetes;
    private List<Pedido> pedidos;
    public string Nombre {get => nombre;}
    public string Telefono {get => telefono;}
    public List<Cadete> Cadetes {get => cadetes; set => cadetes = value;}
    public List<Pedido> Pedidos {get => pedidos; set => pedidos = value;}

    public Cadeteria(string Nombre, string Telefono)
    {
        nombre = Nombre;
        telefono = Telefono;
        cadetes = Cadetes;
        pedidos = new List<Pedido> ();
        cadetes = new List<Cadete> ();
    }

    private float JornalACobrar(int idCadete)
    {
        float monto = CantEntregasCadete(idCadete)*500;
        return monto;
    }

    private int CantEntregasCadete(int idCadete)
    {
        var PedidosCadeteAConsultar = Pedidos.Where(p => p.Cadete.Id == idCadete).ToList();
        int cantEntregada = PedidosCadeteAConsultar.Count(p => p.Estado == Estados.Entregado);
        return cantEntregada;
    }

    public void AsignarCadeteAPedido(int idCadete, int nroPedido)
    {
        var pedidoAsignar = Pedidos.Where(p => p.Nro == nroPedido).ToList();
        var cadeteAAsignar = Cadetes.Where(c => c.Id == idCadete).ToList();
        pedidoAsignar[0].Cadete = cadeteAAsignar[0];
    }

    public Cadete AsignarPedidos(Pedido pedido)
    {
        int menosPedidos = 100;
        Cadete cadeteMenosPedidos = cadetes[0];
        foreach (var cadete in cadetes)
        {
            var PedidosCadete = Pedidos.Where(p => p.Cadete.Id == cadete.Id).ToList();
            int cantEntregada = PedidosCadete.Count(p => p.Estado != Estados.Entregado);
            if (cantEntregada <= menosPedidos)
            {
                cadeteMenosPedidos = cadete;
                menosPedidos = cantEntregada;
            }
        }
        AsignarCadeteAPedido(cadeteMenosPedidos.Id, pedido.Nro);
        return cadeteMenosPedidos;
    }

    public Cadete ReasignarPedidos(int nro)
    {
        var pedidoAReasignar = Pedidos.Where(p => p.Nro == nro).ToList();
        Cadete cadeteMenosPedidos = null;
        if (pedidoAReasignar.Count != 0)
        {
            var cadetesDisponibles = cadetes.Where(c => c.Nombre != pedidoAReasignar[0].Cadete.Nombre).ToList();
            int menosPedidos = int.MaxValue;
            cadeteMenosPedidos = cadetesDisponibles[0];
            foreach (var cadete in cadetesDisponibles)
            {
                var PedidosCadete = Pedidos.Where(p => p.Cadete.Id == cadete.Id).ToList();
                int cantEntregada = PedidosCadete.Count(p => p.Estado != Estados.Entregado);
                if (cantEntregada <= menosPedidos)
                {
                    cadeteMenosPedidos = cadete;
                    menosPedidos = cantEntregada;
                }
            }
            AsignarCadeteAPedido(cadeteMenosPedidos.Id, pedidoAReasignar[0].Nro);
        } else
        {
            Console.WriteLine("El número ingresado no se corresponde con ningún pedido");
        }

        return cadeteMenosPedidos;
    }

    public void CambiarEstadoDelPedido(int nro)
    {
        var pedidosConNro = Pedidos.Where(p => p.Nro == nro).ToList();
        if (pedidosConNro.Count != 0)
        {
            Menu menuDeSeleccion = new Menu("Seleccione el estado al que desea cambiar el pedido:", ["En camino", "Entregado"]);
            int seleccion = menuDeSeleccion.MenuDisplay();
            switch (seleccion)
            {
                case 0:
                    pedidosConNro[0].Estado = Estados.EnCamino;
                    break;
                case 1:
                    pedidosConNro[0].Estado = Estados.Entregado;
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
            Console.WriteLine($"Nombre: {cadete.Nombre} || Cantidad de pedidos realizados: {CantEntregasCadete(cadete.Id)} || Monto ganado: ${JornalACobrar(cadete.Id)}");
            totalEnvios = totalEnvios + CantEntregasCadete(cadete.Id);
        }
        float promedioEnviosXCadete = totalEnvios/(float)cadetes.Count;
        Console.WriteLine("\nInforme general:");
        Console.WriteLine($"Cantidad total de envios: {totalEnvios}");
        Console.WriteLine($"Promedio de envios por cadete: {promedioEnviosXCadete}");
    }
}