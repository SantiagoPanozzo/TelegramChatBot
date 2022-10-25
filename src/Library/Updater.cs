using System.Collections;

namespace Library;

public class Updater
{
    private ArrayList Components = new ArrayList();

    public void Update(RegistryHandler registryHandler, OfertasHandler ofertasHandler, ContratoHandler contratoHandler)
    {
        foreach (Solicitud solicitud in contratoHandler.Catalogo.Solicitudes)
        {
            solicitud.Update();
        }
    }
}