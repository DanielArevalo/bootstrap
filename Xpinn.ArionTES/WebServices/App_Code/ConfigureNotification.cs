using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Xpinn.Util;

namespace WebServices
{
    
    public class ConfigureNotification
    {

        static Dictionary<ProcesoAtencionCliente, string> ProcesosAplicacionDictionary = new Dictionary<ProcesoAtencionCliente, string>()
        {
            { ProcesoAtencionCliente.ActualizacionDatos, "ACTUALIZACIÓN DE DATOS" },
            { ProcesoAtencionCliente.ModificacionProducto, "MODIFICACIÓN DE PRODUCTO" },
            { ProcesoAtencionCliente.RegistroAsociado, "REGISTRO DE ASOCIADO" },
            { ProcesoAtencionCliente.RenovacionCdat, "RENOVACIÓN DE CDAT" },
            { ProcesoAtencionCliente.RealizacionPagoACH, "APLICACIÓN DE PAGO PSE" },
            { ProcesoAtencionCliente.SolicitudAfiliacion, "SOLICITUD DE AFILIACIÓN" },
            { ProcesoAtencionCliente.SolicitudCredito, "SOLICITUD DE CRÉDITO" },
            { ProcesoAtencionCliente.SolicitudCruceCooperativa, "SOLICITUD DE CRUCE DE AHORRO A PRODUCTO" },
            { ProcesoAtencionCliente.SolicitudAhorroProgramado, "SOLICITUD AHORRO PROGRAMADO" },
            { ProcesoAtencionCliente.SolicitudAhorros, "SOLICITUD AHORROS" },
            { ProcesoAtencionCliente.SolicitudCDAT, "SOLICITUD CDAT" },
            { ProcesoAtencionCliente.SolicitudServicio, "SOLICITUD SERVICIO" },
            { ProcesoAtencionCliente.SolicitudRetiroAhorros, "SOLICITUD RETIRO DE AHORROS" },
            { ProcesoAtencionCliente.SolicitudCambioCuota, "SOLICITUD CAMBIO CUOTA DE AHORROS" },
            { ProcesoAtencionCliente.SolicitudRetiroAsociado, "SOLICITUD DE RETIRO" },
        };

        public static bool SendNotifification(ProcesoAtencionCliente pProcesoGenerado, Usuario pUsuario, long? pCodPersona = null, string pIdentificacion = null, string pNombre = null)
        {
            if (pCodPersona == null && string.IsNullOrEmpty(pIdentificacion))
                return false;
            if (ConfigurationManager.AppSettings["SendNotification"] == null || ConfigurationManager.AppSettings["SendEmailCompany"] == null)
                return false;
            if (ConfigurationManager.AppSettings["SendNotification"].ToString() != "1")
                return false;

            string URLWebServices = ConfigurationManager.AppSettings["URLWebServices"] as string;
            string pNameCompany = ConfigurationManager.AppSettings["NameCompany"] != null ? ConfigurationManager.AppSettings["NameCompany"].ToString() : "Atención al Cliente";
            string correoApp = ConfigurationManager.AppSettings["CorreoServidor"] as string;
            string claveCorreoApp = ConfigurationManager.AppSettings["Clave"] as string;
            string hosting = ConfigurationManager.AppSettings["Hosting"] as string;
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["Puerto"].ToString());
            string UrlImageApp = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "Imagenes", "logoEmpresa.jpg");

            string fileName = "SendEmailNotification.txt";
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", fileName);
            string htmlCorreo = File.ReadAllText(path); 

