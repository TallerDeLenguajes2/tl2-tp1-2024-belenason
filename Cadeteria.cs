public class Cadeteria
{
    private string nombre;
    private string telefono;
    private List<Cadete> cadetes;
    private List<Pedido> pedidos;
    public string Nombre {get => nombre;}
    public string Telefono {get => telefono;}
    public List<Cadete> Cadetes {get => cadetes;}
    public List<Pedido> Pedidos {get => pedidos; set => pedidos = value;}

    public Cadeteria(string Nombre, string Telefono)
    {
        nombre = Nombre;
        telefono = Telefono;
        cadetes = Cadetes;
        pedidos = new List<Pedido> ();
    }

    private Pedido CrearPedido(string[] informacionPedido)
    {
        int numPedido = Pedidos.Count + 1;
        Pedido pedidoNuevo = new Pedido(numPedido, informacionPedido[0], informacionPedido[1], informacionPedido[2], informacionPedido[3], informacionPedido[4]);
        return pedidoNuevo;
    }

    public void GuardarPedido(string[] informacionPedido)
    {
        Pedido pedido = CrearPedido(informacionPedido);
        Pedidos.Add(pedido);
    }

    public void AsignarCadetes(List<Cadete> cadetes)
    {
        this.cadetes = cadetes; 
    }

    public float JornalACobrar(int idCadete)
    {
        float monto = CantEntregasCadete(idCadete)*500;
        return monto;
    }

    public int CantEntregasCadete(int idCadete)
    {
        var PedidosCadeteAConsultar = Pedidos.Where(p => p.Cadete.Id == idCadete).ToList();
        int cantEntregada = PedidosCadeteAConsultar.Count(p => p.Estado == Estados.Entregado);
        return cantEntregada;
    }

    public void AsignarCadeteAPedido(int idCadete, int nroPedido)
    {
        var pedidoAsignar = Pedidos.Find(p => p.Nro == nroPedido);
        var cadeteAAsignar = Cadetes.Find(c => c.Id == idCadete);
        pedidoAsignar.Cadete = cadeteAAsignar;
    }

    public Cadete AsignarPedidos(int nroPedido)
    {
        int menosPedidos = 100;
        Cadete cadeteMenosPedidos = cadetes[0];
        foreach (var cadete in cadetes)
        {
            var PedidosCadete = Pedidos.Where(p => p.Cadete == cadete).ToList();
            int cantEntregada = PedidosCadete.Count(p => p.Estado != Estados.Entregado);
            if (cantEntregada <= menosPedidos)
            {
                cadeteMenosPedidos = cadete;
                menosPedidos = cantEntregada;
            }
        }
        AsignarCadeteAPedido(cadeteMenosPedidos.Id, nroPedido);
        return cadeteMenosPedidos;
    }

    public Cadete ReasignarPedidos(int nro)
    {
        var pedidoAReasignar = Pedidos.Find(p => p.Nro == nro && p.Estado != Estados.Entregado && p.Cadete != null);
        Cadete cadeteMenosPedidos = null;
        if (pedidoAReasignar != null)
        {
            var cadetesDisponibles = cadetes.Where(c => c != pedidoAReasignar.Cadete).ToList();
            int menosPedidos = int.MaxValue;
            cadeteMenosPedidos = cadetesDisponibles[0];
            foreach (var cadete in cadetesDisponibles)
            {
                var PedidosCadete = Pedidos.Where(p => p.Cadete == cadete).ToList();
                int cantEntregada = PedidosCadete.Count(p => p.Estado != Estados.Entregado);
                if (cantEntregada <= menosPedidos)
                {
                    cadeteMenosPedidos = cadete;
                    menosPedidos = cantEntregada;
                }
            }
            AsignarCadeteAPedido(cadeteMenosPedidos.Id, pedidoAReasignar.Nro);
        }

        return cadeteMenosPedidos;
    }

    public bool CambiarEstadoDelPedido(int nro, int estado)
    {
        var pedidoConNro = Pedidos.Find(p => p.Nro == nro);
        var success = false;
        if (pedidoConNro != null)
        {
            switch (estado)
            {
                case 0:
                    pedidoConNro.Estado = Estados.EnCamino;
                    break;
                case 1:
                    pedidoConNro.Estado = Estados.Entregado;
                    break;
            }
            success = true;
        }
        return success;
    }
}