using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Ahorros.Entities
{
    [DataContract]
    [Serializable]
    public class SolicitudProductosWeb
    {
        [DataMember]
        public Int64 IDSOLICITUD { get; set; }

        [DataMember]
        public String TIPOAHORRO { get; set; }

        [DataMember]
        public String COD_LINEAAHORRO { get; set; }

        [DataMember]
        public Int64 COD_PERSONA { get; set; }

        [DataMember]
        public DateTime FECHA { get; set; }
        
        [DataMember]
        public Decimal VALOR_CUOTA { get; set; }
         
        [DataMember]
        public Int64 PERIODICIDAD { get; set; }

        [DataMember]
        public int ESTADO { get; set; }
        [DataMember]
        public string ESTADOS { get; set; }

        [DataMember]
        public int FORMA_PAGO { get; set; }

        [DataMember]
        public int MODALIDAD { get; set; }

        [DataMember]
        public int OFICINA { get; set; }

        [DataMember]
        public string Nom_Linea { get; set; }

        [DataMember]
        public string Identificacion { get; set; }

        [DataMember]
        public string tipo_identificacion { get; set; }

        [DataMember]
        public string nom_persona { get; set; }

        [DataMember]
        public string DESCRIPCION { get; set; }

        [DataMember]
        public int COD_ASESOR { get; set; }

        [DataMember]
        public int PLAZO { get; set; }

        [DataMember]
        public string NUM_CUENTA { get; set; }

        [DataMember]
        public DateTime FECHA_CIERRE { get; set; }

        [DataMember]
        public Decimal SALDO_TOTAL { get; set; }

        [DataMember]
        public Decimal SALDO_CANJE { get; set; }

        [DataMember]
        public int FORMA_TASA { get; set; }

        [DataMember]
        public int TIPO_TASA { get; set; }

        [DataMember]
        public Decimal TASA { get; set; }

        [DataMember]
        public Decimal SALDO_INTERESES { get; set; }

        [DataMember]
        public Decimal RETENCION { get; set; }

        [DataMember]
        public int COD_EMPRESA { get; set; }

        [DataMember]
        public Decimal TASA_INTERES { get; set; }

        [DataMember]
        public DateTime FECHA_INTERES { get; set; }

        [DataMember]
        public Decimal TOTAL_RETENCION { get; set; }

        [DataMember]
        public string COD_PERIODICIDAD_INT { get; set; }

        [DataMember]
        public string COD_MODALIDA { get; set; }



    }
}
