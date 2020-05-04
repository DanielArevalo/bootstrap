using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class ParametrosAporte
    {
        [DataMember]
        public int cod_opcion { get; set; }
        [DataMember]
        public string codigoStr { get; set; }
        [DataMember]
        public int codigoInt { get; set; }
        [DataMember]
        public string descripcion { get; set; }

        [DataMember]
        public string nom_opcion { get; set; }
    }
}