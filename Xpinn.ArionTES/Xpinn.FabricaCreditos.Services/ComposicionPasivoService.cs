using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ComposicionPasivoService
    {
        private ComposicionPasivoBusiness BOComposicionPasivo;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para ComposicionPasivo
        /// </summary>
        public ComposicionPasivoService()
        {
            BOComposicionPasivo = new ComposicionPasivoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100102"; } } //100123

        public string CodigoProgramaobli { get { return "100102"; } }  //100151

        /// <summary>
        /// Servicio para crear ComposicionPasivo
        /// </summary>
        /// <param name="pEntity">Entidad ComposicionPasivo</param>
        /// <returns>Entidad ComposicionPasivo creada</returns>
        public ComposicionPasivo CrearComposicionPasivo(ComposicionPasivo pComposicionPasivo, Usuario pUsuario)
        {
            try
            {
                return BOComposicionPasivo.CrearComposicionPasivo(pComposicionPasivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComposicionPasivoService", "CrearComposicionPasivo", ex);
                return null;
            }
        }

        public ComposicionPasivo creaobligacion(ComposicionPasivo pComposicionPasivo, Usuario pUsuario)
        {
            try
            {
                return BOComposicionPasivo.creaobligacion(pComposicionPasivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComposicionPasivoService", "CrearComposicionPasivo", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para modificar ComposicionPasivo
        /// </summary>
        /// <param name="pComposicionPasivo">Entidad ComposicionPasivo</param>
        /// <returns>Entidad ComposicionPasivo modificada</returns>
        public ComposicionPasivo ModificarComposicionPasivo(ComposicionPasivo pComposicionPasivo, Usuario pUsuario)
        {
            try
            {
                return BOComposicionPasivo.ModificarComposicionPasivo(pComposicionPasivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComposicionPasivoService", "ModificarComposicionPasivo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar ComposicionPasivo
        /// </summary>
        /// <param name="pId">identificador de ComposicionPasivo</param>
        public void EliminarComposicionPasivo(Int64 pId, Usuario pUsuario, Int64 Cod_persona, Int64 Cod_InfFin)
        {
            try
            {
                BOComposicionPasivo.EliminarComposicionPasivo(pId, pUsuario, Cod_persona, Cod_InfFin);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarComposicionPasivo", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener ComposicionPasivo
        /// </summary>
        /// <param name="pId">identificador de ComposicionPasivo</param>
        /// <returns>Entidad ComposicionPasivo</returns>
        public ComposicionPasivo ConsultarComposicionPasivo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOComposicionPasivo.ConsultarComposicionPasivo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComposicionPasivoService", "ConsultarComposicionPasivo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de ComposicionPasivos a partir de unos filtros
        /// </summary>
        /// <param name="pComposicionPasivo">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ComposicionPasivo obtenidos</returns>
        public List<ComposicionPasivo> ListarComposicionPasivo(ComposicionPasivo pComposicionPasivo, Usuario pUsuario)
        {
            try
            {
                return BOComposicionPasivo.ListarComposicionPasivo(pComposicionPasivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComposicionPasivoService", "ListarComposicionPasivo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de ComposicionPasivos a partir de unos filtros
        /// </summary>
        /// <param name="pComposicionPasivo">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ComposicionPasivo obtenidos</returns>
        public List<ComposicionPasivo> ListarComposicionPasivoRepo(ComposicionPasivo pComposicionPasivo, Usuario pUsuario)
        {
            try
            {
                return BOComposicionPasivo.ListarComposicionPasivoRepo(pComposicionPasivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComposicionPasivoService", "ListarComposicionPasivoRepo", ex);
                return null;
            }
        }

        public List<ComposicionPasivo> Listarobligacion(long infin, Usuario pUsuario)
        {
            try
            {
                return BOComposicionPasivo.Listarobligacion(infin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComposicionPasivoService", "ListarComposicionPasivo", ex);
                return null;
            }
        }
    }
}