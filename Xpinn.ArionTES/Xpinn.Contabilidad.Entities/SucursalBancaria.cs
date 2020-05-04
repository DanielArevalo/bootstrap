using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class SucursalBancaria
    {
        [DataMember]
        public int idsucursal { get; set; }
        [DataMember]
        public int cod_suc { get; set; }
        [DataMember]
        public int cod_banco { get; set; }
        [DataMember]
        public string nom_banco { get; set; }
        [DataMember]
        public string nom_suc { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public int codciudad { get; set; }
        [DataMember]
        public string nom_ciudad { get; set; }
        [DataMember]
        public int cod_int { get; set; }
    }
}