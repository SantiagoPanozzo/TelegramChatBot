using Library.DistanceMatrix;
using System.Linq;
namespace Library;

/// <summary>  </summary>
public class SearchHandler { 
    /// <summary> Método para filtrar las <see cref="OfertaDeServicio"/> por <see cref="Categoria"/> </summary>
    /// <param name="category"> <see cref="Categoria"/> de la cual se quieren ver las ofertas </param>
    /// <returns> Lista con las ofertas filtradas </returns>
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

    /// <summary> Método para filtrar las <see cref="OfertaDeServicio"/> por distancia </summary>
    /// <param name="emp"> Empleador que llama al método </param>
    /// <returns> Lista con las ofertas filtradas por distancia </returns>
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
    public List<OfertaDeServicio> FiltrarDistanciaFinal(Empleador emp) {      //TODO: Emplear con API, cambiar clases para este propósito
        Administrador adm = new("aasfasfqwef", "saaggfqwfdqa", "092999222", "aoisfn@gmail.com");
        Dictionary<OfertaDeServicio, double> finalOffers = new();
        var regHandler = RegistryHandler.GetInstance();
        List<OfertaDeServicio> resultOffers = new();
        OfertasHandler handler = OfertasHandler.GetInstance();

        var ofertasTot = handler.GetOfertasIgnoreId();

        var lat = emp.Ubicacion.Item1;
        var lng = emp.Ubicacion.Item2;

        foreach(var offer in ofertasTot)
        {
            var user = regHandler.GetUserForAdmin(adm, offer.GetOfertante());
            var userLat = user.Ubicacion.Item1;
            var userLng = user.Ubicacion.Item2;
            double latDist = Math.Abs(userLat - lat);
            double lngDist = Math.Abs(userLng - lng);
            double totalDist = latDist + lngDist;

            finalOffers.Add(offer, totalDist);
        }

        var sortedFinalOffers = from entry in finalOffers orderby entry.Value ascending select entry;
        sortedFinalOffers = sortedFinalOffers.ToDictionary<KeyValuePair<List<OfertaDeServicio>, double>, OfertaDeServicio, double>(pair => pair.Key, pair => pair.Value);
        return sortedFinalOffers;
    }

    /// <summary> Método para obtener la <see cref="OfertaDeServicio"/> más cercana </summary>
    /// <param name="myPos"> Posición actual del <see cref="Usuario"/> </param>
    /// <param name="lista"> Lista de la que se está filtrando </param>
    /// <returns></returns>
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

    /// <summary> Método para filtrar las <see cref="OfertaDeServicio"/> por reputación </summary>
    /// <returns> Lista con las ofertas filtradas por reputación </returns>
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
