using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data;

namespace Xpinn.Reporteador.Entities
{
    /// <summary>
    /// Entidad Reporte
    /// </summary>
    [DataContract]
    [Serializable]
    public class Cosechas
    {
        #region Encabezado
        /// <summary>
        /// Propiedad para identificar la fecha del comportamiento en tipo de datos string
        /// </summary>
        [DataMember]
        public string stg_comportamiento { get; set; }
        /// <summary>
        /// Propiedad para identificar la fecha del comportamiento
        /// </summary>
        [DataMember]
        public DateTime comportamiento { get; set; }
        /// <summary>
        /// Propiedad para identificar la fecha de desembolso
        /// </summary>
        [DataMember]
        public DateTime fecha { get; set; }
        /// <summary>
        /// Propiedad para identificar el valor de la cosecha
        /// </summary>
        [DataMember]
        public decimal valor { get; set; }
        /// <summary>
        /// Propiedad para identificar la cantidad de la cosecha
        /// </summary>
        [DataMember]
        public decimal cantidad { get; set; }

        /// <summary>
        /// Propiedad para la agrupasion de la cosecha
        /// </summary>
        [DataMember]
        public string GrupoLinea { get; set; }
        /// <summary>
        /// Propiedad para el porcentaje de cantidad de la cosecha
        /// </summary>
        [DataMember]
        public string Participacion_valor { get; set; }
        /// <summary>
        /// Propiedad para el porcentaje del valor de la cosecha 
        /// </summary>
        [DataMember]
        public string Participacion_Cantidad { get; set; }
        #endregion
    }

    public class Titulos
    {
        /// <summary>
        /// Propiedad para identificar el dia del titulo
        /// </summary>
        [DataMember]
        public Int64 dia { get; set; }
        /// <summary>
        ///  Propiedad para identificar el mes del titulo
        /// </summary>
        [DataMember]
        public string mes { get; set; }
        /// <summary>
        ///  Propiedad para identificar el año del titulo
        /// </summary>
        [DataMember]
        public Int64 anio { get; set; }
        /// <summary>
        ///  Propiedad para identificar el titulo completo
        /// </summary>
        [DataMember]
        public string titulos { get; set; }
    }
}
