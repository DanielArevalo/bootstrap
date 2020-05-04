using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad Familiares
    /// </summary>
    [DataContract]
    [Serializable]
    public class Familiares
    {
        [DataMember] 
        public Int64 codfamiliar { get; set; }
        [DataMember] 
        public Int64 cod_persona { get; set; }
        [DataMember]
        public String nombres { get; set; }
        [DataMember]
        public String identificacion { get; set; }
        [DataMember] 
        public Int64 codparentesco { get; set; }
        [DataMember] 
        public string sexo { get; set; }
        [DataMember] 
        public Int64 acargo { get; set; }
        [DataMember] 
        public string observaciones { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public DateTime fechanacimiento { get; set; }
        [DataMember]
        public Int64 estudia { get; set; }
       
        //Auditoria
        [DataMember] 
        public string UsuarioEdita { get; set; }
        [DataMember]
        public string UsuarioCrea { get; set; }
        [DataMember]
        public DateTime FechaCrea { get; set; }
        [DataMember]
        public DateTime FechaEdita { get; set; }

        //Listas desplegables:
        [DataMember]
        public string ListaDescripcion { get; set; }
        [DataMember]
        public Int64 ListaId { get; set; }
       
           
    }
}