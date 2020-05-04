using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web.Services;
using System.Web.UI.WebControls;
using Xpinn.Util;

namespace WebServices
{
    /// <summary>
    /// Descripción breve de WSEstadoCuenta
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class WSEstadoCuenta : System.Web.Services.WebService
    {
        #region Métodos Atención al Cliente

        [WebMethod]
        public List<Xpinn.Asesores.Entities.ProductoResumen> EstadoCuenta(Boolean pResult, string pIdentificacion, string sClave)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            //usuario = conexion.DeterminarUsuario();
            //if (usuario == null)
            //    return null;
            // Determinar datos de la persona        
            Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 ePersona = new Xpinn.FabricaCreditos.Entities.Persona1();
            ePersona.seleccionar = "Identificacion";
            ePersona.identificacion = pIdentificacion;
            ePersona.soloPersona = 1;
            ePersona = personaServicio.ConsultarPersona1Param(ePersona, usuario);
            if (ePersona == null)
                return null;
            // Validar clave de la persona
            //if (ePersona.clave != sClave)
            //    return null;
            // Consultar productos de la persona
            Xpinn.Asesores.Services.ProductoService productoServicio = new Xpinn.Asesores.Services.ProductoService();
            List<Xpinn.Asesores.Entities.ProductoResumen> lstProducto = new List<Xpinn.Asesores.Entities.ProductoResumen>();
            lstProducto = productoServicio.ListarCreditosClubAhorrador(ePersona.cod_persona.ToString(), pResult, usuario);
            // Devolver listado de productos
            return lstProducto;
        }

        [WebMethod]
        public List<Xpinn.Aportes.Entities.Aporte> ListarAportesEstadoCuenta(Int64 pCliente, Boolean pResult, string pFiltro, DateTime pFecha)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();

