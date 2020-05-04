using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Indicadores.Entities
{
    [DataContract]
    [Serializable]
    public class CarteraVencida
    {
        [DataMember]
        public string fecha_historico { get; set; }
        [DataMember]
        public string valor_vencido { get; set; }
        [DataMember]
        public decimal porcentaje_vencido { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string fecha_historico_tot { get; set; }
        [DataMember]
        public string valor_vencido_tot { get; set; }
        [DataMember]
        public decimal porcentaje_vencido_tot { get; set; }
        [DataMember]
        public string descripcion_tot { get; set; }
        [DataMember]
        public string cod_linea_credito { get; set; }
        [DataMember]
        public string nombre { get; set; }

    }


}


