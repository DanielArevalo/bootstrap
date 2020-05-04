using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class Afiliacion
    {
        [DataMember]
        public Int64 idafiliacion { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public DateTime fecha_afiliacion { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public DateTime fecha_retiro { get; set; }
        [DataMember]
        public decimal? valor { get; set; }
        [DataMember]
        public decimal? saldo { get; set; }
        [DataMember]
        public DateTime? fecha_primer_pago { get; set; }
        [DataMember]
        public DateTime? fecha_proximo_pago { get; set; }
        [DataMember]
        public int cuotas { get; set; }
        [DataMember]
        public int cod_periodicidad { get; set; }
        [DataMember]
        public int forma_pago { get; set; }
        [DataMember]
        public int? empresa_formapago { get; set; }
        [DataMember]
        public int asist_ultasamblea { get; set; }
        [DataMember]
        public int numero_asistencias { get; set; }

        //agregado para el caso de uso causación afiliación
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public object causacionafiliacion { get; set; }
        
        /// <summary>
        /// Agregado para la operación
        /// </summary>
        [DataMember]
        public Int64 cod_ope { get; set; }

        /// <summary>
        /// Agregado para rellenar la gridview de aporte
        /// </summary>
        [DataMember]
        public DateTime fecha_prox_pago { get; set; }
        [DataMember]
        public DateTime fecha_apertura { get; set; }
        [DataMember]
        public long numero_aporte { get; set; }
        [DataMember]
        public int cod_linea_aporte { get; set; }
        [DataMember]
        public string nom_linea_aporte { get; set; }
        [DataMember]
        public string cuota { get; set; }
        [DataMember]
        public int estados { get; set; }
        [DataMember]
        public long? cod_asesor { get; set; }
        [DataMember]
        public long? cod_asociado_especial { get; set; }
        [DataMember]
        public bool Es_PEPS { get; set; }
        [DataMember]
        public string cargo_PEPS { get; set; }
        [DataMember]
        public string institucion { get; set; }
        [DataMember]
        public DateTime? fecha_vinculacion_PEPS { get; set; }
        [DataMember]
        public DateTime? fecha_desvinculacion_PEPS { get; set; }
        [DataMember]
        public bool Administra_recursos_publicos { get; set; }
        [DataMember]
        public DateTime fecha_ultima_afiliacion { get; set; }
        [DataMember]
        public string nombre_empresa_pagaduria { get; set; }
        [DataMember]
        public int? Num_Reafiliaciones { get; set; }
        [DataMember]
        public DateTime fecha_cierre { get; set; }
        [DataMember]
        public string estadocierre { get; set; }
        [DataMember]
        public string consultaRIESGO { get; set; }
        [DataMember]
        public string entidad_externa { get; set; }
        [DataMember]
        public string cargo_directivo { get; set; }
        [DataMember]
        public DateTime? fechacreacion { get; set; }
        [DataMember]
        public bool Miembro_administracion { get; set; }
        [DataMember]
        public bool Miembro_control { get; set; }
        [DataMember]
        public DateTime FechaActualizacion { get; set; }
    }
}
