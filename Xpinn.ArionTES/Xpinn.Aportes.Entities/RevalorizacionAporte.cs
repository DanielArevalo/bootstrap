using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class RevalorizacionAportes
    {
        #region RevalorizacionAportes
        [DataMember]
        public DateTime fecha { get; set; }

        [DataMember]
        public Int64 lineaaporte { get; set; }
        [DataMember]
        public Int64 tipodistrib { get; set; }
        [DataMember]
        public decimal valordist { get; set; }
        [DataMember]
        public decimal pordist { get; set; }
        [DataMember]
        public Int64 asretirados { get; set; }
        [DataMember]
        public DateTime fecharevalorizacion{ get; set; }       
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public Int64 codigo { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public Int64 tipooperacion { get; set; }
        [DataMember]
        public String nombres { get; set; }
        [DataMember]
        public String estado { get; set; }
        [DataMember]
        public Int64 num_aporte { get; set; }
        [DataMember]
        public decimal saldo_base { get; set; }
        [DataMember]
        public Int64 id { get; set; }
        [DataMember]
        public Int64 oficina{ get; set; }
        [DataMember]
        public decimal retencion { get; set; }
        [DataMember]
        public List<RevalorizacionAportes> lstRevalorizacionAportes { get; set; }
        
        #endregion RevalorizacionAportes


    }
}