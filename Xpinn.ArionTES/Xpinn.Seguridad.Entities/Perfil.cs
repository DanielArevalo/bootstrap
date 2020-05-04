using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Seguridad.Entities
{
    /// <summary>
    /// Entidad Perfil
    /// </summary>
    [DataContract]
    [Serializable]
    public class Perfil
    {
        [DataMember]
        public Int64 codperfil { get; set; }
        [DataMember]
        public string nombreperfil { get; set; }        
        [DataMember]
        public int? cod_periodicidad { get; set; }
        [DataMember]
        public int es_administrador { get; set; }
        [DataMember]
        public List<Acceso> lstAccesos { get; set; }
        [DataMember]
        public Boolean caracter { get; set; }
        [DataMember]
        public int longitud { get; set; }
        [DataMember]
        public Boolean numero { get; set; }
        [DataMember]
        public Boolean mayuscula { get; set; }

        #region ENTIDADES CAMBIO DE CLAVE      
        [DataMember]
        public DateTime? fecha { get; set; }
        [DataMember]
        public int? numero_dias { get; set; }
        #endregion

    }
}