using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad SolicitudCreditosRecogidos
    /// </summary>
    [DataContract]
    [Serializable]
    public class SolicitudCreditosRecogidos
    {
        [DataMember] 
        public Int64 idsolicitudrecoge { get; set; }
        [DataMember] 
        public Int64 numerosolicitud { get; set; }
        [DataMember] 
        public Int64 numero_recoge { get; set; }
        [DataMember] 
        public DateTime fecharecoge { get; set; }
        [DataMember] 
        public Int64 valorrecoge { get; set; }
        [DataMember] 
        public DateTime fechapago { get; set; }
        [DataMember] 
        public Int64 saldocapital { get; set; }
        [DataMember] 
        public Int64 saldointcorr { get; set; }
        [DataMember] 
        public Int64 saldointmora { get; set; }
        [DataMember] 
        public Int64 saldomipyme { get; set; }
        [DataMember] 
        public Int64 saldoivamipyme { get; set; }
        [DataMember] 
        public Int64 saldootros { get; set; }

        //Parametros que recibe el sp "usp_xpinn_solicred_recoger"
        [DataMember]
        public String identificacion { get; set; }
        [DataMember]
        public DateTime fecha_pago { get; set; }

        //Campos de la tabla temp_recoger
        [DataMember]
        public Int64 numeroRadicacion { get; set; }
        [DataMember]
        public String linea { get; set; }
        [DataMember]
        public Int64 monto { get; set; }
        [DataMember]
        public Int64 saldocapitalTemp { get; set; }
        [DataMember]
        public Int64 interescorriente { get; set; }
        [DataMember]
        public Int64 interesmora { get; set; }
        [DataMember]
        public Int64 seguro { get; set; }
        [DataMember]
        public Int64 leymipyme { get; set; }
        [DataMember]
        public Int64 ivaLeymipyme { get; set; }
        [DataMember]
        public Int64 otros { get; set; }
        [DataMember]
        public Int64 totalRecoger { get; set; }
        [DataMember]
        public Int64 valor_nominas { get; set; }



    }
}