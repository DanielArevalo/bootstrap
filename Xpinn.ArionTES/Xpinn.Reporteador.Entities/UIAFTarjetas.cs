using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Xpinn.Reporteador.Entities
{
    [DataContract]
    [Serializable]
    public class UIAFTarjetas
    {
        #region
        [DataMember]
        public int idreporte { get; set; }
        [DataMember]
        public int consecutivo { get; set; }
        [DataMember]
        public DateTime? fecha_tran { get; set; }
        [DataMember]
        public decimal? valor_tran { get; set; }
        [DataMember]
        public long? valor_tran_no_dec
        {
            get
            {
                return valor_tran.HasValue ? Convert.ToInt64(valor_tran) : default(long?);
            }
        }
        [DataMember]
        public string tipo_tran { get; set; }
        [DataMember]
        public string pais { get; set; }
        [DataMember]
        public string departamento { get; set; }
        [DataMember]
        public string departamento2 { get; set; }
        [DataMember]
        public string tipo_tarjeta { get; set; }
        [DataMember]
        public string numero_tarjeta { get; set; }
        [DataMember]
        public string numero_cuenta { get; set; }
        [DataMember]
        public decimal? valor_cupo { get; set; }
        [DataMember]
        public string franquicia { get; set; }
        [DataMember]
        public decimal? saldo_tarjeta { get; set; }
        [DataMember]
        public string tipo_identificacion { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string digito_verificacion { get; set; }
        [DataMember]
        public string primer_apellido { get; set; }
        [DataMember]
        public string segundo_apellido { get; set; }
        [DataMember]
        public string primer_nombre { get; set; }
        [DataMember]
        public string segundo_nombre { get; set; }
        [DataMember]
        public string razon_social { get; set; }
        [DataMember]
        public string red { get; set; }
        [DataMember]
        public string lugar { get; set; }
        #endregion
    }

}
