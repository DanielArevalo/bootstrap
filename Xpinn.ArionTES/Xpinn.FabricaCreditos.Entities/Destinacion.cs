using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class Destinacion
    {
        [DataMember]
        public int cod_destino { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        //Agregado para oficina virtual
        [DataMember]
        public byte[] foto { get; set; }
        [DataMember]
        public string enlace { get; set; }
        [DataMember]
        public byte[] banner { get; set; }
        [DataMember]
        public Int32? oficinaVirtual { get; set; }
    }

    [DataContract]
    [Serializable]
    public class LineaCred_Destinacion
    {
        [DataMember]
        public int cod_linea_credito { get; set; }
        [DataMember]
        public int cod_destino { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        //Agregado para oficina virtual
        [DataMember]
        public byte[] foto { get; set; }
        [DataMember]
        public string enlace { get; set; }
        [DataMember]
        public byte[] banner { get; set; }
        [DataMember]
        public Int32? oficinaVirtual { get; set; }
    }


}