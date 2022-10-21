namespace Library;

public class OfertasHandler{


    public void Ofertar(Categoria Category,Trabajador ofertante, string descripcion, string empleo, double precio){

//Por patron Creator se crea instancia de oferta de servicio en esta clase        
        
        OfertaDeServicio Oferta = new OfertaDeServicio(ofertante, descripcion, empleo, precio);

        Category.AddOferta(Oferta);

    }
}