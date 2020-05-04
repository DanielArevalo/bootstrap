using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class Imagenes
    {
        [DataMember]
        public Int64 idimagen { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public Int64 tipo_imagen { get; set; }
        [DataMember]
        public Int64 tipo{ get; set; }
        [DataMember]
        public int tipo_documento { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public byte[] imagen { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public byte[] foto { get; set; }
        [DataMember]
        public bool imagenEsPDF { get; set; }
        [DataMember]
        public string tipodocumento { get; set; }
        [DataMember]
        public Int64 numero_radiacion { get; set; }
        [DataMember]
        public string Formato { get; set; }
    }
}
