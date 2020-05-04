using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class TipoListaRecaudo
    {
        [DataMember]
        public int idtipo_lista { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public List<TipoListaRecaudoDetalle> lstDetalle { get; set; }
    }

}



