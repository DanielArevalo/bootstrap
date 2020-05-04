using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Indicadores.Entities
{
    [DataContract]
    [Serializable]
    public class GestionDiaria
    {
        [DataMember]
        public DateTime FECHA_HISTORICO { get; set; }
        [DataMember]
        public Int64 COD_OFICINA { get; set; }
        [DataMember]
        public string OFICINA { get; set; }
        [DataMember]
        public Int64 COD_ASESOR_COM { get; set; }
        [DataMember]
        public string NOMBRE { get; set; }
        [DataMember]
        public decimal NUM_CRED_CIERRE { get; set; }
        [DataMember]
        public decimal SALDO_CAPITAL_CIERRE { get; set; }
        [DataMember]
        public decimal NUM_CRED_ACTUAL { get; set; }
        [DataMember]
        public decimal SALDO_CAPITAL_ACTUAL { get; set; }
        [DataMember]
        public decimal NO_COLOCACION_CIERRE { get; set; }
        [DataMember]
        public decimal MONTO_COLOCACION_CIERRE { get; set; }
        [DataMember]
        public decimal NO_COLOCACION_ACTUAL { get; set; }
        [DataMember]
        public decimal MONTO_COLOCACION_ACTUAL { get; set; }
        [DataMember]
        public decimal META_COLOCACIONES { get; set; }
        [DataMember]
        public decimal CUMPLIMIENTO_COLOCACIONES { get; set; }
        [DataMember]
        public decimal NUM_CREDMORA_CIERRE { get; set; }
        [DataMember]
        public decimal SALDO_MORA_CIERRE { get; set; }
        [DataMember]
        public decimal NUM_CREDMORA_ACTUAL { get; set; }
        [DataMember]
        public decimal SALDO_MORA_ACTUAL { get; set; }
        [DataMember]
        public decimal SALDO_GCOMUNITARIA_CIERRE { get; set; }
        [DataMember]
        public decimal SALDO_GCOMUNITARIA_ACTUAL { get; set; }
        [DataMember]
        public decimal NUM_CREDMORAMAYOR30_ACTUAL { get; set; }
        [DataMember]
        public decimal SALDO_MORAMAYOR30_ACTUAL { get; set; }
        [DataMember]
        public decimal META_MORAMENOR30 { get; set; }
        [DataMember]
        public decimal CUMPLIMIENTO_MORAMENOR30 { get; set; }
        [DataMember]
        public decimal NUM_CREDMORAMAYOR60_ACTUAL { get; set; }
        [DataMember]
        public decimal SALDO_MORAMAYOR60_ACTUAL { get; set; }
        [DataMember]
        public decimal NUM_CREDMORAMENOR30_ACTUAL { get; set; }
        [DataMember]
        public decimal SALDO_MORAMENOR30_ACTUAL { get; set; }
        [DataMember]
        public decimal META_MORAMAYOR30 { get; set; }
        [DataMember]
        public decimal CUMPLIMIENTO_MORAMAYOR30 { get; set; }
    }
}


