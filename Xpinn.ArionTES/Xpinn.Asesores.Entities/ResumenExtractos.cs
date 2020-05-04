using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Asesores.Entities
{

    [DataContract]
    [Serializable]
    public class ResumenExtractos
    {
        [DataMember]
        public Int64 NumeroRadicacion { get; set; }
        [DataMember]
        public Int64 CodPersona { get; set; }
        [DataMember]
        public Int64 Identificacion { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public Int32 CodigoLineaDeCredito { get; set; }
        [DataMember]
        public string Linea { get; set; }
        [DataMember]
        public double SaldoInicial { get; set; }
        [DataMember]
        public double Debitos { get; set; }
        [DataMember]
        public double Creditos { get; set; }
        [DataMember]
        public double SaldoFinal { get; set; }
        [DataMember]
        public string Oficina { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public double Pagos { get; set; }
        [DataMember]
        public string Ciudad { get; set; }
        [DataMember]
        public string Barrio { get; set; }
        [DataMember]
        public string Direccion { get; set; }
        [DataMember]
        public DateTime FechaCorte { get; set; }
        [DataMember]
        public DateTime FechaInicial { get; set; }
        [DataMember]
        public DateTime FechaFinal { get; set; }
        [DataMember]
        public string Asesor { get; set; }
        [DataMember]
        public string numpagare { get; set; }
        [DataMember]
        public string nomperiodicidad { get; set; }
        [DataMember]
        public string estadocre { get; set; }
        [DataMember]
        public decimal valor_cuota { get; set; }
        [DataMember]
        public DateTime fec_proximo_pago { get; set; }
        [DataMember]
        public int plazo { get; set; }

        [DataMember]
        public decimal capital { get; set; }
        [DataMember]
        public decimal interes_cte { get; set; }
        [DataMember]
        public decimal seguro { get; set; }
        [DataMember]
        public decimal interes_mora { get; set; }
        [DataMember]
        public string tasa_interes { get; set; }       
        
        //AGREGADO PARA LISTAR BANCOS
        [DataMember]
        public Int64 cod_banco_para { get; set; }
        [DataMember]
        public string num_cuenta { get; set; }
        [DataMember]
        public string nombrebanco { get; set; }

    }
}
