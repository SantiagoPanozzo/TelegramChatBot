using System.Collections;

namespace Library;

/// <summary>  </summary>
public class Updater
{
    
    private static Updater? _instance;

    private static Updater Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Updater();
            }

            return _instance;
        }
    }
    public static void Wipe(Usuario user)
    {
        if (user.GetTipo().Equals(TipoDeUsuario.Administrador))
        {
            Updater._instance = null;
        }
    }
    
    public static Updater GetInstance()
    {
        return Updater.Instance;
    }
    
    private Updater()
    {
    }

    // TODO documentar parametros
    
    /// <summary> Método para actualizar todas las clases que necesiten ser actualizadas periódicamente </summary>
    /// <param name="registryHandler">  </param>
    /// <param name="ofertasHandler">  </param>
    /// <param name="contratoHandler">  </param>
    private void BaseUpdate(DateTime fecha, RegistryHandler registryHandler, OfertasHandler ofertasHandler,
        ContratoHandler contratoHandler)
    {
        foreach (Solicitud solicitud in contratoHandler.Catalogo.Solicitudes)
        {
            solicitud.Update(DateTime.Now);
        }
    }
    
    /// <summary> Método para actualizar con la fecha actual todas las clases que necesiten ser actualizadas periódicamente </summary>
    /// <param name="registryHandler">  </param>
    /// <param name="ofertasHandler">  </param>
    /// <param name="contratoHandler">  </param>
    public void Update(RegistryHandler registryHandler, OfertasHandler ofertasHandler, ContratoHandler contratoHandler)
    {
        BaseUpdate(DateTime.Now, registryHandler, ofertasHandler, contratoHandler);
    }
    
    /// <summary> Método para actualizar con una fecha falsa todas las clases que necesiten ser actualizadas periódicamente</summary>
    /// <param name="registryHandler">  </param>
    /// <param name="ofertasHandler">  </param>
    /// <param name="contratoHandler">  </param>
    public void FakeUpdate(DateTime fecha, RegistryHandler registryHandler, OfertasHandler ofertasHandler, ContratoHandler contratoHandler)
    {
        BaseUpdate(fecha, registryHandler, ofertasHandler, contratoHandler);
    }

    
}
