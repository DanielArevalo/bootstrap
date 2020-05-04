using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad ControlTiempos
    /// </summary>
    [DataContract]
    [Serializable]
    public class ControlTiempos
    {
        [DataMember] 
        public string numerocredito { get; set; }
        [DataMember] 
        public string identificacion { get; set; }
        [DataMember]
        public string cod_nomina { get; set; }
        [DataMember] 
        public string nombredeudor { get; set; }
        [DataMember] 
        public DateTime fechas { get; set; }
        [DataMember] 
        public string asesor { get; set; }
        [DataMember]
        public string aprobador { get; set; }
        [DataMember]
        public string nombreaprobador { get; set; }
        [DataMember] 
        public Int64 monto { get; set; }
        [DataMember] 
        public string estado { get; set; }
        [DataMember]
        public DateTime fechau { get; set; }
        [DataMember]
        public String fechaproceso { get; set; }
        [DataMember]
        public String fechaprocesoin { get; set; }
        [DataMember]
        public String fechaprocesofin { get; set; }
        [DataMember] 
        public string ultimoproceso { get; set; }
        [DataMember]
        public string proceso1 { get; set; }
        [DataMember]
        public string proceso2 { get; set; }
        [DataMember]
        public string observaciones1 { get; set; }
        [DataMember]
        public string observaciones2 { get; set; }
        [DataMember]
        public String tiempototal { get; set; }
        [DataMember]
        public String tiempoentrega { get; set; }
        [DataMember]
        public String tiempodatacredito { get; set; }
        [DataMember] 
        public string encargado { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public String nom_oficina { get; set; }
        [DataMember]
        public Int64 tiempoMayor { get; set; }
        [DataMember]
        public Int64 tiempoMenor { get; set; }
        [DataMember]
        public Int64 cod_proceso { get; set; }
        [DataMember]
        public string nom_proceso { get; set; }
        [DataMember]
        public string nom_tipo_proceso { get; set; }
        [DataMember]
        public string sig_proceso { get; set; }
        [DataMember]
        public string sig_proceso_tipo { get; set; }
        [DataMember]
        public string sig_proceso_nom { get; set; }
        [DataMember]
        public DateTime? fechadata { get; set; }
        [DataMember]
        public DateTime? fechaentrega { get; set; }
        [DataMember]
        public DateTime? fecha1{ get; set; }
        [DataMember]
        public DateTime? fecha2 { get; set; }

        //Agregado para proceso anterior
        [DataMember]
        public string nom_proceso_anterior { get; set; }
        [DataMember]
        public Int64 tipo_proceso_anterior { get; set; }


        //Agregado para aumentar datos informativos en lista
        [DataMember]
        public string nom_linea { get; set; }

        // LISTAS DESPLEGABLES
        [DataMember]
        public Int64 ListaId { get; set; }
        [DataMember]
        public String ListaIdStr { get; set; }
        [DataMember]
        public String ListaDescripcion { get; set; }

    }
}