using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class ProcesoTipoGiro
    {
        [DataMember]
        public int idprocesogiro { get; set; }
        [DataMember]
        public int cod_proceso { get; set; }
        [DataMember]
        public int tipo_acto { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string descripcion_cod_cuenta { get; set; }
    }
}