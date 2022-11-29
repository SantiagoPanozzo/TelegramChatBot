namespace Library;

public class PlainTextTrabajadorSolicitudPrinter<Administrador> {
    public string Print(List<Solicitud> solicitudes) {
        string result = "Solicitudes:\n";
        foreach (Solicitud sol in solicitudes) {
            result += $"»» ID: {sol.GetId()} || Trabajador: {sol.Oferta.GetOfertante()} || ID de la oferta: {sol.Oferta.GetId()}";
        }
        return result;
    }
}
