using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad ViabilidadFinanciera
    /// </summary>
    [DataContract]
    [Serializable]
    public class ViabilidadFinanciera
    {
        [DataMember]
        public Int64 cod_viabilidad { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public double datacredito { get; set; }
        [DataMember]
        public double disponible { get; set; }
        [DataMember]
        public string observaciones { get; set; }

        //PARAMETROS PARA RETORNAR LOS CALCULOS DE LA VIABILIDAD
        [DataMember]
        public Int64 numeroSolicitud { get; set; }
        [DataMember]
        public double prueba { get; set; }
        [DataMember]
        public double endeudamiento { get; set; }
        [DataMember]
        public double rotacioncuentas { get; set; }
        [DataMember]
        public double gastos { get; set; }
        [DataMember]
        public double rotacioncuentaspagar { get; set; }
        [DataMember]
        public double rotacioncapital { get; set; }
        [DataMember]
        public double rotacioninventarios { get; set; }
        [DataMember]
        public double puntoequilibrio { get; set; }
        [DataMember]
        public double ef { get; set; }
    }
}