using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Xpinn.Reporteador.Entities
{
    [DataContract]
    [Serializable]
    public class UIAF
    {
        #region 
        [DataMember]
        public int idreporte { get; set; }
        [DataMember]
        public string idformato { get; set; }
        [DataMember]
        public DateTime fecha_inicial { get; set; }
        [DataMember]
        public DateTime fecha_final { get; set; }
        [DataMember]
        public DateTime fecha_generacion { get; set; }
        [DataMember]
        public int numero_registros { get; set; }
        [DataMember]
        public int codusuario { get; set; }
        //AGREGADO 
        [DataMember]
        public string NomUsuario { get; set; }
        [DataMember]
        public int consecutivo { get; set; }
        [DataMember]
        public List<UIAFDetalle> lstProductos { get; set; } 
        #endregion
    }

   
}
