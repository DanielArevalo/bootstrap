using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Programado.Entities
{
    [DataContract]
    [Serializable]
     public class CierreCuentaAhorroProgramado
    {
        [DataMember]
        public String Cuenta { get; set; }

        [DataMember]
        public String Linea { get; set; }

        [DataMember]
        public String Oficina { get; set; }

        [DataMember]
        public DateTime Fecha_Apertura { get; set; }

        [DataMember]
        public String Motivo_Apertura { get; set; }

        [DataMember]
        public String Identificacion { get; set; }

        [DataMember]
        public String Nombre { get; set; }

        [DataMember]
        public String Cod_nomina { get; set; }

        [DataMember]
        public DateTime Fecha_Proximo_Pago { get; set; }

        [DataMember]
        public Int32 Plazo { get; set; }

        [DataMember]
        public Int32 Saldo { get; set; }

        [DataMember]
        public String  Forma_Depago { get; set; }

        [DataMember]
        public String Estado { get; set; }
        [DataMember]
        public String retiro { get; set; }

        [DataMember]
        public DateTime Fecha_Prorroga
        {  get; set;  }

        [DataMember]
        public DateTime Fecha_Renovacion
        { get; set; }

        [DataMember]
        public DateTime Fecha_Vencimiento
        { get; set; }

        [DataMember]
        public DateTime Fecha_Inicio
        { get; set; }


        [DataMember]
        public DateTime Fecha_Final
        { get; set; }

        [DataMember]
        public String Cuenta_renovada { get; set; }

    }

}
