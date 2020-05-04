using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Seguridad.Entities
{
    /// <summary>
    /// Entidad Acceso
    /// </summary>
    [DataContract]
    [Serializable]
    public class Acceso
    {
        [DataMember] 
        public Int64 codacceso { get; set; }
        [DataMember] 
        public Int64 codigoperfil { get; set; }
        [DataMember]
        public Int64 cod_proceso { get; set; }
        [DataMember] 
        public Int64 cod_opcion { get; set; }
        [DataMember]
        public string ruta { get; set; }
        [DataMember] 
        public Int64 insertar { get; set; }
        [DataMember] 
        public Int64 modificar { get; set; }
        [DataMember] 
        public Int64 borrar { get; set; }
        [DataMember] 
        public Int64 consultar { get; set; }
        [DataMember]
        public Int64 generalog { get; set; }
        [DataMember]
        public string accion { get; set; }
        [DataMember]
        public string nombreperfil { get; set; }
        [DataMember]
        public string nombreopcion { get; set; }
        [DataMember]
        public string nombreproceso { get; set; }
        [DataMember]
        public Int64 cod_modulo { get; set; }
        [DataMember]
        public string nom_modulo { get; set; }
        [DataMember]
        public int PermisoCampo { get; set; }


    }

    public class CamposPermiso
    {
        [DataMember]
        public long IdComPerfil { get; set; }
        [DataMember]
        public string Campo { get; set; }
        [DataMember]
        public int Visible { get; set; }
        [DataMember]
        public int Editable { get; set; }
        [DataMember]
        public int CodPerfl { get; set; }
    }
}