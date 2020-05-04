using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class CuentasBancarias
    {
        [DataMember]
        public Int64 idcuentabancaria { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public int? tipo_cuenta { get; set; }
        [DataMember]
        public string numero_cuenta { get; set; }
        [DataMember]
        public int? cod_banco { get; set; }
        [DataMember]
        public string sucursal { get; set; }
        [DataMember]
        public DateTime? fecha_apertura { get; set; }
        [DataMember]
        public int? principal { get; set; }
        [DataMember]
        public string nombre_banco { get; set; }
    }

}