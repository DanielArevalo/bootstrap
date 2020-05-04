using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad InformacionNegocio
    /// </summary>
    [DataContract]
    [Serializable]
    public class InformacionNegocio
    {
        [DataMember] 
        public Int64 cod_negocio { get; set; }
        [DataMember]
        public Int64 valor_arriendo { get; set; }
        [DataMember] 
        public Int64 cod_persona { get; set; }
        [DataMember] 
        public string direccion { get; set; }
        [DataMember] 
        public string telefono { get; set; }
        [DataMember] 
        public string localidad { get; set; }
        [DataMember]
        public Int64 barrio { get; set; }
        [DataMember] 
        public string nombrenegocio { get; set; }
        [DataMember] 
        public string descripcion { get; set; }
        [DataMember] 
        public Int64 antiguedad { get; set; }
        [DataMember] 
        public Int64 propia { get; set; }
        [DataMember]
        public string tipo_propiedad { get; set; }
        [DataMember] 
        public string arrendador { get; set; }
        [DataMember] 
        public string telefonoarrendador { get; set; }
        [DataMember] 
        public Int64 codactividad { get; set; }
        [DataMember]
        public string descactividad { get; set; }
        [DataMember] 
        public Decimal experiencia { get; set; }
        [DataMember] 
        public Int64 emplperm { get; set; }
        [DataMember] 
        public Int64 empltem { get; set; }
        [DataMember] 
        public DateTime fechacreacion { get; set; }
        [DataMember] 
        public string usuariocreacion { get; set; }
        [DataMember] 
        public DateTime fecultmod { get; set; }
        [DataMember] 
        public string usuultmod { get; set; }
        [DataMember]
        public Int64 sector { get; set; }
        [DataMember]
        public string barrioneg { get; set; }
         [DataMember]
        public string actividad { get; set; }
        // LISTAS DESPLEGABLES

        [DataMember]
        public Int64 ListaId { get; set; }
        [DataMember]
        public String ListaIdStr { get; set; }
        [DataMember]
        public String ListaDescripcion { get; set; }
        [DataMember]
        public String ListaSolicitada { get; set; }

    }
}