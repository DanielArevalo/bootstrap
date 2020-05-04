using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Auxilios.Entities
{
    [DataContract]
    [Serializable]
    public class LineaAuxilio
    {
        [DataMember]
        public string cod_linea_auxilio { get; set; }
        [DataMember]
        public int cod_linea_auxilios { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public int estado { get; set; }
        [DataMember]
        public decimal monto_maximo { get; set; }
        [DataMember]
        public decimal monto_minimo { get; set; }
        [DataMember]
        public int cod_periodicidad { get; set; }
        [DataMember]
        public string tipo_persona { get; set; }
        [DataMember]
        public int numero_auxilios { get; set; }
        [DataMember]
        public int dias_desembolso { get; set; }
        [DataMember]
        public int cobra_retencion { get; set; }
        [DataMember]
        public int educativo { get; set; }
        [DataMember]
        public decimal porc_matricula { get; set; }
        [DataMember]
        public int orden_servicio { get; set; }

        //agregado
        [DataMember]
        public string nomestado { get; set; }
        [DataMember]
        public string nomperiodicidad { get; set; }
        [DataMember]
        public string retencion { get; set; }
        [DataMember]
        public List<RequisitosLineaAuxilio> lstRequisitos { get; set; }

        [DataMember]
        public Int64 idparametro { get; set; }
        [DataMember]
        public int cod_atr { get; set; }
        [DataMember]
        public int cod_est_det { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public int? tipo_mov { get; set; }
        [DataMember]
        public int? tipo { get; set; }
        [DataMember]
        public int? tipo_tran { get; set; }

        [DataMember]
        public int tipo_cuenta { get; set; }
        [DataMember]
        public string nombrelinea { get; set; }
        [DataMember]
        public string nomestructura { get; set; }
        [DataMember]
        public string nomCuenta { get; set; }
        [DataMember]
        public string nom_tipo_mov { get; set; }
        [DataMember]
        public string nomtipo_tran { get; set; }
        [DataMember]
        public int permite_mora { get; set; }
        [DataMember]
        public int permite_retirados { get; set; }
    }



    [DataContract]
    [Serializable]
    public class RequisitosLineaAuxilio
    {
        [DataMember]
        public Int64 codrequisitoaux { get; set; }
        [DataMember]
        public string cod_linea_auxilio { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public int? requerido { get; set; }
    }

}
