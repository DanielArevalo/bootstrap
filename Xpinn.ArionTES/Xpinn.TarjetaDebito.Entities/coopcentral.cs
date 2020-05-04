using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xpinn.TarjetaDebito.Entities
{
    #region clasesTransaccionesCOOPCENTRAL

    public class RespuestaCoopcentral
    {
        public string datos { get; set; }
        public string datos_encriptados { get; set; }
        public string respuesta { get; set; }
        public string estado { get; set; }
        public string mensaje { get; set; }
        public string secuencia { get; set; }
        public string saldo_disponible { get; set; }
        public string saldo_total { get; set; }
    }

    public class TransaccionCoopcentral
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
        public string cupo_retiros { get; set; }
        public string defecto { get; set; }
        public string max_retiros { get; set; }
        public string cupo_compras { get; set; }
        public string max_compras { get; set; }
        public string saldo_disponible { get; set; }
        public string saldo_total { get; set; }
        public string bloqueo { get; set; }

        // Datos para registro del cliente
        public string fecha_expedicion { get; set; }
        public string actualizar { get; set; }
        public string nombre1 { get; set; }
        public string nombre2 { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string direccion_casa { get; set; }
        public string direccion_trabajo { get; set; }
        public string telefono_casa { get; set; }
        public string telefono_trabajo { get; set; }
        public string celular { get; set; }
        public string fecha_nacimiento { get; set; }
        public string sexo { get; set; }
        public string email { get; set; }
        public string cuenta_defecto { get; set; }
        public string canal { get; set; }
        public string noperaciones { get; set; }
        public string monto_maximo { get; set; }
        public string bancodebogota { get; set; }
        public string conv_bogota { get; set; }
        public string pago_minimo { get; set; }
        public string pago_total { get; set; }
        public string fecha_vencimiento { get; set; }
        public string sms { get; set; }
    }


    #endregion clasesTransaccionesCOOPCENTRAL

}
