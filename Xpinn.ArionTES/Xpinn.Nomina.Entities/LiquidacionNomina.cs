using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class LiquidacionNomina
    {
        [DataMember]
        public long consecutivo { get; set; }
        [DataMember]
        public int codigousuariocreacion { get; set; }
        [DataMember]
        public string separador { get; set; }

        [DataMember]
        public int origen { get; set; }
        [DataMember]
        public String usuariocreacion { get; set; }

        [DataMember]
        public DateTime fechageneracion { get; set; }
        [DataMember]
        public DateTime fechainicio { get; set; }
        [DataMember]
        public DateTime fechaterminacion { get; set; }
        [DataMember]
        public long codigonomina { get; set; }
        [DataMember]
        public long codigocentrocosto { get; set; }
        [DataMember]
        public string desc_nomina { get; set; }
        [DataMember]
        public string desc_centro_costo { get; set; }
        [DataMember]
        public string nom_usuario { get; set; }
        [DataMember]
        public long dias { get; set; }

        [DataMember]
        public DateTime fechaultliquidacion{ get; set; }
        [DataMember]
        public long tiponomina { get; set; }

        [DataMember]
        public long tipoanticipo{ get; set; }
        [DataMember]
        public string estado { get; set; }

        [DataMember]
        public string num_comp { get; set; }

        [DataMember]
        public string tipo_comp { get; set; }
        //Anticipso de nomina
        [DataMember]
        public decimal salario { get; set; }
        [DataMember]
        public long porcentaje_anticipo { get; set; }

        [DataMember]
        public decimal valor_anticipo { get; set; }

        [DataMember]
        public long cod_concepto { get; set; }
        [DataMember]
        public long cod_concepto_trans { get; set; }


        [DataMember]
        public long porcentaje_anticipo_sub { get; set; }

        [DataMember]
        public decimal valor_anticipo_sub { get; set; }


        [DataMember]
        public long dias_liquidados { get; set; }

        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public long codigoempleado { get; set; }

        [DataMember]
        public Int64 codigoanticipo { get; set; }

        [DataMember]
        public Int64 codorigen { get; set; }

        [DataMember]
        public int idlinea { get; set; }
        [DataMember]
        public string linea { get; set; }

        [DataMember]
        public string observaciones { get; set; }

        [DataMember]
        public string identificacion { get; set; }

    }
}
