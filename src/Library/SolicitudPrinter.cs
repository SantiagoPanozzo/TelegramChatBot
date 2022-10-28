namespace Library;

public class OfertaDeServicioPrinter : IPrinter<OfertaDeServicio> {
    public void PrintCatalog(List<OfertaDeServicio> ofertas) {
        Console.WriteLine($"Ofertas de servicio\n");
        foreach (var ofe in ofertas) {
            if (!ofe.IsActiva().Equals(false)) { //TODO
                Console.WriteLine($"»» Descripción: {ofe.Descripcion} ║ Empleo: {ofe.Empleo} ║ Trabajador: {ofe.Ofertante.Nombre} {ofe.Ofertante.Apellido} ║ Precio: {ofe.Precio} ║ ID: {ofe.GetId()}\n");
            }
        }
    }
}
