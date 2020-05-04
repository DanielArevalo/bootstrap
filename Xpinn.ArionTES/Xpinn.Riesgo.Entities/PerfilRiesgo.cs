using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Riesgo.Entities
{

    [DataContract]
    [Serializable()]

    public class PerfilRiesgo
    {
        [DataMember]
        public Int64  Cod_perfil { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string valoracion { get; set; }
        //Agregado 05/08/2019 Cristhian Perez
        [DataMember]
        public string tipoPersona { get; set; }
        [DataMember]
        public string nomtipoPersona { get; set; }
    }
}

