using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Xpinn.Asesores.Entities.Common;

namespace Xpinn.Asesores.Entities
{
    public class Negocio
    {
        public Negocio()
        {
            Persona = new Persona();
            Actividad = new Xpinn.Asesores.Entities.Common.Actividad();
        }

        [DataMember]
        public Int64 IdNegocio { get; set; }
        [DataMember]
        public Persona Persona { get; set; }
        [DataMember]
        public string Direccion { get; set; }
        [DataMember]
        public string Telefono { get; set; }
        [DataMember]
        public string Localidad { get; set; }
        [DataMember]
        public string NombreNegocio { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public Int64 Antiguedad { get; set; }
        [DataMember]
        public Int64 Propia { get; set; }
        [DataMember]
        public string Arrendador { get; set; }
        [DataMember]
        public string TelefonoArrendador { get; set; }
        [DataMember]
        public Xpinn.Asesores.Entities.Common.Actividad Actividad { get; set; }
        [DataMember]
        public Int64 Experencia { get; set; }
        [DataMember]
        public Int64 EmpleadosPermanentes { get; set; }
        [DataMember]
        public Int64 EmpleadosTemporales { get; set; }
        [DataMember]
        public DateTime FechaCreacion { get; set; }
        [DataMember]
        public string UsuarioCreacion { get; set; }
        [DataMember]
        public DateTime FechaUltMod { get; set; }
        [DataMember]
        public string UsuarioUltMod { get; set; }
    }
}