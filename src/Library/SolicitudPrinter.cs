namespace Library;

public class SolicitudPrinter : IPrinter<Solicitud> {
    public void PrintCatalog(List<Solicitud> solicitudes) {
        Console.WriteLine($"solrtas de servicio\n");
        foreach (var sol in solicitudes) {
            Console.WriteLine($"»» Descripción: {sol} ║ Empleo: {sol} ║ Trabajador: {sol} {sol} ║ Precio: {sol} ║ ID: {sol.GetId()}\n");
        }
    }
}
