using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class CreditoSolicitado
    {
        [DataMember]
        public Int64 CodigoCliente { get; set; }
        [DataMember]
        public string Identificacion { get; set; }
        [DataMember]
        public string Nombres { get; set; }
        [DataMember]
        public string cod_nomina { get; set; }
        [DataMember]
        public Int32 Edad { get; set; }
        [DataMember]
        public Int32 Calificacion { get; set; }
        [DataMember]
        public Int64 NumeroCredito { get; set; }
        [DataMember]
        public string cod_linea_credito { get; set; }
        [DataMember]
        public string LineaCredito { get; set; }
        [DataMember]
        public decimal Monto { get; set; }
        [DataMember]
        public decimal MontoAprobado { get; set; }
        [DataMember]
        public decimal saldo_capital { get; set; }
        [DataMember]
        public string Asesor { get; set; }
        [DataMember]
        public string Plazo { get; set; }
        [DataMember]
        public Int64 Disponible { get; set; }
        [DataMember]
        public decimal Cuota { get; set; }
        [DataMember]
        public string Concepto { get; set; }
        [DataMember]
        public string Observaciones { get; set; }
        [DataMember]
        public string ObservacionesAprobacion { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public Int64 Cod_Periodicidad { get; set; }
        [DataMember]
        public string Periodicidad { get; set; }
        [DataMember]
        public decimal tasa { get; set; }
        [DataMember]
        public string tipotasa { get; set; }
        [DataMember]
        public string cod_tipotasa { get; set; }
        [DataMember]
        public int cod_clasifica { get; set; }
        [DataMember]
        public int reqpoliza { get; set; }
        [DataMember]
        public string error { get; set; }
        [DataMember]
        public List<Persona1> lstCodeudores { get; set; }
        [DataMember]
        public List<CuotasExtras> lstCuoExt { get; set; }
        [DataMember]
        public DateTime fecha_solicitud { get; set; }
        [DataMember]
        public String fechasolicitud { get; set; }
        [DataMember]
        public string forma_pago { get; set; }
        [DataMember]
        public Int32 cod_oficina { get; set; }
        [DataMember]
        public Int64 Cod_asesor { get; set; }
        [DataMember]
        public Int32 Tipo_liqu { get; set; }
        [DataMember]
        public Int32 cod_Periodicidad { get; set; }
        [DataMember]
        public string observaciones { get; set; }
        [DataMember]
        public Int64 numsolicitud { get; set; }
        [DataMember]
        public Int64 Cod_deudor { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public DateTime? fecha_primer_pago { get; set; }
        [DataMember]
        public Int64 cod_empresa { get; set; }
        [DataMember]
        public decimal? comision { get; set; }
        [DataMember]
        public decimal? aporte { get; set; }
        [DataMember]
        public decimal? seguro { get; set; }
        [DataMember]
        public int? condicion_especial { get; set; }
        [DataMember]
        public string Nom_linea_credito { get; set; }
        [DataMember]
        public DateTime fecha_proximo_pago { get; set; }
        /// <summary>
        /// Agregado para la oficina
        /// </summary>
        [DataMember]
        public string oficina { get; set; }
        /// <summary>
        /// Agregado para credito rotativo
        /// </summary>
        [DataMember]
        public string descripcion { get; set; }
        /// <summary>
        /// Agregado para Control Creditos
        [DataMember]
        public Int64 Codigoproceso { get; set; }

        // Agregado para los atributos descontados del crédito
        [DataMember]
        public List<DescuentosCredito> lstDescuentosCredito { get; set; }
        // Agregado para los atributos financiados del crédito
        [DataMember]
        public List<AtributosCredito> lstAtributosCredito { get; set; }
        // Agregado para documentos anexos
        [DataMember]
        public List<Imagenes> lstDocumetnos { get; set; }

        [DataMember]
        public string cod_Destino { get; set; }
        [DataMember]
        public string Obs_Concepto { get; set; }
        [DataMember]
        public Int64 aprobar_avances { get; set; }
        [DataMember]
        public Int64 tipo_credito { get; set; }
        [DataMember]
        public string NomZona { get; set; }

        //Atributos de Credito Tasa
        [DataMember]
        public Int64 TipoTasa { get; set; }
        [DataMember]
        public string calculo_atr { get; set; }
        [DataMember]
        public string TipoHistorico { get; set; }
        [DataMember]
        public string Desviacion { get; set; }
        [DataMember]
        public Int64 CodAtr { get; set; }
        [DataMember]
        public int Operacion { get; set; }

    }
}
