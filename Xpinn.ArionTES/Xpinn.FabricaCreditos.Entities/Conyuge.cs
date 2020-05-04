using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad Conyuge
    /// </summary>
    [DataContract]
    [Serializable]
    public class Conyuge
    {
        [DataMember] 
        public Int64 cod_conyuge { get; set; }
        [DataMember] 
        public Int64 cod_persona { get; set; }
        [DataMember]
        public Int64 identificacion { get; set; }
        [DataMember]
        public Int16 telefono { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public Int16 actividad { get; set; }
        [DataMember]
        public Int16 actividad_empresa { get; set; }
        [DataMember]
        public string nombre_empresa { get; set; }
        [DataMember]
        public Int16 nit_empresa { get; set; }
        [DataMember]
        public Int16 cargo { get; set; }
        [DataMember]
        public Int64 salario { get; set; }
        [DataMember]
        public Int16 antiguedad_empresa { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string direccion_empresa { get; set; }

        // reporte solicitud 

        [DataMember]
        public string primer_nombre { get; set; }
        [DataMember]
        public string segundo_nombre { get; set; }
        [DataMember]
        public string primer_apellido { get; set; }
        [DataMember]
        public string segundo_apellido{ get; set; }
         [DataMember]
        public string celular { get; set; }
         [DataMember]
         public DateTime fechaexpedicion { get; set; }
         [DataMember]
         public string empresa { get; set; }
         [DataMember]
         public string antiguedadempresa { get; set; }
         [DataMember]
         public string telefonoempresa { get; set; }
         [DataMember]
         public string barrio { get; set; }
         [DataMember]
         public string ciudadexpedicion { get; set; }
         [DataMember]
         public string estadocivil { get; set; }
         [DataMember]
         public string escolaridad { get; set; }
         [DataMember]
         public string cargorepo { get; set; }
         [DataMember]
         public string tipocontrato { get; set; }
         [DataMember]
         public string tipoidentificacion { get; set; }
    }
}