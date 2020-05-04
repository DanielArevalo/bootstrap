using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class PersonaEmpresaRecaudo
    {
        [DataMember]
        public Int64? idempresarecaudo { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public Int64? cod_empresa { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public bool seleccionar { get; set; }
        [DataMember]
        public Int64? idempexcluyente { get; set; }

        //Agregado
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nom_empresa { get; set; }
        [DataMember]
        public decimal porcentaje { get; set; }
    }
}
