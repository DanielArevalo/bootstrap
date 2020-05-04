using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad InventarioMateriaPrima
    /// </summary>
    [DataContract]
    [Serializable]
    public class InventarioMateriaPrima
    {
        [DataMember] 
        public Int64 cod_matprima { get; set; }
        [DataMember] 
        public Int64 cod_inffin { get; set; }
        [DataMember] 
        public string descripcion { get; set; }
        [DataMember] 
        public Int64 valor { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }


        

        //Totales
        [DataMember]
        public Int64 totalMateriaPrima { get; set; }

    }
}