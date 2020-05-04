using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;
using System.IO;
using Xpinn.Aportes.Entities;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class Persona1Service
    {
        private Persona1Business BOPersona1;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Persona1
        /// </summary>
        public Persona1Service()
        {
            BOPersona1 = new Persona1Business();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100102"; } }
        public string CodigoProgramaDatacredito { get { return "100101"; } }
        public string CodigoProgramaConyuge { get { return "100102"; } }  //100119
        public string CodigoProgramaCodeudor { get { return "100102"; } } //100110
        public string CodigoProgramaadicionarCodeudor { get { return "100109"; } }
        public string CodigoProgramaPreAnalisis { get { return "100158"; } }
        public string CodigoProgramaCreditoE { get { return "100160"; } }
        public string CodigoProgramaCre { get { return "100153"; } }
        public string CodigoProgramaMod { get { return "100152"; } }
        public string CodigoProgramaModificacion { get { return "100150"; } }
        public string CodigoProgramaTrasladoOficina { get { return "170121"; } }
        public string CodigoProgramaActualizacionDatos { get { return "170123"; } }
        /// <summary>
        /// Servicio para crear Persona1
        /// </summary>
        /// <param name="pEntity">Entidad Persona1</param>
        /// <returns>Entidad Persona1 creada</returns>
        public Persona1 CrearPersona1(Persona1 pPersona1, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.CrearPersona1(pPersona1, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "CrearPersona1", ex);
                return null;
            }
        }

        public Persona1 ConsultarPersona2Param(Persona1 identificacion, Usuario pUsuario)
        {
            try
            {
                Persona1 pPersona = new Persona1();
                pPersona = BOPersona1.ConsultarPersona2Param(identificacion, pUsuario);
                return pPersona;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarPersona2", ex);
                return null;
            }

        }

        public Persona1 CrearPersonasImagenes(Persona1 pPersona1, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.CrearPersonasImagenes(pPersona1, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "CrearPersonasImagenes", ex);
                return null;
            }
        }

        public Persona1 ModificarPersonasImagenes(Persona1 pPersona1, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ModificarPersonasImagenes(pPersona1, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ModificarPersonasImagenes", ex);
                return null;
            }
        }

        public Persona1 CrearPersonaAporte(Persona1 pPersona1, bool EsMenor, PersonaResponsable pDataMenor, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.CrearPersonaAporte(pPersona1, EsMenor, pDataMenor, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "CrearPersonaAporte", ex);
                return null;
            }
        }

        public Persona1 ModificarPersonaAporte(Persona1 pPersona1, bool EsMenor, PersonaResponsable pDataMenor, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ModificarPersonaAporte(pPersona1, EsMenor, pDataMenor, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ModificarPersonaAporte", ex);
                return null;
            }
        }


        public Persona1 ModificarConyugeAporte(Persona1 vPersona1, bool EsMenor, PersonaResponsable pDataMenor, Usuario usuario)
        {
            try
            {
                return BOPersona1.ModificarConyugeAporte(vPersona1, EsMenor, pDataMenor, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ModificarConyugeAporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Persona1
        /// </summary>
        /// <param name="pPersona1">Entidad Persona1</param>
        /// <returns>Entidad Persona1 modificada</returns>
        public Persona1 ModificarPersona1(Persona1 pPersona1, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ModificarPersona1(pPersona1, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ModificarPersona1", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Persona1
        /// </summary>
        /// <param name="pId">identificador de Persona1</param>
        public void EliminarPersona1(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOPersona1.EliminarPersona1(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "EliminarPersona1", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Persona1
        /// </summary>
        /// <param name="pId">identificador de Persona1</param>
        /// <returns>Entidad Persona1</returns>
        public Persona1 ConsultarPersona1(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ConsultarPersona1(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarPersona1", ex);
                return null;
            }
        }
        public Persona1 ConsultarNotificacion(Int64 pId, Usuario pUsuario, int opcion)
        {
            try
            {
                return BOPersona1.ConsultarNotificacion(pId, pUsuario, opcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarNotificacion", ex);
                return null;
            }
        }

        public Persona1 ConsultarPersona1TrasladoOficina(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ConsultarPersona1TrasladoOficina(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarPersona1TrasladoOficina", ex);
                return null;
            }
        }

        public Persona1 ModificarTrasladoOficinas(Persona1 pTrasladoOficina, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ModificarTrasladoOficina(pTrasladoOficina, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TrasladoPagaduriasServices", "ModificarTrasladoPagadurias", ex);
                return null;
            }
        }

        public Persona1 Consultarnegocio(string cod, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.Consultarnegocio(cod, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarPersona1", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener Persona1 por parametros
        /// </summary>
        /// <param name="pId">identificador de Persona1</param>
        /// <returns>Entidad Persona1</returns>
        public Persona1 ConsultarPersona1Param(Persona1 pPersona1, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ConsultarPersona1Param(pPersona1, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarPersona1Param", ex);
                return null;
            }
        }

        public Persona1 crearidentificacion(Persona1 pPersona1, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.crearidentificacion(pPersona1, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "crearidentificacion", ex);
                return null;
            }
        }

        public string ConsultarIdentificacionPersona(long codPersona, Usuario usuario)
        {
            try
            {
                return BOPersona1.ConsultarIdentificacionPersona(codPersona, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarIdentificacionPersona", ex);
                return null;
            }
        }

        public void eliminaridentificacion(Persona1 pPersona1, Usuario pUsuario)
        {
            try
            {
                BOPersona1.eliminaridentificacion(pPersona1, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "eliminaridentificacion", ex);

            }
        }

        public Persona1 consultaridentificacion(Usuario pUsuario)
        {
            try
            {
                return BOPersona1.consultaridentificacion(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarPersona1Param", ex);
                return null;
            }
        }

        public Int64? ConsultarPersona1Imagen(Int64 pCodPersona, Int64 TipoDoc, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ConsultarPersona1Imagen(pCodPersona, TipoDoc, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarPersona1Imagen", ex);
                return null;
            }
        }

        public bool validaMorosoServices(Usuario pUsuario, string identificacion)
        {
            try
            {
                return BOPersona1.validaMorosoBusiness(pUsuario, identificacion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarPersona1", ex);
                throw;
            }
        }

        public Persona1 ConsultarDatosCliente(Persona1 pPersona, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ConsultarDatosCliente(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarDatosCliente", ex);
                throw;
            }
        }

        public Persona1 ConsultarPersonaAPP(Persona1 pPersona1, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ConsultarPersonaAPP(pPersona1, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarPersona1", ex);
                return null;
            }
        }

        public Persona1 ConsultarPersona1conyuge(Persona1 pPersona1, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ConsultarPersona1conyuge(pPersona1, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarPersona1conyuge", ex);
                return null;
            }
        }

        public List<Referncias> ListadoPersonas1ReporteReferencias(Referncias preferencias, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ListadoPersonas1ReporteReferencias(preferencias, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ListadoPersonas1ReporteReferencias", ex);
                return null;
            }
        }

        public Referncias ListadoPersonas1ReporteReferencias(Int64 pnumero_credito, string pidentificacion, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ListadoPersonas1ReporteReferencias(pnumero_credito, pidentificacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ListadoPersonas1ReporteReferencias", ex);
                return null;
            }
        }

        public List<Persona1> ListadoPersonas1ReporteFamiliares(Persona1 Ppersona, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ListadoPersonas1ReporteFamiliares(Ppersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ListadoPersonas1ReporteFamiliares", ex);
                return null;
            }
        }

        public List<Persona1> ListadoPersonas1ReporteCodeudor(Persona1 Pcodeudor, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ListadoPersonas1ReporteCodeudor(Pcodeudor, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ListadoPersonas1ReporteCodeudor", ex);
                return null;
            }
        }
        public List<Referencia> referencias(Persona1 pPersona1, long radicacion, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.referencias(pPersona1, radicacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarPersona1", ex);
                return null;
            }
        }

        public List<Persona1> ListadoPersonas1Reporte(Persona1 pPersona1, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ListadoPersonas1Reporte(pPersona1, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarPersona1", ex);
                return null;
            }
        }
        public InformacionNegocio Consultardatosnegocio(long radicacion, string identificacion, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.Consultardatosnegocio(radicacion, identificacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarPersona1", ex);
                return null;
            }
        }
        public List<VentasSemanales> ListadoEstacionalidadSemanal(VentasSemanales identificacion, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ListadoEstacionalidadSemanal(identificacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ListadoEstacionalidadSemanal", ex);
                return null;
            }
        }
        public List<EstacionalidadMensual> ListadoEstacionalidadMensual(EstacionalidadMensual identificacion, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ListadoEstacionalidadMensual(identificacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ListadoEstacionalidadMensual", ex);
                return null;
            }
        }

        public long ConsultarCodigoEmpresaPagaduria(string identificacion, Usuario usuario)
        {
            try
            {
                return BOPersona1.ConsultarCodigoEmpresaPagaduria(identificacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarCodigoEmpresaPagaduria", ex);
                return 0;
            }
        }

        public long ConsultarCodigoEmpresaPagaduria(Int64 pCodPersona, Usuario usuario)
        {
            try
            {
                return BOPersona1.ConsultarCodigoEmpresaPagaduria(pCodPersona, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarCodigoEmpresaPagaduria", ex);
                return 0;
            }
        }

        public List<Persona1> ConsultarPersonasAfiliadas(string filtro, Usuario usuario)
        {
            try
            {
                return BOPersona1.ConsultarPersonasAfiliadas(filtro, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarPersonasAfiliadas", ex);
                return null;
            }
        }

        public CreditoPlan ConsultarPersona1Paramcred(long radicacion, string identificacion, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ConsultarPersona1Paramcred(radicacion, identificacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarPersona1Paramcred", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Persona1s a partir de unos filtros
        /// </summary>
        /// <param name="pPersona1">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Persona1 obtenidos</returns>
        public List<Persona1> ListarPersona1(Persona1 pPersona1, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ListarPersona1(pPersona1, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ListarPersona1", ex);
                return null;
            }
        }

        public List<Persona1> Listarsolicitudesdecredito(Persona1 pPersona1, Usuario pUsuario, String filtro)
        {
            try
            {
                return BOPersona1.Listarsolicitudesdecredito(pPersona1, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "Listarsolicitudesdecredito", ex);
                return null;
            }
        }
        public List<Persona1> ListasBarrios(Int32 ciudad, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ListasBarrios(ciudad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ListasDesplegables", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene  listas desplegables
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de elementos obtenidos</returns>
        public List<Persona1> ListasDesplegables(String ListaSolicitada, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ListasDesplegables(ListaSolicitada, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ListasDesplegables", ex);
                return null;
            }
        }


        public long consultarSolicitud(long radicacion, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.consultarSolicitud(radicacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "consultarSolicitud", ex);
                return 0;
            }
        }


        public List<Persona1> listaddlServices(string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ListarddLinea(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "listaddlBusines", ex);
                return null;
            }
        }

        /// <summary>
        /// Listado para mostrar las personas en los comprobantes
        /// </summary>
        /// <param name="pPersona1"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<Persona1> ListadoPersonas1(Persona1 pPersona1, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ListadoPersonas1(pPersona1, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ListadoPersonas1", ex);
                return null;
            }
        }


        public Persona1 ConsultaDatosPersona(string pidentificacion, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ConsultaDatosPersona(pidentificacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultaDatosPersona", ex);
                return null;
            }
        }

        public Persona1 ConsultaDatosPersona(Int64 pCodigo, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ConsultaDatosPersona(pCodigo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultaDatosPersona", ex);
                return null;
            }
        }

        public List<Imagenes> Handler(Imagenes pImagenes, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.Handler(pImagenes, pUsuario);
            }
            catch
            {
                return null;
            }
        }

        public List<Imagenes> HandlerHuella(Imagenes pImagenes, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.HandlerHuella(pImagenes, pUsuario);
            }
            catch
            {
                return null;
            }
        }


        public Persona1 ModificarPersonaAtencionCliente(Persona1 pPersona1, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ModificarPersonaAtencionCliente(pPersona1, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ModificarPersonaAtencionCliente", ex);
                return null;
            }
        }


        public Boolean ModificarPersonaAPP(Persona1 pPersona1, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ModificarPersonaAPP(pPersona1, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ModificarPersonaAPP", ex);
                return false;
            }
        }


        public bool ConsultaClavePersona(string pIdentificacion, string pClave, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ConsultaClavePersona(pIdentificacion, pClave, pUsuario);
            }
            catch
            {
                return false;
            }
        }


        public long ConsultarCodigopersona(string pIdentificacion, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ConsultarCodigopersona(pIdentificacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarCodigopersona", ex);
                return 0;
            }
        }


        public List<Xpinn.FabricaCreditos.Entities.PersonaAutorizacion> ListarPersonaAutorizacion(Xpinn.FabricaCreditos.Entities.PersonaAutorizacion pPersonaAutorizacion, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ListarPersonaAutorizacion(pPersonaAutorizacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ListarPersonaAutorizacion", ex);
                return null;
            }
        }

        public List<Xpinn.FabricaCreditos.Entities.PersonaAutorizacion> ListarPersonaAutorizacion(string pIdentificacion, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ListarPersonaAutorizacion(pIdentificacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ListarPersonaAutorizacion", ex);
                return null;
            }
        }

        public Xpinn.FabricaCreditos.Entities.PersonaAutorizacion ModificarPersonaAutorizacion(Xpinn.FabricaCreditos.Entities.PersonaAutorizacion pPersonaAutorizacion, Usuario pUsuario)
        {
            try
            {
                pPersonaAutorizacion = BOPersona1.ModificarPersonaAutorizacion(pPersonaAutorizacion, pUsuario);

                return pPersonaAutorizacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ModificarPersonaAutorizacion", ex);
                return null;
            }
        }

        public List<Xpinn.FabricaCreditos.Entities.PersonaAutorizacion> ListarUsuarioAutorizacion(Int64 pCodUsuario, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ListarUsuarioAutorizacion(pCodUsuario, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ListarUsuarioAutorizacion", ex);
                return null;
            }
        }

        public bool validaMorosoBusiness(Usuario pUsuario, string identificacion)
        {
            try
            {
                return BOPersona1.validaMorosoBusiness(pUsuario, identificacion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarPersona1", ex);
                throw;
            }
        }

        public Imagenes ConsultarImagenesPersonaIdentificacion(string pId, Int64 pTipoImagen, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ConsultarImagenesPersonaIdentificacion(pId, pTipoImagen, pUsuario);
            }
            catch
            {
                return null;
            }
        }

        public byte[] ConsultarImagenHuellaPersona(Int64 pId, Int64 pDedo, ref Int64 pIdImagen, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ConsultarImagenHuellaPersona(pId, pDedo, ref pIdImagen, pUsuario);
            }
            catch
            {
                return null;
            }
        }

        public Persona1 ConsultarPersona(Int64? pCod, string pId, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ConsultarPersona(pCod, pId, pUsuario);
            }
            catch
            {
                return null;
            }
        }

        public Imagenes CrearImagenesPersona(Imagenes pImagen, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.CrearImagenesPersona(pImagen, pUsuario); ;
            }
            catch
            {
                return pImagen;
            }
        }

        public Xpinn.Comun.Entities.General consultarsalariominimo(Int64? pCod, Usuario pUsuario)
        {
            try
            {

                return BOPersona1.consultarsalariominimo(pCod, pUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "consultarsalariominimo", ex);
                return null;
            }
        }


        public Persona1 consultaraportes(Int64? pCod, string pId, Usuario pUsuario)
        {
            try
            {

                return BOPersona1.consultaraportes(pCod, pId, pUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "consultaraportes", ex);
                return null;
            }
        }

        public Persona1 consultarcreditos(Int64? pCod, string pId, Usuario pUsuario)
        {
            try
            {

                return BOPersona1.consultarcreditos(pCod, pId, pUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "consultarcreditos", ex);
                return null;
            }
        }


        public List<Credito> ConsultarResumenPersona(Int64 pCodPersona, Usuario vUsuario)
        {
            try
            {

                return BOPersona1.ConsultarResumenPersona(pCodPersona, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarResumenPersona", ex);
                return null;
            }
        }

        public Imagenes ModificarImagenesPersona(Imagenes pImagen, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ModificarImagenesPersona(pImagen, pUsuario); ;
            }
            catch
            {
                return pImagen;
            }
        }

        public void CargarPersonas(DateTime pFechaCarga, string pTipoCarga, string pTipoPersona, string sformato_fecha, Stream pstream, ref string perror, ref List<Xpinn.Contabilidad.Entities.Tercero> lstJuridica, ref List<Persona1> lstNatural, ref List<Xpinn.FabricaCreditos.Entities.CuentasBancarias> lstCtaBancarias, ref List<ErroresCarga> plstErrores, Usuario pUsuario)
        {
            try
            { 
                BOPersona1.CargarPersonas(pFechaCarga, pTipoCarga, pTipoPersona, sformato_fecha, pstream, ref perror, ref lstJuridica, ref lstNatural, ref lstCtaBancarias, ref plstErrores, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "CargarPersonas", ex);
            }
        }


        public void CrearPersonaImportacion(DateTime pFechaCarga, ref string pError, string TipoCarga, string TipoPersona, List<Persona1> lstNatural, List<Xpinn.Contabilidad.Entities.Tercero> lstJuridica, List<Xpinn.FabricaCreditos.Entities.CuentasBancarias> lstCtaBancarias, Usuario pUsuario)
        {
            try
            {
                BOPersona1.CrearPersonaImportacion(pFechaCarga, ref pError, TipoCarga, TipoPersona, lstNatural, lstJuridica, lstCtaBancarias, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "CrearPersonaImportacion", ex);
            }
        }

        public List<Persona1> ModificarPersonasAporte(List<Persona1> lstPersona, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ModificarPersonasAporte(lstPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "CargarPersonas", ex);
                return null;
            }
        
        }

        public List<Persona1> ListarPersonasAporte(string pcod_ini, string pcod_fin, string pcod_empresa, string pcod_asesor, string pcod_zona, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ListarPersonasAporte(pcod_ini, pcod_fin, pcod_empresa, pcod_asesor, pcod_zona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ListarPersonasAporte", ex);
                return null;
            }
        }

        public void CargarPersonasDatos(DateTime pFechaCarga, string sformato_fecha, Stream pstream, ref string perror, ref List<Persona1> lstPersonas, ref List<ErroresCarga> plstErrores, Usuario pUsuario)
        {
            try
            {
                BOPersona1.CargarPersonasDatos(pFechaCarga, sformato_fecha, pstream, ref perror, ref lstPersonas, ref plstErrores, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "CargarPersonasDatos", ex);
            }
        }

        public Persona1 ValidarPersona(string pIdentificacion, int pTipo, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ValidarPersona(pIdentificacion, pTipo, pUsuario);
            }
            catch
            {
                return null;
            }
        }

        public Int64? GrabarControl(string pIdentificacion, int pTipo, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.GrabarControl(pIdentificacion, pTipo, pUsuario);
            }
            catch
            {
                return null;
            }
        }

        public Persona1 BuscarDepartamentoPorCodigoCiudad(long ciudad, Usuario usuario)
        {
            try
            {
                return BOPersona1.BuscarDepartamentoPorCodigoCiudad(ciudad, usuario);
            }
            catch
            {
                return null;
            }
        }

        public bool VerificarSiPersonaEsAsociado(long codigoPersona, Usuario usuario)
        {
            try
            {
                return BOPersona1.VerificarSiPersonaEsAsociado(codigoPersona, usuario);
            }
            catch
            {
                return false;
            }
        }

        public bool VerificarSiPersonaEsNatural(long codigoPersona, Usuario usuario)
        {
            try
            {
                return BOPersona1.VerificarSiPersonaEsNatural(codigoPersona, usuario);
            }
            catch
            {
                return false;
            }
        }
        public Persona1 FechaNacimiento(Int64 codcliente, Usuario pUsuario)
        {

            return BOPersona1.FechaNacimiento(codcliente, pUsuario);
        }
        public int NotificacionidMax(Usuario pUsuario)
        {

            return BOPersona1.NotificacionidMax(pUsuario);
        }
        public Persona1 Notificacion(Persona1 pPersona, Usuario pUsuario, int opcion)
        {
            try
            {
                pPersona = BOPersona1.Notificacion(pPersona, pUsuario, opcion);

                return pPersona;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "Notificacion", ex);
                return null;
            }
        }
        public List<Persona1> ProxVencer(Usuario pUsuario)
        {
            try
            {
                return BOPersona1.ProxVencer(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ProxVencer", ex);
                return null;
            }
        }

        public Persona1 TabPersonal(Persona1 pPersona, int opcion, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.TabPersonal(pPersona, opcion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "TabPersonal", ex);
                return null;
            }
        }

        public Persona1 TabLaboral(Persona1 pPersona, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.TabLaboral(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "TabLaboral", ex);
                return null;
            }
        }

        public Persona1 TabEconomica(Persona1 pPersona, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.TabEconomica(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "TabEconomica", ex);
                return null;
            }
        }

        public Persona1 TabAdicional(Persona1 pPersona, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.TabAdicional(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "TabAdicional", ex);
                return null;
            }
        }

        public int CrearSolicitudRetiro(Persona1 pPersona, Usuario pUsuario)
        {
            try
            {
                return BOPersona1.CrearSolicitudRetiro(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "CrearSolicitudRetiro", ex);
                return 0;
            }
        }

        public void InsertarRespuestasRetiro(List<Persona1> pPersona, Usuario pUsuario)
        {
            try
            {
                BOPersona1.InsertarRespuestasRetiro(pPersona, pUsuario);
                return;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "InsertarRespuestasRetiro", ex);
                return;
            }
        }

        public Persona1 ConsultaDatosCliente(Persona1 pPersona, Usuario pUsuario)
        {        
            try
            {
                return BOPersona1.ConsultaDatosCliente(pPersona, pUsuario);                   
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarDatosCliente", ex);
                return null;
            }                 
        }

        public decimal ConsultarNivelEndeudamiento(string identificacion, Usuario pUsuario)
        {
            return BOPersona1.ConsultarNivelEndeudamiento(identificacion, pUsuario);
        }

        public Int64? ConsultarCodigoPersona(string pIdentificacion, Usuario pUsuario)
        {
            return BOPersona1.ConsultarCodigopersona(pIdentificacion, pUsuario);
        }


    }
}