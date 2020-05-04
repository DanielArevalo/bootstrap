using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    /// <summary>
    /// Datos de los conceptos
    /// </summary>
    [DataContract]
    [Serializable]
    public class InterfazNomina
    {
        public string   iden_empleado { get; set; }
        [DataMember]
        public string	identificacion { get; set; }
        [DataMember]
        public string	nombre1 { get; set; }
        [DataMember]
        public string   nombre2 { get; set; }
        [DataMember]
        public string	apellido1 { get; set; }
        [DataMember]
        public string   apellido2 { get; set; }
        [DataMember]
        public string	nombre { get; set; }
        [DataMember]
        public string	iden { get; set; }
        [DataMember]
        public string	iden_concepto { get; set; }
        [DataMember]
        public decimal	total { get; set; }
        [DataMember]
        public decimal  totalempleador { get; set; }
        [DataMember]
        public decimal  baseingreso { get; set; }
        [DataMember]
        public decimal  valorcuotaprestamo { get; set; }
        [DataMember]
        public string	iden_pago { get; set; }
        [DataMember]
        public DateTime	fechacreacion { get; set; }
        [DataMember]
        public string	tercero { get; set; }
        [DataMember]
        public string   centrocosto { get; set; }
        [DataMember]
        public string iden_periodo { get; set; }
        [DataMember]
        public string nom_periodo { get; set; }
        [DataMember]
        public DateTime? fechainicio { get; set; }
        [DataMember]
        public DateTime? fechafinal { get; set; }
        [DataMember]
        public string iden_prestamo { get; set; }
        [DataMember]
        public decimal valorprestamo { get; set; }
        [DataMember]
        public decimal valorcuota { get; set; }
        [DataMember]
        public decimal saldoprestamo { get; set; }
        [DataMember]
        public Int64? numero_radicacion { get; set; }
        [DataMember]
        public string cod_linea_credito { get; set; }
        [DataMember]
        public string nom_linea_credito { get; set; }
        [DataMember]
        public decimal monto_aprobado { get; set; }
        [DataMember]
        public decimal valor_cuota { get; set; }
        [DataMember]
        public decimal saldo_capital { get; set; }
        [DataMember]
        public DateTime? fecha_proximo_pago { get; set; }        
        [DataMember]
        public string descripcion { get; set; }  
        [DataMember]
        public Int64? cod_cliente { get; set; }
        [DataMember]
        public DateTime? fecha_aplicacion { get; set; }
        [DataMember]
        public decimal sobrante { get; set; }      
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string nom_cuenta { get; set; }
        [DataMember]
        public int estado { get; set; }
        [DataMember]
        public string formapago { get; set; }
        [DataMember]
        public string tipomov { get; set; }  
        [DataMember]
        public string iden_fsalud { get; set; }
        [DataMember]
        public string iden_fpension { get; set; }
        [DataMember]
        public string iden_fpensionvoluntaria { get; set; }
        [DataMember]
        public string iden_fsolidaridad { get; set; }
        [DataMember]
        public string tipo_tercero { get; set; }          
        [DataMember]
        public string iden_tercero { get; set; }
        [DataMember]
        public string cod_cuenta_gasto { get; set; }
    }
}
