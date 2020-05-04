using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace Xpinn.Asesores.Entities
{
    [DataContract]
    [Serializable]
    public class Extracto
    {
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string tipo_persona { get; set; }
        [DataMember]
        public string tipo_identificacion { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string apellidos { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public Int64 codciudadresidencia { get; set; }
        [DataMember]
        public string nomciudad { get; set; }
        [DataMember]
        public DateTime? fechaProx_pago_Aporte { get; set; }
        [DataMember]
        public decimal? vr_totalPagar_aporte { get; set; }
        [DataMember]
        public DateTime? fechaProx_pago_Credito { get; set; }
        [DataMember]
        public decimal? vr_totalPagar_credito { get; set; }


        //Detalle de Extracto
        [DataMember]
        public Int64? codtipo_producto { get; set; }
        [DataMember]
        public string tipo_producto { get; set; }
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public string linea { get; set; }
        [DataMember]
        public DateTime fec_prox_pago { get; set; }
        [DataMember]
        public decimal vr_apagar { get; set; }
        [DataMember]
        public string nom_linea { get; set; }
        [DataMember]
        public decimal movimiento { get; set; }
        [DataMember]
        public decimal movimientoD { get; set; }
        [DataMember]
        public decimal movimientoC { get; set; }
        [DataMember]
        public decimal saldo_inicial { get; set; }
        [DataMember]
        public decimal saldo_final { get; set; }
        [DataMember]
        public decimal interes_corriente { get; set; }
        [DataMember]
        public decimal interes_mora { get; set; }
        [DataMember]
        public decimal otros_pagos { get; set; }
        [DataMember]
        public decimal saldo_pagado { get; set; }
        [DataMember]
        public List<Extracto> lista_extracto_aportes { get; set; }
        [DataMember]
        public List<Extracto> lista_extracto_creditos { get; set; }
        [DataMember]
        public List<Extracto> lista_extracto_ahorros { get; set; }
        //Lista para el extracto de CDATS
        [DataMember]
        public List<Extracto> lista_extracto_cdats { get; set; }
        [DataMember]
        public List<Extracto> lista_extracto_programado { get; set; }
        [DataMember]
        public decimal interes { get; set; }
        [DataMember]
        public decimal retencion { get; set; }

        [DataMember]
        public decimal apertura { get; set; }
    
    
}
}
