using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    /// <summary>
    /// Entidad SaldoCaja
    /// </summary>
    [DataContract]
    [Serializable]
    public class ParametrosFlujoCaja
    {
        [DataMember]
        public Int64? cod_concepto { get; set; }
        [DataMember]
        public Int64? cod_cuenta_con { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public Int64 tipo_concepto { get; set; }
        [DataMember]
        public string nom_tipo_concepto { get; set; }
        [DataMember]
        public Int64? cod_cuenta { get; set; }
        [DataMember]
        public string nom_cuenta { get; set; }
        [DataMember]
        public string tipo_mov { get; set; }

        //Valores para reporte
        [DataMember]
        public DateTime fecha_inicial { get; set; }
        [DataMember]
        public DateTime fecha_final { get; set; }
        //Valores para reporte
        [DataMember]
        public string valores { get; set; }
        [DataMember]
        public string fechas { get; set; }


        //Atributos que corresponden al valor generado al mes correspondiente


        //Atributos correspondientes al titulo del mes
        [DataMember]
        public Int64 dia { get; set; }
        [DataMember]
        public string mes { get; set; }
        [DataMember]
        public Int64 anio { get; set; }
        [DataMember]
        public string titulos { get; set; }


        [DataMember]
        public List<ParametrosFlujoCaja> lstIngresos { get; set; }
        [DataMember]
        public List<ParametrosFlujoCaja> lstEgresos { get; set; }
        [DataMember]
        public List<ParametrosFlujoCaja> lstOtrosEgresos { get; set; }
        [DataMember]
        public List<ParametrosFlujoCaja> lstConceptos { get; set; }
    }
}
