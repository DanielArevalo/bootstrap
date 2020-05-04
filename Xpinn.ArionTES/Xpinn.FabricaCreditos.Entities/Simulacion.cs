using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
   public  class Simulacion
    {
        [DataMember]
        public Int32 monto { get; set; }
        [DataMember]
        public Int32 plazo { get; set; }
        [DataMember]
        public decimal? tasa { get; set; }
        [DataMember]
        public Int32 periodic { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public Int32 for_pag { get; set; }
        [DataMember]
        public String cod_credi { get; set; }
        [DataMember]
        public decimal cuota { get; set; }
        [DataMember]
        public Int64 tipo_liquidacion { get; set; }
        [DataMember]
        public decimal tasaseguro { get; set; }
        [DataMember]
        public decimal? comision { get; set; }
        [DataMember]
        public decimal? aporte { get; set; }
        [DataMember]
        public decimal? seguro { get; set; }
        [DataMember]
        public DateTime? fecha_primer_pago { get; set; }
        [DataMember]
        public List<CuotasExtras> lstCuotasExtras { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        public string message { get; set; }
        //Agregado
        [DataMember]
        public decimal totalCuotasExtra { get; set; }
    }

}
