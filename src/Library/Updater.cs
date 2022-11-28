using System.Collections;

namespace Library;

/// <summary>
/// Clase estática que actúa como reloj del programa, encargada de actualizar todos los objetos que requieran saber
/// la fecha actual.
/// </summary>
public static class Updater
{

    private static Timer? _timer = null;
    
    /// <summary>
    /// Fecha actual del programa. Es la establecida por <see cref="Updater.BaseUpdate"/> para ser pasada a todos los objetos que
    /// necesiten conocerla.
    /// </summary>
    public static DateTime FechaActual { get; private set; }

    private static RegistryHandler RegistryHandler { get { return RegistryHandler.GetInstance(); } }

    private static OfertasHandler OfertasHandler { get { return OfertasHandler.GetInstance(); } }
    private static ContratoHandler ContratoHandler { get { return ContratoHandler.GetInstance(); } }

    /// <summary> Define cada cuánto tiempo se realizarán las actualizaciones automáticas si están habilitadas </summary>
    private static readonly TimeSpan DelayActualizacion = new TimeSpan(seconds: 30, hours: 0, minutes: 0);
    
    private static bool _updating = false;
    /// <summary> Variable cuyo valor representa si el Updater está en modo automático. </summary>
    /// <returns> True si el Updater está actualizando automáticamente con la fecha real actual. </returns>
    /// <returns> False si el Updater necesita ser actualizado manualmente. </returns>
    public static bool IsUpdating
    {
        get
        {
            return _updating;
        }
    }
    
    /// <summary> Método para habilitar la actualización automática con la fecha real actual. </summary>
    public static async void EnableAutoUpdate()
    {
        Updater._updating = true;
        while(_updating)
        {
            await Task.Run(AutoUpdate);
        }
    }
    /// <summary> Método para deshabilitar la actualización automática </summary>
    public static void DisableAutoUpdate()
    {
        Updater._updating = false;
        Console.WriteLine("Subsistema de actualización automática desactivado, esperando a que se complete la última tarea...");
    }

    /// <summary> Crea un búcle en el que se actualizan los objetos de tipo <see cref="IActualizable"/> con la fecha real. </summary>
    /// <returns></returns>
    private static async Task<bool> AutoUpdate() // TODO el async tira warning y no se como arreglarlo
    {
        Console.WriteLine($"Iniciado el subsistema de actualización automática con un temporizador de {Updater.DelayActualizacion.TotalSeconds} segundos");
        while (IsUpdating)
        {
            if(Updater._timer == null){
                Console.WriteLine($"Actualizando de manera automática en {Updater.DelayActualizacion.TotalSeconds} segundos");
                Updater._timer = new Timer(
                    callback: new TimerCallback(UpdateCycle),
                    state: null,
                    dueTime: Updater.DelayActualizacion,
                    period: TimeSpan.Zero);
            }
        }
        return false;
    }

    /// <summary> Método para actualizar todas las clases que necesiten ser actualizadas periódicamente </summary>
    /// <param name="fecha"> Fecha a utilizar para actualizar las clases que lo necesiten. </param>
    private static void BaseUpdate(DateTime fecha)
    {
        Updater.FechaActual = fecha;
        foreach (IActualizable solicitud in ContratoHandler.Catalogo.Solicitudes)
        {
            solicitud.Update(fecha);
        }
    }
    
    /// <summary> Método para actualizar con la fecha actual todas las clases que necesiten ser actualizadas periódicamente </summary>
    public static void Update()
    {
        BaseUpdate(DateTime.Now);
    }

    /// <summary> Método para realizar un ciclo de la actualización automática </summary>
    /// <param name="dummy"> Objeto utilizado por método async </param>
    private static void UpdateCycle(object? dummy) // TODO ver necesidad de object? a
    {
        Console.WriteLine("Actualizando todos los módulos...");
        Update();
        Console.WriteLine($"Actualizado según la fecha {FechaActual.Year}-{FechaActual.Month}-{FechaActual.Day} a las {FechaActual.Hour}:{FechaActual.Minute}:{FechaActual.Second}");
        Updater._timer = null;

    }
    
    /// <summary> Método para actualizar con una fecha falsa todas las clases que necesiten ser actualizadas periódicamente</summary>
    /// <param name="fecha"> Fecha que simular para actualizar las clases que lo necesiten. </param>
    public static void FakeUpdate(DateTime fecha)
    {
        BaseUpdate(fecha);
    }

    /// <summary>
    /// Método para avanzar el reloj del programa una cantidad dada de tiempo. No refleja el tiempo real, solo suma el <see cref="TimeSpan"/> dado
    /// al <see cref="DateTime"/> almacenado en <see cref="FechaActual"/>.
    /// </summary>
    /// <param name="tiempo"> Cantidad de tiempo que sumar a <see cref="FechaActual"/>. </param>
    public static void FastForward(TimeSpan tiempo)
    {
        FakeUpdate(FechaActual.Add(tiempo));
    }
}
