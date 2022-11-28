namespace Library;

public class PlainTextSolicitudPrinter : ITextPrinter<Solicitud>
{
    public string Print(List<Solicitud> solicitudes, Usuario user) {
        string response= ($"Solicitudes en total {solicitudes.Count}\n");
        if (user.GetTipo().Equals(TipoDeUsuario.Empleador)){
            foreach (var sol in solicitudes) {
                response+=($"»» ID: {sol.GetId()} ║ Descripción: {sol.Oferta.Descripcion} ║ Trabajador: {sol} {sol} ║ Fecha de inicio {sol.FechaAceptada}\n ");
            }
        }
        else {
            foreach (var sol in solicitudes) {
                response+=($"»» ID: {sol.GetId()} ║ Descripción: {sol.Oferta.Descripcion} ║ Trabajador: {sol} {sol} ║ Empleador: {sol} {sol} ║ Fecha de inicio {sol.FechaAceptada}\n");
            }
        }
        return response;
    }
}