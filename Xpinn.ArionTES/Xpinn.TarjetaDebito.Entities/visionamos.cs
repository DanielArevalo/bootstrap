using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xpinn.TarjetaDebito.Entities
{
    public class visionamos
    {
    }

    #region Clases para clientes
    public class TransaccionVisionamos
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
        public string monto { get; set; }
        public string comision { get; set; }
        public string fecha_contable { get; set; }
        public string lugar { get; set; }
        public string banco_origen { get; set; }
        public string banco_origen_des { get; set; }
        public string banco_destino { get; set; }
        public string banco_destino_des { get; set; }
        public string inter { get; set; }
    }


    public class RespuestaCoopcentralClientes
    {
        public bool estado { get; set; }
        public string mensaje { get; set; }
        public List<RelacionClienteCoopcentral> relaciones { get; set; }
    }

    public class RelacionClienteCoopcentral
    {
        public string documento { get; set; }
        public string cuenta { get; set; }
        public string tarjeta { get; set; }
    }

    #endregion

}
