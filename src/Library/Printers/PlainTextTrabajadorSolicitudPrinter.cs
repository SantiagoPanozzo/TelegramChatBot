namespace Library;

public class PlainTextTrabajadorSolicitudPrinter : IPlainTextSolicitudPrinter<Trabajador> {
    public string Print(List<Solicitud> solicitudes) {
        string result = "Solicitudes:\n";
        foreach (Solicitud sol in solicitudes) {
            result += $"»» ID: {sol.GetId()} || Empleador: {sol.GetEmpleador()} || ID de la oferta: {sol.Oferta.GetId()}";
        }
        return result;
    }
}
