using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class EmpresaExcluyente
    {
        [DataMember]
        public int idempexcluyente { get; set; }
        [DataMember]
        public int cod_empresa { get; set; }
        [DataMember]
        public int cod_empresa_excluye { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public int seleccionar { get; set; }
    }
}