namespace Library;

public class PlainTextAdminSolicitudPrinter<Administrador> {
    public string Print(List<Solicitud> solicitudes) {
        string result = "Solicitudes:\n";
        foreach (Solicitud sol in solicitudes) {
            result += $"»» ID de la solicitud: {sol.GetId()} || Empleador: {sol.GetEmpleador()} || Trabajador: {sol.Oferta.GetOfertante()} || ID de la oferta: {sol.Oferta.GetId()}\n";
        }
        return result;
    }
}
