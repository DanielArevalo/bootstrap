using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class TipoProducto
    {
        [DataMember]
        public int cod_tipo_prod { get; set; }
        [DataMember]
        public string nombre { get; set; }    
        
        //Tipo Transacciones
        
        [DataMember]
        public Int64? tipo_tran { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
           
    
    }
}