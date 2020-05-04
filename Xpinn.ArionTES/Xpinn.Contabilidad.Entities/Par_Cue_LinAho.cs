using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class Par_Cue_LinAho
    {
        [DataMember]
        public Int64 Codigo { get; set; }
        [DataMember]
        public string TipoAhorro { get; set; }
        [DataMember]
        public string LineaAhorro { get; set; }
        [DataMember]
        public int? tipo_tran { get; set; }
        [DataMember]
        public string tipoTrasaccion { get; set; }
        [DataMember]
        public string CodigoCuenta { get; set; }
        [DataMember]
        public string NombreCuenta { get; set; }
        [DataMember]
        public string cod_cuenta_niif { get; set; }
        [DataMember]
        public string nom_cuenta_niif { get; set; }
        [DataMember]
        public int tipo_mov { get; set; }
        [DataMember]
        public string nomtipo_mov { get; set; }
    }
}
