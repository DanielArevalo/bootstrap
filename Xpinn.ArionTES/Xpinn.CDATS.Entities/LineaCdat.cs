using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.CDATS.Entities
{
    [DataContract]
    [Serializable]
    public class LineaCDAT
    {
        [DataMember]
        public string cod_lineacdat { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public int? calculo_tasa { get; set; }
        [DataMember]
        public string nom_calculo_tasa { get; set; }
        [DataMember]
        public int? cod_tipo_tasa { get; set; }
        [DataMember]
        public string nom_tipo_tasa { get; set; }
        [DataMember]
        public decimal? tasa { get; set; }
        [DataMember]
        public int? tipo_historico { get; set; }
        [DataMember]
        public string nom_tipo_historico { get; set; }
        [DataMember]
        public decimal? desviacion { get; set; }
        [DataMember]
        public int? estado { get; set; }
        [DataMember]
        public int? cod_moneda { get; set; }
        [DataMember]
        public string nom_moneda { get; set; }
        [DataMember]
        public List<RangoCDAT> lstRangos { get; set; }

        [DataMember]
        public int? interes_por_cuenta { get; set; }
        
        
        [DataMember]
        public int? interes_prroroga { get; set; }
        [DataMember]
        public int? tipo_calendario { get; set; }
        [DataMember]
        public int? retencion { get; set; }

        [DataMember]
        public int?  interes_anticipado { get; set; }
        //Agregados
        [DataMember]
        public int? calculo_tasa_ven { get; set; }
        [DataMember]
        public int? cod_tipo_tasa_ven { get; set; }
        [DataMember]
        public decimal? tasa_ven { get; set; }
        [DataMember]
        public int? tipo_historico_ven { get; set; }
        [DataMember]
        public decimal? desviacion_ven { get; set; }

        [DataMember]
        public int? capitaliza_interes { get; set; }

        [DataMember]
        public int? numero_pre_impreso { get; set; }

        [DataMember]
        public int? tasa_simulacion { get; set; }

        //GREGADO OFICINA VIRTUAL
        [DataMember]
        public int plazo_minimo { get; set; }
        [DataMember]
        public int plazo_maximo { get; set; }
    }
}
