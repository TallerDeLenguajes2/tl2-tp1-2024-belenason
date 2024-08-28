public class Cadete
{
    private int id;
    private string nombre;
    private string direccion;
    private string telefono; 
    private List<Pedido> pedidos;

    public Cadete(int id, string nombre, string direccion, string telefono)
    {
        this.id = id;
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
        pedidos = new List<Pedido>();
        
    }

    public int Id { get => id;}
    public string Nombre { get => nombre; }
    public string Direccion { get => direccion; }
    public string Telefono { get => telefono; }
    public List<Pedido> Pedidos { get => pedidos; set => pedidos = value; }

    public int CantidadDePedidosCompletados()
    {
        return pedidos.Count(p => p.Estado == Estados.Entregado);
    }

    public int JornalACobrar()
    {
        return CantidadDePedidosCompletados()*500;
    }

    public void EntregarPedido(int nro)
    {
        var pedidoAQuitar = Pedidos.Where(p => p.Nro == nro).ToList();
        pedidoAQuitar[0].Estado = Estados.Entregado; 
    }

    public Pedido DarDeBajaPedido(int nro)
    {
        var pedidoADarDeBaja = Pedidos.Where(p => p.Nro == nro).ToList();
        Pedidos.Remove(pedidoADarDeBaja[0]);
        return pedidoADarDeBaja[0];
    }

    public void RetirarPedido(int nro)
    {
        var pedidoRetirado = Pedidos.Where(p => p.Nro == nro).ToList();
        pedidoRetirado[0].Estado = Estados.EnCamino; 
    }
}
