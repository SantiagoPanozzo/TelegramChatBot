namespace Library;

public class SolicitudPrinter : IPrinter<Solicitud> {
    public void PrintCatalog(List<Solicitud> solicitudes, Usuario user) {
        Console.WriteLine($"Solicitudes\n");
        if (user.GetTipo().Equals(TipoDeUsuario.Empleador)){
            foreach (var sol in solicitudes) {
                Console.WriteLine($"»» ID: {sol.GetId()} ║ Descripción: {sol.Oferta.Descripcion} ║ Trabajador: {sol} {sol} ║ Fecha de inicio {sol.FechaAceptada}\n ");
            }
        }
        else if (user.GetTipo().Equals(TipoDeUsuario.Trabajador)){
            foreach (var sol in solicitudes) {
                Console.WriteLine($"»» ID: {sol.GetId()} ║ Descripción: {sol.Oferta.Descripcion} ║ Empleador: {sol} {sol} ║ Fecha de inicio {sol.FechaAceptada}\n");
            }
        }
        else {
            foreach (var sol in solicitudes) {
                Console.WriteLine($"»» ID: {sol.GetId()} ║ Descripción: {sol.Oferta.Descripcion} ║ Trabajador: {sol} {sol} ║ Empleador: {sol} {sol} ║ Fecha de inicio {sol.FechaAceptada}\n");
            }
        }
    }
}
