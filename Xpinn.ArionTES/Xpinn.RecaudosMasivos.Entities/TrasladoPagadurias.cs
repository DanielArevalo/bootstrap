using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class TrasladoPagadurias
    {
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public string tipo_persona { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public Int64? cod_empresa { get; set;}
        [DataMember]
        public List<Productos_Persona> Lista_Producto = new List<Productos_Persona>();
    }
    public class Productos_Persona
    {
        [DataMember]
        public Int64 cod_tipo_producto { get; set; }
        [DataMember]
        public string nom_tipo_producto { get; set; }
        [DataMember]
        public Int64 num_producto { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public Int64 cod_linea { get; set; }
        [DataMember]
        public string nom_linea { get; set; }
        [DataMember]
        public string forma_pago { get; set; }
        [DataMember]
        public Int64? cod_empresa { get; set; }
        [DataMember]
        public Int64 porcentaje { get; set; }
    }

}
