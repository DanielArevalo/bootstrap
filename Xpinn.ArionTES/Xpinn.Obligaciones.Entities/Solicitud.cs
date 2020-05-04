using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Obligaciones.Entities
{
    /// <summary>
    /// Entidad Solicitud
    /// </summary>
    [DataContract]
    [Serializable]
    public class Solicitud
    {
        //Estos campos hacen parte de la tabla OBCREDITO
        [DataMember]
        public Int64 codobligacion { get; set; }
        [DataMember]
        public Int64 codlineaobligacion { get; set; }
        [DataMember]
        public Int64 codentidad { get; set; }
        [DataMember]
        public decimal montosolicitado { get; set; }
        [DataMember]
        public decimal montoaprobado { get; set; }
        [DataMember]
        public Int64 tipomoneda { get; set; }
        [DataMember]
        public decimal saldocapital { get; set; }
        [DataMember]
        public DateTime fechasolicitud { get; set; }
        [DataMember]
        public DateTime fecha_aprobacion { get; set; }
        [DataMember]
        public DateTime fecha_inicio { get; set; }
        [DataMember]
        public DateTime fecha_terminacion { get; set; }
        [DataMember]
        public Int64 cuota { get; set; }
        [DataMember]
        public DateTime fechaproximopago { get; set; }
        [DataMember]
        public DateTime fechaultimopago { get; set; }
        [DataMember]
        public Int64 plazo { get; set; }
        [DataMember]
        public Int64 gracia { get; set; }
        [DataMember]
        public Int64 tipo_gracia { get; set; }
        [DataMember]
        public Int64 tipoliquidacion { get; set; }
        [DataMember]
        public Int64 codperiodicidad { get; set; }
        [DataMember]
        public string estadoobligacion { get; set; }
        [DataMember]
        public Int64 numeropagare { get; set; }
        [DataMember]
        public Int64 cuotaspagadas { get; set; }

        //estos campos hacen parte de la tabla OBComponente
        [DataMember]
        public Int64 calculocomponente { get; set; }
        [DataMember]
        public Int64 tipo_historico { get; set; }
        [DataMember]
        public Int64 cod_tipo_tasa { get; set; }
        [DataMember]
        public decimal tasa { get; set; }
        [DataMember]
        public decimal spread { get; set; }

        //Agregada a la solicituda para saber a que cuenta se desembolsa
        [DataMember]
        public string cuenta { get; set; }
        [DataMember]
        public Int64? cod_ope { get; set; }


    }
}