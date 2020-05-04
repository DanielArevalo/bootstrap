using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Integracion.Entities
{
    [DataContract]
    [Serializable]
    public class PQR
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public long cod_persona { get; set; }

        [DataMember]
        public int tipo_pqr { get; set; }

        [DataMember]
        public string descripcion { get; set; }

        [DataMember]
        public int estado { get; set; }

        [DataMember]
        public DateTime fecha { get; set; }

        [DataMember]
        public List<PQR_Respuesta> lstRespuestas { get; set; }

        [DataMember]
        public string nombre { get; set; }

        [DataMember]
        public string asesor { get; set; }

        [DataMember]
        public byte[] adjunto { get; set; }

        [DataMember]
        public string nom_estado { get; set; }

        [DataMember]
        public string nom_tipo { get; set; }
    }

    public class PQR_Respuesta
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public int id_pqr { get; set; }
        [DataMember]
        public string observacion { get; set; }
        [DataMember]
        public string nom_persona { get; set; }
        [DataMember]
        public DateTime fecha_respuesta { get; set; }        
    }
}