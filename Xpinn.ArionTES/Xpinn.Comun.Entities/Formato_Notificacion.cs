using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Comun.Entities
{
    [DataContract]
    [Serializable]
    public class Formato_Notificacion
    {

        public Formato_Notificacion(int Cod_persona, int Codigo, string Parametros = null, byte[] Adjunto = null)
        {
            cod_persona = Cod_persona;
            codigo = Codigo;
            parametros_reemp = Parametros;
            adjunto = Adjunto;
        }

        public Formato_Notificacion(int Codigo)
        {
            codigo = Codigo;
        }

        [DataMember]
        public int codigo { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string texto { get; set; }
        [DataMember]
        public string textoBase { get; set; }
        [DataMember]
        public string parametros { get; set; }
        [DataMember]
        public string parametros_reemp { get; set; }
        [DataMember]
        public byte[] adjunto { get; set; }
        [DataMember]
        public int cod_persona { get; set; }
        [DataMember]
        public string enviador { get; set; }
        [DataMember]
        public string claveEnviador { get; set; }
        [DataMember]
        public string mensaje { get; set; }
        [DataMember]
        public string emailReceptor { get; set; }
        [DataMember]
        public string receptor { get; set; }
        [DataMember]
        public DateTime fecha_consulta { get; set; }
    }
}