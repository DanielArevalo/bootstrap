using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.TarjetaDebito.Entities
{
    [DataContract]
    [Serializable]
    public class MovimientoCoopcentralEnLinea
    {

        public string fecha { get; set; }
        public string hora { get; set; }
        public string grupo_trans { get; set; }
        public string transaccion { get; set; }
        public string secuencia { get; set; }
        public string origen_trans { get; set; }
        public string documento_origen { get; set; }
        public string documento_destino { get; set; }
        public string causal_trans { get; set; }
        public string adquiriente { get; set; }
        public string canal { get; set; }
        public string tipo_terminal { get; set; }
        public string entidad_origen { get; set; }
        public string entidad_destino { get; set; }
        public string cuenta_origen { get; set; }
        public string cuenta_destino { get; set; }
        public string tipo_cuenta_origen { get; set; }
        public string tipo_cuenta_destino { get; set; }
        public string tarjeta { get; set; }
        public decimal monto { get; set; }
        public decimal comision { get; set; }
        public string fecha_contable { get; set; }
        public string lugar { get; set; }
        public string banco_origen { get; set; }
        public string banco_origen_dest { get; set; }
        public string banco_destino { get; set; }
        public string banco_destino_des { get; set; }
        public string inter { get; set; }
        public string numero_terminal { get; set; }
        public string usuario_terminal { get; set; }

        // Atributos para manejo en financial
        public string tipocuenta { get; set; }
        public string operacion { get; set; }
        public int? cod_ofi { get; set; }
        public string documento { get; set; }
        public string tipotransaccion { get; set; }
        public int tipo_tran { get; set; }
    }
}
