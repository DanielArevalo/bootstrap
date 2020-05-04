using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class EmpresaRecaudo
    {
        [DataMember]
        public Int64 cod_empresa { get; set; }
        [DataMember]
        public string nom_empresa { get; set; }
        [DataMember]
        public int estado { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string responsable { get; set; }
        [DataMember]
        public int tipo_novedad { get; set; }
        [DataMember]
        public string nomtipo_novedad { get; set; }
        [DataMember]
        public int? dias_novedad { get; set; }
        [DataMember]
        public DateTime? fecha_convenio { get; set; }
        [DataMember]
        public int? tipo_recaudo { get; set; }
        [DataMember]
        public List<EMPRESARECAUDO_CONCEPTO> lstConcepto { get; set; }
        [DataMember]
        public List<EmpresaRecaudo_Programacion> lstProgramacion { get; set; }
        [DataMember]
        public List<EmpresaEstructuraCarga> lstEstructura { get; set; }
        [DataMember]
        public List<EmpresaExcluyente> lstExcluyente { get; set; }
        [DataMember]
        public int? aplica_novedades { get; set; }
        [DataMember]
        public bool deshabilitar_desc_inhabiles { get; set; }
        [DataMember]
        public int? aplica_refinanciados { get; set; }
        [DataMember]
        public int? mayores_valores { get; set; }
        [DataMember]
        public int? forma_cobro { get; set; }
        [DataMember]
        public int aplicar_poroficina { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public int? numero_planilla { get; set; }
        [DataMember]
        public string codigo_recaudo_estructura { get; set; }
        [DataMember]
        public DateTime periodo_novedad { get; set; }
        [DataMember]
        public int maneja_atributos { get; set; }
        [DataMember]
        public int descuento_retiro { get; set; }
        [DataMember]
        public int mantener_condicion_inicial { get; set; }
        [DataMember]
        public int aplica_mora { get; set; }
        [DataMember]
        public int aporte_vacaciones { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public Int64 identificacion { get; set; }
        [DataMember]
        public int debito_automatico { get; set; }
        [DataMember]
        public int? tipo_lista { get; set; }

        [DataMember]
        public int debito_automatico_sem { get; set; }

        [DataMember]
        public bool descuentos_productos_inact { get; set; }


    }


    [DataContract]
    [Serializable]
    public class EmpresaRecaudo_Programacion
    {
        [DataMember]
        public Int64 idprogramacion { get; set; }
        [DataMember]
        public Int64 cod_empresa { get; set; }
        [DataMember]
        public int? tipo_lista { get; set; }
        [DataMember]
        public int? cod_periodicidad { get; set; }
        [DataMember]
        public DateTime? fecha_inicio { get; set; }
        [DataMember]
        public int? principal { get; set; }
    }


    [DataContract]
    [Serializable]
    public class EMPRESARECAUDO_CONCEPTO
    {
        [DataMember]
        public Int64? idempconcepto { get; set; }
        [DataMember]
        public Int64 cod_empresa { get; set; }
        [DataMember]
        public int? tipo_producto { get; set; }
        [DataMember]
        public string cod_linea { get; set; }
        [DataMember]
        public string cod_concepto { get; set; }
        [DataMember]
        public int? prioridad { get; set; }
    }
}



