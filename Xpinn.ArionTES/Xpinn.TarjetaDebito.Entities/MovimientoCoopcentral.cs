using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.TarjetaDebito.Entities
{
    [DataContract]
    [Serializable]
    public class MovimientoCoopcentral
    {
        #region atributos para cargue de la información de COOPCENTRAL

        [DataMember]
        public string idasociado { get; set; }
        [DataMember]
        public string tarjeta { get; set; }
        [DataMember]
        public string cuenta_origen { get; set; }
        [DataMember]
        public string tipo_cuenta_origen { get; set; }
        [DataMember]
        public string cuenta_destino { get; set; }
        [DataMember]
        public string tipo_cuenta_destino { get; set; }
        [DataMember]
        public string fecha_transaccion { get; set; }
        [DataMember]
        public string hora_transaccion { get; set; }
        [DataMember]
        public string fecha_contable { get; set; }
        [DataMember]
        public string transaccion { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string escenario { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public decimal comision_asociado { get; set; }
        [DataMember]
        public decimal utilidad_entidad { get; set; }
        [DataMember]
        public string secuencia { get; set; }
        [DataMember]
        public string descripcion_terminal { get; set; }
        [DataMember]
        public string codigo_terminal { get; set; }
        [DataMember]
        public string tipo_terminal { get; set; }
        [DataMember]
        public decimal comision_asumida { get; set; }
        [DataMember]
        public string ubicacion_terminal { get; set; }

        #endregion

        #region Atributos para el control de la transaccion en FINANCIAL
        [DataMember]
        public Int32? num_tran { get; set; } // Esta es para saber el número que le asigno al momento de crear la transacción
        [DataMember]
        public string tipocuenta { get; set; }
        [DataMember]
        public Int64? cod_ofi { get; set; }
        [DataMember]
        public int? tipo_tran { get; set; }
        [DataMember]
        public Int64? cod_cliente { get; set; }
        [DataMember]
        public Int64 idtarjeta { get; set; }
        [DataMember]
        public string numero_tarjeta { get; set; }
        [DataMember]
        public Int64? cod_ope { get; set; }
        [DataMember]
        public decimal? saldo_total { get; set; }
        [DataMember]
        public string fecha_corte { get; set; }
        [DataMember]
        public string cod_caja { get; set; }
        [DataMember]
        public Int64? cod_ope_apl { get; set; }
        [DataMember]
        public decimal? valor_apl { get; set; }
        [DataMember]
        public decimal? comision_apl { get; set; }
        [DataMember]
        public Int64? num_comp_apl { get; set; }
        [DataMember]
        public Int64? tipo_comp_apl { get; set; }
        [DataMember]
        public decimal cuenta_porcobrar { get; set; }
        [DataMember]
        public Int64? num_tran_tarjeta { get; set; }
        #endregion

    }

    public class Datafono
    {
        [DataMember]
        public string cod_caja { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string cod_datafono { get; set; }
    }


}
