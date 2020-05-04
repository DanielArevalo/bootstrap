using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.NIIF.Entities
{
    [DataContract]
    [Serializable]
    public class EstadosFinancierosNIIF
    {
        [DataMember]
        public int codigo { get; set; }

        [DataMember]
        public int depende_de { get; set; }

        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public Int64 cod_concepto { get; set; }
        [DataMember]
        public string descripcion_concepto { get; set; }
        [DataMember]
        public Int64 cod_cuenta { get; set; }

        [DataMember]
        public string cuentascontables { get; set; }

        [DataMember]
        public int? corriente { get; set; }
        [DataMember]
        public string desccorriente { get; set; }


        [DataMember]
        public int? nocorriente { get; set; }

        [DataMember]
        public string descnocorriente { get; set; }

        [DataMember]
        public int cod_estado_financiero { get; set; }

        [DataMember]
        public string cod_cuenta_niif { get; set; }

        [DataMember]
        public string desc_cuenta_niif { get; set; }
        [DataMember]
        public List<EstadosFinancierosNIIF> lstDetalle { get; set; }

        [DataMember]
        public Int64 nivel { get; set; }

        [DataMember]
        public int?  titulo { get; set; }
    }
}

