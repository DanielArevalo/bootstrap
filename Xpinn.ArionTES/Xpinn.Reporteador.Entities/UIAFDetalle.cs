using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Xpinn.Reporteador.Entities
{
    [DataContract]
    [Serializable]
    public class UIAFDetalle
    {
        #region
        [DataMember]
        public int idreporteproductos { get; set; }
        [DataMember]
        public int idreporte { get; set; }
        [DataMember]
        public string numero_producto { get; set; }
        [DataMember]
        public DateTime fecha_apertura { get; set; }
        [DataMember]
        public string tipo_producto { get; set; }
        [DataMember]
        public string departamento { get; set; }
        [DataMember]

        public string departamento2 { get; set; }
        [DataMember]
        public string tipo_identificacion1 { get; set; }
        [DataMember]
        public string identificacion1 { get; set; }
        [DataMember]
        public string primer_apellido1 { get; set; }
        [DataMember]
        public string segundo_apellido1 { get; set; }
        [DataMember]
        public string primer_nombre1 { get; set; }
        [DataMember]
        public string segundo_nombre1 { get; set; }
        [DataMember]
        public string razon_social1 { get; set; }
        [DataMember]
        public string tipo_identificacion2 { get; set; }
        [DataMember]
        public string identificacion2 { get; set; }
        [DataMember]
        public string primer_apellido2 { get; set; }
        [DataMember]
        public string segundo_apellido2 { get; set; }
        [DataMember]
        public string primer_nombre2 { get; set; }
        [DataMember]
        public string segundo_nombre2 { get; set; }
        [DataMember]
        public string razon_social2 { get; set; }
        [DataMember]
        public string linea { get; set; }
        //ADD
        [DataMember]
        public int consecutivo { get; set; }
        [DataMember]
        public int tipo_producto_vista { get; set; }
        [DataMember]
        public int departamento_vista { get; set; }
        [DataMember]
        public int tipo_identificacion1_vista { get; set; }
        [DataMember]
        public int tipo_identificacion2_vista { get; set; }
        [DataMember]
        public string fecha_apertura_vista { get; set; }
        [DataMember]
        public int linea_vista { get; set; }
        [DataMember]
        public DateTime fecha_vista { get; set; }
        [DataMember]
        public string tipo_tran { get; set; }
        #endregion
    }

}
