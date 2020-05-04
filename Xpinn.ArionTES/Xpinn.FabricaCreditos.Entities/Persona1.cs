using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad Persona1
    /// </summary>
    [DataContract]
    [Serializable]
    public class Persona1
    {
        [DataMember]
        public string tipo_identificacion_descripcion { get; set; }
        [DataMember]
        public String origen { get; set; }   //Parametro util para modificacion. Permite seleccionar que campos se desean actualizar en la tabla
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public string cod_nomina_empleado { get; set; }
        [DataMember]
        public String tipo_persona { get; set; }
        [DataMember]
        public String identificacion { get; set; }
        [DataMember]
        public Int64 digito_verificacion { get; set; }
        [DataMember]
        public Int64 tipo_identificacion { get; set; }
        [DataMember]
        public string nomtipo_identificacion { get; set; }
        [DataMember]
        public DateTime? fechaexpedicion { get; set; }
        [DataMember]
        public Int64? codciudadexpedicion { get; set; }
        [DataMember]
        public String sexo { get; set; }
        public String nombreYApellido
        {
            get
            {
                return primer_nombre + " " + primer_apellido;
            }
        }
        public String nombreCompleto
        {
            get
            {
                return primer_nombre + " " + segundo_nombre + " " + primer_apellido + " " + segundo_apellido ;
            }
        }
        [DataMember]
        public String primer_nombre { get; set; }
        [DataMember]
        public String segundo_nombre { get; set; }
        [DataMember]
        public String primer_apellido { get; set; }
        [DataMember]
        public String segundo_apellido { get; set; }
        [DataMember]
        public String template { get; set; }
        [DataMember]
        public String razon_social { get; set; }
        [DataMember]
        public DateTime? fechanacimiento { get; set; }
        [DataMember]
        public string fechanacimientoAPP { get; set; }
        [DataMember]
        public Int64? codciudadnacimiento { get; set; }
        [DataMember]

        public Int64? Sueldo_Persona { get; set; }
        [DataMember]

        public Int64? cuota { get; set; }
        [DataMember]

        public Int64? codestadocivil { get; set; }
        [DataMember]
        public Int64? codescolaridad { get; set; }
        [DataMember]
        public Int64? codactividad { get; set; }
        [DataMember]
        public String direccion { get; set; }
        [DataMember]
        public String telefono { get; set; }
        [DataMember]
        public Int64? codciudadresidencia { get; set; }
        [DataMember]
        public Int64 antiguedadlugar { get; set; }
        [DataMember]
        public String tipovivienda { get; set; }
        [DataMember]
        public String arrendador { get; set; }
        [DataMember]
        public String telefonoarrendador { get; set; }
        [DataMember]
        public Int64 ValorArriendo { get; set; }
        [DataMember]
        public String celular { get; set; }
        [DataMember]
        public String email { get; set; }
        [DataMember]
        public String empresa { get; set; }
        [DataMember]
        public String telefonoempresa { get; set; }
        [DataMember]
        public String direccionempresa { get; set; }
        [DataMember]
        public String email_empresa { get; set; }
        [DataMember]
        public Int64 antiguedadlugarempresa { get; set; }
        [DataMember]
        public Int64 codcargo { get; set; }
        [DataMember]
        public Int64 codtipocontrato { get; set; }
        [DataMember]
        public Int64 cod_asesor { get; set; }
        [DataMember]
        public String residente { get; set; }
        [DataMember]
        public DateTime fecha_residencia { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]

        public string cod_nomina { get; set; }
        [DataMember]
        public String tratamiento { get; set; }
        [DataMember]
        public String estado { get; set; }
        [DataMember]
        public String nomestado { get; set; }
        [DataMember]
        public DateTime fechacreacion { get; set; }
        [DataMember]
        public String usuariocreacion { get; set; }
        [DataMember]
        public DateTime fecultmod { get; set; }
        [DataMember]
        public String usuultmod { get; set; }
        [DataMember]
        public String cod_negocio { get; set; }
        [DataMember]
        public Int64? barrioResidencia { get; set; }
        [DataMember]
        public String dirCorrespondencia { get; set; }
        [DataMember]
        public Int64? barrioCorrespondencia { get; set; }
        [DataMember]
        public String barrioCorresponden { get; set; }
        [DataMember]
        public String telCorrespondencia { get; set; }
        [DataMember]
        public Int64? ciuCorrespondencia { get; set; }
        [DataMember]
        public Int64? ActividadEconomicaEmpresa { get; set; }
        [DataMember]
        public string ActividadEconomicaEmpresaStr { get; set; }
        [DataMember]
        public Int64? ciudad { get; set; }
        [DataMember]
        public int relacionEmpleadosEmprender { get; set; }
        [DataMember]
        public string CelularEmpresa { get; set; }
        [DataMember]
        public int? Estrato { get; set; }
        [DataMember]
        public Int64? PersonasAcargo { get; set; }
        [DataMember]
        public string profecion { get; set; }
        [DataMember]
        public Int64? idimagen { get; set; }
        [DataMember]
        public byte[] foto { get; set; }
        [DataMember]
        public byte[] ImgFirma { get; set; }
        [DataMember]
        public int idescalafon { get; set; }
        [DataMember]
        public Int64? sector { get; set; }
        [DataMember]
        public Int64? zona { get; set; }
        [DataMember]
        public string nom_zona { get; set; }
        [DataMember]
        public Int64? nacionalidad { get; set; }
        [DataMember]
        public Int32? ubicacion_correspondencia { get; set; }
        [DataMember]
        public Int32? ubicacion_residencia { get; set; }
        [DataMember]
        public Int64? nit_empresa { get; set; }
        [DataMember]
        public Int32? tipo_empresa { get; set; }
        [DataMember]
        public string act_ciiu_empresa { get; set; }
        [DataMember]
        public int? ubicacion_empresa { get; set; }

        // LISTAS DESPLEGABLES
        [DataMember]
        public Int64 ListaId { get; set; }
        [DataMember]
        public String ListaIdStr { get; set; }
        [DataMember]
        public String ListaDescripcion { get; set; }

        // Permite reconocer el tipo de relacion (Conyuge, codeudor)
        [DataMember]
        public int? noTraerHuella { get; set; }
        [DataMember]
        public int? soloPersona { get; set; }
        [DataMember]
        public int? sinImagen { get; set; }
        [DataMember]
        public String seleccionar { get; set; }
        [DataMember]
        public Int64 numeroRadicacion { get; set; }

        //Campos adicionales de conyuge que aplican para todas las personas
        [DataMember]
        public Int64 numHijos { get; set; }
        [DataMember]
        public Int64 numPersonasaCargo { get; set; }
        [DataMember]
        public String ocupacion { get; set; }
        [DataMember]
        public Int64 salario { get; set; }
        [DataMember]
        public Int64 antiguedadLaboral { get; set; }

        // Campos para consultar datos de la vista V_PERSONA
        [DataMember]
        public String nombres { get; set; }
        [DataMember]
        public String apellidos { get; set; }
        [DataMember]
        public String nombre { get; set; }

        // consultar datos codeudor
        [DataMember]
        public String oficina { get; set; }
        [DataMember]
        public Int64 NUMERO_OBLIGACION { get; set; }

        // consultar datos familiares
        [DataMember]
        public string parentesco { get; set; }
        [DataMember]
        public string acargo { get; set; }
        [DataMember]
        public string Observaciones { get; set; }

        // consultar datos informacion comercial
        [DataMember]
        public string medio { get; set; }

        // reporte solicitud
        [DataMember]
        public String tipo_identif { get; set; }
        [DataMember]
        public String ciudadexpedicion { get; set; }
        [DataMember]
        public String tipocontrato { get; set; }

        // Datos de los beneficiarios
        [DataMember]
        public List<Beneficiario> lstBeneficiarios { get; set; }

        //Datos de las Actividades
        [DataMember]
        public List<Actividades> lstActividad { get; set; }

        //Datos de las Actividades
        [DataMember]
        public List<Actividades> lstActEconomicasSecundarias { get; set; }
                
        // Datos de las cuentas bancarias
        [DataMember]
        public List<CuentasBancarias> lstCuentasBan { get; set; }

        // DAtos de información adicional
        [DataMember]
        public List<InformacionAdicional> lstInformacion { get; set; }

        // Datos para las pagadurias
        [DataMember]
        public List<PersonaEmpresaRecaudo> lstEmpresaRecaudo { get; set; }

        //Campo para valor de afiliacion desde aportes 
        [DataMember]
        public Int64 valor_afiliacion { get; set; }

        //APORTE/PERSONA/INFORMACION CONYUGE
        [DataMember]
        public Int64 cod_persona_cony { get; set; }
        [DataMember]
        public string codactividadStr { get; set; }
        [DataMember]
        public string nombre_funcionario { get; set; } 
        [DataMember]
        public DateTime fecha_ingresoempresa { get; set; }
        [DataMember]
        public int empleado_entidad { get; set; }
        [DataMember]
        public int mujer_familia { get; set; }
        [DataMember]
        public int jornada_laboral { get; set; }
        [DataMember]
        public int ocupacionApo { get; set; }
        [DataMember]
        public int? parentesco_madminis { get; set; }
        [DataMember]
        public int? parentesco_mcontrol { get; set; }
        [DataMember]
        public int? parentesco_pep { get; set; }

        //APP
        [DataMember]
        public string clave { get; set; }
        [DataMember]
        public string clavesinecriptar { get; set; }
        [DataMember]
        public Boolean rptaingreso { get; set; }
        [DataMember]
        public string nombre_app { get; set; }
        [DataMember]
        public string apellidos_app { get; set; }
        [DataMember]
        public string email_app { get; set; }
        [DataMember]
        public string nomciudad_resid { get; set; }
        [DataMember]
        public string nomciudad_lab { get; set; }
        [DataMember]
        public string fechacreacionAPP { get; set; }
        [DataMember]
        public long codigo { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string valor { get; set; }
        [DataMember]
        public long SALDOaportes { get; set; }
        [DataMember]
        public long saldocreditos { get; set; }
        [DataMember]
        public string ipPersona { get; set; }

        //agregado para grilla de codeudores
        [DataMember]
        public long idcodeudor { get; set; }
        [DataMember]
        public Int64 idEmpresa { get; set; }
        [DataMember]
        public String nit { get; set; }
        [DataMember]
        public DateTime fecha_afiliacion { get; set; }
        [DataMember]
        public DateTime fecha_retiro { get; set; }
        [DataMember]
        public string InformacionGeneral { get; set; }
        [DataMember]
        public long nom_ciudad { get; set; }

        [DataMember]
        public string nombre_ciudad { get; set; }
        [DataMember]
        public string departamento { get; set; }
        [DataMember]
        public string barCode { get; set; }

        //agregado para la lista de productos por persona
        public List<Productos_Persona> Lista_Producto = new List<Productos_Persona>();

        //Agregado para guardar información de manejo de moneda extranjera
        [DataMember]
        public List<EstadosFinancieros> lstMonedaExt { get; set; }
        [DataMember]
        public List<EstadosFinancieros> lstProductosFinExt { get; set; }

        //Notificacion
        [DataMember]
        public Int64 idNotificaion { get; set; }
        [DataMember]
        public string Texto { get; set; }
        [DataMember]
        public int DiasAviso { get; set; }
        [DataMember]
        public string TipoNot { get; set; }
        [DataMember]
        public int? Edad { get; set; }
        [DataMember]
        public string NumeroPoducto { get; set; }
        [DataMember]
        public DateTime VencimientoProducto { get; set; }
        [DataMember]
        public string url_droid_app { get; set; }
        [DataMember]
        public string url_ios_app { get; set; }
        [DataMember]
        public string version_app { get; set; }
        [DataMember]
        public DateTime fec_ult_publicacion_app { get; set; }
        [DataMember]
        public int orden { get; set; }
        [DataMember]
        public string email_asesor { get; set; }
        //Agregado para la solicitud de retiro de asociado en la Web
        [DataMember]
        public int cod_motivo { get; set; }
        [DataMember]
        public int id_solicitud { get; set; }
        [DataMember]
        public string pregunta { get; set; }
        [DataMember]
        public string respuesta { get; set; }

        //agregado para restringir acceso oficina
        public int acceso_oficina { get; set; }
    } 
}