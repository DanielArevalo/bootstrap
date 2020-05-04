using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class Analisis_Credito
    {
        [DataMember]
        public int idanalisis { get; set; }
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public decimal? capacidad_pago { get; set; }
        [DataMember]
        public string garantias_ofrecidas { get; set; }
        [DataMember]
        public string documentos_provistos { get; set; }
        [DataMember]
        public string analisis_docs { get; set; }
        [DataMember]
        public string analisis_doc_cod1 { get; set; }
        [DataMember]
        public string analisis_doc_cod2 { get; set; }
        [DataMember]
        public string analisis_doc_cod3 { get; set; }
        [DataMember]
        public string viabilidad { get; set; }
        [DataMember]
        public string justificacion { get; set; }
        [DataMember]
        public long? cod_usuario { get; set; }
        [DataMember]
        public DateTime fecha_analisis { get; set; }
        [DataMember]
        public int ReqAfiancol { get; set; }
    }

    public class AnalisisInfo
    {
        [DataMember]
        public long? NumeroRadicacion { get; set; }

        [DataMember]
        public long? CodPersona { get; set; }

        [DataMember]
        public int? CodPersonacou { get; set; }

        [DataMember]
        public decimal? Ingresos { get; set; }

        [DataMember]
        public decimal? OtrosIngresos { get; set; }

        [DataMember]
        public decimal? Arrendamientos { get; set; }

        [DataMember]
        public decimal? Honorarios { get; set; }

        [DataMember]
        public int? CobroJuridico { get; set; }

        [DataMember]
        public int? CIfinPrincipal { get; set; }

        [DataMember]
        public int? CifinCodor { get; set; }

        [DataMember]
        public decimal? Gastosfalimiares { get; set; }

        [DataMember]
        public decimal? OtrosDescuentos { get; set; }

        [DataMember]
        public decimal? DasTercero { get; set; }

        [DataMember]
        public int? DatoPersona { get; set; }

        [DataMember]
        public int? Aportes { get; set; }

        [DataMember]
        public int? CreditosVigentes { get; set; }

        [DataMember]
        public int? Servicios { get; set; }

        [DataMember]
        public int? ProteccionSalarial { get; set; }

        [DataMember]
        public string calif_criesgo { get; set; }

        [DataMember]
        public DateTime? fecha_consulta { get; set; }

    }
}