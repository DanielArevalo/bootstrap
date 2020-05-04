using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
//using SecuGen.FDxSDKPro.Windows;
using System.Web.Script.Services;

namespace WebServices
{
    /// <summary>
    /// Descripción breve de WSBiometria
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class WSBiometria : System.Web.Services.WebService
    {
        [WebMethod(Description = "Determinar los datos de la conexión")]
        public string ProbarConexion(string entidadBD)
        {
            if (string.IsNullOrWhiteSpace(entidadBD)) entidadBD = "DataBase";

            // Definición de entidades y servicios
            Xpinn.Seguridad.Services.UsuarioService usuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
            // Realizar conexión a la base de datos
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            string error = "";
            usuario = conexion.DeterminarUsuarioSinClave(ref error, entidadBD);
            if (usuario == null)
            {
                usuario = new Xpinn.Util.Usuario();
                string aux = usuarioServicio.ProbarConexión(usuario);
                return "No pudo determinar el usuario => Conexión:" + entidadBD + " String:" + aux + " Usuario:" + conexion.DeterminarNombreUsuario(entidadBD) + " Error:" + error;
            }
            // Validar usuario y obtener accesos
            if (usuarioServicio.ProbarConexión(usuario) == null)
                return "No pudo realizar conexión => Conexión:" + entidadBD + " Usuario:" + conexion.DeterminarNombreUsuario(entidadBD) + " Clave:" + usuarioServicio.ProbarConexión(usuario);
            usuario.clave = usuario.clave_sinencriptar;
            usuario = usuarioServicio.ValidarUsuario(usuario.identificacion, usuario.clave, "", "", usuario);
            if (usuario == null)
            {
                return "No pudo identificar el usuario => Conexión:" + entidadBD + " Usuario:" + conexion.DeterminarNombreUsuario(entidadBD) + " Estado:" + usuarioServicio.ProbarConexión(usuario);
            }
            // Devolver listado de productos
            return "Usuario: " + usuario.nombre;            
        }

        [WebMethod(Description = "Determinar los datos de una persona a partir del código")]
        public Xpinn.FabricaCreditos.Entities.Persona1 ConsultaPersonaPorCodigo(Int64 pCodPersona, string entidadBD)
        {
            if (string.IsNullOrWhiteSpace(entidadBD)) entidadBD = "DataBase";

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioSinClave(entidadBD);
            if (usuario == null)
                return null;

            // Determinar datos de la persona        
            Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 ePersona = new Xpinn.FabricaCreditos.Entities.Persona1();
            ePersona.seleccionar = "Cod_persona";
            ePersona.cod_persona = pCodPersona;
            ePersona.noTraerHuella = 1;
            ePersona.soloPersona = 1;
            ePersona = personaServicio.ConsultarPersona1Param(ePersona, usuario);
            if (ePersona == null)
                return null;
            // Devolver listado de productos
            return ePersona;
        }

        [WebMethod(Description = "Determinar los datos de una persona a partir de la identificación")]
        public Xpinn.FabricaCreditos.Entities.Persona1 ConsultaPersona(string pIdentificacion, string entidadBD = "DataBase")
        {
            if (string.IsNullOrWhiteSpace(entidadBD)) entidadBD = "DataBase";

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioSinClave(entidadBD);
            if (usuario == null)
                return null;

            // Determinar datos de la persona        
            Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 ePersona = new Xpinn.FabricaCreditos.Entities.Persona1();
            ePersona.seleccionar = "Identificacion";
            ePersona.identificacion = pIdentificacion;
            ePersona.noTraerHuella = 1;
            ePersona.soloPersona = 1;
            ePersona = personaServicio.ConsultarPersona1Param(ePersona, usuario);
            if (ePersona == null)
                return null;
            // Devolver listado de productos
            return ePersona;
        }

        [WebMethod(Description = "Grabar la huella de la persona")]
        public Int64 GrabarHuella(string pIdentificacion, int pDedo, Byte[] pHuella, string pHuellaTemplate, string entidadBD)
        {
            if (string.IsNullOrWhiteSpace(entidadBD)) entidadBD = "DataBase";

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioSinClave(entidadBD);
            if (usuario == null)
                return -1;

            // Determinar datos de la persona        
            Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 ePersona = new Xpinn.FabricaCreditos.Entities.Persona1();
            ePersona.seleccionar = "Identificacion";
            ePersona.identificacion = pIdentificacion;
            ePersona.noTraerHuella = 1;
            ePersona.soloPersona = 1;
            ePersona = personaServicio.ConsultarPersona1Param(ePersona, usuario);
            if (ePersona == null)
                return -3;
            if (ePersona.nombre == "errordedatos")
                return -3;
            // Grabar la huella
            Xpinn.FabricaCreditos.Services.PersonaBiometriaService biometriaServicio = new Xpinn.FabricaCreditos.Services.PersonaBiometriaService();
            Xpinn.FabricaCreditos.Entities.PersonaBiometria biometria = new Xpinn.FabricaCreditos.Entities.PersonaBiometria();
            biometria.idbiometria = 0;
            biometria.cod_persona = ePersona.cod_persona;
            biometria.numero_dedo = pDedo;
            biometria.huella = pHuella;
            biometria.huella_secugen = pHuella;
            biometria.template_huella = pHuellaTemplate;
            
            biometria.fecha = System.DateTime.Now;
            biometria.idbiometria = biometriaServicio.ExistePersonaBiometria(biometria.cod_persona, Convert.ToInt32(biometria.numero_dedo), usuario);
            if (biometria.idbiometria > 0)
                biometria = biometriaServicio.ModificarPersonaBiometria(biometria, usuario);
            else
                biometria = biometriaServicio.CrearPersonaBiometria(biometria, usuario);
            // Devolver listado de productos
            return biometria.idbiometria;
        }

        [WebMethod(Description = "Grabar el template de la huella de la persona")]
        public Int64 GrabarTemplateHuella(string pIdentificacion, string pHuellaTemplate, string entidadBD)
        {
            if (string.IsNullOrWhiteSpace(entidadBD)) entidadBD = "DataBase";

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioSinClave(entidadBD);
            if (usuario == null)
                return -1;

            // Determinar datos de la persona        
            Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 ePersona = new Xpinn.FabricaCreditos.Entities.Persona1();
            ePersona.seleccionar = "Identificacion";
            ePersona.identificacion = pIdentificacion;
            ePersona.soloPersona = 1;
            ePersona = personaServicio.ConsultarPersona1Param(ePersona, usuario);
            if (ePersona == null)
                return -5;
            // Grabar la huella
            Xpinn.FabricaCreditos.Services.PersonaBiometriaService biometriaServicio = new Xpinn.FabricaCreditos.Services.PersonaBiometriaService();
            Xpinn.FabricaCreditos.Entities.PersonaBiometria biometria = new Xpinn.FabricaCreditos.Entities.PersonaBiometria();
            biometria.idbiometria = 0;
            biometria.cod_persona = ePersona.cod_persona;
            biometria.numero_dedo = 1;
            biometria.template_huella = pHuellaTemplate;
            biometria.codusuario = usuario.codusuario;
            biometria.fecha = System.DateTime.Now;
            biometria.idbiometria = biometriaServicio.ExistePersonaBiometria(biometria.cod_persona, Convert.ToInt32(biometria.numero_dedo), usuario);
            if (biometria.idbiometria > 0)
                biometria = biometriaServicio.ModificarPersonaBiometria(biometria, usuario);
            else
                biometria = biometriaServicio.CrearPersonaBiometria(biometria, usuario);
            // Devolver listado de productos
            return biometria.idbiometria;
        }

        [WebMethod(Description = "Consultar la huella de la persona")]
        public Xpinn.FabricaCreditos.Entities.PersonaBiometria ConsultarHuella(string pIdentificacion, string entidadBD)
        {
            if (string.IsNullOrWhiteSpace(entidadBD)) entidadBD = "DataBase";

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioSinClave(entidadBD);
            if (usuario == null)
                return null;

            // Determinar datos de la persona        
            Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 ePersona = new Xpinn.FabricaCreditos.Entities.Persona1();
            ePersona.seleccionar = "Identificacion";
            ePersona.identificacion = pIdentificacion;
            ePersona.soloPersona = 1;
            ePersona = personaServicio.ConsultarPersona1Param(ePersona, usuario);
            if (ePersona == null)
                return null;
            // Consultar la huella
            Xpinn.FabricaCreditos.Services.PersonaBiometriaService biometriaServicio = new Xpinn.FabricaCreditos.Services.PersonaBiometriaService();
            Xpinn.FabricaCreditos.Entities.PersonaBiometria biometria = new Xpinn.FabricaCreditos.Entities.PersonaBiometria();                        
            biometria = biometriaServicio.ConsultarPersonaBiometria(ePersona.cod_persona, 1, usuario);
            // Devolver listado de productos
            return biometria;
        }

        [WebMethod(Description = "Listar todas las huellas de las personas")]
        public List<Xpinn.FabricaCreditos.Entities.PersonaBiometria> ListarHuellas(string entidadBD)
        {
            if (string.IsNullOrWhiteSpace(entidadBD)) entidadBD = "DataBase";

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioSinClave(entidadBD);
            if (usuario == null)
                return null;

            // Listado de huellas
            Xpinn.FabricaCreditos.Services.PersonaBiometriaService biometriaServicio = new Xpinn.FabricaCreditos.Services.PersonaBiometriaService();
            Xpinn.FabricaCreditos.Entities.PersonaBiometria biometria = new Xpinn.FabricaCreditos.Entities.PersonaBiometria();
            List<Xpinn.FabricaCreditos.Entities.PersonaBiometria> lstbiometria = new List<Xpinn.FabricaCreditos.Entities.PersonaBiometria>();
            lstbiometria = biometriaServicio.ListarPersonaBiometria(biometria, usuario);
            // Devolver listado de huellas
            return lstbiometria;
        }

        [WebMethod(Description = "Listar las huellas de una persona")]
        public List<Xpinn.FabricaCreditos.Entities.PersonaBiometria> ListarHuellasPersona(string pIdentificacion, string entidadBD)
        {
            if (string.IsNullOrWhiteSpace(entidadBD)) entidadBD = "DataBase";

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioSinClave(entidadBD);
            if (usuario == null)
                return null;
            // Determinar datos de la persona        
            Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 ePersona = new Xpinn.FabricaCreditos.Entities.Persona1();
            ePersona.seleccionar = "Identificacion";
            ePersona.identificacion = pIdentificacion;
            ePersona.soloPersona = 1;
            ePersona = personaServicio.ConsultarPersona1Param(ePersona, usuario);
            if (ePersona == null)
                return null;
            // Listado de huellas
            Xpinn.FabricaCreditos.Services.PersonaBiometriaService biometriaServicio = new Xpinn.FabricaCreditos.Services.PersonaBiometriaService();
            Xpinn.FabricaCreditos.Entities.PersonaBiometria biometria = new Xpinn.FabricaCreditos.Entities.PersonaBiometria();
            List<Xpinn.FabricaCreditos.Entities.PersonaBiometria> lstbiometria = new List<Xpinn.FabricaCreditos.Entities.PersonaBiometria>();
            biometria.cod_persona = ePersona.cod_persona;
            lstbiometria = biometriaServicio.ListarPersonaBiometria(biometria, usuario);
            // Devolver listado de huellas
            return lstbiometria;
        }

        [WebMethod(Description = "Listar las autorizaciones para validar por el usuario")]
        public List<Xpinn.FabricaCreditos.Entities.PersonaAutorizacion> ListarUsuarioAutorizacion(Int64 pCodUsuario, string entidadBD)
        {
            if (string.IsNullOrWhiteSpace(entidadBD)) entidadBD = "DataBase";

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioSinClave(entidadBD);
            if (usuario == null)
                return null;
            // Consultar listado de autorizaciones de la persona
            Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            List<Xpinn.FabricaCreditos.Entities.PersonaAutorizacion> lstautorizacion = new List<Xpinn.FabricaCreditos.Entities.PersonaAutorizacion>();
            lstautorizacion = personaServicio.ListarUsuarioAutorizacion(pCodUsuario, usuario);
            // Devolver listado de productos
            return lstautorizacion;
        }

        [WebMethod(Description = "Listar las autorizaciones de la persona")]
        public List<Xpinn.FabricaCreditos.Entities.PersonaAutorizacion> ListarAutorizaciones(string pIdentificacion, string entidadBD)
        {
            if (string.IsNullOrWhiteSpace(entidadBD)) entidadBD = "DataBase";

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioSinClave(entidadBD);
            if (usuario == null)
                return null;
            // Consultar listado de autorizaciones de la persona
            Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();            
            List<Xpinn.FabricaCreditos.Entities.PersonaAutorizacion> lstautorizacion = new List<Xpinn.FabricaCreditos.Entities.PersonaAutorizacion>();
            lstautorizacion = personaServicio.ListarPersonaAutorizacion(pIdentificacion, usuario);
            // Devolver listado de productos
            return lstautorizacion;
        }

        [WebMethod(Description = "Actualizar el estado de una autorizaciòn")]
        public bool ActualizarAutorizacion(string pIdentificacion, Int32 pAutorizacion, Int32 pEstado, string entidadBD)
        {
            if (string.IsNullOrWhiteSpace(entidadBD)) entidadBD = "DataBase";

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioSinClave(entidadBD);
            if (usuario == null)
                return false;
            // Determinar datos de la autorizacion        
            Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            List<Xpinn.FabricaCreditos.Entities.PersonaAutorizacion> lstautorizacion = new List<Xpinn.FabricaCreditos.Entities.PersonaAutorizacion>();
            Xpinn.FabricaCreditos.Entities.PersonaAutorizacion personaAutoriza = new Xpinn.FabricaCreditos.Entities.PersonaAutorizacion();
            personaAutoriza.idautorizacion = pAutorizacion;
            personaAutoriza.estado = 1;
            lstautorizacion = personaServicio.ListarPersonaAutorizacion(personaAutoriza, usuario);
            if (lstautorizacion == null)
                return false;
            if (lstautorizacion.Count <= 0 || lstautorizacion.Count > 1)
                return false;
            // Actualizar estado de la autorizacion
            try
            {
                lstautorizacion[0].estado = 2;
                personaAutoriza = personaServicio.ModificarPersonaAutorizacion(lstautorizacion[0], usuario);
                if (personaAutoriza == null)
                    return false;
            }
            catch
            {
                return false;
            }
            return true;
        }

        [WebMethod(Description = "Consultar la huella de la persona - SECUGEN")]
        public Xpinn.FabricaCreditos.Entities.PersonaBiometria ConsultarHuellaSECUGEN(string pIdentificacion, string entidadBD)
        {
            if (string.IsNullOrWhiteSpace(entidadBD)) entidadBD = "DataBase";

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioSinClave(entidadBD);
            if (usuario == null)
                return null;
            // Determinar datos de la persona        
            Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 ePersona = new Xpinn.FabricaCreditos.Entities.Persona1();
            ePersona.seleccionar = "Identificacion";
            ePersona.identificacion = pIdentificacion;
            ePersona.soloPersona = 1;
            ePersona = personaServicio.ConsultarPersona1Param(ePersona, usuario);
            if (ePersona == null)
                return null;
            // Consultar la huella
            Xpinn.FabricaCreditos.Services.PersonaBiometriaService biometriaServicio = new Xpinn.FabricaCreditos.Services.PersonaBiometriaService();
            Xpinn.FabricaCreditos.Entities.PersonaBiometria biometria = new Xpinn.FabricaCreditos.Entities.PersonaBiometria();
            biometria = biometriaServicio.ConsultarPersonaBiometriaSECUGEN(ePersona.cod_persona, 1, usuario);
            // Devolver listado de productos
            return biometria;
        }

        /*
        [WebMethod(Description = "Validar la huella de la persona - SECUGEN")]
        public int ValidarHuellaSECUGEN(string pIdentificacion, Byte[] pHuella, string pHuellaTemplate, ref int? pDedo, ref int pMatch_score, string entidadBD)
        {
            if (string.IsNullOrWhiteSpace(entidadBD)) entidadBD = "DataBase";

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioSinClave(entidadBD);
            if (usuario == null)
                return -100;
            // Determinar datos de la persona        
            Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 ePersona = new Xpinn.FabricaCreditos.Entities.Persona1();
            ePersona.seleccionar = "Identificacion";
            ePersona.identificacion = pIdentificacion;
            ePersona.soloPersona = 1;
            ePersona = personaServicio.ConsultarPersona1Param(ePersona, usuario);
            if (ePersona == null)
                return -200;
            // Listado de huellas
            Xpinn.FabricaCreditos.Services.PersonaBiometriaService biometriaServicio = new Xpinn.FabricaCreditos.Services.PersonaBiometriaService();
            Xpinn.FabricaCreditos.Entities.PersonaBiometria biometria = new Xpinn.FabricaCreditos.Entities.PersonaBiometria();
            List<Xpinn.FabricaCreditos.Entities.PersonaBiometria> lstbiometria = new List<Xpinn.FabricaCreditos.Entities.PersonaBiometria>();
            biometria.cod_persona = ePersona.cod_persona;
            lstbiometria = biometriaServicio.ListarPersonaBiometria(biometria, usuario);
            // Realizar la validación contra las huellas de la persona
            Int32 iError = 0;
            pMatch_score = 0;
            pDedo = 0;
            Int32 image_width = 260;
            Int32 image_height = 300;
            Int32 image_dpi = 500;
            SGFPMSecurityLevel secu_level = (SGFPMSecurityLevel)5;
            SGFingerPrintManager m_FPM = new SGFingerPrintManager();
            iError = m_FPM.InitEx(image_width, image_height, image_dpi);
            if (iError != (Int32)SGFPMError.ERROR_NONE)
                return -300;
            foreach (Xpinn.FabricaCreditos.Entities.PersonaBiometria eHuella in lstbiometria)
            {
                bool matched = false;
                iError = m_FPM.MatchTemplate(pHuella, eHuella.huella_secugen, secu_level, ref matched);
                iError = m_FPM.GetMatchingScore(pHuella, eHuella.huella_secugen, ref pMatch_score);
                if (iError == (Int32)SGFPMError.ERROR_NONE)
                    if (matched)
                    {
                        pDedo = eHuella.numero_dedo;
                        return 0;
                    }
            }
            return -400;
        }

        [WebMethod(Description = "Identificar la persona según la huella - SECUGEN")]
        public Xpinn.FabricaCreditos.Entities.PersonaBiometria IdentificarHuellaSECUGEN(Byte[] pHuella, string pHuellaTemplate, string entidadBD)
        {
            if (string.IsNullOrWhiteSpace(entidadBD)) entidadBD = "DataBase";

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioSinClave(entidadBD);
            if (usuario == null)
                return null;
            // Listado de huellas
            Xpinn.FabricaCreditos.Services.PersonaBiometriaService biometriaServicio = new Xpinn.FabricaCreditos.Services.PersonaBiometriaService();
            Xpinn.FabricaCreditos.Entities.PersonaBiometria biometria = new Xpinn.FabricaCreditos.Entities.PersonaBiometria();
            List<Xpinn.FabricaCreditos.Entities.PersonaBiometria> lstbiometria = new List<Xpinn.FabricaCreditos.Entities.PersonaBiometria>();
            lstbiometria = biometriaServicio.ListarPersonaBiometria(biometria, usuario);
            // Realizar la validación contra las huellas de la persona
            Int32 iError = 0;
            Int32 image_width = 260;
            Int32 image_height = 300;
            Int32 image_dpi = 500;
            bool matched = false;
            Int32 match_score = 0;
            SGFPMSecurityLevel secu_level = (SGFPMSecurityLevel)5;
            SGFingerPrintManager m_FPM = new SGFingerPrintManager();
            iError = m_FPM.InitEx(image_width, image_height, image_dpi);
            if (iError != (Int32)SGFPMError.ERROR_NONE)
                return null;
            foreach (Xpinn.FabricaCreditos.Entities.PersonaBiometria eHuella in lstbiometria)
            {
                try
                {
                    // Comparando las huellas
                    iError = m_FPM.MatchTemplate(pHuella, eHuella.huella_secugen, secu_level, ref matched);
                    iError = m_FPM.GetMatchingScore(pHuella, eHuella.huella_secugen, ref match_score);
                    if (iError == (Int32)SGFPMError.ERROR_NONE)
                        if (matched)
                            return eHuella;
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }
        */

        [WebMethod(Description = "Consultar la foto de una persona")]
        public byte[] ConsultarImagenPersonaIdentificacion(string pIdentificacion, string entidadBD)
        {
            if (string.IsNullOrWhiteSpace(entidadBD)) entidadBD = "DataBase";

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioSinClave(entidadBD);
            if (usuario == null)
                return null;
            // Determinar datos de la persona        
            Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Imagenes eImagen = new Xpinn.FabricaCreditos.Entities.Imagenes();
            eImagen = personaServicio.ConsultarImagenesPersonaIdentificacion(pIdentificacion, 1, usuario);
            if (eImagen == null)
                return null;
            // Devolver la foto
            return eImagen.imagen;
        }

        [WebMethod(Description = "Consultar la foto de una persona")]
        public Int64? GrabarImagenPersonaIdentificacion(string pIdentificacion, byte[] pFoto, string entidadBD)
        {
            if (string.IsNullOrWhiteSpace(entidadBD)) entidadBD = "DataBase";

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioSinClave(entidadBD);
            if (usuario == null)
                return null;
            // Determinar datos de la persona        
            Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 ePersona = new Xpinn.FabricaCreditos.Entities.Persona1();
            ePersona.seleccionar = "Identificacion";
            ePersona.identificacion = pIdentificacion;
            ePersona.soloPersona = 1;
            ePersona = personaServicio.ConsultarPersona1Param(ePersona, usuario);
            if (ePersona == null)
                return null;
            if (ePersona.nombre == "errordedatos")
                return null;
            // Grabar datos de la foto
            Xpinn.FabricaCreditos.Entities.Imagenes pImagenes = new Xpinn.FabricaCreditos.Entities.Imagenes();
            if (pFoto != null)
            {                                                
                pImagenes.cod_persona = ePersona.cod_persona;
                pImagenes.tipo_imagen = 1;
                pImagenes.tipo_documento = Convert.ToInt32(ePersona.tipo_identificacion);
                pImagenes.imagen = pFoto;
                pImagenes.fecha = System.DateTime.Now;
                if (ePersona.idimagen == 0)
                {
                    pImagenes.idimagen = 0;
                    pImagenes = personaServicio.CrearImagenesPersona(pImagenes, usuario);
                }
                else
                {
                    pImagenes.idimagen = Convert.ToInt64(ePersona.idimagen); 
                    pImagenes = personaServicio.ModificarImagenesPersona(pImagenes, usuario);
                }
            }
            return pImagenes.idimagen;
        }

        #region WebServices para el control de entrega de regalos para COOACEDED. Comprobar.

        [WebMethod(Description = "Determinar si la persona cumple requisitos")]
        public Xpinn.FabricaCreditos.Entities.Persona1 ComprobarPersona(string pIdentificacion, int pTipo, string entidadBD)
        {
            if (string.IsNullOrWhiteSpace(entidadBD)) entidadBD = "DataBase";

            // Determinar datos de usuario para conexión a base de datos
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioSinClave(entidadBD);
            if (usuario == null)
                return null;

            // Determinar datos de la persona        
            Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 ePersona = new Xpinn.FabricaCreditos.Entities.Persona1();
            ePersona = personaServicio.ValidarPersona(pIdentificacion, pTipo, usuario);
            if (ePersona == null)
                return null;

            // Devolver datos de persona y su validación
            return ePersona;
        }

        [WebMethod(Description = "Grabar registro de entrega del regalo")]
        public Int64? GrabarControl(string pIdentificacion, int pTipo, string entidadBD)
        {
            if (string.IsNullOrWhiteSpace(entidadBD)) entidadBD = "DataBase";

            // Determinar datos de usuario para conexión a base de datos
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioSinClave(entidadBD);
            if (usuario == null)
                return null;

            // Determinar datos de la persona        
            Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Int64? idControl = null;
            idControl = personaServicio.GrabarControl(pIdentificacion, pTipo, usuario);
            return idControl;
        }

        #endregion

    }
}

