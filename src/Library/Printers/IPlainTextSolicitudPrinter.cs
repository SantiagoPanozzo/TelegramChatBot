namespace Library;

public interface IPlainTextSolicitudPrinter<U> : ITextPrinter<Solicitud> {
    public string Print(List<Solicitud> solicitudes);
}
