using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace Xpinn.Asesores.Entities
{
    public class Oficina
    {
        [DataMember]
        public Int64 IdOficina { get; set; } //Corresponde al consecutivo de la tabla usuarios que es asignado al asesor  al ser registrado en el sistema  
        [DataMember]
        public Int64 IdEmpresa { get; set; } //Corresponde al consecutivo de la tabla usuarios que es asignado al asesor  al ser registrado en el sistema  
        [DataMember]
        public string NombreOficina { get; set; }//Corresponde al primer nombre del asesor o ejecutivo 
        [DataMember]
        public DateTime FechaCreacion { get; set; }//Corresponde al primer nombre del asesor o ejecutivo 
        [DataMember]
        public Int64 idCiudad { get; set; }//Corresponde al primer nombre del asesor o ejecutivo
        [DataMember]
        public string Direccion { get; set; }//Corresponde al primer nombre del asesor o ejecutivo 
        [DataMember]
        public string Telefono { get; set; }//Corresponde al primer nombre del asesor o ejecutivo 
        [DataMember]
        public string Responsable { get; set; }//Corresponde al primer nombre del asesor o ejecutivo 
        [DataMember]
        public Int64 CentroCosto { get; set; }//Corresponde al primer nombre del asesor o ejecutivo 
        [DataMember]
        public Int64 Estado { get; set; }//Corresponde al primer nombre del asesor o ejecutivo
        [DataMember]
        public string nomciudad { get; set; }//Corresponde al primer nombre del asesor o ejecutivo 
        [DataMember]
        public string fecha_creacion { get; set; }
    }
}