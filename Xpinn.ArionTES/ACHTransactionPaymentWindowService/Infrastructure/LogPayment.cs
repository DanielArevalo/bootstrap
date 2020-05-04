using System;

namespace ACHTransactionPaymentWindowService.Infrastructure
{
    public class LogPayment
    {
        public LogPayment() { }

        public LogPayment(string usuario, string aplicacion, string mensajeError, string detalleError)
        {
            Usuario = usuario;
            Aplicacion = aplicacion;
            FechaHora = DateTime.Now;
            MensajeError = mensajeError;
            DetalleError = detalleError;
        }

        public string Usuario { get; set; }
        public string Aplicacion { get; set; }
        public DateTime FechaHora { get; set; }
        public string MensajeError { get; set; }
        public string DetalleError { get; set; }
        
    }
}
