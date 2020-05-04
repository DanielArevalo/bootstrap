using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad consultasdatacredito
    /// </summary>
    [DataContract]
    [Serializable]
    public class consultasdatacredito
    {
        [DataMember] 
        public Int64 numerofactura { get; set; }
        [DataMember] 
        public DateTime fechaconsulta { get; set; }
        [DataMember] 
        public string cedulacliente { get; set; }
        [DataMember] 
        public string usuario { get; set; }
        [DataMember] 
        public string ip { get; set; }
        [DataMember] 
        public string oficina { get; set; }
        [DataMember] 
        public Int64 valorconsulta { get; set; }
        [DataMember]
        public Int64 puntaje { get; set; }
        
    }

    [DataContract]
    [Serializable]
    public class CreditoEmpresaRecaudo
    {
        //TABLA PERSONA_EMPRESA_RECAUDO
        [DataMember]
        public Int64 idempresarecaudo { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public int cod_empresa { get; set; }
        [DataMember]
        public string nom_empresa { get; set; }


        [DataMember]
        public int idcrerecaudo { get; set; }
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        //[DataMember]
        //public Int64 cod_empresa { get; set; }
        [DataMember]
        public decimal porcentaje { get; set; }
        [DataMember]
        public decimal valor { get; set; }


        /// <summary>
        /// hecho para el filtro de la forma de pago en participacion pagadurias
        /// </summary>
        /// 
        [DataMember]
        public int forma_pago { get; set; }
        [DataMember]
        public string filtropago { get; set; }
    }

}