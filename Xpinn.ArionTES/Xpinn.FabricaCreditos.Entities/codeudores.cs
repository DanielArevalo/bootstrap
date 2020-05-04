using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad codeudores
    /// </summary>
    [DataContract]
    [Serializable]
    public class codeudores
    {
        [DataMember] 
        public Int64 idcodeud { get; set; }
        [DataMember]
        public Int64 numero_solicitud { get; set; }
        [DataMember] 
        public Int64 numero_radicacion { get; set; }
        [DataMember] 
        public Int64 codpersona { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string tipo_identificacion { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember] 
        public string tipo_codeudor { get; set; }
        [DataMember]
        public Int64 parentesco { get; set; }
        [DataMember]
        public String opinion { get; set; }
        [DataMember]
        public String responsabilidad { get; set; }
        [DataMember]
        public string mensaje { get; set; }
        [DataMember]
        public int orden { get; set; }

        #region DatosAprobacion
        [DataMember]
        public string primer_nombre { get; set; }
        [DataMember]
        public string segundo_nombre { get; set; }
        [DataMember]
        public string primer_apellido { get; set; }
        [DataMember]
        public string segundo_apellido { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string barrio { get; set; }
        #endregion
        #region DatosReporte
        [DataMember]
        public DateTime fechaexpedicion { get; set; }
        [DataMember]
        public string ciudadexpedicion { get; set; }
        [DataMember]
        public string estadocivil { get; set; }
        [DataMember]
        public string escolaridad { get; set; }
        [DataMember]
        public string personascargo { get; set; }
        [DataMember]
        public Int64 codactividad { get; set; }
        [DataMember]
        public Int64 codciudadresidencia { get; set; }
        [DataMember]
        public Int64 antiguedadlugar { get; set; }
        [DataMember]
        public string tipovivienda { get; set; }
        [DataMember]
        public string arrendador { get; set; }
        [DataMember]
        public string telefonoarrendador { get; set; }
        [DataMember]
        public string empresa { get; set; }
        [DataMember]
        public string cargo { get; set; }
        [DataMember]
        public Int64 antiguedadempresa { get; set; }
        [DataMember]
        public String tipocontrato { get; set; }
        [DataMember]
        public Int64 valorarriendo { get; set; }
        [DataMember]
        public string telefonoempresa { get; set; }
        [DataMember]
        public string direccionempresa { get; set; }
        [DataMember]
        public Int32? cantidad { get; set; }
        #endregion
    }
}