            Xpinn.Aportes.Services.AporteServices BOAporteServ = new Xpinn.Aportes.Services.AporteServices();
            Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 ePersona = new Xpinn.FabricaCreditos.Entities.Persona1();
            ePersona.seleccionar = "Cod_persona";
            ePersona.cod_persona = pCliente;
            ePersona.soloPersona = 1;
            //consultar si existe la persona
            ePersona = personaServicio.ConsultarPersona1Param(ePersona, usuario);
            if (ePersona == null)
                return null;
            // Consultar productos de la persona
            List<Xpinn.Aportes.Entities.Aporte> lstProductoAporte = new List<Xpinn.Aportes.Entities.Aporte>();
            lstProductoAporte = BOAporteServ.ListarAportesClubAhorradores(pCliente, pResult, pFiltro, pFecha, usuario);
            // Devolver listado de productos
            return lstProductoAporte;
        }

        [WebMethod]
        public List<Xpinn.Asesores.Entities.Comentario> ListarComentarios(Int64 pCliente, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            Xpinn.Asesores.Services.ComentarioService serviceComentario = new Xpinn.Asesores.Services.ComentarioService();
            Xpinn.Asesores.Entities.Producto producto = new Xpinn.Asesores.Entities.Producto();
            producto.Persona = new Xpinn.Asesores.Entities.Persona();
            producto.Persona.IdPersona = pCliente;
            List<Xpinn.Asesores.Entities.Comentario> lstComentarios = serviceComentario.ListarComentario(producto, usuario);

            if (lstComentarios != null)
            {
                // Devolver listado de comentarios
                return lstComentarios.Where(x => x.puedeVerAsociado == true).ToList();
            }
            else
            {
                return null;
            }
        }

        [WebMethod]
        public List<Xpinn.Asesores.Entities.ProductoResumen> EstadoCuentaSinClave(string pIdentificacion)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuario();
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
            // Consultar productos de la persona
            Xpinn.Asesores.Services.ProductoService productoServicio = new Xpinn.Asesores.Services.ProductoService();
            List<Xpinn.Asesores.Entities.ProductoResumen> lstProducto = new List<Xpinn.Asesores.Entities.ProductoResumen>();
            lstProducto = productoServicio.ListarProductosResumen(ePersona.cod_persona.ToString(), usuario);
            // Devolver listado de productos
            return lstProducto;
        }

        [WebMethod]
        public List<Xpinn.Asesores.Entities.MovimientoResumen> UltimosMovimientos(string pIdentificacion, int pNumeroTransacciones, string pClave)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuario();
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
            // Validar clave de la persona
            //if (ePersona.clave != sClave)
            //    return null;
            // Consultar productos de la persona
            Xpinn.Asesores.Services.ProductoService productoServicio = new Xpinn.Asesores.Services.ProductoService();
            List<Xpinn.Asesores.Entities.MovimientoResumen> lstProducto = new List<Xpinn.Asesores.Entities.MovimientoResumen>();
            lstProducto = productoServicio.ListarMovimientoPersonaResumen(pIdentificacion, pNumeroTransacciones, usuario);
            // Devolver listado de productos
            return lstProducto;
        }

        [WebMethod]
        public List<Xpinn.Asesores.Entities.MovimientoResumen> UltimosMovimientosSinClave(string pIdentificacion, int pNumeroTransacciones)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuario();
            if (usuario == null)
                return null;
            // Consultar productos de la persona
            Xpinn.Asesores.Services.ProductoService productoServicio = new Xpinn.Asesores.Services.ProductoService();
            List<Xpinn.Asesores.Entities.MovimientoResumen> lstProducto = new List<Xpinn.Asesores.Entities.MovimientoResumen>();
            lstProducto = productoServicio.ListarMovimientoPersonaResumen(pIdentificacion, pNumeroTransacciones, usuario);
            // Devolver listado de productos
            return lstProducto;
        }

        [WebMethod]
        public Xpinn.FabricaCreditos.Entities.Persona1 LoginAtencionCliente(string pIdentificacion, string sClave)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuario();
            if (usuario == null)
                return null;
            // Determinar datos de la persona        
            Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 ePersona = new Xpinn.FabricaCreditos.Entities.Persona1();
            ePersona.seleccionar = "Identificacion";
            ePersona.soloPersona = 1;
            ePersona.identificacion = pIdentificacion;
            ePersona = personaServicio.ConsultarPersona1Param(ePersona, usuario);
            if (ePersona == null)
                return null;
            return ePersona;
        }

        [WebMethod]
        public Xpinn.FabricaCreditos.Entities.Persona1 ActualizarDatos(Xpinn.FabricaCreditos.Entities.Persona1 pPersona)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuario();
            if (usuario == null)
                return null;
            // Definición de entidades y servicios
            Xpinn.FabricaCreditos.Services.Persona1Service PersonaService = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 Datos = new Xpinn.FabricaCreditos.Entities.Persona1();
            Datos = PersonaService.ModificarPersonaAtencionCliente(pPersona, usuario);
            // Devolver listado de productos
            return Datos;
        }

        [WebMethod(Description = "Creacion de Solicitud de Afiliación por parte de la Persona")]
        public Xpinn.Aportes.Entities.SolicitudPersonaAfi ActualizarDatosWeb(Xpinn.Aportes.Entities.SolicitudPersonaAfi pPersona, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            Xpinn.Aportes.Services.PersonaActDatosServices PersonaService = new Xpinn.Aportes.Services.PersonaActDatosServices();
            Xpinn.Aportes.Entities.SolicitudPersonaAfi Datos = new Xpinn.Aportes.Entities.SolicitudPersonaAfi();
            Datos = PersonaService.ActualizarDatosPersona(pPersona, usuario);

            return pPersona;            
        }


        [WebMethod]
        public Xpinn.FabricaCreditos.Entities.Persona1 ConsultarDatosCliente(Xpinn.FabricaCreditos.Entities.Persona1 pPersona, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;
            // Definición de entidades y servicios
            Xpinn.FabricaCreditos.Services.Persona1Service PersonaService = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 Datos = new Xpinn.FabricaCreditos.Entities.Persona1();
            Datos = PersonaService.ConsultarDatosCliente(pPersona, usuario);
            // Devolver listado de productos
            return Datos;
        }

        [WebMethod]
        public Xpinn.FabricaCreditos.Entities.Persona1 ConsultarPersona(Xpinn.FabricaCreditos.Entities.Persona1 pPersona)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            //usuario = conexion.DeterminarUsuario();
            //if (usuario == null)
            //    return null;
            // Definición de entidades y servicios
            Xpinn.FabricaCreditos.Services.Persona1Service PersonaService = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 Datos = new Xpinn.FabricaCreditos.Entities.Persona1();
            pPersona.soloPersona = 1;
            Datos = PersonaService.ConsultarPersona1(pPersona.cod_persona, usuario);
            // Devolver listado de productos
            return Datos;
        }


        [WebMethod]
        public Xpinn.Aportes.Entities.SolicitudPersonaAfi  ConsultarPersonaAfi(string filtro)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            Xpinn.Aportes.Services.SolicitudPersonaAfiServices per_service = new Xpinn.Aportes.Services.SolicitudPersonaAfiServices();
            Xpinn.Aportes.Entities.SolicitudPersonaAfi persona = new Xpinn.Aportes.Entities.SolicitudPersonaAfi();
            persona = per_service.ConsultarPersona1(filtro, usuario);
            // Devolver listado de productos
            return persona;
        }

        [WebMethod]
        public Xpinn.FabricaCreditos.Entities.Persona1 ConsultarPersonaParam(Xpinn.FabricaCreditos.Entities.Persona1 pPersona)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            //usuario = conexion.DeterminarUsuario();
            //if (usuario == null)
            //    return null;
            // Definición de entidades y servicios
            Xpinn.FabricaCreditos.Services.Persona1Service PersonaService = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 Datos = new Xpinn.FabricaCreditos.Entities.Persona1();
            Datos = PersonaService.ConsultarPersona1Param(pPersona, usuario);
            // Devolver listado de productos
            return Datos;
        }
        

        [WebMethod]
        public Xpinn.FabricaCreditos.Entities.Persona1 NotificacionConsulta(Int64 CodPersona, int opcion)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuario();
            if (usuario == null)
                return null;
            // Definición de entidades y servicios
            Xpinn.FabricaCreditos.Services.Persona1Service PersonaService = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 Datos = new Xpinn.FabricaCreditos.Entities.Persona1();

            Datos = PersonaService.ConsultarNotificacion(CodPersona, usuario, opcion);
            // Devolver listado de productos
            return Datos;
        }

        [WebMethod]
        public Xpinn.Aportes.Entities.ConsultarPersonaBasico ConsultarDatoBasicosPersona(string pEntidad, string pIdentificador, string pTipo)
        {
            Xpinn.Aportes.Entities.ConsultarPersonaBasico pResult = new Xpinn.Aportes.Entities.ConsultarPersonaBasico();

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();

            //GENERANDO VALIDACION
            bool isDefined = Enum.IsDefined(typeof(Xpinn.Util.ClienteEnt), pEntidad.ToUpper());
            if (!isDefined)
            {
                pResult.result = false;
                return pResult;
            }

            usuario = conexion.DeterminarUsuarioSinClave(pEntidad);
            if (usuario == null)
            {
                pResult.result = false;
                return pResult;
            }
            if (string.IsNullOrEmpty(pIdentificador))
            {
                pResult.result = false;
                return pResult;
            }
            if (pTipo != "Cod_persona" && pTipo != "Identificacion" && pTipo != "CodNomina")
            {
                pResult.result = false;
                return pResult;
            }


            // Definición de entidades y servicios
            Xpinn.Seguridad.Services.UsuarioService usuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
            Xpinn.FabricaCreditos.Entities.Persona1 pPersona = new Xpinn.FabricaCreditos.Entities.Persona1();
            try
            {
                Xpinn.FabricaCreditos.Services.Persona1Service PersonaService = new Xpinn.FabricaCreditos.Services.Persona1Service();
                Xpinn.FabricaCreditos.Entities.Persona1 Datos = new Xpinn.FabricaCreditos.Entities.Persona1();
                if (pTipo != null)
                {
                    Datos.seleccionar = pTipo;
                    switch (pTipo.ToUpper())
                    {
                        case "COD_PERSONA":
                            Datos.cod_persona = Convert.ToInt64(pIdentificador);
                            break;
                        case "IDENTIFICACION":
                            Datos.identificacion = pIdentificador;
                            break;
                        case "CODNOMINA":
                            Datos.cod_nomina_empleado = pIdentificador;
                            break;
                    }

                    Datos = PersonaService.ConsultarPersonaAPP(Datos, usuario);
                    if (Datos != null)
                    {
                        pResult = CruzarResultados(Datos);
                    }
                    else
                        pResult.result = false;
                }
                else
                    pResult.result = false;

            }
            catch
            {
                pResult.result = false;
            }

            return pResult;
        }


        public Xpinn.Aportes.Entities.ConsultarPersonaBasico CruzarResultados(Xpinn.FabricaCreditos.Entities.Persona1 pData)
        {
            Xpinn.Aportes.Entities.ConsultarPersonaBasico pResult = new Xpinn.Aportes.Entities.ConsultarPersonaBasico();
            if (pData.nombre == "errordedatos")
                pResult.result = false;
            else
            {
                pResult.result = true;
                pResult.cod_persona = pData.cod_persona;
                pResult.tipo_persona = pData.tipo_persona;
                if (pResult.tipo_persona == "J")
                {
                    pResult.razon_social = pData.razon_social;
                }
                else {
                    pResult.primer_nombre = pData.primer_nombre;
                    pResult.segundo_nombre = pData.segundo_nombre;
                    pResult.primer_apellido = pData.primer_apellido;
                    pResult.segundo_apellido = pData.segundo_apellido;
                }

                pResult.tipo_identificacion = pData.tipo_identificacion;
                pData.codciudadresidencia = pData.codciudadresidencia != null ? pData.codciudadresidencia : 0;
                pResult.codciudadresidencia = Convert.ToInt64(pData.codciudadresidencia);
                pResult.direccion = pData.direccion;
                pResult.telefono = pData.telefono;
                pResult.ciudadempresa = pData.ciudad;
                pResult.direccionempresa = pData.direccionempresa;
                pResult.telefonoempresa = pData.telefonoempresa;
                pResult.email = pData.email;
                pResult.estado = pData.estado;
                pResult.nomciudadresidencia = pData.nomciudad_resid;
                pResult.nomciudadempresa = pData.nomciudad_lab;
                pResult.identificacion = pData.identificacion;
                pResult.cod_oficina = pData.cod_oficina;
                pResult.fecha_nacimiento = pData.fechanacimiento != null ? Convert.ToDateTime(pData.fechanacimiento) : DateTime.MinValue;
                pResult.fechacreacion = pData.fechacreacion;
                pResult.genero = pData.sexo;
                pResult.codestadocivil = pData.codestadocivil != null ? Convert.ToInt64(pData.codestadocivil) : 0;
                pResult.clavesinencriptar = pData.clavesinecriptar;
            }
            return pResult;
        }


        [WebMethod]
        public List<Xpinn.Asesores.Entities.ProductoResumen> DatosAportesSociales(string pIdentificacion, string sClave)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuario();
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
            // Consultar productos de la persona
            Xpinn.Asesores.Services.ProductoService productoServicio = new Xpinn.Asesores.Services.ProductoService();
            List<Xpinn.Asesores.Entities.ProductoResumen> lstProducto = new List<Xpinn.Asesores.Entities.ProductoResumen>();
            lstProducto = productoServicio.ListarProductosResumen(ePersona.cod_persona.ToString(), usuario);
            // Devolver listado de productos
            return lstProducto;
        }

        [WebMethod]
        public List<Xpinn.Asesores.Entities.ProductoResumen> DatosCreditos(string pIdentificacion, string sClave)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuario();
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
            // Consultar productos de la persona
            Xpinn.Asesores.Services.ProductoService productoServicio = new Xpinn.Asesores.Services.ProductoService();
            List<Xpinn.Asesores.Entities.ProductoResumen> lstProducto = new List<Xpinn.Asesores.Entities.ProductoResumen>();
            lstProducto = productoServicio.ListarProductosResumen(ePersona.cod_persona.ToString(), usuario);
            // Devolver listado de productos
            return lstProducto;
        }

        #endregion
        
        [WebMethod(Description = "Consulta de los procesos contables existentes")]
        public List<Xpinn.Contabilidad.Entities.ProcesoContable> ConsultaProceso(Int64 pcod_ope, Int64 ptip_ope, DateTime pfecha, string pIdentificacion, string pClave)
        {
            Xpinn.Contabilidad.Services.ComprobanteService BOComprobante = new Xpinn.Contabilidad.Services.ComprobanteService();
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuario();
            if (usuario == null)
                return null;
            List<Xpinn.Contabilidad.Entities.ProcesoContable> lstData = BOComprobante.ConsultaProceso(pcod_ope, ptip_ope, pfecha, usuario);
            return lstData;
        }

        [WebMethod(Description = "Cantidad de Asociados")]
        public int ConsultarCantidadAfiliados(string pCondicion, string pIdentificacion, string pClave, int pTipoUsuario, string sec)
        {
            Xpinn.Aportes.Services.AfiliacionServices BOAfiliados = new Xpinn.Aportes.Services.AfiliacionServices();
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return 0;

            Xpinn.FabricaCreditos.Entities.Persona1 pPersona = new Xpinn.FabricaCreditos.Entities.Persona1();
            Xpinn.Seguridad.Services.UsuarioService usuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
            bool result = false;
            if (pTipoUsuario == 2)
            {
                pPersona = usuarioServicio.ValidarPersonaUsuario(pIdentificacion, pClave, usuario);
                if (pPersona != null)
                {
                    result = pPersona.cod_persona > 0 && pPersona.identificacion != null ? true : false;
                }
                else
                    return 0;
            }
            else
            {
                usuario = usuarioServicio.ValidarUsuario(pIdentificacion, pClave, "", "", usuario);
                if (usuario != null)
                {
                    result = usuario.codusuario > 0 && usuario.identificacion != null ? true : false;
                }
                else
                    return 0;
            }
            if (!result)
                return 0;
            string Condicion = pCondicion == "" || pCondicion == null ? null : pCondicion;
            int response = BOAfiliados.ConsultarCantidadAfiliados(Condicion, usuario);
            return response;
        }

        [WebMethod(Description = "Lista de Solo Afiliados")]
        public List<Xpinn.Aportes.Entities.ConsultarPersonaBasico> ListarSoloAfiliados(string pCondicion, int pIndicePagina, int pRegistrosPagina, string pIdentificacion, string pClave, int pTipoUsuario, string sec)
        {
            Xpinn.Aportes.Services.AfiliacionServices BOAfiliados = new Xpinn.Aportes.Services.AfiliacionServices();
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            Xpinn.FabricaCreditos.Entities.Persona1 pPersona = new Xpinn.FabricaCreditos.Entities.Persona1();
            Xpinn.Seguridad.Services.UsuarioService usuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
            bool result = false;
            if (pTipoUsuario == 2)
            {
                pPersona = usuarioServicio.ValidarPersonaUsuario(pIdentificacion, pClave, usuario);
                if (pPersona != null)
                {
                    result = pPersona.cod_persona > 0 && pPersona.identificacion != null ? true : false;
                }
                else
                    return null;
            }
            else
            {
                usuario = usuarioServicio.ValidarUsuario(pIdentificacion, pClave, "", "", usuario);
                if (usuario != null)
                {
                    result = usuario.codusuario > 0 && usuario.identificacion != null ? true : false;
                }
                else
                    return null;
            }
            if (!result)
                return null;
            string Condicion = pCondicion == "" || pCondicion == null ? null : pCondicion;
            List<Xpinn.Aportes.Entities.ConsultarPersonaBasico> lstResponse = BOAfiliados.ListarPersonasAfiliadasPaginado(Condicion, pIndicePagina, pRegistrosPagina, usuario);
            return lstResponse;
        }

        
        #region Métodos APP

        [WebMethod(Description = "DESCRIPCION DE PERSONA. WEB APPLICATION")]
        public Xpinn.FabricaCreditos.Entities.Persona1 ConsultarPersonaAPP(String pIdentificacion)
        {
            Xpinn.FabricaCreditos.Entities.Persona1 DatosRetorno = new Xpinn.FabricaCreditos.Entities.Persona1();
            try
            {
                Conexion conexion = new Conexion();
                Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
                // Definición de entidades y servicios
                Xpinn.FabricaCreditos.Services.Persona1Service PersonaService = new Xpinn.FabricaCreditos.Services.Persona1Service();
                Xpinn.FabricaCreditos.Entities.Persona1 Datos = new Xpinn.FabricaCreditos.Entities.Persona1();
                Datos.seleccionar = "Identificacion";
                Datos.identificacion = pIdentificacion;
                Datos.soloPersona = 1;
                DatosRetorno = PersonaService.ConsultarPersonaAPP(Datos, usuario);
                DatosRetorno.rptaingreso = true;
                // Devolver listado de productos                
            }
            catch
            {
                DatosRetorno.rptaingreso = false;
            }
            return DatosRetorno;
        }

        [WebMethod(Description = "Validar existencia de la persona")]
        public Xpinn.FabricaCreditos.Entities.Persona1 ConsultarPersonaExiste(String pIdentificacion)
        {
            Xpinn.FabricaCreditos.Entities.Persona1 DatosRetorno = new Xpinn.FabricaCreditos.Entities.Persona1();
            try
            {
                Conexion conexion = new Conexion();
                Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
                // Definición de entidades y servicios
                Xpinn.FabricaCreditos.Services.Persona1Service PersonaService = new Xpinn.FabricaCreditos.Services.Persona1Service();
                Xpinn.FabricaCreditos.Entities.Persona1 Datos = new Xpinn.FabricaCreditos.Entities.Persona1();
                Datos.seleccionar = "Identificacion";
                Datos.identificacion = pIdentificacion;
                Datos.soloPersona = 1;
                DatosRetorno = PersonaService.ConsultarPersonaAPP(Datos, usuario);
                if(DatosRetorno.cod_persona > 0)
                    DatosRetorno.rptaingreso = true;
            }
            catch
            {
                DatosRetorno.rptaingreso = false;
            }
            return DatosRetorno;
        }

        [WebMethod(Description = "REGISTRO DE ASOCIADO POR WEB Y APP")]
        public Xpinn.Seguridad.Entities.PersonaUsuario CrearUsuarioPerAPP(Int64 pCodPersona, string pClave, string pNombres, string pApellidos,
            string pEmail, string pFechaNac)
        {
            Boolean rpta = false;
            Xpinn.Seguridad.Entities.PersonaUsuario vDatos = new Xpinn.Seguridad.Entities.PersonaUsuario();
            try
            {
                Conexion conexion = new Conexion();
                Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
                Xpinn.Seguridad.Services.UsuarioService UsuarioService = new Xpinn.Seguridad.Services.UsuarioService();

                //Validar si existe el usuario a crear
                Xpinn.Seguridad.Entities.PersonaUsuario pValida = new Xpinn.Seguridad.Entities.PersonaUsuario();
                pValida = UsuarioService.ConsultarPersonaUsuario(pCodPersona, usuario);
                if (pValida.cod_persona == 0)
                {
                    // Definición de entidades y servicios
                    vDatos.idacceso = 0;
                    vDatos.cod_persona = pCodPersona;
                    vDatos.clave = pClave;
                    vDatos.fecha_creacion = DateTime.Now;
                    vDatos.fecultmod = DateTime.Now;
                    vDatos.nombres = pNombres;
                    vDatos.apellidos = pApellidos;
                    vDatos.email = pEmail;
                    vDatos.fechanacimiento = Convert.ToDateTime(pFechaNac);
                    rpta = UsuarioService.CrearPersonaUsuario(vDatos, usuario);
                    vDatos.rpta = rpta;
                    vDatos.mensaje = "";
                    if (rpta)
                    {
                        try
                        {
                            bool resultNotification = ConfigureNotification.SendNotifification(ProcesoAtencionCliente.RegistroAsociado, usuario, pCodPersona);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                else
                {
                    vDatos.mensaje = "El usuario que desea registrar ya existe";
                    vDatos.rpta = false;
                }
            }
            catch (Exception ex)
            {
                vDatos.mensaje = ex.Message;
                vDatos.rpta = false;
            }
            return vDatos;
        }

        [WebMethod]
        public Boolean ActualizarDatosPerAPP(Int64 pCodPersona, string pDireccion, string pTelefono, Int64 pCod_ciudad, string pDirecLAB,
            string pTeleLAB, Int64 pCod_CiudadLAB, string pCelular, string pEmail)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();


            // Definición de entidades y servicios
            Xpinn.FabricaCreditos.Services.Persona1Service PersonaService = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 Datos = new Xpinn.FabricaCreditos.Entities.Persona1();
            Datos.cod_persona = pCodPersona;
            Datos.direccion = pDireccion;
            Datos.telefono = pTelefono;
            Datos.codciudadresidencia = pCod_ciudad;
            Datos.direccionempresa = pDirecLAB;
            Datos.telefonoempresa = pTeleLAB;
            Datos.ciudad = pCod_CiudadLAB;
            Datos.celular = pCelular;
            Datos.email = pEmail;
            Boolean rpta = false;
            rpta = PersonaService.ModificarPersonaAPP(Datos, usuario);
            if (rpta)
            {
                try
                {
                    bool resultNotification = ConfigureNotification.SendNotifification(ProcesoAtencionCliente.ActualizacionDatos, usuario, pCodPersona);
                }
                catch (Exception)
                {
                }
            }
            // Devolver listado de productos
            return rpta;
        }

        [WebMethod]
        public Boolean ActualizarDatosPersona(string pEntidad, Int64 pCodPersona, string pDireccion, string pTelefono, Int64 pCod_ciudad, string pDirecLAB,
            string pTeleLAB, Int64 pCod_CiudadLAB, string pCelular, string pEmail, string pUsuario, string pClave, int pTipoUsuario)
        {
            Xpinn.Seguridad.Services.UsuarioService usuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioSinClave(pEntidad);
            if (usuario == null)
                return false;
            Xpinn.FabricaCreditos.Entities.Persona1 pPersona = new Xpinn.FabricaCreditos.Entities.Persona1();
            if (pTipoUsuario == 2)
            {
                pPersona = usuarioServicio.ValidarPersonaUsuario(pUsuario, pClave, usuario);
                if (pPersona.cod_persona == 0)
                    return false;
            }
            else
            {
                usuario = usuarioServicio.ValidarUsuario(pUsuario, pClave, "", "", usuario);
                if (usuario.codusuario == 0)
                    return false;
            }
            
            // Definición de entidades y servicios
            Xpinn.FabricaCreditos.Services.Persona1Service PersonaService = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 Datos = new Xpinn.FabricaCreditos.Entities.Persona1();
            Datos.cod_persona = pCodPersona;
            Datos.direccion = pDireccion;
            Datos.telefono = pTelefono;
            Datos.codciudadresidencia = pCod_ciudad;
            Datos.direccionempresa = pDirecLAB;
            Datos.telefonoempresa = pTeleLAB;
            Datos.ciudad = pCod_CiudadLAB;
            Datos.celular = pCelular;
            Datos.email = pEmail;
            Boolean rpta = false;
            rpta = PersonaService.ModificarPersonaAPP(Datos, usuario);
            if (rpta)
            {
                try
                {
                    bool resultNotification = ConfigureNotification.SendNotifification(ProcesoAtencionCliente.ActualizacionDatos, usuario, pCodPersona);
                }
                catch (Exception)
                {
                }
            }
            // Devolver listado de productos
            return rpta;
        }

        [WebMethod]
        public List<Xpinn.Caja.Entities.Ciudad> ListadoCiudadesAPP()
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();

            // Definición de entidades y servicios
            Xpinn.Caja.Services.CiudadService CiudadService = new Xpinn.Caja.Services.CiudadService();
            List<Xpinn.Caja.Entities.Ciudad> lstCiudades = new List<Xpinn.Caja.Entities.Ciudad>();
            Xpinn.Caja.Entities.Ciudad Ciudad = new Xpinn.Caja.Entities.Ciudad();
            lstCiudades = CiudadService.ListarCiudad(Ciudad, usuario);
            // Devolver listado de Ciudades
            return lstCiudades;
        }

        [WebMethod]
        public List<Xpinn.Asesores.Entities.ProductoPersonaAPP> ListarProductosXPersonaAPP(Int64 codPersona)
        {
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 ePersona = new Xpinn.FabricaCreditos.Entities.Persona1();
            ePersona.seleccionar = "Cod_persona";
            ePersona.cod_persona = codPersona;
            ePersona.soloPersona = 1;
            ePersona = personaServicio.ConsultarPersona1Param(ePersona, usuario);
            if (ePersona == null)
                return null;
            Xpinn.Asesores.Services.ProductoService productoServicio = new Xpinn.Asesores.Services.ProductoService();
            List<Xpinn.Asesores.Entities.ProductoPersonaAPP> lstProducto = new List<Xpinn.Asesores.Entities.ProductoPersonaAPP>();
            lstProducto = productoServicio.ListarProductosXPersonaAPP(codPersona, usuario);
            // Devolver listado de productos
            return lstProducto;
        }

        #endregion

        #region METODOS ADICIONADOS DE ATENCION AL CLIENTE

        [WebMethod]
        public List<Xpinn.Comun.Entities.ListaDesplegable> PoblarListaDesplegable(string pTabla, string pColumnas, string pCondicion, string pOrden, string sec)
        {
            List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
            Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
            Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario pUsuario = new Xpinn.Util.Usuario();
            pUsuario = conexion.DeterminarUsuarioOficina(sec);
            if (pUsuario == null)
            {
                return null;
            }

            plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, pUsuario);
            return plista;
        }

        [WebMethod]
        public List<Xpinn.FabricaCreditos.Entities.Credito> RealizarPreAnalisis(DateTime pfecha, Int64 pCodPersona, decimal pDisponible, Int64 pNumeroCuotas, decimal pMontoSolicitado, Int32 pCodPeriodicidad, bool pEducativo)
        {
            Xpinn.FabricaCreditos.Services.CreditoService CreditoServ = new Xpinn.FabricaCreditos.Services.CreditoService();
            List<Xpinn.FabricaCreditos.Entities.Credito> lstPreAnalisis = new List<Xpinn.FabricaCreditos.Entities.Credito>();
            Xpinn.Util.Usuario pUsuario = new Xpinn.Util.Usuario();

            lstPreAnalisis = CreditoServ.RealizarPreAnalisis(true, pfecha, pCodPersona, pDisponible, pNumeroCuotas, pMontoSolicitado, pCodPeriodicidad, pEducativo, pUsuario);
            return lstPreAnalisis;
        }

        [WebMethod]
        public Xpinn.Comun.Entities.General consultarsalariominimo(Int64? pCod)
        {
            Xpinn.Comun.Entities.General pEntidad = new Xpinn.Comun.Entities.General();
            Xpinn.FabricaCreditos.Services.Persona1Service CreditoServ = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.Util.Usuario pUsuario = new Xpinn.Util.Usuario();

            pEntidad = CreditoServ.consultarsalariominimo(pCod, pUsuario);
            return pEntidad;
        }


        [WebMethod(Description = "MODIFICACION DE CLAVE. WEB APPLICACTION")]
        public Xpinn.Seguridad.Entities.RespuestaApp CambiarClavePersona(Int64 pCod_Persona,  string pClaveAntigua, string pNuevaClave, string sec)
        {
            Xpinn.Seguridad.Entities.RespuestaApp pEntidad = new Xpinn.Seguridad.Entities.RespuestaApp();
            Xpinn.Seguridad.Services.UsuarioService usuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
            string pError = string.Empty;
            bool rpta = false;
            try
            {
                Conexion conexion = new Conexion();
                Xpinn.Util.Usuario pUsuario = new Xpinn.Util.Usuario();
                pUsuario = conexion.DeterminarUsuarioOficina(sec);
                if (pUsuario == null)
                    return null;

                Xpinn.FabricaCreditos.Entities.Persona1 DatosRetorno = new Xpinn.FabricaCreditos.Entities.Persona1();
                Xpinn.FabricaCreditos.Services.Persona1Service PersonaService = new Xpinn.FabricaCreditos.Services.Persona1Service();
                Xpinn.FabricaCreditos.Entities.Persona1 Datos = new Xpinn.FabricaCreditos.Entities.Persona1();
                Datos.seleccionar = "Cod_persona";
                Datos.cod_persona = pCod_Persona;
                Datos.soloPersona = 1;
                DatosRetorno = PersonaService.ConsultarPersonaAPP(Datos, pUsuario);

                string pIdenti = DatosRetorno.identificacion;
                rpta = usuarioServicio.CambiarClavePersona(pIdenti, pClaveAntigua, pNuevaClave, ref pError);
                if (rpta == false)
                {
                    if (!string.IsNullOrEmpty(pError.Trim()))
                        pEntidad.rpta = false;
                    pEntidad.Mensaje = pError == "" ? "Se generó un error al realizar la petición del método" : pError;
                }
                else
                {
                    if (pError.Trim() != "")
                    {
                        pEntidad.rpta = false;
                        pEntidad.Mensaje = pError;
                    }
                    else
                    {
                        pEntidad.rpta = true;
                    }
                }
                    
            }
            catch (Exception ex)
            {
                pEntidad.rpta = false;
                pEntidad.Mensaje = ex.Message;
            }
            return pEntidad;
        }

        [WebMethod]
        public List<Int32> ListarAniosPersonaCertificado(Int64 pCodAsociado)
        {
            List<Int32> lstAnios = new List<Int32>();
            Xpinn.Aportes.Services.Persona_infcertificadoServices AniosServicio = new Xpinn.Aportes.Services.Persona_infcertificadoServices();
            Xpinn.Util.Usuario pUsuario = new Xpinn.Util.Usuario();
            lstAnios = AniosServicio.ListarAniosPersonaCertificado(pCodAsociado, pUsuario);
            return lstAnios;
        }


        [WebMethod]
        public List<Xpinn.Aportes.Entities.Persona_infcertificado> ListarInformacionCertificado(Int64 pCodAsociado, string pFiltro)
        {
            Xpinn.Aportes.Services.Persona_infcertificadoServices InformacionCertService = new Xpinn.Aportes.Services.Persona_infcertificadoServices();
            List<Xpinn.Aportes.Entities.Persona_infcertificado> lstInformacion = new List<Xpinn.Aportes.Entities.Persona_infcertificado>();
            Xpinn.Aportes.Entities.Persona_infcertificado pEntidad = new Xpinn.Aportes.Entities.Persona_infcertificado();
            Xpinn.Util.Usuario pUsuario = new Xpinn.Util.Usuario();
            pEntidad.cod_persona = pCodAsociado;
            lstInformacion = InformacionCertService.ListarInformacionCertificado(pEntidad, pFiltro, pUsuario);

            return lstInformacion;
        }


        [WebMethod(Description = "GRABACION DE SOLICITUD DE ACTUALIZACION")]
        public Xpinn.Aportes.Entities.PersonaActualizacion InsertarPersonaActualizacion(Xpinn.Aportes.Entities.PersonaActualizacion pPersona, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;
            // Definición de entidades y servicios
            Xpinn.Aportes.Services.ParametrosAfiliacionServices PersonaService = new Xpinn.Aportes.Services.ParametrosAfiliacionServices();
            Xpinn.Aportes.Entities.PersonaActualizacion Datos = new Xpinn.Aportes.Entities.PersonaActualizacion();
            Datos = PersonaService.CrearPersona_Actualizacion(pPersona, usuario);
            // Devolver listado de productos
            return Datos;
        }

        [WebMethod(Description = "MODIFICACION PRODUCTO ATENCION WEB - CUOTA APORTE")]
        public Xpinn.Aportes.Entities.Aporte InsertarSolicitudCambioCuotaAportes(Xpinn.Aportes.Entities.Aporte pAporte, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            Xpinn.Aportes.Services.AporteServices BOAporteServ = new Xpinn.Aportes.Services.AporteServices();
            pAporte = BOAporteServ.CrearNovedadCambio(pAporte, usuario);

            try
            {
                bool resultNotification = ConfigureNotification.SendNotifification(ProcesoAtencionCliente.ModificacionProducto, usuario, pAporte.cod_persona);
            }
            catch (Exception)
            {
            }

            return pAporte;
        }

        [WebMethod(Description = "CREACIÓN DE LA SOLICITUD DE RETIRO DEL ASOCIADO")]
        public int InsertarSolicitudRetiro(Xpinn.FabricaCreditos.Entities.Persona1 pPersona, string sec)
        {            
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return 0;

            Xpinn.FabricaCreditos.Services.Persona1Service BOPersona1Serv = new Xpinn.FabricaCreditos.Services.Persona1Service();
            int retorno = BOPersona1Serv.CrearSolicitudRetiro(pPersona, usuario);
            if (retorno > 0)
            {
                try
                {
                    bool resultNotification = ConfigureNotification.SendNotifification(ProcesoAtencionCliente.SolicitudRetiroAsociado, usuario, pPersona.cod_persona);
                }
                catch (Exception)
                {
                }
            }

            return retorno;
        }

        [WebMethod(Description = "CREACIÓN DE LA SOLICITUD DE RETIRO DEL ASOCIADO")]
        public void InsertarRespuestasRetiro(List<Xpinn.FabricaCreditos.Entities.Persona1> pPersona, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return;

            Xpinn.FabricaCreditos.Services.Persona1Service BOPersona1Serv = new Xpinn.FabricaCreditos.Services.Persona1Service();
            BOPersona1Serv.InsertarRespuestasRetiro(pPersona, usuario);
            return;
        }


        [WebMethod]
        public bool? ValidarFechaSolicitudCambio(Xpinn.Aportes.Entities.Aporte pAporte, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            Xpinn.Aportes.Services.AporteServices BOAporteServ = new Xpinn.Aportes.Services.AporteServices();

            bool? valido = BOAporteServ.ValidarFechaSolicitudCambio(pAporte, usuario);

            return valido;
        }

        [WebMethod]
        public string ValidarAporte(Xpinn.Aportes.Entities.Aporte pAporte, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            Xpinn.Aportes.Services.AporteServices BOAporteServ = new Xpinn.Aportes.Services.AporteServices();

            string valido = BOAporteServ.ValidarAporte(pAporte, usuario);

            return valido;
        }


        [WebMethod(Description="METODO USADO PARA SOLICITAR ACTUALIZACION DE DATOS POR LA APP")]
        public Xpinn.Seguridad.Entities.RespuestaApp InsertarPersonaActualizacionAPP(Int64 pCod_Persona, string pPrimerNombre, string pSegundoNombre, string pPrimerApellido, string pSegundoApellido,
            Int64 pCodCiuRedisend, string pDireccion, string pTelefono, Int32 pCodCiuEmpresa, string pDirecEmpresa, string pTelefEmpresa, string pEmail, string pCelular, string sec)
        {
            Xpinn.Seguridad.Entities.RespuestaApp pEntidadRpta = new Xpinn.Seguridad.Entities.RespuestaApp();

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
            {
                pEntidadRpta.rpta = false;
                pEntidadRpta.Mensaje = "Se generó un error al realizar la conexión.";
                return pEntidadRpta;
            }
            Xpinn.Aportes.Entities.PersonaActualizacion pPersona = new Xpinn.Aportes.Entities.PersonaActualizacion();
            pPersona.idconsecutivo = 0;
            if (pCod_Persona != 0)
                pPersona.cod_persona = pCod_Persona;
            else
            {
                pEntidadRpta.rpta = false;
                pEntidadRpta.Mensaje = "El código de la persona es inválido, por favor verifique los datos enviados.";
                return pEntidadRpta;
            }
            Xpinn.FabricaCreditos.Entities.Persona1 pEntidad = new Xpinn.FabricaCreditos.Entities.Persona1();
            Xpinn.FabricaCreditos.Services.Persona1Service PersonaConsultaService = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 Datos = new Xpinn.FabricaCreditos.Entities.Persona1();
            pEntidad.soloPersona = 1;
            pEntidad.cod_persona = pCod_Persona;

            try
            {
                Datos = PersonaConsultaService.ConsultarPersona1(pEntidad.cod_persona, usuario);

                pPersona.primer_nombre = string.IsNullOrWhiteSpace(pPrimerNombre) ? null : pPrimerNombre.ToUpper().Trim();
                pPersona.segundo_nombre = string.IsNullOrWhiteSpace(pSegundoNombre) ? null : pSegundoNombre.ToUpper().Trim();
                pPersona.primer_apellido = string.IsNullOrWhiteSpace(pPrimerApellido) ? null : pPrimerApellido.ToUpper().Trim();
                pPersona.segundo_apellido = string.IsNullOrWhiteSpace(pSegundoApellido) ? null : pSegundoApellido.ToUpper().Trim();

                pPersona.codciudadresidencia = pCodCiuRedisend != 0 ? pCodCiuRedisend : 0;
                pPersona.direccion = pDireccion != null ? pDireccion : null;
                pPersona.telefono = pTelefono != null ? pTelefono : null;

                pPersona.ciudadempresa = pCodCiuEmpresa != 0 ? pCodCiuEmpresa : 0;
                pPersona.direccionempresa = pDirecEmpresa != null ? pDirecEmpresa : null;
                pPersona.telefonoempresa = pTelefEmpresa != null ? pTelefEmpresa : null;
                pPersona.email = pEmail != null ? pEmail : null;
                pPersona.estado = 0;
                pPersona.celular = pCelular;
                // Definición de entidades y servicios
                Xpinn.Aportes.Services.ParametrosAfiliacionServices PersonaService = new Xpinn.Aportes.Services.ParametrosAfiliacionServices();
                Xpinn.Aportes.Entities.PersonaActualizacion DatosActualizacion = new Xpinn.Aportes.Entities.PersonaActualizacion();
                
                DatosActualizacion = PersonaService.CrearPersona_Actualizacion(pPersona, usuario);
                pEntidadRpta.Mensaje = "OK";
                pEntidadRpta.rpta = true;
            }
            catch (Exception ex)
            {
                pEntidadRpta.rpta = false;
                pEntidadRpta.Mensaje = ex.Message;
            }

            // Devolver listado de productos
            return pEntidadRpta;
        }

        [WebMethod]
        public Xpinn.Aportes.Entities.PersonaActualizacion ConsultarPersona_actualizacion(Int64 pId, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;
            // Definición de entidades y servicios
            Xpinn.Aportes.Services.ParametrosAfiliacionServices PersonaService = new Xpinn.Aportes.Services.ParametrosAfiliacionServices();
            Xpinn.Aportes.Entities.PersonaActualizacion Datos = new Xpinn.Aportes.Entities.PersonaActualizacion();
            Datos = PersonaService.ConsultarPersona_actualizacion(pId, usuario);
            // Devolver listado de productos
            return Datos;
        }

        [WebMethod(Description = "Listado de Acodeudados por asociado")]
        public List<Xpinn.Asesores.Entities.Acodeudados> ListarAcodeudadoss(Xpinn.Asesores.Entities.Cliente pCliente)
        {
            Xpinn.Asesores.Services.AcodeudadoService BOAcodeudados = new Xpinn.Asesores.Services.AcodeudadoService();
            List<Xpinn.Asesores.Entities.Acodeudados> lstAcodeudados = new List<Xpinn.Asesores.Entities.Acodeudados>();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();

            lstAcodeudados = BOAcodeudados.ListarAcodeudadoss(pCliente, usuario);
            return lstAcodeudados;
        }


        [WebMethod(Description = "Simula plan de pagos sin tomar cuenta lineas")]
        public List<Xpinn.FabricaCreditos.Entities.DatosPlanPagos> SimularPlanPagosInterno(Xpinn.FabricaCreditos.Entities.Simulacion datosApp)
        {
            Xpinn.FabricaCreditos.Services.SimulacionService SimulacionServicio = new Xpinn.FabricaCreditos.Services.SimulacionService();
            List<Xpinn.FabricaCreditos.Entities.DatosPlanPagos> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.DatosPlanPagos>();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();

            lstConsulta = SimulacionServicio.SimularPlanPagosInterno(datosApp, usuario);
            return lstConsulta;
        }


        [WebMethod(Description = "Consulta del Correo de la Persona- Restablecer contraseña")]
        public Xpinn.Seguridad.Entities.PersonaUsuario ConsultarPersonaUsuarioGeneral(string pEmail, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            if(string.IsNullOrEmpty(pEmail))
                return null;

            Xpinn.Seguridad.Services.UsuarioService BOPersonaService = new Xpinn.Seguridad.Services.UsuarioService();
            Xpinn.Seguridad.Entities.PersonaUsuario pEntidad = new Xpinn.Seguridad.Entities.PersonaUsuario();
            string pFiltro = " WHERE TRIM(LOWER(EMAIL)) = '" + pEmail.Trim().ToLower() + "'";

            pEntidad = BOPersonaService.ConsultarPersonaUsuarioGeneral(pEntidad, usuario, pFiltro);
            return pEntidad;
        }


        [WebMethod(Description = "Creacion de Solicitud de Afiliación por parte de la Persona")]
        public Xpinn.Aportes.Entities.SolicitudPersonaAfi CrearSolicitudPersonaAfi(Xpinn.Aportes.Entities.SolicitudPersonaAfi pSolicitudPersonaAfi, int pOpcion, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;
            Xpinn.Aportes.Services.SolicitudPersonaAfiServices BOAfiliacion = new Xpinn.Aportes.Services.SolicitudPersonaAfiServices();
            Xpinn.Aportes.Entities.SolicitudPersonaAfi pEntidad = new Xpinn.Aportes.Entities.SolicitudPersonaAfi();

            pEntidad = BOAfiliacion.CrearSolicitudPersonaAfi(pSolicitudPersonaAfi, usuario, pOpcion);

            if(pEntidad.id_persona > 0)
            {
                if(pOpcion == 8)
                {
                    //Si se creó almacena el registro de control
                    Xpinn.Aportes.Entities.ParametrizacionProcesoAfiliacion control = new Xpinn.Aportes.Entities.ParametrizacionProcesoAfiliacion();
                    control.numero_solicitud = Convert.ToInt32(pEntidad.id_persona);
                    control.identificacion = Convert.ToInt64(pEntidad.identificacion);
                    control.cod_persona = 0;
                    control.cod_proceso = 1;
                    var host = Dns.GetHostEntry(Dns.GetHostName());
                    try
                    {
                        foreach (var ip in host.AddressList)
                        {
                            if (ip.AddressFamily == AddressFamily.InterNetwork)
                            {
                                control.ip_local = ip.ToString();
                                break;
                            }
                        }
                    }
                    catch
                    {
                        control.ip_local = "";
                    }
                    try
                    {
                        //Guarda el control de afiliación
                        Xpinn.Aportes.Services.AfiliacionServices afi = new Xpinn.Aportes.Services.AfiliacionServices();
                        afi.controlRutaAfiliacion(control, usuario);

                        // RECUPERANDO DATOS DEL CORREO SERVER
                        string URLWebServices = ConfigurationManager.AppSettings["URLWebServices"] as string;
                        string pNameCompany = ConfigurationManager.AppSettings["NameCompany"] != null ? ConfigurationManager.AppSettings["NameCompany"].ToString() : "Atención al Cliente";
                        string correoApp = ConfigurationManager.AppSettings["CorreoServidor"] as string;
                        string claveCorreoApp = ConfigurationManager.AppSettings["Clave"] as string;
                        string hosting = ConfigurationManager.AppSettings["Hosting"] as string;
                        int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["Puerto"].ToString());
                        string UrlBase = Path.Combine(URLWebServices, "Files", "Imagenes", "logoEmpresa.jpg");
                        string pSubject = "Solicitud de Afiliación";

                        // RECUPERANDO DATOS DE HTML
                        string fileName = "SendEmailNotification.txt";
                        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", fileName);
                        string htmlCorreo = File.ReadAllText(path);

                        CorreoHelper correoHelper = new CorreoHelper(pEntidad.email, correoApp, claveCorreoApp);
                        string UrlImageApp = Path.Combine(URLWebServices, "Files", "Imagenes", "AtencionWeb.png");
                        htmlCorreo = htmlCorreo.Replace("@_URL_IMAGE_@", UrlImageApp);
                        htmlCorreo = htmlCorreo.Replace("@_PROCESO_GENERADO_@", "SOLICITUD DE AFILIACIÓN");
                        string pNombre = string.Format("{0} {1} {2} {3}", pSolicitudPersonaAfi.primer_apellido, pSolicitudPersonaAfi.segundo_apellido, pSolicitudPersonaAfi.primer_nombre, pSolicitudPersonaAfi.segundo_nombre);
                        htmlCorreo = htmlCorreo.Replace("@_NOMBRE_@", pNombre);
                        htmlCorreo = htmlCorreo.Replace("@_TIPO_CEDULA_@", "");
                        htmlCorreo = htmlCorreo.Replace("@_IDENTIFICACION_@", pEntidad.identificacion);
                        htmlCorreo = htmlCorreo.Replace("@_FECHA_GENERACION_@", string.Format("{0}   {1}", DateTime.Now.ToLongDateString(), DateTime.Now.ToShortTimeString()));

                        ///Determina a quien enviar correos según parametrización
                        string copia = "", copia2 = "", salida = "";
                        if (pEntidad.envia_asociado == 1)
                        {
                            if (pEntidad.envia_asesor == 1 && !string.IsNullOrWhiteSpace(pEntidad.email_asesor))
                                copia = pEntidad.email_asesor;
                            if (!string.IsNullOrWhiteSpace(pEntidad.envia_otro))
                                copia2 = pEntidad.envia_otro;
                        }
                        else
                        if (pEntidad.envia_asesor == 1 && !string.IsNullOrWhiteSpace(pEntidad.email_asesor))
                        {
                            correoHelper._correoDestinatario = pEntidad.email_asesor;
                            if (!string.IsNullOrWhiteSpace(pEntidad.envia_otro))
                                copia = pEntidad.envia_otro;
                        }
                        else
                        if (!string.IsNullOrWhiteSpace(pEntidad.envia_otro))
                        {
                            correoHelper._correoDestinatario = pEntidad.envia_otro;
                        }
                        bool resultNotification = correoHelper.sendEmail(htmlCorreo, out salida, pSubject, copia, copia2);
                    }
                    catch (Exception ex)
                    {
                        pEntidad.mensaje_error = ex.Message.ToString();
                    }
                }
                              
            }
            return pEntidad;            
        }


        [WebMethod]
        public string ObtenerFormato(string pVariable, int id_formato, string sec)
        {
            Xpinn.Aportes.Services.FormatoDocumentoServices BOFormato = new Xpinn.Aportes.Services.FormatoDocumentoServices();
            string documento = "";

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            documento = BOFormato.ObtenerDocumento(Convert.ToInt64(id_formato), pVariable, usuario);
            return documento;
            
        }

        [WebMethod]
        public void guardarPersonaTema(List<Xpinn.FabricaCreditos.Entities.Persona1> lstTemas, string sec)
        {
            Xpinn.Aportes.Services.SolicitudPersonaAfiServices BOAfiliacion = new Xpinn.Aportes.Services.SolicitudPersonaAfiServices();            

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario != null)
                BOAfiliacion.guardarPersonaTema(lstTemas, usuario);
        }


        [WebMethod(Description = "Creacion de Solicitud de Afiliación con datos básicos")]
        public Xpinn.Seguridad.Entities.RespuestaApp SolicitudAfiliacionPersona(string pEntidad, int pTipoIdentificacion, string pIdentificación, string pPrimer_nombre, string pSegundo_nombre, string pPrimer_apellido,string pSegundo_apellido,
            string pSexo, Int64 pEstadoCivil, string pDireccion, Int64 pCodCiudadResid, string pEmail, string pDirecLab, string pTelefLab, Int64 pCodCiudadLab)
        {
            Xpinn.Seguridad.Entities.RespuestaApp pResult = new Xpinn.Seguridad.Entities.RespuestaApp();
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioSinClave(pEntidad);
            if (usuario == null)
                return null;
            
            Xpinn.Aportes.Services.SolicitudPersonaAfiServices BOAfiliacion = new Xpinn.Aportes.Services.SolicitudPersonaAfiServices();
            Xpinn.Aportes.Entities.SolicitudPersonaAfi pData = new Xpinn.Aportes.Entities.SolicitudPersonaAfi();
            //pendiente de asignacion de datos
            pData.fecha_creacion = DateTime.Now;
            pData.tipo_identificacion = pTipoIdentificacion;
            pData.identificacion = pIdentificación;
            pData.primer_nombre = pPrimer_nombre;
            pData.segundo_nombre = pSegundo_nombre;
            pData.primer_apellido = pPrimer_apellido;
            pData.segundo_apellido = pSegundo_apellido;
            pData.sexo = pSexo;
            pData.codestadocivil = pEstadoCivil;
            pData.direccion = pDireccion;
            pData.ciudad = pCodCiudadResid;
            pData.email = pEmail;
            pData.direccion_empresa = pDirecLab;
            pData.telefono_empresa = pTelefLab;
            pData.ciudad_empresa = pCodCiudadLab;
            try
            {
                pData = BOAfiliacion.CrearSolicitudPersonaAfi(pData, usuario, 1);
                pResult.Mensaje = "Se genero un error al grabar la solicitud de Afiliación";
                pResult.rpta = false;
                if (pData != null)
                {
                    if (pData.id_persona > 0)
                    {
                        pResult.Mensaje = "";
                        pResult.valorRpta = pData.id_persona.ToString();
                        pResult.rpta = true;
                    }
                }
            }
            catch (Exception ex)
            {
                pResult.Mensaje = ex.Message;
                pResult.rpta = false;
            }
            return pResult;
        }


        [WebMethod]
        public List<Xpinn.Servicios.Entities.Servicio> ListarServiciosClubAhorrador(Int64 pCodPersona, string pFiltro, Boolean pSinClubAhorrador, string pIdentificacion, string pClave, string sec)
        {
            Xpinn.Servicios.Services.AprobacionServiciosServices BOServicios = new Xpinn.Servicios.Services.AprobacionServiciosServices();
            List<Xpinn.Servicios.Entities.Servicio> lstServicios = new List<Xpinn.Servicios.Entities.Servicio>();

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            lstServicios = BOServicios.ListarServiciosClubAhorrador(pCodPersona, pFiltro, pSinClubAhorrador, usuario);
            return lstServicios;
        }


        [WebMethod]
        public Xpinn.Comun.Entities.General ConsultarGeneral(Int64 pId, string pIdentificacion, string pClave, string sec)
        {
            Xpinn.Comun.Services.GeneralService ServiceGeneral = new Xpinn.Comun.Services.GeneralService();
            Xpinn.Comun.Entities.General pEntidad = new Xpinn.Comun.Entities.General();

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            pEntidad = ServiceGeneral.ConsultarGeneral(pId, usuario);
            return pEntidad;
        }


        [WebMethod]
        public bool ExisteRegistrosEmail(string pFiltro, string sec)
        {
            Xpinn.FabricaCreditos.Entities.Persona1 pEntidad = new Xpinn.FabricaCreditos.Entities.Persona1();
            Xpinn.Seguridad.Services.UsuarioService ServiceUsuario = new Xpinn.Seguridad.Services.UsuarioService();

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return false;
            bool rpta = false;
            try
            {
                pEntidad = ServiceUsuario.ConsultaPersonaAcceso(pFiltro, usuario);
                if (pEntidad != null)
                {
                    if (pEntidad.rptaingreso == true)
                        rpta = true;
                }
            }
            catch
            {
                rpta = false;
            }
            
            return rpta;
        }

        #endregion

        #region Extacto_Masivo
        //Para el extracto de recaudos masivos

        [WebMethod(Description = "Listar Empresas Recaudos")]
        public List<Xpinn.Tesoreria.Entities.EmpresaRecaudo> ListarEmpresaRecaudo(Xpinn.Tesoreria.Entities.EmpresaRecaudo pEmpresaRecaudo, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            Xpinn.Tesoreria.Services.RecaudosMasivosService WSRecaudosMasivos = new Xpinn.Tesoreria.Services.RecaudosMasivosService();
            List<Xpinn.Tesoreria.Entities.EmpresaRecaudo> lstResponse = WSRecaudosMasivos.ListarEmpresaRecaudo(pEmpresaRecaudo, usuario);
            return lstResponse;
        }
        
        [WebMethod(Description = "Recaudos masivos")]
        public List<Xpinn.Tesoreria.Entities.RecaudosMasivos> ListarRecaudoExtracto(Xpinn.Tesoreria.Entities.RecaudosMasivos pRecaudosMasivos, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            Xpinn.Tesoreria.Services.RecaudosMasivosService WSRecaudosMasivos = new Xpinn.Tesoreria.Services.RecaudosMasivosService();
            List<Xpinn.Tesoreria.Entities.RecaudosMasivos> lstResponse = WSRecaudosMasivos.ListarRecaudoExtracto(pRecaudosMasivos, usuario);
            return lstResponse;
        }

        [WebMethod(Description = "Detalle del Recaudo Masivo ")]
        public List<Xpinn.Tesoreria.Entities.RecaudosMasivos> ListarDetalleRecaudoConsultaExtracto(int pNumeroRecaudo, string estadoNom, bool bDetallado, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            Xpinn.Tesoreria.Services.RecaudosMasivosService WSRecaudosMasivos = new Xpinn.Tesoreria.Services.RecaudosMasivosService();
            List<Xpinn.Tesoreria.Entities.RecaudosMasivos> lstResponse = WSRecaudosMasivos.ListarDetalleRecaudoConsultaExtracto(pNumeroRecaudo, estadoNom, bDetallado, usuario); ;
            return lstResponse;
        }

        [WebMethod(Description = "Recaudos por persona")]
        public List<Xpinn.Tesoreria.Entities.RecaudosMasivos> ListarDetalleRecaudoConsultaExtractoxPersona(Xpinn.Tesoreria.Entities.RecaudosMasivos pRecaudosMasivos, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            Xpinn.Tesoreria.Services.RecaudosMasivosService WSRecaudosMasivos = new Xpinn.Tesoreria.Services.RecaudosMasivosService();
            List<Xpinn.Tesoreria.Entities.RecaudosMasivos> lstResponse = WSRecaudosMasivos.ListarDetalleRecaudoConsultaExtractoxPersona(pRecaudosMasivos, usuario);
            return lstResponse;
        }

        [WebMethod(Description = "Deducciones de Recaudo por persona")]
        public List<Xpinn.Tesoreria.Entities.RecaudosMasivos> ListarDeduccionesxPersona(Xpinn.Tesoreria.Entities.RecaudosMasivos pRecaudos, ref string pError, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            Xpinn.Tesoreria.Services.RecaudosMasivosService WSRecaudosMasivos = new Xpinn.Tesoreria.Services.RecaudosMasivosService();
            List<Xpinn.Tesoreria.Entities.RecaudosMasivos> lstResponse = WSRecaudosMasivos.ListarDeduccionesxPersona(pRecaudos, ref pError, usuario);
            return lstResponse;
        }

        [WebMethod(Description = "Recaudos Masivos Existente")]
        public Xpinn.Tesoreria.Entities.RecaudosMasivos ConsultarRecaudo(String pIdObjeto, string sec)
        {
            Xpinn.Tesoreria.Entities.RecaudosMasivos DatosRetorno = new Xpinn.Tesoreria.Entities.RecaudosMasivos();
            try
            {
                Conexion conexion = new Conexion();
                Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
                usuario = conexion.DeterminarUsuarioOficina(sec);
                if (usuario == null)
                    return null;
                Xpinn.Tesoreria.Services.RecaudosMasivosService WSRecaudosMasivos = new Xpinn.Tesoreria.Services.RecaudosMasivosService();
                // List<Xpinn.Tesoreria.Entities.RecaudosMasivos> lstabc = new List<Xpinn.Tesoreria.Entities.RecaudosMasivos>();
                DatosRetorno = WSRecaudosMasivos.ConsultarRecaudo(pIdObjeto, usuario);
                //  return lstabc;              
            }
            catch
            {
                DatosRetorno = null;
            }
            return DatosRetorno;
        }

        #endregion
        [WebMethod]
        public List<Xpinn.Ahorros.Entities.AhorroVista> ListarAhorrosEstadoCuenta(Int64 pCliente, Boolean pResult, string pFiltro)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();

            Xpinn.Ahorros.Services.AhorroVistaServices BOAhorroServ = new Xpinn.Ahorros.Services.AhorroVistaServices();
            Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 ePersona = new Xpinn.FabricaCreditos.Entities.Persona1();
            ePersona.seleccionar = "Cod_persona";
            ePersona.cod_persona = pCliente;
            ePersona.soloPersona = 1;
            //consultar si existe la persona
            ePersona = personaServicio.ConsultarPersona1Param(ePersona, usuario);
            if (ePersona == null)
                return null;
            // Consultar productos de la persona
            List<Xpinn.Ahorros.Entities.AhorroVista> lstProductoAhorro = new List<Xpinn.Ahorros.Entities.AhorroVista>();
            lstProductoAhorro = BOAhorroServ.ListarAportesClubAhorradores(pCliente, pResult, pFiltro, usuario);
            // Devolver listado de productos
            return lstProductoAhorro;
        }

        [WebMethod]
        public List<Xpinn.Programado.Entities.CuentasProgramado> ListarPrograEstadoCuenta(Int64 pCliente, Boolean pResult, string pFiltro)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();

            Xpinn.Programado.Services.CuentasProgramadoServices BOProgramadoServ = new Xpinn.Programado.Services.CuentasProgramadoServices();
            Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 ePersona = new Xpinn.FabricaCreditos.Entities.Persona1();
            ePersona.seleccionar = "Cod_persona";
            ePersona.cod_persona = pCliente;
            ePersona.soloPersona = 1;
            //consultar si existe la persona
            ePersona = personaServicio.ConsultarPersona1Param(ePersona, usuario);
            if (ePersona == null)
                return null;
            // Consultar productos de la persona
            List<Xpinn.Programado.Entities.CuentasProgramado> lstProductoAhorro = new List<Xpinn.Programado.Entities.CuentasProgramado>();
            lstProductoAhorro = BOProgramadoServ.ListarPrograClubAhorradores(pCliente, pResult, pFiltro, usuario);
            // Devolver listado de productos
            return lstProductoAhorro;
        }

        [WebMethod]
        public Xpinn.Ahorros.Entities.AhorroVista InsertarCambioCuotaAhorro(Xpinn.Ahorros.Entities.AhorroVista pAhorro, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            Xpinn.Ahorros.Services.AhorroVistaServices BOAhorroServ = new Xpinn.Ahorros.Services.AhorroVistaServices();
            pAhorro = BOAhorroServ.CrearNovedadCambio(pAhorro, usuario);
            if(pAhorro != null)
            {
                try
                {
                    bool resultNotification = ConfigureNotification.SendNotifification(ProcesoAtencionCliente.SolicitudCambioCuota, usuario, pAhorro.cod_persona);
                }
                catch (Exception)
                {
                }
            }
            return pAhorro;
        }

        [WebMethod]
        public Xpinn.Programado.Entities.CuentasProgramado InsertarCambioCuotaProgra(Xpinn.Programado.Entities.CuentasProgramado pProgramado, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            Xpinn.Programado.Services.CuentasProgramadoServices BOPrograServ = new Xpinn.Programado.Services.CuentasProgramadoServices();
            pProgramado = BOPrograServ.CrearNovedadCambio(pProgramado, usuario);
            if (pProgramado != null)
            {
                try
                {
                    bool resultNotification = ConfigureNotification.SendNotifification(ProcesoAtencionCliente.SolicitudCambioCuota, usuario, pProgramado.cod_persona);
                }
                catch (Exception)
                {
                }
            }

            return pProgramado;
        }

        [WebMethod]
        public Xpinn.Aportes.Entities.SolicitudPersonaAfi ListarRepresentantes(Int64 pIdent, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;
            Xpinn.Aportes.Services.SolicitudPersonaAfiServices BOAfiliacion = new Xpinn.Aportes.Services.SolicitudPersonaAfiServices();
            Xpinn.Aportes.Entities.SolicitudPersonaAfi pEntidad = new Xpinn.Aportes.Entities.SolicitudPersonaAfi();

            pEntidad = BOAfiliacion.ListarPersonasRepresentante(pIdent, usuario);
            return pEntidad;
        }
        [WebMethod]
        public List<Xpinn.Aportes.Entities.PlanTelefonico> ListarLineasTelefonicas(string Identificacion, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;
            Xpinn.Aportes.Services.PlanesTelefonicosService PlanService = new Xpinn.Aportes.Services.PlanesTelefonicosService();
            Xpinn.Aportes.Entities.PlanTelefonico pEntidad = new Xpinn.Aportes.Entities.PlanTelefonico();
            List<Xpinn.Aportes.Entities.PlanTelefonico> lstLineas = new List<Xpinn.Aportes.Entities.PlanTelefonico>();
         
            lstLineas = PlanService.ListarLineasAtencionWeb(usuario, Identificacion );
            return lstLineas;
        } 
      
        [WebMethod]
        public Xpinn.FabricaCreditos.Entities.Persona1 FechaEdad(Int64 CodCliente, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            Xpinn.FabricaCreditos.Services.Persona1Service Personaservice = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 pResponse = Personaservice.FechaNacimiento(CodCliente, usuario);
            return pResponse;
        }

        [WebMethod] 
        public Xpinn.FabricaCreditos.Entities.Persona1 Notificacion(Xpinn.FabricaCreditos.Entities.Persona1 pPersona,int opcion, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;
            Xpinn.FabricaCreditos.Services.Persona1Service BOPersonaService = new Xpinn.FabricaCreditos.Services.Persona1Service();
            pPersona = BOPersonaService.Notificacion(pPersona, usuario,opcion);

            return pPersona;
        }

        [WebMethod]
        public int NotificacionMax(string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return 0;

            Xpinn.FabricaCreditos.Services.Persona1Service BOPersonaService = new Xpinn.FabricaCreditos.Services.Persona1Service();
           int Max=  BOPersonaService.NotificacionidMax( usuario);

            return Max;
        }


        [WebMethod(Description = "Lista de Devoluciones por Asociado")]
        public List<Xpinn.Tesoreria.Entities.Devolucion> ListarDevolucion(string pFiltro, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;
            Xpinn.Tesoreria.Services.DevolucionServices DevolucionesService = new Xpinn.Tesoreria.Services.DevolucionServices();

            List<Xpinn.Tesoreria.Entities.Devolucion> lstConsulta = DevolucionesService.ListarDevolucion(new Xpinn.Tesoreria.Entities.Devolucion(), DateTime.MinValue, usuario, pFiltro);

            return lstConsulta;
        }

        [WebMethod]
        public Xpinn.Ahorros.Entities.AhorroVista ConsultarCuentaBancaria(string pCodPersona, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;
            Xpinn.Ahorros.Services.AhorroVistaServices ahorroService = new Xpinn.Ahorros.Services.AhorroVistaServices();
            Xpinn.Ahorros.Entities.AhorroVista Datos = ahorroService.ConsultarCuentaBancaria(pCodPersona, usuario);
            
            // Devolver cuenta
            return Datos;
        }

        [WebMethod]
        public List<Xpinn.CDATS.Entities.ReporteMovimiento> ListarMovCDAT(long pNumeroAporte, DateTime pfechaInicial, DateTime pfechaFinal, string sec)
        {
            try
            {
                Xpinn.CDATS.Services.ReporteMovimientoServices BOcdat = new Xpinn.CDATS.Services.ReporteMovimientoServices();
                Conexion conexion = new Conexion();
                Usuario usuario = new Usuario();
                usuario = conexion.DeterminarUsuarioOficina(sec);
                if (usuario == null)
                    return null;

                List<Xpinn.CDATS.Entities.ReporteMovimiento> lstMovimientos = new List<Xpinn.CDATS.Entities.ReporteMovimiento>();
                lstMovimientos = BOcdat.ListarReporteMovimiento(pNumeroAporte, pfechaInicial, pfechaFinal, usuario);                
                return lstMovimientos;
            }
            catch
            {
                return null;
            }
        }


       

    }
}
