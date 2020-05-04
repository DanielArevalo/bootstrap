using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


namespace Xpinn.FabricaCreditos.Entities
{

    [DataContract]
    [Serializable]
    public class PlanesSeguros
    {
        [DataMember]
        public Int64 tipo_plan  { get; set; }
        public string descripcion {get;set;}
        public Int64 prima_individual  {get;set;}
        public Int64 prima_conyuge  {get;set;}
        public Int64 prima_accidentes_individual  {get;set;}
        public Int64 prima_accidentes_familiar { get; set; }
    }
}
