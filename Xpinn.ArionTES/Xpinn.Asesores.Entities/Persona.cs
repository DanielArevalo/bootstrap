using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Xpinn.Asesores.Entities.Common;

namespace Xpinn.Asesores.Entities
{
    [DataContract]
    [Serializable]
    public class Persona
    {

        public Persona()
        {
            TipoIdentificacion = new TipoIdentificacion();
            Ciudad = new Ciudad();
            CiudadNacimiento = new Ciudad();
            Actividad = new Actividad();
            CiudadResidencia = new Ciudad();
            Cargo = new Cargo();
            TipoContrato = new TipoContrato();
            Oficina = new Oficina();
            Asesor = new Ejecutivo();
            Zona = new Zona();
            Acodeudados = new List<Codeudor>();
        }

        #region TablaPersona
        [DataMember]
        public Int64 IdPersona { get; set; }
        [DataMember]
        public string TipoPersona { get; set; }
        [DataMember]
        public TipoIdentificacion TipoIdentificacion { get; set; }
        [DataMember]
        public string NumeroDocumento { get; set; }
        [DataMember]
        public int DigitoVerificacion { get; set; }
        [DataMember]
        public DateTime FechaExpedicion { get; set; }
        [DataMember]
        public Ciudad Ciudad { get; set; }
        [DataMember]
        public string Sexo { get; set; }
        [DataMember]
        public string PrimerNombre { get; set; }
        [DataMember]
        public string SegundoNombre { get; set; }
        [DataMember]
        public string PrimerApellido { get; set; }
        [DataMember]
        public string SegundoApellido { get; set; }
        [DataMember]
        public string RazonSocial { get; set; }
        [DataMember]
        public DateTime FechaNacimiento { get; set; }
        [DataMember]
        public Ciudad CiudadNacimiento { get; set; }
        [DataMember]
        public string EstadoCivil { get; set; }
        [DataMember]
        public Int64 CodigoEscolaridad { get; set; }
        [DataMember]
        public Actividad Actividad { get; set; }
        [DataMember]
        public string Direccion { get; set; }
        [DataMember]
        public string Telefono { get; set; }
        [DataMember]
        public Ciudad CiudadResidencia { get; set; }
        [DataMember]
        public int AntiguedadLugar { get; set; }
        [DataMember]
        public string TipoVivienda { get; set; }
        [DataMember]
        public string Arrendador { get; set; }
        [DataMember]
        public string TelefonoArrendador { get; set; }
        [DataMember]
        public string Celular { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Empresa { get; set; }
        [DataMember]
        public string TelefonoEmpresa { get; set; }
        [DataMember]
        public string SiglaNegocio { get; set; }
        [DataMember]
        public Cargo Cargo { get; set; }
        [DataMember]
        public TipoContrato TipoContrato { get; set; }
        [DataMember]
        public Oficina Oficina { get; set; }
        [DataMember]
        public Ejecutivo Asesor { get; set; }
        [DataMember]
        public string Residente { get; set; }
        [DataMember]
        public string CodigoNomina { get; set; }
        [DataMember]
        public DateTime FechaResidencia { get; set; }
        [DataMember]
        public string Tratamiento { get; set; }
        [DataMember]
        public string Estado { get; set; }
        [DataMember]
        public string EstadoJuridico { get; set; }
        [DataMember]
        public DateTime FechaCreacion { get; set; }
        [DataMember]
        public string UsuarioCreacion { get; set; }
        [DataMember]
        public DateTime FechaUltModificacion { get; set; }
        [DataMember]
        public string UsuarioUltModificacion { get; set; }
        [DataMember]
        public DateTime FechaAfiliacion { get; set; }
        [DataMember]
        public string NUM_COMP { get; set; }
        [DataMember]
        public DateTime FechaEstadoCuenta { get; set; }
        [DataMember]
        public string EstadoAfiliacion { get; set; }
        #endregion
        
        #region  Productos
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public Int64 TipoProducto { get; set; }
        [DataMember]
        public string producto { get; set; }
        [DataMember]
        public Int64 codpersona { get; set; }
        [DataMember]
        public String numproducto { get; set; }
        [DataMember]
        public String  codlinea { get; set; }
        [DataMember]
        public string nomlinea { get; set; }
        [DataMember]       
        public DateTime  fechaapertura { get; set; }
        [DataMember]
        public DateTime fechaproxpago { get; set; }
        [  DataMember]
        public Int64 codoficina { get; set; }
        [DataMember]
        public decimal  saldo { get; set; }
        [DataMember]
        public decimal cuota { get; set; }
         [DataMember]
        public string nombrecliente { get; set; }
        #endregion
        
        #region TablaRelacionadas
        [DataMember]
        public Zona Zona { get; set; }
        [DataMember]
        public int Calificacion { get; set; }
        [DataMember]
        public string TipoCliente { get; set; }
        [DataMember]
        public List<Codeudor> Acodeudados { get; set; }
        [DataMember]
        public Int64 NoCredito { get; set; }

        #endregion


        public string nommotivo { get; set; }
    }
}