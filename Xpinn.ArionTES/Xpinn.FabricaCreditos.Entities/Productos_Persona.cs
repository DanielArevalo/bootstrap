using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad Persona1
    /// </summary>
    [DataContract]
    [Serializable]  
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
        public Int64? cod_empresa { get; set; }
        [DataMember]
        public Int64 saldo { get; set; }
        [DataMember]
        public Int64? cod_oficina { get; set; }
    }
}