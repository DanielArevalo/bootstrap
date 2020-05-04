using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace Xpinn.Asesores.Entities
{
    public class MotivoAfiliacion
    {
        #region columnasBaseDatos
        [DataMember]
        public Int64 IdMotivoAfiliacion { get; set; } //Corresponde icodmotivo
        [DataMember]
        public string Observaciones { get; set; }//Corresponde al smotivoafiliacion 

        #endregion

    }
}
