using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Riesgo.Entities
{
    [DataContract]
    [Serializable()]

    public class JurisdiccionDepa
    {

        [DataMember]
        public Int64 Id_Jurid { get; set; }
        [DataMember]
        public Int64 Cod_Depa { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string valoracion { get; set; }
        [DataMember]
        public Int64 cod_usua { get; set; }
        [DataMember]
        public int? Tipo { get; set; }
        // LISTAS DESPLEGABLES
        [DataMember]
        public Int64 ListaId { get; set; }
        [DataMember]
        public String ListaIdStr { get; set; }
        [DataMember]
        public String ListaDescripcion { get; set; }
    }
}
