using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Auxilios.Entities
{
    [DataContract]
    [Serializable]
    public class SolicitudAuxilio
    {

        [DataMember]
        public int numero_auxilio { get; set; }
        [DataMember]
        public DateTime fecha_solicitud { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public string cod_linea_auxilio { get; set; }
        [DataMember]
        public decimal valor_solicitado { get; set; }
        [DataMember]
        public DateTime fecha_aprobacion { get; set; }
        [DataMember]
        public decimal valor_aprobado { get; set; }
        [DataMember]
        public DateTime fecha_desembolso { get; set; }
        [DataMember]
        public string detalle { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string nomestado { get; set; }
        [DataMember]
        public int cod_usuario { get; set; }

        //DATOS LINEA AUXILIO
       
        [DataMember]
        public string descripcion { get; set; }        
        [DataMember]
        public decimal monto_maximo { get; set; }       
        [DataMember]
        public int cupos { get; set; }
        [DataMember]
        public int educativo { get; set; }
        [DataMember]
        public decimal porc_matricula { get; set; }
        [DataMember]
        public Int64? numero_radicacion { get; set; }
        [DataMember]
        public int orden_servicio { get; set; }

        //AGREGADO
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string cod_nomina{ get; set; }
        [DataMember]
        public string observacion { get; set; } 
        [DataMember]
        public List<DetalleSolicitudAuxilio> lstDetalle { get; set; }
        [DataMember]
        public List<Requisitos> lstValidacion { get; set; }
        [DataMember]
        public string oficina { get; set; }
        [DataMember]
        public int permite_retirados { get; set; }
    }


    [DataContract]
    [Serializable]
    public class DetalleSolicitudAuxilio
    {
        [DataMember]
        public int codbeneficiarioaux { get; set; }
        [DataMember]
        public int numero_auxilio { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public int? cod_parentesco { get; set; }
        [DataMember]
        public decimal? porcentaje_beneficiario { get; set; }
    }

    [DataContract]
    [Serializable]
    public class Requisitos
    {        
        [DataMember]
        public string cod_linea_auxilio { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        

        [DataMember]
        public int codrequisitoauxilio { get; set; }
        [DataMember]
        public int numero_auxilio { get; set; }
        [DataMember]
        public Int64 codrequisitoaux { get; set; }
        [DataMember]
        public int aceptado { get; set; }
    }


}
