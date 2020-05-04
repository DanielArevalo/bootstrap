using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace Xpinn.Asesores.Entities
{
    [DataContract]
    [Serializable]
    public class Garantia
    {
        [DataMember]
        public Int64 IdGarantia { get; set; }
        [DataMember]
        public Int64 IdActivo { get; set; }
        [DataMember]
        public Int64 NumeroRadicacion { get; set; }
        [DataMember]
        public string Tipo { get; set; }
        [DataMember]
        public DateTime FechaGarantia { get; set; }
        [DataMember]
        public DateTime? FechaAvaluo { get; set; }
        [DataMember]
        public DateTime? FechaVencimiento { get; set; }
        [DataMember]
        public DateTime? FechaLiberacion { get; set; }
        [DataMember]
        public String Descripcion { get; set; }
        [DataMember]
        public Int64 Valor { get; set; }
        [DataMember]
        public String Estado { get; set; }
        [DataMember]
        public String estado_descripcion { get; set; }
        [DataMember]
        public String Ubicacion { get; set; }
        [DataMember]
        public String Encargado { get; set; }
        [DataMember]
        public String Aseguradora { get; set; }

        //se utiliza en Fabrica de Creditos para el Maestro de Garantias Filtro Consultas
        [DataMember]
        public decimal MontoInicial { get; set; }
        [DataMember]
        public decimal MontoFinal { get; set; }
        [DataMember]
        public Int64 PlazoInicial { get; set; }
        [DataMember]
        public Int64 PlazoFinal { get; set; }
        [DataMember]
        public Int64 cod_linea_cred { get; set; }
        [DataMember]
        public string nom_linea_cred { get; set; }
        [DataMember]
        public Int64 tipo_garantia { get; set; }
        [DataMember]
        public string nombre_tipo_garantia { get; set; }   
        [DataMember]
        public DateTime FechaGarantiaInicial { get; set; }
        [DataMember]
        public DateTime FechaGarantiaFinal { get; set; }
        [DataMember]
        public DateTime? Fecha_adquisicionactivo { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public String nom_persona { get; set; }
        [DataMember]
        public String identificacion { get; set; }
        [DataMember]
        public Int64 cod_ident { get; set; }
        [DataMember]
        public decimal valor_garantia { get; set; }
        [DataMember]
        public decimal valor_avaluo { get; set; }
        [DataMember]
        public decimal valor_seguro { get; set; }
        [DataMember]
        public String nombre_asesor { get; set; }
        [DataMember]
        public String oficina { get; set; }
        [DataMember]
        public Int64 monto { get; set; }
        [DataMember]
        public Int64 plazo { get; set; }
        [DataMember]
        public Int64 cuota { get; set; }
        [DataMember]
        public String periocidad { get; set; }
        [DataMember]
        public String descripcion_activo { get; set; }
        [DataMember]
        public Int64? valor_comercial { get; set; } 
        [DataMember]
        public String fechaproxpago { get; set; }
        [DataMember]
        public String diasmora { get; set; }
        [DataMember]
        public String reclamacion { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public Int64 cod_proceso { get; set; }
        [DataMember]
        public Int64 num_comp { get; set; }
        [DataMember]
        public Int64 tipo_comp { get; set; }
        [DataMember]
        public string message { get; set; }

         [DataMember]
        public Int16 origen { get; set; }
    }
}