using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Asesores.Entities
{
    [DataContract]
    public class DetalleProducto
    {
        public DetalleProducto()
        {
            Producto = new Producto();
            Documentos = new List<DocumentoProducto>();
            Garantias = new List<Garantia>();
            MovimientosProducto = new List<MovimientoProducto>();
            DetallePagos = new List<DetallePago>();
            ConsultaAvance = new List<ConsultaAvance>();    
          
        }

        [DataMember]
        public Int64 NumeroRadicacion { get; set; }
        [DataMember]
        public Int64 NumeroObligacion { get; set; }
        [DataMember]
        public Producto Producto { get; set; }
        [DataMember]
        public string EstadoCredito { get; set; }
        [DataMember]
        public string Destinacion { get; set; }
        [DataMember]
        public string FormaPago { get; set; }
        [DataMember]
        public string FormatoPlazo { get; set; }
        [DataMember]
        public string periodicidad { get; set; }
        [DataMember]
        public string numcomprobante { get; set; }
        [DataMember]
        public Int64 codMoneda { get; set; }
        [DataMember]
        public Double TasaNM { get; set; }
        [DataMember]
        public Int64 MontoSolicitado { get; set; }
        [DataMember]
        public Int64 SaldoPendiente { get; set; }
        [DataMember]
        public DateTime FechaAprobacion { get; set; }
        [DataMember]
        public DateTime FechaDesembolso { get; set; }
        [DataMember]
        public DateTime FechaTerminacion { get; set; }
        [DataMember]
        public DateTime FechaUltimoPago { get; set; }
        [DataMember]
        public DateTime FechaProximoPago { get; set; }
        [DataMember]
        public List<DocumentoProducto> Documentos { get; set; }
        [DataMember]
        public List<Garantia> Garantias { get; set; }
        [DataMember]
        public List<MovimientoProducto> MovimientosProducto { get; set; }
        [DataMember]
        public List<DetallePago> DetallePagos { get; set; }
        [DataMember]
        public Int64  TIPOLINEA { get; set; }
        [DataMember]
        public List<ConsultaAvance> ConsultaAvance { get; set; }
       
    }
}