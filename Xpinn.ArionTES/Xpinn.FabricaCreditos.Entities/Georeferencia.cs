using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad Georeferencia
    /// </summary>
    [DataContract]
    [Serializable]
    public class Georeferencia
    {
        [DataMember] 
        public Int64 codgeoreferencia { get; set; }
        [DataMember] 
        public Int64 cod_persona { get; set; }
        [DataMember] 
        public string latitud { get; set; }
        [DataMember] 
        public string longitud { get; set; }
        [DataMember] 
        public string observaciones { get; set; }
        [DataMember] 
        public DateTime fechacreacion { get; set; }
        [DataMember] 
        public string usuariocreacion { get; set; }
        [DataMember] 
        public DateTime fecultmod { get; set; }
        [DataMember] 
        public string usuultmod { get; set; }
        [DataMember] 
        public string NOMBRE_REFERENCIAS { get; set; }
        [DataMember] 
        public string TIEMPO_NEGOCIO { get; set; }
        [DataMember] 
        public string PROPIETARIO_SI_NO { get; set; }
        [DataMember] 
        public string CONCEPTO { get; set; }
        ////////////////////////
        [DataMember]
        public Int64 cod_referencia { get; set; }
        
        [DataMember]
        public int cod_Clasificacion { get; set; }
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string primer_apellido { get; set; }
        [DataMember]
        public string segundo_apellido { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string cod_nomina { get; set; }
        [DataMember]
        public string oficina { get; set; }
        [DataMember]
        public string linea_credito { get; set; }
        [DataMember]
        public Int64 telefono { get; set; }
        [DataMember]
        public string detalle { get; set; }
        [DataMember]
        public string resultado { get; set; }
        [DataMember]
        public Int64 codigo_verificador { get; set; }
        [DataMember]
        public string nombre_verificador { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string periodicidad { get; set; }
        [DataMember]
        public string forma_pago { get; set; }
        [DataMember]
        public string fecha_solicitud { get; set; }
        [DataMember]
        public Int64 monto { get; set; }
        [DataMember]
        public Int64 plazo { get; set; }
        [DataMember]
        public Int64 cuota { get; set; }
        [DataMember]
        public string direccion { get; set; }


     
    
    }
}