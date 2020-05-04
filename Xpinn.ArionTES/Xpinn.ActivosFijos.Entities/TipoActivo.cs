using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.ActivosFijos.Entities
{
    [DataContract]
    [Serializable]
    public class TipoActivo
    {
        [DataMember]
        public int tipo { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string cod_cuenta_activo { get; set; }
        [DataMember]
        public string cod_cuenta_depreciacion { get; set; }
        [DataMember]
        public string cod_cuenta_gasto_venta_baja { get; set; }
        [DataMember]
        public string cod_cuenta_ingreso_venta_baja { get; set; }
        [DataMember]
        public string cod_cuenta_depreciacion_gasto { get; set; }
        [DataMember]
        public string nom_cuenta_depreciacion { get; set; }
        [DataMember]
        public string nom_cuenta_gasto_venta_baja { get; set; }
        [DataMember]
        public string nom_cuenta_ingreso_venta_baja { get; set; }
        [DataMember]
        public string nom_cuenta_depreciacion_gasto { get; set; }


        //AGREGADO

        #region  TIPO_ACTIVOS_NIF

        [DataMember]
        public int tipo_activo_nif { get; set; }
        [DataMember]
        public string descripcion { get; set; }

        #endregion


       

        #region  UNIGENERADORA

        [DataMember]
        public int codunigeneradora_nif { get; set; }
        [DataMember]
        public string descripcionUNIGE { get; set; }
        
        #endregion
      
    }
}
