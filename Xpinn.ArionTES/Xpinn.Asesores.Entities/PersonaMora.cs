using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Xpinn.Asesores.Entities.Common;

namespace Xpinn.Asesores.Entities
{
    [DataContract]
    [Serializable]
    public class PersonaMora
    {

     

        #region TablaPersona
        [DataMember]
        public Int64 IdPersona { get; set; }
       
        [DataMember]
        public string NumeroDocumento { get; set; }
        [DataMember]
      
        
        public string Nombres { get; set; }
        [DataMember]
        
   
        public string Oficina { get; set; }
        
        [DataMember]
        public string CodigoNomina { get; set; }
        #endregion
        #region  Productos
        [DataMember]
        public int  CantCreditos { get; set; }


        [DataMember]
        public int CantAportes { get; set; }


        [DataMember]
        public int CantAfiliacion { get; set; }

        [DataMember]
        public int CantServicios { get; set; }


        public decimal MoraCreditos { get; set; }


        [DataMember]
        public decimal MoraAportes { get; set; }


        [DataMember]
        public decimal  MoraAfiliacion { get; set; }

        [DataMember]
        public decimal MoraServicios { get; set; }

        [DataMember]
        public int Productos { get; set; }




        #endregion





    }
}