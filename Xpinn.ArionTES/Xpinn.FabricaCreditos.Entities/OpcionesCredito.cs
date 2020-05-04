using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad LineasCredito
    /// </summary>
    [DataContract]
    [Serializable]
    public class OpcionesCredito
    {
        #region Lineas
        [DataMember]
        public Int64 cod_clasifica { get; set; }
        [DataMember]
        public Int64 cod_opcion { get; set; }
        [DataMember]
        public Int64 codigoperfil { get; set; }
        [DataMember]
        public String nombreopcion { get; set; }
        [DataMember]
        public Int64 cod_modulo { get; set; }
        [DataMember]
        public String nom_modulo { get; set; }
        [DataMember]
        public List<OpcionesCredito> lstAtributos { get; set; }
        [DataMember]
        public Boolean check { get; set; }
        #endregion
       
    }
    
}
