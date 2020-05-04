using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Xpinn.Comun.Entities
{
    [DataContract]
    [Serializable]
    public class Cierea
    {
        [DataMember]
        public DateTime fecha { get; set; } 
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string campo1 { get; set; }
        [DataMember]
        public string campo2 { get; set; }
        [DataMember]
        public DateTime fecrea { get; set; }
        [DataMember]
        public Int64 codusuario { get; set; }
        //Agregado para seguimiento de cierres
        [DataMember]
        public string nom_cierre_ant { get; set; }
        [DataMember]
        public string nom_cierre_sig { get; set; }
        [DataMember]
        public Int64 cod_proceso { get; set; }
        [DataMember]
        public string nom_usuario { get; set; }
        [DataMember]
        public string ruta_proceso { get; set; }

        //Control de inconsistencias al cierre

        [DataMember]
        public string codproducto { get; set; }
        [DataMember]
        public string saldo { get; set; }
        [DataMember]
        public string descripcionerror { get; set; }

    }
}
