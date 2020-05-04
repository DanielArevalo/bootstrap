using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Aportes.Entities
{
    public class DetalleCruce
    {
        [DataMember]
        public Int64 Cod_persona { get; set; }

        [DataMember]
        public String Linea_Producto { get; set; }

        [DataMember]
        public String Numero_Producto { get; set; }

        [DataMember]
        public String Concepto { get; set; }

        [DataMember]
        public Decimal Capital { get; set; }

        [DataMember]
        public Decimal Interes_rendimiento { get; set; }

        [DataMember]
        public Decimal Interes_Mora { get; set; }

        [DataMember]
        public Decimal Otros { get; set; }

        [DataMember]
        public Decimal Retencion { get; set; }

        [DataMember]
        public String Signo { get; set; }

        [DataMember]
        public Decimal Total { get; set; }

        [DataMember]
        public Decimal Saldo { get; set; }

        [DataMember]
        public Decimal Interes_Causado { get; set; }

        [DataMember]
        public Decimal Retencion_Causada { get; set; }

        [DataMember]
        public Int64? Cod_ope { get; set; }
        

    }
}