            try
            {
                long cod_persona = pCodPersona != null ? (long)pCodPersona : 0;
                Xpinn.FabricaCreditos.Services.Persona1Service PersonaService = new Xpinn.FabricaCreditos.Services.Persona1Service();
                Xpinn.FabricaCreditos.Entities.Persona1 Datos = new Xpinn.FabricaCreditos.Entities.Persona1()
                {
                    seleccionar = pCodPersona == null ? "Identificacion" : "Cod_persona",
                    cod_persona = cod_persona,
                    identificacion = pIdentificacion,
                    soloPersona = 1
                };
                Xpinn.FabricaCreditos.Entities.Persona1 pPersona = null;
                if (pProcesoGenerado != ProcesoAtencionCliente.SolicitudAfiliacion)
                    pPersona = PersonaService.ConsultarPersonaAPP(Datos, pUsuario);
                if (pPersona.nombre == "errordedatos")
                    return false;
                
                CorreoHelper correoHelper = new CorreoHelper(pPersona.email, correoApp, claveCorreoApp);                
                
                htmlCorreo = htmlCorreo.Replace("@_URL_IMAGE_@", UrlImageApp);
                htmlCorreo = htmlCorreo.Replace("@_PROCESO_GENERADO_@", ProcesosAplicacionDictionary[pProcesoGenerado]);
                if (pProcesoGenerado == ProcesoAtencionCliente.SolicitudAfiliacion)
                {
                    htmlCorreo = htmlCorreo.Replace("@_NOMBRE_@", pNombre);
                    htmlCorreo = htmlCorreo.Replace("@_TIPO_CEDULA_@", "");
                    htmlCorreo = htmlCorreo.Replace("@_IDENTIFICACION_@", pIdentificacion);
                }
                else
                {
                    htmlCorreo = htmlCorreo.Replace("@_NOMBRE_@", pPersona.nombreCompleto);
                    htmlCorreo = htmlCorreo.Replace("@_TIPO_CEDULA_@", pPersona.nomtipo_identificacion.ToLower());
                    htmlCorreo = htmlCorreo.Replace("@_IDENTIFICACION_@", pPersona.identificacion);
                }
                
                htmlCorreo = htmlCorreo.Replace("@_FECHA_GENERACION_@", string.Format("{0}   {1}", DateTime.Now.ToLongDateString(), DateTime.Now.ToShortTimeString()));
                string salida = "";
                bool exitoso = correoHelper.sendEmail(htmlCorreo, out salida, "Notificación - Atención al Cliente", pPersona.email_asesor);
                return exitoso;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public static string SendNotifification2(ProcesoAtencionCliente pProcesoGenerado, Usuario pUsuario, long? pCodPersona = null, string pIdentificacion = null, string pNombre = null)
        {
            if (pCodPersona == null && string.IsNullOrEmpty(pIdentificacion))
                return "sin datos de la persona";
            if (ConfigurationManager.AppSettings["SendNotification"] == null || ConfigurationManager.AppSettings["SendEmailCompany"] == null)
                return "sin parametro de enviar";
            if (ConfigurationManager.AppSettings["SendNotification"].ToString() != "1")
                return "parametro configurado para no enviar";

            string URLWebServices = ConfigurationManager.AppSettings["URLWebServices"] as string;
            string pNameCompany = ConfigurationManager.AppSettings["NameCompany"] != null ? ConfigurationManager.AppSettings["NameCompany"].ToString() : "Atención al Cliente";
            string correoApp = ConfigurationManager.AppSettings["CorreoServidor"] as string;
            string claveCorreoApp = ConfigurationManager.AppSettings["Clave"] as string;
            string hosting = ConfigurationManager.AppSettings["Hosting"] as string;
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["Puerto"].ToString());
            string UrlImageApp = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "Imagenes", "logoEmpresa.jpg");

            string fileName = "SendEmailNotification.txt";
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", fileName);
            string htmlCorreo = File.ReadAllText(path);

