using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.Contabilidad.Entities
{
    /// <summary>
    /// Datos de los terceros
    /// </summary>
    [DataContract]
    [Serializable]
    public class Tercero
    {
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public string tipo_persona { get; set; }
        [DataMember]
        public string nom_tipo_persona { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public int digito_verificacion { get; set; }
        [DataMember]
        public Int64 tipo_identificacion { get; set; }
        [DataMember]
        public DateTime? fechaexpedicion { get; set; }
        [DataMember]
        public Int64? codciudadexpedicion { get; set; }
        [DataMember]
        public string sexo { get; set; }
        [DataMember]
        public string primer_nombre { get; set; }
        [DataMember]
        public string segundo_nombre { get; set; }
        [DataMember]
        public string primer_apellido { get; set; }
        [DataMember]
        public string segundo_apellido { get; set; }
        [DataMember]
        public string razon_social { get; set; }
        [DataMember]
        public DateTime? fechanacimiento { get; set; }
        [DataMember]
        public Int64? codciudadnacimiento { get; set; }
        [DataMember]
        public Int64? codestadocivil { get; set; }
        [DataMember]
        public Int64? codescolaridad { get; set; }
        [DataMember]
        public Int64? codactividad { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public Int64? codciudadresidencia { get; set; }
        [DataMember]
        public Int64? antiguedadlugar { get; set; }
        [DataMember]
        public string tipovivienda { get; set; }
        [DataMember]
        public string arrendador { get; set; }
        [DataMember]
        public string telefonoarrendador { get; set; }
        [DataMember]
        public string celular { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string empresa { get; set; }
        [DataMember]
        public int tipo_empresa { get; set; }
        [DataMember]
        public string telefonoempresa { get; set; }
        [DataMember]
        public Int64? codcargo { get; set; }
        [DataMember]
        public Int64? codtipocontrato { get; set; }
        [DataMember]
        public Int64? cod_oficina { get; set; }
        [DataMember]
        public Int64? cod_asesor { get; set; }
        [DataMember]
        public string residente { get; set; }
        [DataMember]
        public DateTime? fecha_residencia { get; set; }
        [DataMember]
        public string tratamiento { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public Int64? cod_zona { get; set; }
        [DataMember]
        public decimal valorarriendo { get; set; }
        [DataMember]
        public string direccionempresa { get; set; }
        [DataMember]
        public Int64? antiguedadlugarempresa { get; set; }
        [DataMember]
        public Int64? barresidencia { get; set; }
        [DataMember]
        public string dircorrespondencia { get; set; }
        [DataMember]
        public string telcorrespondencia { get; set; }
        [DataMember]
        public Int64? ciucorrespondencia { get; set; }
        [DataMember]
        public Int64? barcorrespondencia { get; set; }
        [DataMember]
        public int? ubicacion_empresa { get; set; }
        [DataMember]
        public Int64? numhijos { get; set; }
        [DataMember]
        public Int64? numpersonasacargo { get; set; }
        [DataMember]
        public string ocupacion { get; set; }
        [DataMember]
        public decimal? salario { get; set; }
        [DataMember]
        public Int64? antiguedadlaboral { get; set; }
        [DataMember]
        public Int64? estrato { get; set; }
        [DataMember]
        public string celularempresa { get; set; }
        [DataMember]
        public Int64? ciudadempresa { get; set; }
        [DataMember]
        public Int64? posicionempresa { get; set; }
        [DataMember]
        public Int64? actividadempresa { get; set; }
        [DataMember]
        public Int64? parentescoempleado { get; set; }
        [DataMember]
        public DateTime? fechacreacion { get; set; }
        [DataMember]
        public string usuariocreacion { get; set; }
        [DataMember]
        public DateTime? fecultmod { get; set; }
        [DataMember]
        public string usuultmod { get; set; }
        [DataMember]
        public string error { get; set; }

        [DataMember]
        public string regimen { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string apellidos { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public decimal valor_afiliacion { get; set; }
        //Agregado
        [DataMember]
        public string camara_comercio { get; set; }
        [DataMember]
        public Int64 cod_representante { get; set; }
        [DataMember]
        public string nom_representante { get; set; }
        [DataMember]
        public Int64 tipo_id_representante { get; set; }
        [DataMember]
        public Int64 id_representante { get; set; }
        //ADD
        [DataMember]
        public List<InformacionAdicional> lstInformacion { get; set; }
        [DataMember]
        public int tipo_acto_creacion { get; set; }
        [DataMember]
        public string num_acto_creacion { get; set; }
        [DataMember]
        public string codactividadStr { get; set; }
        [DataMember]
        public string ActividadEconomicaEmpresaStr { get; set; }
        [DataMember]
        public int biometria { get; set; }
        [DataMember]
        public Int64 idimagen { get; set; }
        [DataMember]
        public byte[] foto { get; set; }
        [DataMember]
        public List<PersonaEmpresaRecaudo> lstEmpresaRecaudo { get; set; }
        [DataMember]
        public bool ExisteEnOFAC { get; set; }

        //Agregado para guardar información de manejo de moneda extranjera
        [DataMember]
        public List<EstadosFinancieros> lstMonedaExt { get; set; }
        [DataMember]
        public List<EstadosFinancieros> lsProductosExt { get; set; }

        [DataMember]
        public List<Actividades> lstActividadCIIU { get; set; }
        [DataMember]
        public int EnteTerritorial { get; set; }
        [DataMember]
        public int porcentaje_patrimonio { get; set; }

        public List<Tercero> lstAsociados { get; set; }

        //Agregado para modificación de asociados con 5% ticket 5172
        [DataMember]
        public int cotiza_bolsa { get; set; }
        [DataMember]
        public int vincula_pep { get; set; }
        [DataMember]
        public int tributacion { get; set; }
    }
}
