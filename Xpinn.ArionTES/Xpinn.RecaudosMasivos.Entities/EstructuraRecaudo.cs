using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace Xpinn.Tesoreria.Entities
{

    [DataContract]
    [Serializable]
    public class Estructura_Carga
    {
        [DataMember]
        public int? cod_estructura_carga { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public int tipo_archivo { get; set; }
        [DataMember]
        public int tipo_datos { get; set; }
        [DataMember]
        public string separador_campo { get; set; }
        [DataMember]
        public int? encabezado { get; set; }
        [DataMember]
        public int? final { get; set; }
        [DataMember]
        public string formato_fecha { get; set; }
        [DataMember]
        public string separador_decimal { get; set; }
        [DataMember]
        public string separador_miles { get; set; }
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public int? totalizar { get; set; }
        [DataMember]
        public List<Estructura_Carga_Detalle> lstDetalle { get; set; }
        [DataMember]
        public int? cod_estructura { get; set; }
    }

    [DataContract]
    [Serializable]
    public class Estructura_Carga_Detalle
    {
        [DataMember]
        public int? cod_estructura_detalle { get; set; }
        [DataMember]
        public int? cod_estructura_carga { get; set; }
        [DataMember]
        public int? codigo_campo { get; set; }
        [DataMember]
        public int? numero_columna { get; set; }
        [DataMember]
        public int? posicion_inicial { get; set; }
        [DataMember]
        public int? longitud { get; set; }
        [DataMember]
        public string justificacion { get; set; }
        [DataMember]
        public string justificador { get; set; }
        [DataMember]
        public string vr_campo_fijo { get; set; }
    }

}



