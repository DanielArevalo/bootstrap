using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class TipoListaRecaudoDetalle
    {
        [DataMember]
        public Int64 codtipo_lista_detalle { get; set; }
        [DataMember]
        public int? idtipo_lista { get; set; }
        [DataMember]
        public int? tipo_producto { get; set; }
        [DataMember]
        public string cod_linea { get; set; }
    }
}