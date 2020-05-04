using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO; 

namespace Xpinn.Asesores.Entities
{
    public class MotivoModificacion
    {
        #region columnasBaseDatos
        [DataMember]
        public Int64 IdMotivoModificacion { get; set; } //Corresponde icodmotivo
        [DataMember]
        public string NombreMotivoModificacion { get; set; }//Corresponde al smotivoafiliacion 

        #endregion
    }
}
