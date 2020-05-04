using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Riesgo.Entities
{
    [DataContract]
    [Serializable]
    public class HistoricoSegmentacion
    {
        [DataMember]
        public int consecutivo { get; set; }
        [DataMember]
        public Int64 codigopersona { get; set; }
        [DataMember]
        public DateTime fechacierre { get; set; }
        [DataMember]
        public int usuariocierre { get; set; }
        [DataMember]
        public int? segmentoactual { get; set; }
        [DataMember]
        public int? segmentoanterior { get; set; }
        [DataMember]
        public string analisisoficialcumplimiento { get; set; }
        [DataMember]
        public DateTime? fechaanalisis { get; set; }
        [DataMember]
        public int? usuarioanalisis { get; set; }
        [DataMember]
        public long? puntajecalificacion { get; set; }
        [DataMember]
        public long? endeudamiento { get; set; }
        [DataMember]
        public long? ingresosmensuales { get; set; }
        [DataMember]
        public long? edad { get; set; }
        [DataMember]
        public long? personasacargo { get; set; }
        [DataMember]
        public long? tipovivienda { get; set; }
        [DataMember]
        public long? estrato { get; set; }
        [DataMember]
        public long? nivelacademico { get; set; }
        [DataMember]
        public long? sexo { get; set; }
        [DataMember]
        public long? montodelcreditosmlv { get; set; }
        [DataMember]
        public long? ciudadresidencia { get; set; }
        [DataMember]
        public long? actividadeconomica { get; set; }
        [DataMember]
        public long? saldopromedioahorros { get; set; }
        [DataMember]
        public long? saldopromedioaportes { get; set; }
        [DataMember]
        public long? saldocreditosactivos { get; set; }
        [DataMember]
        public long? numerocreditosactivos { get; set; }
        [DataMember]
        public long? operacionesproductosalmes { get; set; }
        [DataMember]
        public long? montooperacioneslmes { get; set; }
        [DataMember]
        public string identificacion_persona { get; set; }
        [DataMember]
        public string nombre_persona { get; set; }
        [DataMember]
        public long cod_oficina { get; set; }
        [DataMember]
        public string nombre_oficina { get; set; }
        [DataMember]
        public string nombre_segmentoactual { get; set; }
        [DataMember]
        public string nombre_segmentoanterior { get; set; }
        [DataMember]
        public string nombre_usuario_analisis { get; set; }
        [DataMember]
        public string calificacion { get; set; }
        [DataMember]
        public string segmentoaso { get; set; }
        [DataMember]
        public string segmentopro { get; set; }
        [DataMember]
        public string segmentocan { get; set; }
        [DataMember]
        public string segmentojur { get; set; }
        [DataMember]
        public string tipo_rol { get; set; }
        //Agregado para mostrar calificación anterior
        [DataMember]
        public string segmentoaso_anterior { get; set; }
        [DataMember]
        public string segmentopro_anterior { get; set; }
        [DataMember]
        public string segmentocan_anterior { get; set; }
        [DataMember]
        public string segmentojur_anterior { get; set; }
        [DataMember]
        public string calificacion_anterior { get; set; }
    }
}
