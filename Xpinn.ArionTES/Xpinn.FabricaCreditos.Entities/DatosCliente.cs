using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities

{
    [DataContract]
    [Serializable]
    public class DatosCliente
    {
        [DataMember]
        public Int64 idc { get; set; }
        [DataMember]
        public String tipoPersona { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string apellidos { get; set; }
        [DataMember]
        public Int64 tipo { get; set; }
        [DataMember]
        public string tipostr { get; set; }
        [DataMember]
        public string numero { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string razon { get; set; }
        [DataMember]
        public string sigla { get; set; }
        [DataMember]
        public Int64 actividad { get; set; }
        [DataMember]
        public string actividadstr { get; set; }
        [DataMember]
        public string UsuarioCrea { get; set; }
        [DataMember]
        public DateTime FechaCrea { get; set; }
        [DataMember]
        public string UsuarioEdita { get; set; }
        [DataMember]
        public DateTime FechaEdita { get; set; }

        //Listas desplegables:
        [DataMember]
        public string ListaDescripcion { get; set; }
        [DataMember]
        public Int64 ListaId { get; set; }
        [DataMember]
        public string ListaIdStr { get; set; }

        // Detalle Cliente:
        [DataMember]
        public DateTime fechaExpedicion { get; set; }
        [DataMember]
        public Int64 lugarExpedicionInt { get; set; }
        [DataMember]
        public String lugarExpedicionStr { get; set; }
        [DataMember]
        public String sexo { get; set; }
        [DataMember]
        public String primerNombre { get; set; }
        [DataMember]
        public String segundoNombre { get; set; }
        [DataMember]
        public String primerApellido { get; set; }
        [DataMember]
        public String segundoApellido { get; set; }
        [DataMember]
        public DateTime fechaNacimiento { get; set; }
        [DataMember]
        public String lugarNacimientoStr { get; set; }
        [DataMember]
        public Int64 lugarNacimientoInt { get; set; }
        [DataMember]
        public Int64 estadoCivilInt { get; set; }
        [DataMember]
        public String estadoCivilStr { get; set; }
        [DataMember]
        public Int64 nivelEscolaridadInt { get; set; }
        [DataMember]
        public String nivelEscolaridadStr { get; set; }
        [DataMember]
        public String antiguedad { get; set; }
        [DataMember]
        public String tipoVivienda { get; set; }
        [DataMember]
        public String arrendatario { get; set; }
        [DataMember]
        public String telefonoArrendatario { get; set; }
               

    } 
        
    
}
