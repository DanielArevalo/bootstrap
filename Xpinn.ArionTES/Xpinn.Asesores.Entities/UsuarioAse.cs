using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
 
namespace Xpinn.Asesores.Entities
{
    /// <summary>
    /// Entidad Usuario
    /// </summary>
    [DataContract]
    [Serializable]
    public class UsuarioAse
    {
        [DataMember]
        public Int64 codusuario { get; set; }
        [DataMember]
        public string nombre { get; set; }
        
        [DataMember]
        public string identificacion { get; set; }
       
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string login { get; set; }
        [DataMember]
        public Int64 codperfil { get; set; }
        [DataMember]
        public DateTime fechacreacion { get; set; }
        [DataMember]
        public DateTime fecha_actual { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public Int64 estado { get; set; }
        [DataMember]
        public Int64 cod_cajero { get; set; }
        [DataMember]
        public string nom_cajero { get; set; }

    }
}