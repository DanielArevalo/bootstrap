using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad TiposDocumento
    /// </summary>
    [DataContract]
    [Serializable]
    public class TiposDocumento
    {
        [DataMember] 
        public Int64 tipo_documento { get; set; }
        [DataMember] 
        public string descripcion { get; set; }
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public string nomtipo { get; set; }
        [DataMember]
        public string texto { get; set; }
        [DataMember]
        public int es_orden { get; set; }
        [DataMember]
        public byte[] Textos { get; set; }
    }

    //trae los datos de latabla Tipo_DelDocuymento 
    public class TipoDocumento
    {

        [DataMember]
        public string idTipo { get; set; }
        [DataMember]
        public string Detalle { get; set; }
    }
}