using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Asesores.Entities
{
    [DataContract]
    [Serializable]
  public  class TrasladarClientes
    {
        [DataMember]
        public Int32 COD_MOTIVO_TRASLADO { get; set; }
        [DataMember]
        public string DESCRIPCION { get; set; }
        #region operacion
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public Int64 tipo_ope { get; set; }
        [DataMember]
        public Int64 cod_ofi { get; set; }
        [DataMember]
        public DateTime fecha_oper { get; set; }
        [DataMember]
        public Int64 cod_cliente { get; set; }
    #endregion operacion
    }
    

}
