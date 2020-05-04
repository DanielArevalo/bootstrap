using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.Cartera.Entities
{
    [DataContract]
    [Serializable]
    public class Reestructuracion
    {
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public Int64 cod_deudor { get; set; }
        [DataMember]
        public Double monto_solicitado { get; set; }
        [DataMember]
        public Int64 numero_cuotas { get; set; }
        [DataMember]
        public string cod_linea_credito { get; set; }
        [DataMember]
        public Int64 cod_periodicidad { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public string forma_pago { get; set; }
        [DataMember]
        public int cod_empresa { get; set; }
        [DataMember]
        public DateTime fecha_primer_pago { get; set; }
        [DataMember]
        public Double monto_nocapitaliza { get; set; }
        [DataMember]
        public Int64 num_cuo_nocap { get; set; }
        [DataMember]
        public Boolean bGarantias { get; set; }
        [DataMember]
        public Int64? numero_radicacion { get; set; }
        [DataMember]
        public Int64? cod_asesor { get; set; }
        [DataMember]
        public Double honorarios { get; set; }
        [DataMember]
        public Double datacredito { get; set; }
        [DataMember]
        public List<Atributos> lstAtributos { get; set; }
        [DataMember]
        public List<codeudores> lstCodeudores { get; set; }
    }
}
