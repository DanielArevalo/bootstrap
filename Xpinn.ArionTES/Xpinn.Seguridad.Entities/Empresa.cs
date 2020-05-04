using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Seguridad.Entities
{
    [DataContract]
    [Serializable]
    public class Empresa
    {
        [DataMember]
        public int cod_empresa { get; set; }
        [DataMember]
        public string nit { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string sigla { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public DateTime? fecha_constitución { get; set; }
        [DataMember]
        public int? ciudad { get; set; }
        [DataMember]
        public string e_mail { get; set; }
        [DataMember]
        public string representante_legal { get; set; }
        [DataMember]
        public string contador { get; set; }
        [DataMember]
        public string tarjeta_contador { get; set; }
        [DataMember]
        public decimal? tipo { get; set; }
        [DataMember]
        public string reporte_egreso { get; set; }
        [DataMember]
        public string revisor { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public string reporte_ingreso { get; set; }
        [DataMember]
        public string logoempresa { get; set; }
        [DataMember]
        public byte[] logoempresa_bytes { get; set; }
        [DataMember]
        public string cod_uiaf { get; set; }
        [DataMember]
        public string clavecorreo { get; set; }
        [DataMember]
        public int? maneja_sincronizacion { get; set; }
        //Agregado 
        [DataMember]
        public int? tipo_empresa { get; set; }

        [DataMember]
        public string desc_regimen { get; set; }
        [DataMember]
        public string resol_facturacion { get; set; }

    }
}