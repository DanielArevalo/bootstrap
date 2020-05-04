using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class CreditoPlan
    {
        public Int64 Numero_radicacion { get; set; }
        [DataMember]
        public Int64 Numero_Obligacion { get; set; }
        [DataMember]
        public string Identificacion { get; set; }
        [DataMember]
        public string Tipo_Identificacion { get; set; }
        [DataMember]
        public string Nombres { get; set; }
        [DataMember]
        public string LineaCredito { get; set; }
        [DataMember]
        public string Linea { get; set; }
        [DataMember]
        public Int64 MontoApr { get; set; }
        [DataMember]
        public Int64 Monto { get; set; }
        [DataMember]
        public Int64 Plazo { get; set; }
        [DataMember]
        public Int64 Cuota { get; set; }
        [DataMember]
        public Int64 Cod_persona { get; set; }
        [DataMember]
        public string Estado { get; set; }
        [DataMember]
        public string Periodicidad { get; set; }
        [DataMember]
        public string FormaPago { get; set; }
        [DataMember]
        public DateTime FechaInicio { get; set; }
        [DataMember]
        public DateTime FechaDesembolso { get; set; }
        [DataMember]

        public DateTime FechaAprobacion { get; set; }
        [DataMember]
        public DateTime? FechaPrimerPago { get; set; }
        [DataMember]
        public Int64 DiasAjuste { get; set; }
        [DataMember]
        public Double TasaInteres { get; set; }
        [DataMember]
        public Double TasaEfe { get; set; }
        [DataMember]
        public Double TasaNom { get; set; }
        [DataMember]
        public Int64 LeyMiPyme { get; set; }
        [DataMember]
        public string Moneda { get; set; }
        [DataMember]
        public string numero_cuotas { get; set; }
        [DataMember]
        public string ciudad { get; set; }
        [DataMember]
        public string pagare { get; set; }
        [DataMember]
        public Int64 cod_clasifica { get; set; }
        [DataMember]
        public decimal tir { get; set; }         
        [DataMember]
        public Int64 NumeroSolicitud { get; set; }
        [DataMember]
        public Int64 Numero_Radicacion { get; set; }
        [DataMember]
        public DateTime FechaSolicitud { get; set; }
        [DataMember]
        public string Direccion { get; set; }
        [DataMember]
        public string Telefono { get; set; }
        [DataMember]
        public string Oficina { get; set; }
        [DataMember]
        public string Ejecutivo { get; set; }
        [DataMember]
        public string DescripcionIdentificacion { get; set; }
       
        //  PARA IMPRESION DE REPORTE DATOS PERSONA
        [DataMember]
        public string Medio { get; set; }        
        [DataMember]
        public string EstadoCivil { get; set; }
        [DataMember]
        public string tipo_propiedad { get; set; }
        [DataMember]
        public string arrendador{ get; set; }
        [DataMember]
        public string antiguedad{ get; set; }
        [DataMember]
        public string telefonoarren { get; set; }
        [DataMember]
        public string valorarriendo { get; set; }
        [DataMember]
        public string concepto { get; set; }
        [DataMember]
        public string garantiareal { get; set; }
        [DataMember]
        public string garantiacom { get; set; }
        [DataMember]
        public string poliza { get; set; }
        [DataMember]
        public string Observaciones { get; set; }

        /// <summary>
        /// Añadido para la empresa en planpagos 
        /// </summary>
        [DataMember]
        public string empresa { get; set; }
        /// <summary>
        /// Añadido para impresion de ordenes 
        /// </summary>
        [DataMember]
        public string oficina { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public decimal cuotas_pagadas { get; set; }
        [DataMember]
        public decimal cuotas_pendientes { get; set; }
        [DataMember]
        public long tipo_linea { get; set; }
        [DataMember]
        public string nom_estado { get; set; }
        [DataMember]
        public int Reestructurado { get; set; }
    }
}