            try
            {
                long cod_persona = pCodPersona != null ? (long)pCodPersona : 0;
                Xpinn.FabricaCreditos.Services.Persona1Service PersonaService = new Xpinn.FabricaCreditos.Services.Persona1Service();
                Xpinn.FabricaCreditos.Entities.Persona1 Datos = new Xpinn.FabricaCreditos.Entities.Persona1()
                {
                    seleccionar = pCodPersona == null ? "Identificacion" : "Cod_persona",
                    cod_persona = cod_persona,
                    identificacion = pIdentificacion,
                    soloPersona = 1
                };
                Xpinn.FabricaCreditos.Entities.Persona1 pPersona = null;
                if (pProcesoGenerado != ProcesoAtencionCliente.SolicitudAfiliacion)
                    pPersona = PersonaService.ConsultarPersonaAPP(Datos, pUsuario);
                if (pPersona.nombre == "errordedatos")
                    return "error al consultar la persona";

                CorreoHelper correoHelper = new CorreoHelper(pPersona.email, correoApp, claveCorreoApp);

                htmlCorreo = htmlCorreo.Replace("@_URL_IMAGE_@", UrlImageApp);
                htmlCorreo = htmlCorreo.Replace("@_PROCESO_GENERADO_@", ProcesosAplicacionDictionary[pProcesoGenerado]);
                if (pProcesoGenerado == ProcesoAtencionCliente.SolicitudAfiliacion)
                {
                    htmlCorreo = htmlCorreo.Replace("@_NOMBRE_@", pNombre);
                    htmlCorreo = htmlCorreo.Replace("@_TIPO_CEDULA_@", "");
                    htmlCorreo = htmlCorreo.Replace("@_IDENTIFICACION_@", pIdentificacion);
                }
                else
                {
                    htmlCorreo = htmlCorreo.Replace("@_NOMBRE_@", pPersona.nombreCompleto);
                    htmlCorreo = htmlCorreo.Replace("@_TIPO_CEDULA_@", pPersona.nomtipo_identificacion.ToLower());
                    htmlCorreo = htmlCorreo.Replace("@_IDENTIFICACION_@", pPersona.identificacion);
                }

                htmlCorreo = htmlCorreo.Replace("@_FECHA_GENERACION_@", string.Format("{0}   {1}", DateTime.Now.ToLongDateString(), DateTime.Now.ToShortTimeString()));
                string salida = "";
                bool exitoso = correoHelper.sendEmail(htmlCorreo, out salida, "Notificación - Atención al Cliente", pPersona.email_asesor);
                return "enviado";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public static string PruebaNotifification(Usuario pUsuario, long? pCodPersona = null)
        {
            if (pCodPersona == null)
                return "sin identificación ni código";
            if (ConfigurationManager.AppSettings["SendNotification"] == null || ConfigurationManager.AppSettings["SendEmailCompany"] == null)
                return "Faltan parametros en el web config";
            if (ConfigurationManager.AppSettings["SendNotification"].ToString() != "1")
                return "Está configurado para no enviar correo";

            string URLWebServices = ConfigurationManager.AppSettings["URLWebServices"] as string;
            string pNameCompany = ConfigurationManager.AppSettings["NameCompany"] != null ? ConfigurationManager.AppSettings["NameCompany"].ToString() : "Atención al Cliente";
            string correoApp = ConfigurationManager.AppSettings["CorreoServidor"] as string;
            string claveCorreoApp = ConfigurationManager.AppSettings["Clave"] as string;
            string hosting = ConfigurationManager.AppSettings["Hosting"] as string;
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["Puerto"].ToString());

            string fileName = "SendEmailNotification.txt";
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", fileName);
            string htmlCorreo = File.ReadAllText(path);

            try
            {
                long cod_persona = pCodPersona != null ? (long)pCodPersona : 0;
                Xpinn.FabricaCreditos.Services.Persona1Service PersonaService = new Xpinn.FabricaCreditos.Services.Persona1Service();
                Xpinn.FabricaCreditos.Entities.Persona1 Datos = new Xpinn.FabricaCreditos.Entities.Persona1()
                {
                    seleccionar = "Cod_persona",
                    cod_persona = cod_persona,
                    soloPersona = 1
                };
                Xpinn.FabricaCreditos.Entities.Persona1 pPersona = null;
                pPersona = PersonaService.ConsultarPersonaAPP(Datos, pUsuario);
                if (pPersona.nombre == "errordedatos")
                    return "error en consultarPersonaApp";

                CorreoHelper correoHelper = new CorreoHelper(pPersona.email, correoApp, claveCorreoApp);
                string UrlImageApp = Path.Combine(URLWebServices, "Files", "Imagenes", "AtencionWeb.png");

                htmlCorreo = htmlCorreo.Replace("@_URL_IMAGE_@", UrlImageApp);
                htmlCorreo = htmlCorreo.Replace("@_PROCESO_GENERADO_@", ProcesosAplicacionDictionary[ProcesoAtencionCliente.ActualizacionDatos]);
                htmlCorreo = htmlCorreo.Replace("@_NOMBRE_@", pPersona.nombreCompleto);
                htmlCorreo = htmlCorreo.Replace("@_TIPO_CEDULA_@", pPersona.nomtipo_identificacion.ToLower());
                htmlCorreo = htmlCorreo.Replace("@_IDENTIFICACION_@", pPersona.identificacion);
                htmlCorreo = htmlCorreo.Replace("@_FECHA_GENERACION_@", string.Format("{0}   {1}", DateTime.Now.ToLongDateString(), DateTime.Now.ToShortTimeString()));

                string salida = "";
                bool exitoso = correoHelper.sendEmail(htmlCorreo, out salida, "Notificación - Atención al Cliente", pPersona.email_asesor);
                return salida;
            }
            catch (Exception e)
            {
                return "Error ConfigureNotification " + e.Message;
            }
        }





    }
}