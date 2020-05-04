using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Asesores.Data;
using Xpinn.Asesores.Entities;
 
namespace Xpinn.Asesores.Business
{
    /// <summary>
    /// Objeto de negocio para Diligencia
    /// </summary>
    public class DiligenciaBusiness : GlobalBusiness
    {
        private DiligenciaData DADiligencia;

        /// <summary>
        /// Constructor del objeto de negocio para Diligencia
        /// </summary>
        public DiligenciaBusiness()
        {
            DADiligencia = new DiligenciaData();
        }

        /// <summary>
        /// Crea un Diligencia
        /// </summary>
        /// <param name="pDiligencia">Entidad Diligencia</param>
        /// <returns>Entidad Diligencia creada</returns>
        public Diligencia CrearDiligencia(Diligencia pDiligencia, Usuario pUsuario)
        {
            try
            {
              //  using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pDiligencia = DADiligencia.CrearDiligencia(pDiligencia, pUsuario);

                   // ts.Complete();
                }

                return pDiligencia;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaBusiness", "CrearDiligencia", ex);
                return null;
            }
        }


        /// <summary>
        /// Actualiza los estados de cobro de los creditos 
        /// </summary>
        /// <param name="pId">Identificador de Diligencia</param>
        /// <returns>Entidad Diligencia</returns>
        public Diligencia ActualizarEstadosDiligencia(Diligencia pDiligencia, Usuario pUsuario)
        {
            try
            {
                return DADiligencia.ActualizarEstadosDiligencia(pDiligencia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaBusiness", "ActualizarEstadosDiligencia", ex);
                return null;
            }
        }
        /// <summary>
        /// Modifica un Diligencia
        /// </summary>
        /// <param name="pDiligencia">Entidad Diligencia</param>
        /// <returns>Entidad Diligencia modificada</returns>
        public Diligencia ModificarDiligencia(Diligencia pDiligencia, Usuario pUsuario)
        {
            try
            {
              //  using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pDiligencia = DADiligencia.ModificarDiligencia(pDiligencia, pUsuario);

               //     ts.Complete();
                }

                return pDiligencia;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaBusiness", "ModificarDiligencia", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un Diligencia
        /// </summary>
        /// <param name="pId">Identificador de Diligencia</param>
        public void EliminarDiligencia(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DADiligencia.EliminarDiligencia(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaBusiness", "EliminarDiligencia", ex);
            }
        }

        /// <summary>
        /// Obtiene un Diligencia
        /// </summary>
        /// <param name="pId">Identificador de Diligencia</param>
        /// <returns>Entidad Diligencia</returns>
        public Diligencia ConsultarDiligencia(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DADiligencia.ConsultarDiligencia(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaBusiness", "ConsultarDiligencia", ex);
                return null;
            }
        }

        public Diligencia ConsultarDiligenciaEntidad(Diligencia pDiligencia, Usuario pUsuario)
        {
            try
            {
                return DADiligencia.ConsultarDiligenciaEntidad(pDiligencia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaBusiness", "ConsultarDiligenciaEntidad", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene parametro general habeas data diligencia
        /// </summary>
        /// <param name="pId">identificador de Parametro</param>
        /// <returns>Entidad Diligencia</returns>
        public Diligencia ConsultarparametroHabeasData(Usuario pUsuario)
        {
            try
            {
                return DADiligencia.ConsultarparametroHabeasData(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaBusiness", "ConsultarparametroHabeasData", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene parametro general contacto cartas cobro diligencias
        /// </summary>
        /// <param name="pId">identificador de Parametro</param>
        /// <returns>Entidad Diligencia</returns>
        public Diligencia ConsultarparametroContactoCartas(Usuario pUsuario)
        {
            try
            {
                return DADiligencia.ConsultarparametroContactoCartas(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaBusiness", "ConsultarparametroContactoCartas", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene parametro general habeas data diligencia
        /// </summary>
        /// <param name="pId">identificador de Parametro</param>
        /// <returns>Entidad Diligencia</returns>
        public Diligencia ConsultarparametroCobroPreJuridico(Usuario pUsuario)
        {
            try
            {
                return DADiligencia.ConsultarparametroCobroPreJuridico(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaBusiness", "ConsultarparametroCobroPreJuridico", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene parametro general habeas data diligencia
        /// </summary>
        /// <param name="pId">identificador de Parametro</param>
        /// <returns>Entidad Diligencia</returns>
        public Diligencia ConsultarparametroPrejuridico2(Usuario pUsuario)
        {
            try
            {
                return DADiligencia.ConsultarparametroPrejuridico2(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaBusiness", "ConsultarparametroPrejuridico2", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene parametro general habeas data diligencia
        /// </summary>
        /// <param name="pId">identificador de Parametro</param>
        /// <returns>Entidad Diligencia</returns>
        public Diligencia ConsultarparametroVisitaAbogado(Usuario pUsuario)
        {
            try
            {
                return DADiligencia.ConsultarparametroVisitaAbogado(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaBusiness", "ConsultarparametroVisitaAbogado", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene parametro general habeas data diligencia
        /// </summary>
        /// <param name="pId">identificador de Parametro</param>
        /// <returns>Entidad Diligencia</returns>
        public Diligencia ConsultarparametroCampaña(Usuario pUsuario)
        {
            try
            {
                return DADiligencia.ConsultarparametroCampaña(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaBusiness", "ConsultarparametroCampaña", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene un Diligencia
        /// </summary>
        /// <param name="pId">Identificador de Diligencia</param>
        /// <returns>Entidad Diligencia</returns>
        public Diligencia ConsultarDiligenciaXcredito(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DADiligencia.ConsultarDiligenciaXcredito(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaBusiness", "ConsultarDiligenciaXcredito", ex);
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
                return DADiligencia.ListarDiligencia(pDiligencia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaBusiness", "ListarDiligencia", ex);
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
                return DADiligencia.ListarDiligenciaEstadocuenta(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaBusiness", "ListarDiligenciaEstadocuenta", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pDiligencia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Diligencia obtenidos</returns>
        public List<Diligencia> ListarReporteacuerdos(Usuario pUsuario, DateTime fechaini, DateTime fechafinal, Int64 usuario)
        {
            try
            {
                return DADiligencia.ListarReporteacuerdos(pUsuario, fechaini, fechafinal,usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaBusiness", "ListarReporteacuerdos", ex);
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
                return DADiligencia.ListarReporteacuerdosTodosUsuarios(pUsuario, fechaini, fechafinal, cod_zona);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaBusiness", "ListarReporteacuerdosTodosUsuarios", ex);
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
                return DADiligencia.ListarReporteDiligencia(pUsuario, fechaini, fechafinal,usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaBusiness", "ListarReporteDiligencia", ex);
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
                return DADiligencia.ListarReporteDiligenciaTodos(pUsuario, fechaini, fechafinal, cod_zona);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiligenciaBusiness", "ListarReporteDiligenciaTodos", ex);
                return null;
            }
        }
    }
}