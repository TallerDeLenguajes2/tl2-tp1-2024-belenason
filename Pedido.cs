public class Pedido
{
    private int nro;
    private string obs;
    private Cliente cliente;
    private Estados estado;
    private Cadete cadete;
    public int Nro {get => nro;}
    public string Obs { get => obs;}
    public Estados Estado { get => estado; set => estado = value; }
    public Cadete Cadete { get => cadete; set => cadete = value; }

    public Pedido(int Nro, string Obs, string nombre, string direccion, string telefono, string referencias)
    {
        nro = Nro;
        obs = Obs;
        Estado = Estados.Preparación;
        cliente = new Cliente(nombre, direccion, telefono, referencias);
        cadete = null;
    }

    public void VerDireccionCliente()
    {
        Console.WriteLine($"Direccion de entrega: "+cliente.Direccion);
        if (cliente.DatosReferenciaDireccion != null)
        {
            Console.WriteLine($"Datos de referencia de la dirección: "+cliente.DatosReferenciaDireccion);
        }
    }

    public void VerDatosCliente()
    {
        Console.WriteLine($"Nombre: "+cliente.Nombre);
        Console.WriteLine($"Telefono: "+cliente.Telefono);
        VerDireccionCliente();
    }
}
