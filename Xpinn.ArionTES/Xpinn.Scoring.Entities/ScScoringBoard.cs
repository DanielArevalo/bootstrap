using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Scoring.Entities
{
    /// <summary>
    /// Entidad ScScoringBoard
    /// </summary>
    [DataContract]
    [Serializable]
    public class ScScoringBoard
    {
        [DataMember]
        public Int64 idscore { get; set; }   //Parametro util para modificacion. Permite seleccionar que campos se desean actualizar en la tabla
        [DataMember]
        public Int64 cod_clasifica { get; set; }
        [DataMember]
        public String cod_linea_credito { get; set; }
        [DataMember]
        public Int64 aplica_a { get; set; }
        [DataMember]
        public decimal beta0 { get; set; }
        [DataMember]
        public decimal score_maximo { get; set; }
        [DataMember]
        public Int64 clase { get; set; }
        [DataMember]
        public Int64 modelo { get; set; }
        [DataMember]
        public DateTime fechacreacion { get; set; }
        [DataMember]
        public String usuariocreacion { get; set; }
        [DataMember]
        public DateTime fecultmod { get; set; }
        [DataMember]
        public String usuultmod { get; set; }
        [DataMember]
        public String descripcion { get; set; }
        [DataMember]
        public String nombre { get; set; }
        [DataMember]
        public String filtro { get; set; }
      

        // LISTAS DESPLEGABLES
        [DataMember]
        public Int64 ListaId { get; set; }
        [DataMember]
        public String ListaIdStr { get; set; }
        [DataMember]
        public String ListaDescripcion { get; set; }


    }
}