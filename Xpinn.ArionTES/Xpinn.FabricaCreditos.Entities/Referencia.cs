using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad Referencia
    /// </summary>
    [DataContract]
    [Serializable]
    public class Referencia
    {
        [DataMember]
        public Int64 cod_referencia { get; set; }
        [DataMember]
        public Int64 cod_Persona { get; set; }
        [DataMember]
        public int cod_Clasificacion { get; set; }
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string primer_apellido { get; set; }
        [DataMember]
        public string segundo_apellido { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string cod_nomina { get; set; }
        [DataMember]
        public string oficina { get; set; }
        [DataMember]
        public string linea_credito { get; set; }

        ///////////////////////////////////////////

        [DataMember]
        public string tipo_referencia { get; set; }
        [DataMember]
        public Int64 cod_vinculo { get; set; }
        [DataMember]
        public string vinculo { get; set; }
        [DataMember]
        public string referenciado { get; set; }
        [DataMember]
        public Int64 cedula_referenciado { get; set; }
        [DataMember]
        public string nombre_referenciado { get; set; }
        [DataMember]
        public string direccion_referenciado { get; set; }
        [DataMember]
        public string telefono_referenciado { get; set; }
        [DataMember]
        public string telefonos { get; set; }
        [DataMember]
        public string detalle { get; set; }
        [DataMember]
        public string resultado { get; set; }
        [DataMember]
        public Int64 codigo_verificador { get; set; }
        [DataMember]
        public string nombre_verificador { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string periodicidad { get; set; }
        [DataMember]
        public string forma_pago { get; set; }
        [DataMember]
        public string fecha_solicitud { get; set; }
        [DataMember]
        public Int64 monto { get; set; }
        [DataMember]
        public Int64 plazo { get; set; }
        [DataMember]
        public Int64 cuota { get; set; }
        [DataMember]
        public Int16 check_nombre { get; set; }
        [DataMember]
        public Int16 check_cedula { get; set; }
        [DataMember]
        public Int16 check_direccion { get; set; }
        [DataMember]
        public Int16 check_parentesco { get; set; }
        [DataMember]

        ///////////////////////////////////////////////

        public DateTime FECHANACIMIENTO { get; set; }
        [DataMember]
        public string ACTIVIDAD_PERSONA { get; set; }
        [DataMember]
        public string Celular { get; set; }
        [DataMember]
        public string TELOFICINA { get; set; }
        [DataMember]
        public string DIRECCION { get; set; }
        [DataMember]
        public string BARRIO { get; set; }
        [DataMember]
        public string ESTADO_CIVIL { get; set; }
        [DataMember]
        public string TIPO_VIVIENDA { get; set; }
        [DataMember]
        public string NOMBRE_ARRENDATARIO { get; set; }
        [DataMember]
        public Int64 TELEFONO_ARRENDATARIO { get; set; }
        [DataMember]
        public Int64 VALOR_ARRIENDO { get; set; }
        [DataMember]
        public Int64 PERSONAS_A_CARGO { get; set; }
        [DataMember]
        public string DIRECCION_NEGOCIO { get; set; }
        [DataMember]
        public string ANTIGUEDAD_NEGOCIO { get; set; }
        [DataMember]
        public string ACTIVIDAD_EMPRESA { get; set; }
        [DataMember]
        public string NOMBRE_EMPRESA { get; set; }
        [DataMember]
        public string NIT_EMPRESA { get; set; }
        [DataMember]
        public string CARGO_PERONSA { get; set; }
        [DataMember]
        public Int64 SALARIO_PERSONA { get; set; }
        [DataMember]
        public Int64 ANTIGUEDAD_EMPRESA { get; set; }
        [DataMember]
        public string DIRECCION_EMPRESA_PERSONA { get; set; }

        /////////////////////////////////////////////

        [DataMember]
        public string nombres_conyuge { get; set; }
        [DataMember]
        public Int64 identificacion_conyge { get; set; }
        [DataMember]
        public string ACTIVIDAD_CONYUGE { get; set; }
        [DataMember]
        public string telefono_CONYUGE { get; set; }
        [DataMember]
        public string DIRECCION_CONYUGE { get; set; }
        [DataMember]
        public string ACTIVIDAD_EMPRESA_CONYUGE { get; set; }
        [DataMember]
        public string NOMBRE_EMPRESA_CONYUGE { get; set; }
        [DataMember]
        public string NIT_EMPRESA_CONYUGE { get; set; }
        [DataMember]
        public string CARGO_CONYUGE { get; set; }
        [DataMember]
        public Int64 SALARIO_CONYUGE { get; set; }
        [DataMember]
        public Int64 ANTIGUEDAD_EMPRESA_CONYUGE { get; set; }
        [DataMember]
        public string DIRECCION_EMPRESA_CONYUGE { get; set; }
        [DataMember]
        public string control { get; set; }

        ///////////////////////////////////////////////
        [DataMember]
        public string nombrereferencia { get; set; }
        [DataMember]
        public string tiempo { get; set; }
        [DataMember]
        public string propietario { get; set; }
        [DataMember]
        public string concepto { get; set; }
        [DataMember]
        public string observaciones { get; set; }
        [DataMember]
        public Int64 numero_pregunta { get; set; }
        [DataMember]
        public Int64 numero_respuesta { get; set; }
        ////////////////////////////////////////////////////////
        [DataMember]
        public string correo { get; set; }
    }
}