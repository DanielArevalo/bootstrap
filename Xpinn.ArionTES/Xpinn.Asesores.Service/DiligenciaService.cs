using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class DiligenciaService
    {
        private DiligenciaBusiness BODiligencia;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Diligencia
        /// </summary>
        public DiligenciaService()
        {
            BODiligencia = new DiligenciaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "110108"; } }
        public string CodigoProgramaReportes { get { return "110303"; } }

        /// <summary>
        /// Servicio para crear Diligencia
        /// </summary>
        /// <param name="pEntity">Entidad Diligencia</param>
        /// <returns>Entidad Diligencia creada</returns>
        public Diligencia CrearDiligencia(Diligencia pDiligencia, Usuario pUsuario)
        {
            try
            {
                return BODiligencia.CrearDiligencia(pDiligencia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaService", "CrearDiligencia", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para actualizar los estados de los creditos
        /// </summary>
        /// <param name="pEntity">Entidad Diligencia</param>
        /// <returns>Entidad Diligencia creada</returns>
        public Diligencia ActualizarEstadosDiligencia(Diligencia pDiligencia, Usuario pUsuario)
        {
            try
            {
                return BODiligencia.ActualizarEstadosDiligencia(pDiligencia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaService", "ActualizarEstadosDiligencia", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Diligencia
        /// </summary>
        /// <param name="pDiligencia">Entidad Diligencia</param>
        /// <returns>Entidad Diligencia modificada</returns>
        public Diligencia ModificarDiligencia(Diligencia pDiligencia, Usuario pUsuario)
        {
            try
            {
                return BODiligencia.ModificarDiligencia(pDiligencia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaService", "ModificarDiligencia", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Diligencia
        /// </summary>
        /// <param name="pId">identificador de Diligencia</param>
        public void EliminarDiligencia(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BODiligencia.EliminarDiligencia(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarDiligencia", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Diligencia
        /// </summary>
        /// <param name="pId">identificador de Diligencia</param>
        /// <returns>Entidad Diligencia</returns>
        public Diligencia ConsultarDiligencia(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BODiligencia.ConsultarDiligencia(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaService", "ConsultarDiligencia", ex);
                return null;
            }
        }

        public Diligencia ConsultarDiligenciaEntidad(Diligencia pDiligencia, Usuario pUsuario)
        {
            try
            {
                return BODiligencia.ConsultarDiligenciaEntidad(pDiligencia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaService", "ConsultarDiligenciaEntidad", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener parametro general habeas data diligencia
        /// </summary>
        /// <param name="pId">identificador de Parametro</param>
        /// <returns>Entidad Diligencia</returns>
        public Diligencia ConsultarparametroHabeasData(Usuario pUsuario)
        {
            try
            {
                return BODiligencia.ConsultarparametroHabeasData(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaService", "ConsultarparametroHabeasData", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener parametro general ContactoCarta diligencia
        /// </summary>
        /// <param name="pId">identificador de Parametro</param>
        /// <returns>Entidad Diligencia</returns>
        public Diligencia ConsultarparametroContactoCartas(Usuario pUsuario)
        {
            try
            {
                return BODiligencia.ConsultarparametroContactoCartas(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaService", "ConsultarparametroContactoCartas", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener parametro general CobroPreJuridico diligencia
        /// </summary>
        /// <param name="pId">identificador de Parametro</param>
        /// <returns>Entidad Diligencia</returns>
        public Diligencia ConsultarparametroCobroPreJuridico(Usuario pUsuario)
        {
            try
            {
                return BODiligencia.ConsultarparametroCobroPreJuridico(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaService", "ConsultarparametroCobroPreJuridico", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener parametro general CobroJuridico diligencia
        /// </summary>
        /// <param name="pId">identificador de Parametro</param>
        /// <returns>Entidad Diligencia</returns>
        public Diligencia ConsultarparametroPrejuridico2(Usuario pUsuario)
        {
            try
            {
                return BODiligencia.ConsultarparametroPrejuridico2(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaService", "ConsultarparametroPrejuridico2", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener parametro general CobroJuridico diligencia
        /// </summary>
        /// <param name="pId">identificador de Parametro</param>
        /// <returns>Entidad Diligencia</returns>
        public Diligencia ConsultarparametroVisitaAbogado(Usuario pUsuario)
        {
            try
            {
                return BODiligencia.ConsultarparametroVisitaAbogado(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaService", "ConsultarparametroVisitaAbogado", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener parametro general CobroJuridico diligencia
        /// </summary>
        /// <param name="pId">identificador de Parametro</param>
        /// <returns>Entidad Diligencia</returns>
        public Diligencia ConsultarparametroCampaña(Usuario pUsuario)
        {
            try
            {
                return BODiligencia.ConsultarparametroCampaña(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaService", "ConsultarparametroCampaña", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener Diligencia
        /// </summary>
        /// <param name="pId">identificador de Diligencia</param>
        /// <returns>Entidad Diligencia</returns>
        public Diligencia ConsultarDiligenciaXcredito(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BODiligencia.ConsultarDiligenciaXcredito(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaService", "ConsultarDiligenciaXcredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pDiligencia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Diligencia obtenidos</returns>
        public List<Diligencia> ListarDiligencia(Diligencia pDiligencia, Usuario pUsuario)
        {
            try
            {
                return BODiligencia.ListarDiligencia(pDiligencia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaService", "ConsultarDiligencia", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pDiligencia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Diligencia obtenidos</returns>
        public List<Diligencia> ListarDiligenciaEstadocuenta(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BODiligencia.ListarDiligenciaEstadocuenta(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaService", "ListarDiligenciaEstadocuenta", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pDiligencia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Diligencia obtenidos</returns>
        public List<Diligencia> ListarReporteacuerdos(Usuario pUsuario, DateTime fechaini, DateTime fechafinal,Int64 usuario)
        {
            try
            {
                return BODiligencia.ListarReporteacuerdos(pUsuario, fechaini, fechafinal,usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaService", "ListarReporteacuerdos", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pDiligencia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Diligencia obtenidos</returns>
        public List<Diligencia> ListarReporteacuerdosTodosUsuarios(Usuario pUsuario, DateTime fechaini, DateTime fechafinal, Int64 cod_zona)
        {
            try
            {
                return BODiligencia.ListarReporteacuerdosTodosUsuarios(pUsuario, fechaini, fechafinal, cod_zona);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaService", "ListarReporteacuerdosTodosUsuarios", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pDiligencia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Diligencia obtenidos</returns>
        public List<Diligencia> ListarReporteDiligencia(Usuario pUsuario, DateTime fechaini, DateTime fechafinal,Int64 usuario)
        {
            try
            {
                return BODiligencia.ListarReporteDiligencia(pUsuario, fechaini, fechafinal,usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaService", "ListarReporteDiligencia", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pDiligencia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Diligencia obtenidos</returns>
        public List<Diligencia> ListarReporteDiligenciaTodos(Usuario pUsuario, DateTime fechaini, DateTime fechafinal, Int64 cod_zona)
        {
            try
            {
                return BODiligencia.ListarReporteDiligenciaTodos(pUsuario, fechaini, fechafinal, cod_zona);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaService", "ListarReporteDiligenciaTodos", ex);
                return null;
            }
        }

    }
}