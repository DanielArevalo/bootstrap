using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Ahorros.Entities
{
    [DataContract]
    [Serializable]
    public class ReporteMovimiento
    {

        [DataMember]
        public String numero_cuenta { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public DateTime fecha_oper { get; set; }
        [DataMember]
        public string tipo_ope { get; set; }
        [DataMember]
        public string tipo_tran { get; set; }
        [DataMember]
        public int cod_ope { get; set; }
        [DataMember]
        public string nomtipo_ope { get; set; }
        [DataMember]
        public int num_comp { get; set; }
        [DataMember]
        public int tipo_comp { get; set; }
        [DataMember]
        public string tipo_mov { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public decimal saldo { get; set; }
        [DataMember]
        public int estado { get; set; }
        [DataMember]
        public int cod_linea_ahorro { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string cod_oficina { get; set; }
        [DataMember]
        public string nombre { get; set; }

        [DataMember]
        public string soporte { get; set; }
        [DataMember]
        public decimal total { get; set; }
        [DataMember]
        public decimal Abonos { get; set; }
        [DataMember]
        public decimal Contador_Abonos { get; set; }
        [DataMember]
        public decimal Cargos { get; set; }
        [DataMember]
        public decimal Contador_Cargos { get; set; }
        [DataMember]
        public decimal Intereses { get; set; }
        [DataMember]
        public decimal Contador_Intereses { get; set; }
        [DataMember]
        public decimal Retencion { get; set; }
        [DataMember]
        public decimal GMF { get; set; }
        [DataMember]
        public decimal Contador_GMF { get; set; }
        [DataMember]
        public decimal SaldoInicio { get; set; }
        [DataMember]
        public decimal SaldoFinal { get; set; }
        [DataMember]
        public decimal Contador_Retencion { get; set; }
        [DataMember]
        public decimal SaldoInicioTrans { get; set; }
    }
}