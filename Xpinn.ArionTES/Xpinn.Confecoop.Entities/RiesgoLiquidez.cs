using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Xpinn.Util;

namespace Xpinn.Confecoop.Entities
{
    [DataContract]
    [Serializable]
    public class RiesgoLiquidez
    {
        [DataMember]
        public Int64 consecutivo { get; set; }
        [DataMember]
        public DateTime? fecha_corte { get; set; }
        [DataMember]
        public Int64? renglon { get; set; }
        [DataMember]
        public int? unidad_captura { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public decimal? saldo_actual { get; set; }
        [DataMember]
        public decimal? saldo_madurar { get; set; }
        [DataMember]
        public decimal? vr_brecha1 { get; set; }
        [DataMember]
        public decimal? vr_brecha2 { get; set; }
        [DataMember]
        public decimal? vr_brecha3 { get; set; }
        [DataMember]
        public decimal? vr_brecha4 { get; set; }
        [DataMember]
        public decimal? vr_brecha5 { get; set; }
        [DataMember]
        public decimal? vr_brecha6 { get; set; }
        [DataMember]
        public decimal? vr_brecha7 { get; set; }
        [DataMember]
        public decimal? vr_brecha8 { get; set; }
        [DataMember]
        public decimal? vr_brecha9 { get; set; }
        [DataMember]
        public decimal? vr_brecha10 { get; set; }
        [DataMember]
        public decimal? vr_brecha11 { get; set; }
        [DataMember]
        public decimal? vr_brecha12 { get; set; }
        [DataMember]
        public long cod_cuenta { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public long cod_oficina { get; set; }
        [DataMember]
        public string nom_oficina { get; set; }
        [DataMember]
        public int dia { get; set; }
        [DataMember]
        public List<RiesgoLiquidez> grupo { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string numero_radicacion { get; set; }
        [DataMember]
        public string cod_categoria { get; set; }
        [DataMember]
        public ClasificacionCredito clasificacion_cartera { get; set; }
        [DataMember]
        public decimal cuota_actual { get; set; }
    }
}
