using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad DocumentosAnexos
    /// </summary>
    [DataContract]
    [Serializable]
    public class DocumentosAnexos
    {
        [DataMember] 
        public Int64 iddocumento { get; set; }
        [DataMember] 
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public Int64 numerosolicitud { get; set; }
        [DataMember] 
        public Int64 tipo_documento { get; set; }
        [DataMember] 
        public Int64 cod_asesor { get; set; }
        [DataMember]
        public byte[] imagen { get; set; }
        [DataMember] 
        public string descripcion { get; set; }
        [DataMember] 
        public DateTime? fechaanexo { get; set; }
        [DataMember]
        public Int64 estado { get; set; }
        [DataMember]
        public Boolean estado_doc { get; set; }
        [DataMember]
        public DateTime? fechaentrega { get; set; }
        [DataMember]
        public Int32 cod_linea_credio { get; set; }

        [DataMember]
        public string aplica_codeudor { get; set; }
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public string observaciones { get; set; }

        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public decimal monto_solicitado { get; set; }
        [DataMember]
        public int Nun_Cuoatas { get; set; }
        [DataMember]
        public string estado_cre { get; set; }
        [DataMember]
        public string nom_tipo_documento { get; set; }
        [DataMember]
        public DateTime fecha_solicitud { get; set; }
        [DataMember]
        public DateTime fec_estimada_entrga { get; set; }
        [DataMember]
        public string estados { get; set; }
        [DataMember]
        public Int32 espdf { get; set; }
        [DataMember]
        public Int32 tipo_producto { get; set; }
        [DataMember]
        public string extension { get; set; }
    }
}