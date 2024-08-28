class Cliente
{
    private string nombre;
    private string direccion;
    private string telefono;
    private string datosReferenciaDireccion;

    public Cliente(string nombre, string Direccion, string Telefono, string DatosReferenciaDireccion)
    {
        this.nombre = nombre;
        direccion = Direccion;
        telefono = Telefono;
        datosReferenciaDireccion = DatosReferenciaDireccion;
    }

    public string Nombre { get => nombre; }
    public string Direccion { get => direccion;}
    public string Telefono { get => telefono; }
    public string DatosReferenciaDireccion { get => datosReferenciaDireccion; }
}

