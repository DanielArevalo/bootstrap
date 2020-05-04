using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.ActivosFijos.Entities
{
    [DataContract]
    [Serializable]
    public class ClaseActivo
    {
        [DataMember]
        public int clase { get; set; }
        [DataMember]
        public string nombre { get; set; }



        //AGREGADO

        [DataMember]
        public int cod_clasifica { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public int tipo_historico { get; set; }
    }
}
