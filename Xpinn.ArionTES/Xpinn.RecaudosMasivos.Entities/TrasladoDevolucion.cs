using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class TrasladoDevolucion
    {
        //num_devolucion
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string cod_nomina { get; set; }
        [DataMember]
        public decimal saldo { get; set; }
        [DataMember]
        public string concepto { get; set; }

        //TABLA TRASLADO DEVOLUCIONES
        [DataMember]
        public Int64 numero_transaccion { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public Int64 num_devolucion { get; set; }
        [DataMember]
        public int tipo_tran { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public int estado { get; set; }
        [DataMember]
        public string nomestado { get; set; }
    }

}



