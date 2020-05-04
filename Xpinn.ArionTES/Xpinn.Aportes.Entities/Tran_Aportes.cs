using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Xpinn.Aportes.Entities
{
    
        public class Tran_Aportes
        {
            public Int64 NUMERO_TRANSACCION { get; set; }

            public Int64 COD_OPE { get; set; }

            public String NUMERO_APORTE { get; set; }

            public Int64 COD_CLIENTE { get; set; }

            public Int64 TIPO_TRAN { get; set; }

            public Int64 COD_DET_LIS { get; set; }

            public String DOCUMENTO_SOPORTE { get; set; }

            public Decimal VALOR { get; set; }

            public Int64 ESTADO { get; set; }

            public Int64 NUM_TRAN_ANULA { get; set; }

            [DataMember]
            public DateTime Fecha_Interes { get; set; }
        }
    
}
