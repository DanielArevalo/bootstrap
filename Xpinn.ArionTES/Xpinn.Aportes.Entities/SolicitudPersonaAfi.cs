using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class SolicitudPersonaAfi
    {
        [DataMember]
        public Int64 id_persona { get; set; }
        [DataMember]
        public DateTime fecha_creacion { get; set; }
        [DataMember]
        public string primer_nombre { get; set; }
        [DataMember]
        public string segundo_nombre { get; set; }
        [DataMember]
        public string primer_apellido { get; set; }
        [DataMember]
        public string segundo_apellido { get; set; }
        [DataMember]
        public string sexo { get; set; }
        [DataMember]
        public Int64 tipo_identificacion { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public DateTime fecha_expedicion { get; set; }
        [DataMember]
        public Int64? ciudad_expedicion { get; set; }
        [DataMember]
        public DateTime? fecha_nacimiento { get; set; }
        [DataMember]
        public Int64? ciudad_nacimiento { get; set; }
        [DataMember]
        public Int64? codestadocivil { get; set; }
        [DataMember]
        public int? cabeza_familia { get; set; }
        [DataMember]
        public int? personas_cargo { get; set; }
        [DataMember]
        public int? tiene_hijos { get; set; }
        [DataMember]
        public int? codescolaridad { get; set; }
        [DataMember]
        public string profesion { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public int? estrato { get; set; }
        [DataMember]
        public Int64? barrio { get; set; }
        [DataMember]
        public Int64? ciudad { get; set; }
        [DataMember]
        public Int64? departamento { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string celular { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string estado_empresa { get; set; }
        [DataMember]
        public string empresa { get; set; }
        [DataMember]
        public string nit { get; set; }
        [DataMember]
        public string direccion_empresa { get; set; }
        [DataMember]
        public string telefono_empresa { get; set; }
        [DataMember]
        public Int64? ciudad_empresa { get; set; }
        [DataMember]
        public Int64? departamento_empresa { get; set; }
        [DataMember]
        public Int64? codtipocontrato { get; set; }
        [DataMember]
        public DateTime? fecha_inicio { get; set; }
        [DataMember]
        public string cod_periodicidad_pago { get; set; }
        [DataMember]
        public decimal? salario { get; set; }
        [DataMember]
        public decimal? otros_ingresos { get; set; }
        [DataMember]
        public decimal? deducciones { get; set; }
        [DataMember]
        public DateTime? fecha_ult_liquidacion { get; set; }
        [DataMember]
        public string empresa_contacto { get; set; }
        [DataMember]
        public string telefono_contacto { get; set; }
        [DataMember]
        public string cargo_contacto { get; set; }
        [DataMember]
        public string email_contacto { get; set; }
        [DataMember]
        public string empresa_anterior_contacto { get; set; }
        [DataMember]
        public string cargo_anterior { get; set; }
        [DataMember]
        public string telefono_anterior { get; set; }
        [DataMember]
        public string primer_nombre_cony { get; set; }
        [DataMember]
        public string segundo_nombre_cony { get; set; }
        [DataMember]
        public string primer_apellido_cony { get; set; }
        [DataMember]
        public string segundo_apellido_cony { get; set; }
        [DataMember]
        public decimal? ingresos_cony { get; set; }
        [DataMember]
        public int? codparentesco { get; set; }
        [DataMember]
        public Int64? tipo_identificacion_cony { get; set; }
        [DataMember]
        public string identificacion_cony { get; set; }
        [DataMember]
        public string direccion_cony { get; set; }
        [DataMember]
        public string telefono_cony { get; set; }
        [DataMember]
        public string email_cony { get; set; }
        [DataMember]
        public string estado_cony { get; set; }
        [DataMember]
        public string empresa_cony { get; set; }
        [DataMember]
        public string cargo_cony { get; set; }
        [DataMember]
        public string telefono_lab_cony { get; set; }
        [DataMember]
        public string direccion_lab_cony { get; set; }
        [DataMember]
        public string primer_nombre_referencia { get; set; }
        [DataMember]
        public string segundo_nombre_referencia { get; set; }
        [DataMember]
        public string primer_apellido_referencia { get; set; }
        [DataMember]
        public string segundo_apellido_referencia { get; set; }
        [DataMember]
        public string relacion_referencia { get; set; }
        [DataMember]
        public string direccion_referencia { get; set; }
        [DataMember]
        public string telefono_referencia { get; set; }
        [DataMember]
        public string celular_referencia { get; set; }
        [DataMember]
        public string email_referencia { get; set; }
        [DataMember]
        public string nombres_primer_benef { get; set; }
        [DataMember]
        public string apellidos_primer_benef { get; set; }
        [DataMember]
        public string identificacion_primer_benef { get; set; }
        [DataMember]
        public Int64? codparentesco_primer_benef { get; set; }
        [DataMember]
        public decimal? porcentaje_primer_benef { get; set; }
        [DataMember]
        public string nombres_segun_benef { get; set; }
        [DataMember]
        public string apellidos_segun_benef { get; set; }
        [DataMember]
        public string identificacion_segun_benef { get; set; }
        [DataMember]
        public Int64? codparentesco_segun_benef { get; set; }
        [DataMember]
        public decimal? porcentaje_segun_benef { get; set; }
        [DataMember]
        public int? estado { get; set; }

        //Adicionado
        [DataMember]
        public string nom_tipo_identificacion { get; set; }
        [DataMember]
        public string nom_ciudad { get; set; }
        [DataMember]
        public string nom_ciudadNaci { get; set; }
        [DataMember]
        public string nom_ciudadempresa { get; set; }
        [DataMember]
        public string nom_ciudadExp { get; set; }
        [DataMember]
        public string nom_estadoCivil { get; set; }
        [DataMember]
        public string nom_escolaridad { get; set; }
        [DataMember]
        public string nom_barrio { get; set; }
        [DataMember]
        public string nom_tipo_contrato { get; set; }
        [DataMember]
        public string mensaje_error { get; set; }
        [DataMember]
        public bool rpta { get; set; }

        [DataMember]
        public string razon_social { get; set; }

        [DataMember]
        public string camara_comercio { get; set; }

        [DataMember]
        public string pais { get; set; }

        [DataMember]
        public int tipo_empresa { get; set; }

        [DataMember]
        public string actividad_economica { get; set; }

        [DataMember]
        public string ciiu { get; set; }

        [DataMember]
        public Int64 ingresos_mensuales { get; set; }

        [DataMember]
        public string detotros_ingresos { get; set; }

        [DataMember]
        public Int64 egresos_mensuales { get; set; }

        [DataMember]
        public Int64 total_activos { get; set; }

        [DataMember]
        public Int64 total_pasivos { get; set; }

        [DataMember]
        public Int64 total_patrimonio { get; set; }

        [DataMember]
        public string tipo_persona { get; set; }

        [DataMember]
        public int codcargo { get; set; }

        [DataMember]
        public int admrecursos { get; set; }

        [DataMember]
        public int peps { get; set; }

        [DataMember]
        public Int64 ncuenta { get; set; }

        [DataMember]
        public string nom_banco { get; set; }

        [DataMember]
        public string paismoneda { get; set; }

        [DataMember]
        public string ciudadmoneda { get; set; }

        [DataMember]
        public string moneda { get; set; }

        [DataMember]
        public string operacion { get; set; }

        [DataMember]
        public Int64 cod_representante { get; set; }

        [DataMember]
        public string estadoper { get; set; }

        [DataMember]
        public string email_asesor { get; set; }

        //Datos agregados para formato de afiliación [03/04/2019]
        [DataMember]
        public List<BeneficiarioPersonaAfi> lstBeneficiarios { get; set; }

        [DataMember]
        public string tipoVivienda { get; set; }

        [DataMember]
        public int afecta_vivienda { get; set; }

        [DataMember]
        public string años_vivienda { get; set; }

        [DataMember]
        public string meses_vivienda { get; set; }

        [DataMember]
        public int cod_pagaduria { get; set; }

        [DataMember]
        public string cod_nomina { get; set; }

        [DataMember]
        public int numero_empleados { get; set; }

        [DataMember]
        public int funcion_publica { get; set; }

        [DataMember]
        public int familiares_cargos_pub { get; set; }
        
        [DataMember]
        public int operaciones_extrang { get; set; }

        [DataMember]
        public int tipo_cuenta_ext { get; set; }

        [DataMember]
        public decimal promedio { get; set; }

        [DataMember]
        public int envia_asesor { get; set; }

        [DataMember]
        public int envia_asociado { get; set; }

        [DataMember]
        public string envia_otro { get; set; }
    }
}
