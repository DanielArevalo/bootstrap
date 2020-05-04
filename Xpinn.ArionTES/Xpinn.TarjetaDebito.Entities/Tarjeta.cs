using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.TarjetaDebito.Entities
{
    [DataContract]
    [Serializable]
    public class Tarjeta
    {
        [DataMember]
        public int idtarjeta { get; set; }
        [DataMember]
        public string numtarjeta { get; set; }
        [DataMember]
        public string convenio { get; set; }
        [DataMember]
        public string oficina { get; set; }
        [DataMember]
        public int? codoficina { get; set; }
        [DataMember]
        public DateTime? fecha_asignacion { get; set; }
        [DataMember]
        public string tipo_cuenta { get; set; }       
        [DataMember]
        public string numero_cuenta { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string cod_nomina { get; set; }
        [DataMember]
        public decimal? saldo_total { get; set; }
        [DataMember]
        public decimal? saldo_disponible { get; set; }
        [DataMember]
        public decimal? saldo_canje{ get; set; }
        [DataMember]
        public decimal? saldo { get; set; }        
        [DataMember]
        public decimal? cupo { get; set; }       
        [DataMember]
        public string cuenta_homologa { get; set; }
        [DataMember]
        public int? max_tran { get; set; }
        [DataMember]
        public int? cobra_cuota_manejo { get; set; }
        [DataMember]
        public decimal? cuota_manejo { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public Int64? cod_convenio { get; set; }
        [DataMember]
        public DateTime? fecha_proceso{ get; set; }
        [DataMember]
        public DateTime? fecha_activacion { get; set; }
        // LLENAR COMBOS
        [DataMember]
        public Int64? cod_oficina { get; set; }
        [DataMember]
        public String estado { get; set; }
        [DataMember]
        public int cod_tipocta { get; set; }
        [DataMember]
        public String nomcuenta { get; set; }
        [DataMember]
        public int codconvenio { get; set; }
        [DataMember]
        public String nom_convenio { get; set; }       
        //TARJETA
        [DataMember]
        public int idplastico{ get; set; }
        [DataMember]
        public int cod_ahorro { get; set; }
        [DataMember]
        public int cod_linea_credito{ get; set; }
        [DataMember]
        public int num_radicacion { get; set; }
        [DataMember]
        public long? cod_asesor { get; set; }
        [DataMember]
        public decimal? cupo_cajero { get; set; }
        [DataMember]
        public decimal? transacciones_cajero { get; set; }
        [DataMember]
        public decimal? cupo_datafono { get; set; }
        [DataMember]
        public decimal? transacciones_datafono { get; set; }
        [DataMember]
        public int pendienteParaBloquear { get; set; }
        [DataMember]
        public int pendienteParaDesbloquear { get; set; }
        [DataMember]
        public string desc_estado_pendiente { get; set; }
        [DataMember]
        public int dias_mora { get; set; }
        [DataMember]
        public string desc_estado { get; set; }
        [DataMember]
        public Int32 estado_saldo { get; set; }
    }
    
        
}