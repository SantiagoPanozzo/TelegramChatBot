using System.Collections;

namespace Library;

/// <summary>  </summary>
public class Updater
{
    private ArrayList Components = new ArrayList();

    /// <summary>  </summary>
    /// <param name="registryHandler">  </param>
    /// <param name="ofertasHandler">  </param>
    /// <param name="contratoHandler">  </param>
    public void Update(RegistryHandler registryHandler, OfertasHandler ofertasHandler, ContratoHandler contratoHandler)
    {
        foreach (Solicitud solicitud in contratoHandler.Catalogo.Solicitudes)
        {
            solicitud.Update();
        }
    }
}
