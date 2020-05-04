using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class ParametrosAfiliacion
    {
        [DataMember]
        public int idparametros { get; set; }
        [DataMember]
        public int cod_empresa { get; set; }
        [DataMember]
        public int tipo_calculo { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public int numero_cuotas { get; set; }
        [DataMember]
        public int cod_periodicidad { get; set; }
        [DataMember]
        public string nom_periodicidad { get; set; }

    }
}
