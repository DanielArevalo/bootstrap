using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Servicios.Entities
{
    [DataContract]
    [Serializable]
    public class LineaServicios
    {
        [DataMember]

        public int codigo_oficina { get; set; }
        [DataMember]

        public int beneficiarios { get; set; }
        [DataMember]
        public string cod_linea_servicio { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public int tipo_servicio { get; set; }
        [DataMember]
        public string identificacion_proveedor { get; set; }
        [DataMember]
        public string nombre_proveedor { get; set; }
        [DataMember]
        public int codperiodo_renovacion { get; set; }
        [DataMember]
        public int codperiodo_pago { get; set; }
        [DataMember]
        public int codperiocidad { get; set; }
        [DataMember]
        public int? numero_beneficiarios { get; set; }
        [DataMember]
        public int cobra_interes { get; set; }
        [DataMember]
        public decimal? tasa_interes { get; set; }
        [DataMember]
        public int tipo_tasa { get; set; }
        [DataMember]
        public DateTime fecha_pago_proveedor { get; set; }
        [DataMember]
        public int requierebeneficiarios { get; set; }
        [DataMember]
        public int no_requiere_aprobacion { get; set; }

        //AGREGADO
        [DataMember]
        public string nomtiposervicio { get; set; }
        [DataMember]
        public string periodorenov { get; set; }
        [DataMember]
        public string periodopago { get; set; }
        [DataMember]
        public string cobrainteres { get; set; }
        [DataMember]
        public string nomtipotasa { get; set; }

        [DataMember]
        public DateTime? fecha { get; set; }
        [DataMember]
        public List<planservicios> lstPlan { get; set; }
        [DataMember]
        public int? numero_servicios { get; set; }
        [DataMember]
        public string maximo_plazo { get; set; }
        [DataMember]
        public string maximo_valor { get; set; }
        [DataMember]
        public int tipo_cuota { get; set; }
        [DataMember]
        public int orden { get; set; }
        [DataMember]
        public int ocultar_informacion { get; set; }
        [DataMember]
        public Int64? cod_proveedor { get; set; }        

        //Se agrega para parametrizar la linea de servicio con la destinacion
        [DataMember]
        public int cod_destino { get; set; }
        [DataMember]
        public Int64 cod_lineacred { get; set; }
        [DataMember]
        public int seleccionar { get; set; }
        [DataMember]
        public List<LineaServicios> lstdestinacion { get; set; }

        [DataMember]
        public int maneja_causacion { get; set; }
        [DataMember]
        public int maneja_retirados { get; set; }
        [DataMember]
        public int no_generar_vacaciones { get; set; }
        [DataMember]
        public long? servicio_telefonia { get; set; }
        //Agregado para oficina virtual
        [DataMember]
        public byte[] foto { get; set; }
        [DataMember]
        public string enlace { get; set; }
        [DataMember]
        public byte[] banner { get; set; }
        [DataMember]
        public Int32? oficinaVirtual { get; set; }


    }

    [DataContract]
    [Serializable]
    public class planservicios
    {
        [DataMember]
        public string cod_plan_servicio { get; set; }
        [DataMember]
        public string cod_linea_servicio { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public int? numero_usuarios { get; set; }
        [DataMember]
        public int? edad_minima { get; set; }
        [DataMember]
        public int? edad_maxima { get; set; }
        [DataMember]
        public int? codgrupo_familiar { get; set; }
        [DataMember]
        public decimal? valor { get; set; }        
    }



}
