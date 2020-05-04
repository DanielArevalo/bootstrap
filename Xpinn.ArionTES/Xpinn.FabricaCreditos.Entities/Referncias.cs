using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad Referncias
    /// </summary>
    [DataContract]
    [Serializable]
    public class Referncias
    {
        [DataMember] 
        public Int64 codreferencia { get; set; }
        [DataMember] 
        public Int64 cod_persona { get; set; }
        [DataMember]
        public Int64 cod_persona_quien_referencia { get; set; }
        [DataMember] 
        public Int64 tiporeferencia { get; set; }
        [DataMember] 
        public string nombres { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember] 
        public Int64 codparentesco { get; set; }
        [DataMember] 
        public string direccion { get; set; }
        [DataMember]
        public string direccionref { get; set; }
        [DataMember] 
        public string telefono { get; set; }
        [DataMember]
        public string telefonoref { get; set; }
        [DataMember] 
        public string teloficina { get; set; }
        [DataMember] 
        public string celular { get; set; }
        [DataMember] 
        public Int64 estado { get; set; }
        [DataMember] 
        public Int64 codusuverifica { get; set; }
        [DataMember] 
        public DateTime fechaverifica { get; set; }
        [DataMember] 
        public string calificacion { get; set; }
        [DataMember] 
        public string observaciones { get; set; }
        [DataMember] 
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public long? numero_solicitud { get; set; }     
        [DataMember]
        public String descripcion { get; set; }

        // LISTAS DESPLEGABLES

        [DataMember]
        public Int64 ListaId { get; set; }
        [DataMember]
        public String ListaIdStr { get; set; }
        [DataMember]
        public String ListaDescripcion { get; set; }


    }
}