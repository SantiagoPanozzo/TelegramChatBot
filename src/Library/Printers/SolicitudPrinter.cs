namespace Library;

/// <summary> Método para mostrar por pantalla la lista de solicitudes. </summary>
public class SolicitudPrinter : IConsolePrinter<Solicitud> {

    /// <summary> Método que imprime el texto de la lista. </summary>
    /// <param name="solicitudes"> Lista de solicitudes. </param>
    /// <param name="user"> Tipo de usuario que llama al método. </param>
    public void PrintAll(List<Solicitud> solicitudes, Usuario user) {
        Console.WriteLine($"Solicitudes\n");
        if (user.GetTipo().Equals(TipoDeUsuario.Empleador)){
            foreach (var sol in solicitudes) {
                Console.WriteLine($"»» ID: {sol.GetId()} ║ Descripción: {sol.Oferta.Descripcion} ║ Trabajador: {sol} {sol} ║ Fecha de inicio {sol.FechaAceptada}\n ");
            }
        }
        else {
            foreach (var sol in solicitudes) {
                Console.WriteLine($"»» ID: {sol.GetId()} ║ Descripción: {sol.Oferta.Descripcion} ║ Trabajador: {sol} {sol} ║ Empleador: {sol} {sol} ║ Fecha de inicio {sol.FechaAceptada}\n");
            }
        }
    }
}
