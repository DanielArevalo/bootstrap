using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Asesores.Entities
{
    [DataContract]
    [Serializable]
    public class DetalleTransaccionesExtracto
    {
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public DateTime fecha_corte { get; set; }
        [DataMember]
        public DateTime FechaTransaccion { get; set; }
        [DataMember]
        public Int64 Referencia { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public Int32 Cuotas { get; set; }
        [DataMember]
        public double ValorTransaccion { get; set; }
        [DataMember]
        public double SaldoPorPagar { get; set; }

        //AGREGADO

        [DataMember]
        public DateTime fecha_Inicial { get; set; }
        [DataMember]
        public DateTime fecha_Final { get; set; }

    }
}