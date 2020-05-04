using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class Chequera
    {
        [DataMember]
        public int idchequera { get; set; }
        [DataMember]
        public int idctabancaria { get; set; }
        [DataMember]
        public string prefijo { get; set; }
        [DataMember]
        public int cheque_ini { get; set; }
        [DataMember]
        public int cheque_fin { get; set; }
        [DataMember]
        public DateTime fecha_entrega { get; set; }
        [DataMember]
        public int num_sig_che { get; set; }
        [DataMember]
        public int estado { get; set; }
        [DataMember]
        public DateTime fechacreacion { get; set; }
        [DataMember]
        public string usuariocreacion { get; set; }
        [DataMember]
        public DateTime fecultmod { get; set; }
        [DataMember]
        public string usuariomod { get; set; }

        [DataMember]
        public int cod_banco { get; set; }
        [DataMember]
        public string num_cuenta { get; set; }
        [DataMember]
        public string nombrebanco { get; set; }
        //Reporte chequera
        [DataMember]
        public string nombrepersona { get; set; }
        [DataMember]
        public Int64 Cedula { get; set; }
        [DataMember]
        public decimal? valor { get; set; }
        [DataMember]
        public string TipoCom { get; set; }
        [DataMember]
        public Int64 NumComp { get; set; }

    }
}