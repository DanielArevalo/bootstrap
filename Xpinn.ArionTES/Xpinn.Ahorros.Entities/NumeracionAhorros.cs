using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Ahorros.Entities
{
    [DataContract]
    [Serializable]
    public class NumeracionAhorros
    {
        [DataMember]
        public int idconsecutivo { get; set; }
        [DataMember]
        public int tipo_producto { get; set; }
        [DataMember]
        public string cod_linea_producto { get; set; }
        [DataMember]
        public int?  posicion { get; set; }
        [DataMember]
        public int? tipo_campo { get; set; }       
        [DataMember]
        public string  valor { get; set; }
        [DataMember]
        public int? longitud { get; set; }
        [DataMember]
        public string alinear { get; set; }  
         [DataMember]
        public string caracter_llenado { get; set; }
         [DataMember]
   
         public List<NumeracionAhorros> lstNumeracion { get; set; }
        
    }
}