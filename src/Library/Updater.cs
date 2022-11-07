using System.Collections;

namespace Library;

/// <summary>  </summary>
/// <!-- En esta clase hacemos uso del método Update() que implementamos en las clases que heredan de IActualizable por
/// polymorphism. Gracias a haber utilizado el principio ahora podemos actualizarlas todas dentro de BaseUpdate()-->

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
    
    /// <summary> Método para actualizar todas las clases que necesiten ser actualizadas periódicamente </summary>
    /// <param name="fecha"> Fecha que utilizar para actualizar las clases en base a ella </param>
 
    private void BaseUpdate(DateTime fecha)
    {
        RegistryHandler registryHandler = RegistryHandler.GetInstance();
        OfertasHandler ofertasHandler = OfertasHandler.GetInstance();
        ContratoHandler contratoHandler = ContratoHandler.GetInstance();
        foreach (IActualizable solicitud in contratoHandler.Catalogo.Solicitudes)
        {
            solicitud.Update(DateTime.Now);
        }
    }
    
    /// <summary> Método para actualizar con la fecha actual todas las clases que necesiten ser actualizadas periódicamente </summary>
    public void Update()
    {
        BaseUpdate(DateTime.Now);
    }
    
    /// <summary> Método para actualizar con una fecha falsa todas las clases que necesiten ser actualizadas periódicamente</summary>
    /// <param name="fecha"> Fecha que utilizar para actualizar las clases en base a ella </param>
    public void FakeUpdate(DateTime fecha)
    {
        BaseUpdate(fecha);
    }

    
}
