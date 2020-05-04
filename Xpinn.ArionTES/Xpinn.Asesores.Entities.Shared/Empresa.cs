using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Asesores.Entities.Common
{
    public class Empresa
    {
        public Empresa(){}

        [DataMember]
        public Int64 IdEmpresa { get; set; }

    }
}
