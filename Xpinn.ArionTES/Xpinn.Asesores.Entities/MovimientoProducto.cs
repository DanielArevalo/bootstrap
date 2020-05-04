using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Xpinn.Asesores.Entities.Common;

namespace Xpinn.Asesores.Entities
{
    [DataContract]
    [Serializable]
    public class MovimientoProducto
    {
        [DataMember]
        public Int64 NumeroRadicacion { get; set; }
        [DataMember]
        public Int64 NumCuota { get; set; }
        [DataMember]
        public DateTime FechaCuota { get; set; }
        [DataMember]
        public DateTime FechaOperacion { get; set; }
        [DataMember]
        public DateTime FechaPago { get; set; }
        [DataMember]
        public Int64 DiasMora { get; set; }
        [DataMember]
        public Int64 CodOperacion { get; set; }
        [DataMember]
        public String TipoOperacion { get; set; }
        [DataMember]
        public String TipoMovimiento { get; set; }
        [DataMember]
        public Double Capital { get; set; }
        [DataMember]
        public Double IntCte { get; set; }
        [DataMember]
        public Double IntMora { get; set; }
        [DataMember]
        public Double LeyMiPyme { get; set; }
        [DataMember]
        public Double ivaMiPyme { get; set; }
        [DataMember]
        public Double Seguros { get; set; }
        [DataMember]
        public Double Poliza { get; set; }
        [DataMember]
        public Double Otros { get; set; }
        [DataMember]
        public Double Prejuridico { get; set; }
        [DataMember]
        public Double Saldo { get; set; }
        [DataMember]
        public Int64 Calificacion { get; set; }
        [DataMember]
        public string num_comp { get; set; }
        [DataMember]
        public string TIPO_COMP { get; set; }
        [DataMember]
        public Int64 idavance { get; set; }
        [DataMember]
        public Int64 plazo_avance { get; set; }
        [DataMember]
        public Int64 tipo_tran { get; set; }
        [DataMember]
        public string desc_tran { get; set; }
        [DataMember]
        public Int64 consecutivo { get; set; }

    }
}