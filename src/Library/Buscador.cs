namespace Library;

public class Buscador { 
    public List<OfertaDeServicio> FiltrarCategoria(Categoria cat) {
        List<OfertaDeServicio> of = new();
        OfertasHandler ofh = OfertasHandler.GetInstance();
        foreach (OfertaDeServicio oferta in ofh.GetOfertas(cat.GetId())) {
                of.Add(oferta);
        }
        return of;
    }

    /* Como empleador, quiero ver el resultado de las búsquedas de ofertas de trabajo ordenado en forma ascendente de distancia a mi ubicación, 
    es decir, las más cercanas primero para que de esa forma, pueda poder contratar un servicio. */
    public void FiltrarDistancia(Empleador emp) {

    }

    /* Como empleador, quiero ver el resultado de las búsquedas de ofertas de trabajo ordenado en forma descendente por reputación, 
    es decir, las de mejor reputación primero para que de esa forma, pueda contratar un servicio. */
    public void FiltrarReputacion() {
        
    }
}
