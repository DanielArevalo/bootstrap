using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xpinn.TarjetaDebito.Entities
{
    #region clasesTransaccionesENPACTO
    public class RespuestaEnpacto
    {
        public Respuesta tran { get; set; }
        public string Error { get; set; }
    }

    public class Respuesta
    {
        public string datos { get; set; }
        public string datos_encriptados { get; set; }
        public string respuesta_encriptada { get; set; }
        public string drespuesta { get; set; }
        public string secuencia { get; set; }
        public string saldo_disponible { get; set; }
        public string saldo_total { get; set; }
        public string estado { get; set; }
        public string mensaje { get; set; }
    }

    public class TransaccionEnpacto
    {
        public string fecha { get; set; }
        public string hora { get; set; }
        public string reverso { get; set; }
        public string tipo { get; set; }
        public string secuencia { get; set; }
        public string cuenta { get; set; }
        public string tarjeta { get; set; }
        public string monto { get; set; }
        public string identificacion { get; set; }
        public string tipo_identificacion { get; set; }
        public string nombre { get; set; }
        public string tipo_cuenta { get; set; }
        public string defecto { get; set; }
        public string cupo_retiros { get; set; }
        public string max_retiros { get; set; }
        public string cupo_compras { get; set; }
        public string max_compras { get; set; }
        public string saldo_disponible { get; set; }
        public string saldo_total { get; set; }
    }

    public class RespuestaEnpactoClientes
    {
        public bool estado { get; set; }
        public string mensaje { get; set; }
        public List<RelacionClienteEnpacto> relaciones { get; set; }
    }

    public class RelacionClienteEnpacto
    {
        public string documento { get; set; }
        public string cuenta { get; set; }
        public string tarjeta { get; set; }
    }
    #endregion clasesTransaccionesENPACTO    

}
