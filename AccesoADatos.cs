using System.Text.Json;

public abstract class AccesoADatos
{
    public static bool Existe(string ruta)
    {
        return File.Exists(ruta);
    }

    public abstract List<Cadete> LeerCadetes(string nombreArchivo);

    public abstract Cadeteria LeerCadeteria(string nombreArchivo);
    
}

public class AccesoCSV : AccesoADatos
{

    public override List<Cadete> LeerCadetes(string nombreArchivo)
    {
        string ruta = "csv/" + nombreArchivo + ".csv";
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

    public override Cadeteria LeerCadeteria(string nombreArchivoCadeteria)
    {
        string ruta = "csv/" + nombreArchivoCadeteria + ".csv";
        string[] lineas = File.ReadAllLines(ruta);
        var datos = lineas[0].Split(';');

        Cadeteria cadeteria = new Cadeteria(datos[0], datos[1]);

        return cadeteria;
    }

}

public class AccesoJSON : AccesoADatos
{
    public override List<Cadete> LeerCadetes(string nombreArchivo)
    {
        try
        {
            string jsonCadetes = File.ReadAllText("json/"+nombreArchivo+".json");
            return JsonSerializer.Deserialize<List<Cadete>>(jsonCadetes);
        }
        catch (FileNotFoundException)
        {
            return null;
        }
    }

    public override Cadeteria LeerCadeteria(string nombreArchivoCadeteria)
    {
        try
        {
            string jsonCadeteria = File.ReadAllText("json/"+nombreArchivoCadeteria+".json");
            return JsonSerializer.Deserialize<Cadeteria>(jsonCadeteria);
        }
        catch (FileNotFoundException)
        {
            return null;
        }
    }    
}