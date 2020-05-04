using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class PyG
    {
        #region ParametrosEntrada
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public Int64 centro_costo_ini { get; set; }
        [DataMember]
        public Int64 centro_costo_fin { get; set; }
        [DataMember]
        public Int64 moneda { get; set; }
        [DataMember]
        public Int64 nivel { get; set; }
        [DataMember]
        public Int64 cierreanual { get; set; }
        [DataMember]
        public Int64 saldosperiodo { get; set; }
        [DataMember]
        public Int16 cuentascero { get; set; }
        [DataMember]
        public Int16 comparativo { get; set; }
        #endregion 

        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public Int64? centro_costo { get; set; }
        [DataMember]
        public string nombrecuenta { get; set; }
        [DataMember]
        public Double? valor { get; set; }
        [DataMember]
        public int? item { get; set; }
        [DataMember]
        public int orden { get; set; }

    }

}
