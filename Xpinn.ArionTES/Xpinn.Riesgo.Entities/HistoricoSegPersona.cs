using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Riesgo.Entities

{

    [DataContract]
    [Serializable]

   public class HistoricoSegPersona
    {
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public String identificacion { get; set; }
        [DataMember]
        public String primer_nombre { get; set; }
        [DataMember]
        public String segundo_nombre { get; set; }
        [DataMember]
        public String primer_apellido { get; set; }
        [DataMember]
        public String Direccion { get; set; }
        [DataMember]
        public String Telefono { get; set; }
        [DataMember]
        public String Celular { get; set; }
        [DataMember]
        public String Email { get; set; }
        [DataMember]
        public Int64? Edad { get; set; }
        [DataMember]
        public Int64? Ingresosmensuales { get; set; }
        [DataMember]
        public Int64? Estrato { get; set; }
        [DataMember]
        public String Nom_ofi { get; set; }
        [DataMember]
        public String Dir_ofi { get; set; }
        [DataMember]
        public String Tel_ofi { get; set; }
        [DataMember]
        public String Perfil_riesgo { get; set; }
        [DataMember]
        public Int64? Valor_operacionMes { get; set; }
        [DataMember]
        public Int64? Numero_operacionMes { get; set; }
        [DataMember]
        public DateTime? FECHACIERRE { get; set; }
        [DataMember]
        public String Actividad_Eco { get; set; }
        [DataMember]
        public DateTime fecha_historico { get; set; }
        [DataMember]
        public Int64 captaciones { get; set; }
        [DataMember]
        public Int64 colocaciones { get; set; }
        /*nueva seccion*/
        [DataMember]
        public Int64 MontoCPD { get; set; }
        [DataMember]
        public Int64 MontoCPC { get; set; }
        [DataMember]
        public Int64 MontoCLD { get; set; }
        [DataMember]
        public Int64 MontoCLC { get; set; }
        [DataMember]
        public Int64 NumCPD { get; set; }
        [DataMember]
        public Int64 NumCPC { get; set; }
        [DataMember]
        public Int64 NumCLD { get; set; }
        [DataMember]
        public Int64 NumCLC { get; set; }
        [DataMember]
        public string segmentoActual { get; set; }
        [DataMember]
        public string analisisCumplimiento { get; set; }
        /*Nueva seccion*/
        [DataMember]
        public String segmento_perfil { get; set; }
        [DataMember]
        public DateTime fecha_segemento { get; set; }
        [DataMember]
        public int calificacion_segmento { get; set; }
        [DataMember]
        public string calificacion { get; set; }
        /*Nueva seccion*/
        [DataMember]
        public String segmento_pro { get; set; }
        /*Nueva seccion*/
        [DataMember]
        public String segmento_can { get; set; }
        /*Nueva seccion*/
        [DataMember]
        public String segmento_jur { get; set; }

    }
}
