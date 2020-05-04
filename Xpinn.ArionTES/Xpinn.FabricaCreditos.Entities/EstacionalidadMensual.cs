using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad EstacionalidadMensual
    /// </summary>
    [DataContract]
    [Serializable]
    public class EstacionalidadMensual
    {
        [DataMember] 
        public Int64 cod_ventas { get; set; }
        [DataMember] 
        public string tipoventas { get; set; }
        [DataMember] 
        public Int64 valor { get; set; }
        [DataMember] 
        public Int64 enero { get; set; }
        [DataMember] 
        public Int64 febrero { get; set; }
        [DataMember] 
        public Int64 marzo { get; set; }
        [DataMember] 
        public Int64 abril { get; set; }
        [DataMember] 
        public Int64 mayo { get; set; }
        [DataMember] 
        public Int64 junio { get; set; }
        [DataMember] 
        public Int64 julio { get; set; }
        [DataMember] 
        public Int64 agosto { get; set; }
        [DataMember] 
        public Int64 septiembre { get; set; }
        [DataMember] 
        public Int64 octubre { get; set; }
        [DataMember] 
        public Int64 noviembre { get; set; }
        [DataMember] 
        public Int64 diciembre { get; set; }
        [DataMember] 
        public Int64 total { get; set; }
        [DataMember]
        public Int64 codpersona { get; set; }


        // lISTAS DESPLEGABLES

        [DataMember]
        public Int64 ListaId { get; set; }
        [DataMember]
        public string ListaDescripcion { get; set; }

        //Negocio
        [DataMember]
        public Int64 totalMensual { get; set; }
        [DataMember]
        public Double promedioMensual { get; set; }
        [DataMember]
        public Double ventasMes { get; set; }

        //rEPORTE


        public string enerorepo { get; set; }
        [DataMember]
        public string febrerorepo { get; set; }
        [DataMember]
        public string marzorepo { get; set; }
        [DataMember]
        public string abrilrepo { get; set; }
        [DataMember]
        public string mayorepo { get; set; }
        [DataMember]
        public string juniorepo { get; set; }
        [DataMember]
        public string juliorepo { get; set; }
        [DataMember]
        public string agostorepo { get; set; }
        [DataMember]
        public string septiembrerepo { get; set; }
        [DataMember]
        public string octubrerepo { get; set; }
        [DataMember]
        public string noviembrerepo { get; set; }
        [DataMember]
        public string diciembrerepo { get; set; }

        [DataMember]
        public string tipoventa { get; set; }
        [DataMember]
        public Int64 Totalmes { get; set; }
        [DataMember]
        public string identificacion { get; set; }

    }
}