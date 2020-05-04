using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization; 
using System.IO;

namespace Xpinn.Asesores.Entities
{
    [DataContract]
    [Serializable]
    public class EstadoCuenta
    {
        [DataMember]
        public Int64 Codigo { get; set; }


        ///agregado
        [DataMember]
        public int num_servicio { get; set; }
        [DataMember]
        public DateTime fecha_solicitud { get; set; }
        [DataMember]
        public DateTime fecha_inicio { get; set; }
        [DataMember]
        public DateTime fecha_final { get; set; }
        [DataMember]
        public int valor { get; set; }
        [DataMember]
        public int cuota { get; set; }
        [DataMember]
        public string plan { get; set; }
        [DataMember]
        public string linea { get; set; }
        [DataMember]
        public string nom_estado { get; set; }
        [DataMember]
        public DateTime? fecha_inicio_vigencia { get; set; }
        [DataMember]
        public DateTime? fecha_final_vigencia { get; set; }

        //agregado        
        public int numero_radicacion { get; set; }
        [DataMember]
        public int monto { get; set; }
        [DataMember]
        public Decimal saldo { get; set; }
        [DataMember]
        public Decimal valor_mora { get; set; }
        [DataMember]
        public string nombre_titular { get; set; }
        [DataMember]
        public Decimal cuotas { get; set; }
        [DataMember]
        public DateTime fecha_proximo_pago { get; set; }
        [DataMember]
        public bool cerrado { get; set; }
        [DataMember]
        public string anulado { get; set; }
        [DataMember]
        public string forma_pago { get; set; }

        public DateTime fecha_aprobacion { get; set; }
        [DataMember]
        public DateTime fecha_desembolso { get; set; }

        [DataMember]
        public String email { get; set; }

        [DataMember]
        public Int64 identificacion { get; set; }

        [DataMember]
        public Int64 valor_pendiente { get; set; }

        [DataMember]
        public Int64 Dias_Mora { get; set; }
    }
}
