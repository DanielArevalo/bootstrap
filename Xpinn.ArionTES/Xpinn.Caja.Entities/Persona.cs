using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Caja.Entities
{
    [DataContract]
    [Serializable]
    public class Persona
    {
        [DataMember]
        public Int64 IdPersona { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public string nom_persona { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public Int64 tipo_identificacion { get; set; }
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public string linea_credito { get; set; }
        [DataMember]
        public Int64 cod_linea_credito { get; set; }
        [DataMember]
        public Int64 monto_aprobado { get; set; }
        [DataMember]
        public Int64 valor_cuota { get; set; }
        [DataMember]
        public Int64 saldo_capital { get; set; }
        [DataMember]
        public Int64 garantias_comunitarias { get; set; }
        [DataMember]
        public DateTime fecha_proxima_pago { get; set; }
        [DataMember]
        public DateTime fecha_desembolso { get; set; }
        [DataMember]
        public DateTime fecha_aprobacion { get; set; }
        [DataMember]
        public DateTime? fecha_pago { get; set; }
        [DataMember]
        public Int64 valor_a_pagar { get; set; }

        [DataMember]
        public Int64 total_a_pagar { get; set; }
        [DataMember]
        public Decimal valor_a_pagar_CE { get; set; }
        [DataMember]
        public Decimal valor_CE { get; set; }
        [DataMember]
        public string Dias_mora { get; set; }
        [DataMember]
        public decimal valor_total_efectivo { get; set; }
        [DataMember]
        public decimal valor_total_cheques { get; set; }
        [DataMember]
        public decimal valor_total_otros { get; set; }
        [DataMember]
        public Int64 tipo_linea { get; set; }
        [DataMember]
        public Int64 cod_empresa { get; set; }
        [DataMember]
        public string nom_empresa { get; set; }
        [DataMember]
        public string nit { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string ciudad { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public Int64 idactivo { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string matricula { get; set; }
        [DataMember]
        public string notaria { get; set; }
        [DataMember]
        public string escritura { get; set; }
        [DataMember]
        public decimal valor_comercial { get; set; }
        [DataMember]
        public decimal valor_comprometido  { get; set; }
        [DataMember]
        public Int64 estado { get; set; }
        [DataMember]
        public string marca { get; set; }
        [DataMember]
        public string referencia { get; set; }
        [DataMember]
        public string modelo { get; set; }
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public string capacidad { get; set; }
        [DataMember]
        public string uso { get; set; }
        [DataMember]
        public string numero_chasis { get; set; }
        [DataMember]
        public string serie_motor { get; set; }
        [DataMember]
        public string mensajer_error { get; set; }

        //AGREGADO
        [DataMember]
        public Int64 idafiliacion { get; set; }
        [DataMember]
        public DateTime? fecha_afiliacion { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public int  cod_periodicidad { get; set; }
        [DataMember]
        public decimal saldo { get; set; }
        [DataMember]
        public string razon_social { get; set; }
        [DataMember]
        public Int64 tipopago { get; set; }
        [DataMember]
        public string tipo_producto { get; set; }

    }
}
