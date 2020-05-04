using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;
using System.Data;
  
namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicio para Aprobador
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class AprobadorService
    {
        private AprobadorBusiness BOAprobador;
        private ExcepcionBusiness BOExcepcion; 

        /// <summary>
        /// Constructor del servicio para Caja
        /// </summary>
        public AprobadorService()
        {
            BOAprobador = new AprobadorBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100203"; } }

        /// <summary>
        /// Crea un Aprobador
        /// </summary>
        /// <param name="pEntity">Entidad Programa</param>
        /// <returns>Entidad creada</returns>
        public Aprobador CrearAprobador(Aprobador pAprobador, Usuario pUsuario)
        {
            try
            {
                return BOAprobador.CrearAprobador(pAprobador, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobadorService", "CrearAprobador", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de aprobadores
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de Aprobadores obtenidos</returns>
        public List<Aprobador> ListarAprobador(Aprobador pAprobador, Usuario pUsuario)
        {
            try
            {
                return BOAprobador.ListarAprobador(pAprobador, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobadorService", "ListarAprobador", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de aprobadores
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de Aprobadores obtenidos</returns>
        public List<Aprobador> ListarAprobadorActa(Aprobador pAprobador, Usuario pUsuario)
        {
            try
            {
                return BOAprobador.ListarAprobadorActa(pAprobador, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobadorService", "ListarAprobadorActa", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de aprobadores
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de Aprobadores obtenidos</returns>
        public List<Aprobador> ListarAprobadorActaRestructurados(Aprobador pAprobador, Usuario pUsuario)
        {
            try
            {
                return BOAprobador.ListarAprobadorActaRestructurados(pAprobador, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobadorService", "ListarAprobadorActaRestructurados", ex);
                return null;
            }
        }


        /// <summary>
        /// Elimina un aprobador
        /// </summary>
        /// <param name="pId">identificador del aprobador</param>
        public void EliminarAprobador(Int32 pId, Usuario pUsuario)
        {
            try
            {
                BOAprobador.EliminarAprobador(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobadorService", "EliminarOficina", ex);
            }
        }

        /// <summary>
        /// Modifica un Aprobador
        /// </summary>
        /// <param name="pEntity">Entidad Aprobador</param>
        /// <returns>Entidad modificada</returns>
        public Aprobador ModificarAprobador(Aprobador paprobador, Usuario pUsuario)
        {
            try
            {
                return BOAprobador.ModificarAprobador(paprobador, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobadorService", "ModificarAprobador", ex);
                return null;
            }

        }

        /// <summary>
        /// Obtiene la lista de aprobadores
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de Aprobadores obtenidos</returns>
        public Aprobador ConsultarAprobador(Aprobador pAprobador, Usuario pUsuario)
        {
            try
            {
                return BOAprobador.ConsultarAprobador(pAprobador, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobadorService", "ConsultarAprobador", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene la lista de aprobadores
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de Aprobadores obtenidos</returns>
        public Aprobador ConsultarAprobadorActa(Aprobador pAprobador, Usuario pUsuario)
        {
            try
            {
                return BOAprobador.ConsultarAprobadorActa(pAprobador, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobadorService", "ConsultarAprobadorActa", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene un Id aprobador
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Id Aprobador obtenido</returns>
        public object UltimoIdAprobador(Usuario pUsuario)
        {
            try
            {
                return BOAprobador.UltimoIdAprobador(pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobadorService", "UltimoIdAprobador", ex);
                return null;
            }
        }
    }
}
