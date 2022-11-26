using Library.DistanceMatrix;

namespace Library;

public class SearchHandler { 
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

        Tuple<double,double> myPos = emp.Ubicacion;
        foreach(Categoria cat in categorias)
        {
            foreach(OfertaDeServicio oferta in cat.GetOfertas()){
                offers.Add(oferta);}
        }

        OfertaDeServicio shortest;
        int ofertas = offers.Count;
        for(int k = 0; k < ofertas; k ++){
            shortest = GetShortest(myPos, offers);
            resultOffers.Add(shortest);
            offers.Remove(shortest);
        }
        return resultOffers;
    }

    private OfertaDeServicio? GetShortest(Tuple<double,double> myPos, List<OfertaDeServicio> lista)
    {
        int i = 0;
        double distanciaActual = 0;
        double distanciaMenor = double.MaxValue;
        OfertaDeServicio? shortest = null;
        foreach(OfertaDeServicio oferta in lista)
        {
            i++;               
            double latO = oferta.GetUbicacion().Item1;
            double longO = oferta.GetUbicacion().Item2;
            double latE = myPos.Item1;
            double longE = myPos.Item2;
            double term1 = Math.Pow((latE - latO), 2);
            double term2 = Math.Pow((longE - longO), 2);
            distanciaActual = Math.Sqrt(term1+term2);

            if (i <= 1) continue;
            if (distanciaMenor > distanciaActual)
            {
                distanciaMenor = distanciaActual;
                shortest = oferta;
            }
        }
        return shortest;
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
