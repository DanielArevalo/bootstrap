using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Seguridad.Entities
{
    /// <summary>
    /// Entidad Opcion
    /// </summary>
    [DataContract]
    [Serializable]
    public class Opcion
    {
        [DataMember]
        public Int64 cod_opcion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public Int64 cod_modulo { get; set; }
        [DataMember]
        public Int64 cod_proceso { get; set; }
        [DataMember]
        public string ruta { get; set; }
        [DataMember]
        public string accion { get; set; }
        [DataMember]
        public Int64 generalog { get; set; }
        [DataMember]
        public Int64 refayuda { get; set; }
        [DataMember]
        public string rutaEstadoCuenta { get; set; }
        [DataMember]
        public int? validar_Biometria { get; set; }
        [DataMember]
        public int? maneja_excepciones { get; set; }
        [DataMember]
        public string valor { get; set; }
        [DataMember]
        public int PermisosCampos { get; set; }

    }
}