namespace Library;

public class SolicitudPrinter : IPrinter<Solicitud> {

    public void PrintCatalog(List<Solicitud> solicitudes) {
        Console.WriteLine($"Categorías ({solicitudes.Count} en total) \n");
        foreach (var sol in solicitudes) {
            Console.WriteLine($"»» Descripción: {sol.Oferta.Descripcion}  Trabajador: {sol.GetTrabajador()}\t  ID: {sol.GetId()} ");
        }
    }
}