using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad Documento
    /// </summary>
    [DataContract]
    [Serializable]
    public class Documento
    {
        [DataMember]
        public Int64? iddocumento { get; set; }
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public string numero_consecutivo { get; set; }
        [DataMember]
        public string cod_linea_credito { get; set; }
        [DataMember]
        public Int64 tipo_documento { get; set; }
        [DataMember]
        public string descripcion_documento { get; set; }
        [DataMember]
        public string requerido { get; set; }
        [DataMember]
        public string referencia { get; set; }
        [DataMember]
        public string ruta { get; set; }
        [DataMember]
        public int es_orden { get; set; }


        //Atencion Web
        [DataMember]
        public byte[] foto { get; set; }

    }
}