﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class Area
    {
        [DataMember]
        public Int64 IdArea { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public Int64 CodEmpresa { get; set; }

        //Cuenta Contable
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string nombre_cuenta { get; set; }
        [DataMember]
        public Int64 idparametro { get; set; }
        [DataMember]
        public Int64 consecutivo { get; set; }


    }
}
