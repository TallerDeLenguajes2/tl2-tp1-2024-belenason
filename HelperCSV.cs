public class HelperDeCSV
{
    public static bool Existe(string nombreArchivo)
    {
        string ruta = "csv/"+nombreArchivo;
        return File.Exists(ruta);
    }

    public static List<Cadete> LeerCadetes(string nombreArchivo)
    {
        string ruta = "csv/" + nombreArchivo;
        List<Cadete> cadetes = new List<Cadete>();

        string[] lineas = File.ReadAllLines(ruta);

        foreach (var linea in lineas)
        {
            var datos = linea.Split(';');
            var cadete = new Cadete(int.Parse(datos[0]), datos[1], datos[2], datos[3]);
            cadetes.Add(cadete);
        }
        return cadetes;
    }

    public static Cadeteria LeerCadeteria(string nombreArchivoCadeteria)
    {
        string ruta = "csv/" + nombreArchivoCadeteria;
        string[] lineas = File.ReadAllLines(ruta);
        var datos = lineas[0].Split(';');

        Cadeteria cadeteria = new Cadeteria(datos[0], datos[1], new List<Cadete> ());

        return cadeteria;
    }
    
}