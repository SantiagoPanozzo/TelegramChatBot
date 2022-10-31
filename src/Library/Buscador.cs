using Library.API;

namespace Library;

public class Buscador { 
    public List<OfertaDeServicio> FiltrarCategoria(Categoria category) {
        List<OfertaDeServicio> filteredOffers = new();
        OfertasHandler handler = OfertasHandler.GetInstance();
        foreach (OfertaDeServicio oferta in handler.GetOfertas(category.GetId())) {
                filteredOffers.Add(oferta);
        }
        return filteredOffers;
    }

    /* Como empleador, quiero ver el resultado de las búsquedas de ofertas de trabajo ordenado en forma
    ascendente de distancia a mi ubicación, es decir, las más cercanas primero para que de esa forma,
    pueda poder contratar un servicio. */
    public List<OfertaDeServicio> FiltrarDistancia(Empleador emp) {      //TODO: Emplear con API, cambiar clases para este propósito
        List<OfertaDeServicio> offers = new();
        List<OfertaDeServicio> resultOffers = new();
        OfertasHandler handler = OfertasHandler.GetInstance();
        List<Categoria> categorias = handler.GetCategorias();

        var myPos = emp.Ubicacion;

        foreach(Categoria categoria in categorias)
        {
            var categoryId = categoria.GetId();
            offers.AddRange(handler.GetOfertas(categoryId));
        }

        return resultOffers;
    }

    /* Como empleador, quiero ver el resultado de las búsquedas de ofertas de trabajo ordenado en forma
    descendente por reputación, es decir, las de mejor reputación primero para que de esa forma, pueda
    contratar un servicio. */
    public List<OfertaDeServicio> FiltrarReputacion() {

        List<OfertaDeServicio> offers = new();
        List<OfertaDeServicio> resultOffers = new();
        OfertasHandler handler = OfertasHandler.GetInstance();
        List<Categoria> categorias = handler.GetCategorias();

        foreach(Categoria categoria in categorias)
        {
            var categoryId = categoria.GetId();
            offers.AddRange(handler.GetOfertas(categoryId));
        }

        for(int j = 0; j < 6; j++)
        {
            if(resultOffers.Count == offers.Count) return resultOffers;

            foreach(OfertaDeServicio offer in offers)
            {
                if(offer.GetReputacion() == (Calificacion)j)
                {
                    resultOffers.Insert(0, offer);
                }
            }
        }
        return offers;
    }
}
