using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Caja.Entities
{
    /// <summary>
    /// Entidad TransaccionCaja
    /// </summary>
    [DataContract]
    [Serializable]
    public class TransaccionCaja
    {
        [DataMember] 
        public Int64 cod_movimiento { get; set; }
        [DataMember] 
        public Int64 cod_caja { get; set; }
        [DataMember]
        public string nom_caja { get; set; }


        [DataMember] 
        public Int64 cod_cajero { get; set; }
        [DataMember]
        public string nom_cajero { get; set; }
        [DataMember] 
        public DateTime fecha_movimiento { get; set; }
        [DataMember]
        public DateTime fecha_movimientofinal { get; set; }
        [DataMember]
        public DateTime fecha_aplica { get; set; }
        [DataMember]
        public DateTime fecha_cierre { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public string nom_oficina { get; set; }
        [DataMember] 
        public string tipo_movimiento { get; set; }
        [DataMember] 
        public Int64 cod_persona { get; set; }
        [DataMember] 
        public Int64 num_producto { get; set; }
        [DataMember] 
        public string nom_tipo_producto { get; set; }
        [DataMember] 
        public Int64 cod_moneda { get; set; }
        [DataMember]
        public string nom_moneda { get; set; }
        [DataMember]
        public decimal valor_pago { get; set; }
        [DataMember]
        public decimal valor_pago_ing { get; set; }
        [DataMember]
        public decimal valor_pago_egr { get; set; }
        [DataMember] 
        public Int64 tasa_cambio { get; set; }
        [DataMember]
        public Int64 tipo_pago { get; set; }
        [DataMember]
        public string nom_tipo_tran { get; set; }
        [DataMember]
        public string observacion { get; set; }
        [DataMember]
        public Int64? cod_proceso { get; set; }

        [DataMember]
        public Int64 cod_ope{ get; set; }
        [DataMember]
        public Int64 num_comp { get; set; }
        [DataMember]
        public Int64 tipo_comp { get; set; }
        [DataMember]
        public Int64 tipo_ope { get; set; }
        [DataMember]
        public string nom_tipo_ope { get; set; }

        [DataMember]
        public Int64 cod_motivo_reversion { get; set; }

        // sirve para realizar las consultas en el Reporte de Movimientos de Operaciones
        [DataMember]
        public DateTime fecha_consulta_inicial { get; set; }
        [DataMember]
        public DateTime fecha_consulta_final{ get; set; }
        [DataMember]
        public string cod_linea_credito { get; set; }
        [DataMember]
        public string nombre_atributo { get; set; }
        [DataMember]
        public string tipo_cta { get; set; }
        [DataMember]
        public string numero_radicacion { get; set; }
        [DataMember]
        public string nom_cliente { get; set; }
        [DataMember]
        public string iden_cliente { get; set; }
        [DataMember]
        public string usuario { get; set; }
        [DataMember]
        public Int16 resultado { get; set; }

        [DataMember]
        public string concepto { get; set; }
        [DataMember]
        public decimal efectivo { get; set; }
        [DataMember]
        public decimal cheque { get; set; }
        [DataMember]
        public decimal total { get; set; }

        // sirve para realizar  consulta de  la tabal general de la linea castigada
        [DataMember]
        public String parametro { get; set; }
        [DataMember]
        public String baucher { get; set; }
        //añadido para saber forma de pago
        [DataMember]
        public String nomtipo_pago { get; set; }
        public bool es_super_auxiliar { get; set; }

        public String identificacion { get; set; }


        public String tipoproducto { get; set; }

        [DataMember]
        public String nom_producto { get; set; }

        [DataMember]
        public string nom_Cliente{ get; set; }
        [DataMember]
        public long codigo_usuario { get; set; }
        [DataMember]
        public DateTime fecha_pago { get; set; }
        [DataMember]
        public long tipo_tran { get; set; }
        [DataMember]
        public string sobrante { get; set; }
        [DataMember]
        public string error { get; set; }
        [DataMember]
        public string documento { get; set; }
        [DataMember]
        public long pagoRotativo { get; set; }
    }
    public class PersonaTransaccion
    {
        [DataMember]
        public long tipo_documento { get; set; }
        [DataMember]
        public long documento { get; set; }
        [DataMember]
        public string primer_nombre { get; set; }
        [DataMember]
        public string segundo_nombre { get; set; }
        [DataMember]
        public string primer_apellido{ get; set; }
        [DataMember]
        public string segundo_apellido { get; set; }
        [DataMember]
        public bool titular { get; set; }
    }
}