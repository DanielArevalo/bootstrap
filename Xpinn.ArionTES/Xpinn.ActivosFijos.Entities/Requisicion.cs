using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.ActivosFijos.Entities
{
    [DataContract]
    [Serializable]
    public class Requisicion
    {
        [DataMember]
        public Int64 idrequisicion { get; set; }
        [DataMember]
        public DateTime fecha_requsicion { get; set; }
        [DataMember]
        public DateTime fecha_est_entrega { get; set; }

        [DataMember]
        public Int32 idarea { get; set; }

        [DataMember]
        public Int32 cod_solicita { get; set; }

        [DataMember]
        public String destino { get; set; }


        [DataMember]
        public String observacion { get; set; }

        [DataMember]
        public String cod_usuario_crea { get; set; }

        [DataMember]
        public DateTime fecha_crea { get; set; }

        [DataMember]
        public Int64 estado { get; set; }

     

      


     

    }

    public class Detalle_Requisicion
    {
        [DataMember]
        public Int64 iddetrequisicion { get; set; }
        [DataMember]
        public Int64 idrequisicion { get; set; }
        [DataMember]
        public Int64 idarticulo { get; set; }

        [DataMember]
        public Int64 cantidad { get; set; }

        [DataMember]
        public String detalle { get; set; }




    }


    public class Proceso_Requisicion
    {
        [DataMember]
        public Int64 idconsecutivo { get; set; }
        [DataMember]
        public Int64 idrequisicion { get; set; }
        [DataMember]
        public Int64 tipo_proceso { get; set; }

        [DataMember]
        public Int64 cod_usuario { get; set; }

        [DataMember]
        public DateTime fecha_proceso { get; set; }

        [DataMember]
        public String observacion { get; set; }



    }
}

