using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad VentasSemanales
    /// </summary>
    [DataContract]
    [Serializable]
    public class VentasSemanales
    {
        [DataMember] 
        public Int64 cod_ventas { get; set; }
        [DataMember] 
        public string tipoventas { get; set; }
        [DataMember]
        public string tipoventastxt { get; set; }
        [DataMember] 
        public Int64 valor { get; set; }
        [DataMember] 
        public Int64 lunes { get; set; }
        [DataMember] 
        public Int64 martes { get; set; }
        [DataMember] 
        public Int64 miercoles { get; set; }
        [DataMember] 
        public Int64 jueves { get; set; }
        [DataMember] 
        public Int64 viernes { get; set; }
        [DataMember] 
        public Int64 sabados { get; set; }
        [DataMember] 
        public Int64 domingo { get; set; }
        [DataMember] 
        public Int64 total { get; set; }
        [DataMember]
        public Int64 codPersona { get; set; }


        // lISTAS DESPLEGABLES

        [DataMember]
        public Int64 ListaId { get; set; }
        [DataMember]
        public string ListaDescripcion { get; set; }

        //Negocio
        [DataMember]
        public Int64 totalSemanal { get; set; }
        [DataMember]
        public Int64 ventasMes { get; set; }
        [DataMember]
        public Int64 porContado { get; set; }
        [DataMember]
        public Int64 venContado { get; set; }
        [DataMember]
        public Int64 venCredito { get; set; }
        
        // Reporte
        public String tipoventa { get; set; }
        [DataMember]
        public String lunesrepo { get; set; }
        [DataMember]
        public String martesrepo { get; set; }
        [DataMember]
        public String miercolesrepo { get; set; }
        [DataMember]
        public String juevesrepo { get; set; }
        [DataMember]
        public String viernesrepo { get; set; }
        [DataMember]
        public String sabadorepo { get; set; }
        [DataMember]
        public String domingorepo { get; set; }
        [DataMember]
        public String identificacion { get; set; }
      

    }
}