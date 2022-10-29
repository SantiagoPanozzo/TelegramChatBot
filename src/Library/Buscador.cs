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

    public void FiltrarDistancia(Empleador emp) {

    }

    public void FiltrarReputacion() {
        
    }
}
