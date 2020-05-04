using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Xpinn.Util
{
    [DataContract]
    public class GlobalEntity
    {
        [DataMember]
        public long Codigo { get; set; }
        [DataMember]
        public string PrimerNombre { get; set; }
        [DataMember]
        public string SegundoNombre { get; set; }
        [DataMember]
        public string PrimerApellido { get; set; }
        [DataMember]
        public string SegundoApellido { get; set; }
        public string NombreCompleto
        {
            get
            {
                return string.Format("{0} {1} {2} {3} ", PrimerNombre, SegundoNombre, PrimerApellido, SegundoApellido);
            }
        }

        public string NombreApellido
        {
            get
            {
                return string.Format("{0} {1} ", PrimerNombre, PrimerApellido);
            }
        }
        [DataMember]
        public string TipoIdentificacion { get; set; }
        [DataMember]
        public string Identificacion { get; set; }
        [DataMember]
        public string CodigoUsuario { get; set; }
        [DataMember]
        public string Estado { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Filtro { get; set; }
        [DataMember]
        public byte[] Imagen { get; set; }
        [DataMember]
        public TipoListaHelpDesk TipoLista { get; set; }
    }
}
