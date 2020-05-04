using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class PlanCuentas
    {
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public Int64? nivel { get; set; }
        [DataMember]
        public string depende_de { get; set; }
        [DataMember]
        public Int64? cod_moneda { get; set; }
        [DataMember]
        public string moneda { get; set; }
        [DataMember]
        public Int64? maneja_cc { get; set; }
        [DataMember]
        public Int64? maneja_ter { get; set; }
        [DataMember]
        public Int64? maneja_sc { get; set; }
        [DataMember]
        public Int64? estado { get; set; }
        [DataMember]
        public Int64? maneja_gir { get; set; }
        [DataMember]
        public Int64? maneja_traslado { get; set; }
        [DataMember]
        public Int64? impuesto { get; set; }
        [DataMember]
        public Decimal? base_minima { get; set; }
        [DataMember]
        public Decimal? porcentaje_impuesto { get; set; }
        [DataMember]
        public string codigo_nombre { get; set; }
        [DataMember]
        public int? num_impuesto { get; set; }
        [DataMember]
        public string es_impuesto { get; set; }

        // Datos para creación de cuenta NIIF
        [DataMember]
        public string cod_cuenta_niif { get; set; }
        [DataMember]
        public string nombre_niif { get; set; }
        [DataMember]
        public string depende_de_niif { get; set; }
        //agregado
        [DataMember]
        public int corriente { get; set; }
        [DataMember]
        public int nocorriente { get; set; }
        [DataMember]
        public int tipo_distribucion { get; set; }
        [DataMember]
        public decimal porcentaje_distribucion { get; set; }
        [DataMember]
        public decimal valor_distribucion { get; set; }

        //TIPO IMPUESTOS 
        [DataMember]
        public int cod_tipo_impuesto { get; set; }
        [DataMember]
        public string nombre_impuesto { get; set; }

        public List<PlanCuentasImpuesto> lstImpuestos { get; set; }


        ///AGREGADO ASUMIDOS O NO
        ///
        [DataMember]
        public int asumido { get; set; }
        [DataMember]
        public string cod_cuenta_asumido { get; set; }


        // Cuenta Centro de Costo
        [DataMember]
        public string cod_cuenta_centro_costo { get; set; }
        [DataMember]
        public string cod_cuenta_contrapartida { get; set; }
        [DataMember]
        public int tipo_producto { get; set; }
        [DataMember]
        public int cod_est_det { get; set; }
        [DataMember]
        public int idofcuenta { get; set; }
        [DataMember]
        public Decimal saldo { get; set; }
        [DataMember]
        public Int64? reportarmayor { get; set; }
    }
}
