using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Caja.Business;
using Xpinn.Caja.Entities; 

namespace Xpinn.Caja.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class TipoMotivoAnuService
    {
        private TipoMotivoAnuBusiness BOTipoMotivoAnu;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para TipoMotivoAnu
        /// </summary>
        public TipoMotivoAnuService()
        {
            BOTipoMotivoAnu = new TipoMotivoAnuBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }
        public string CodigoPrograma { get { return "40206"; } }

        /// <summary>
        /// Servicio para crear TipoMotivoAnu
        /// </summary>
        /// <param name="pEntity">Entidad TipoMotivoAnu</param>
        /// <returns>Entidad TipoMotivoAnu creada</returns>
        public TipoMotivoAnu CrearTipoMotivoAnu(TipoMotivoAnu pTipoMotivoAnu, Usuario pUsuario)
        {
            try
            {
                return BOTipoMotivoAnu.CrearTipoMotivoAnu(pTipoMotivoAnu, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoMotivoAnuService", "CrearTipoMotivoAnu", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar TipoMotivoAnu
        /// </summary>
        /// <param name="pTipoMotivoAnu">Entidad TipoMotivoAnu</param>
        /// <returns>Entidad TipoMotivoAnu modificada</returns>
        public TipoMotivoAnu ModificarTipoMotivoAnu(TipoMotivoAnu pTipoMotivoAnu, Usuario pUsuario)
        {
            try
            {
                return BOTipoMotivoAnu.ModificarTipoMotivoAnu(pTipoMotivoAnu, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoMotivoAnuService", "ModificarTipoMotivoAnu", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar TipoMotivoAnu
        /// </summary>
        /// <param name="pId">identificador de TipoMotivoAnu</param>
        public void EliminarTipoMotivoAnu(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOTipoMotivoAnu.EliminarTipoMotivoAnu(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarTipoMotivoAnu", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener TipoMotivoAnu
        /// </summary>
        /// <param name="pId">identificador de TipoMotivoAnu</param>
        /// <returns>Entidad TipoMotivoAnu</returns>
        public TipoMotivoAnu ConsultarTipoMotivoAnu(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOTipoMotivoAnu.ConsultarTipoMotivoAnu(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoMotivoAnuService", "ConsultarTipoMotivoAnu", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de TipoMotivoAnus a partir de unos filtros
        /// </summary>
        /// <param name="pTipoMotivoAnu">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoMotivoAnu obtenidos</returns>
        public List<TipoMotivoAnu> ListarTipoMotivoAnu(TipoMotivoAnu pTipoMotivoAnu, Usuario pUsuario)
        {
            try
            {
                return BOTipoMotivoAnu.ListarTipoMotivoAnu(pTipoMotivoAnu, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoMotivoAnuService", "ListarTipoMotivoAnu", ex);
                return null;
            }
        }


        public TipoMotivoAnu CrearTipoMotivoAnus(TipoMotivoAnu pTipoMotivoAnu, Usuario pusuario)
        {
            try
            {
                pTipoMotivoAnu = BOTipoMotivoAnu.CrearTipoMotivoAnus(pTipoMotivoAnu, pusuario);
                return pTipoMotivoAnu;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoMotivoAnuService", "CrearTipoMotivoAnu", ex);
                return null;
            }
        }


        public TipoMotivoAnu ModificarTipoMotivoAnus(TipoMotivoAnu pTipoMotivoAnu, Usuario pusuario)
        {
            try
            {
                pTipoMotivoAnu = BOTipoMotivoAnu.ModificarTipoMotivoAnus(pTipoMotivoAnu, pusuario);
                return pTipoMotivoAnu;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoMotivoAnuService", "ModificarTipoMotivoAnu", ex);
                return null;
            }
        }


        public void EliminarTipoMotivoAnus(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOTipoMotivoAnu.EliminarTipoMotivoAnus(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoMotivoAnuService", "EliminarTipoMotivoAnu", ex);
            }
        }


        public TipoMotivoAnu ConsultarTipoMotivoAnun(Int64 pId, Usuario pusuario)
        {
            try
            {
                TipoMotivoAnu TipoMotivoAnu = new TipoMotivoAnu();
                TipoMotivoAnu = BOTipoMotivoAnu.ConsultarTipoMotivoAnun(pId, pusuario);
                return TipoMotivoAnu;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoMotivoAnuService", "ConsultarTipoMotivoAnu", ex);
                return null;
            }
        }


        public List<TipoMotivoAnu> ListarTipoMotivoAnus(TipoMotivoAnu pTipoMotivoAnu, Usuario pusuario, String filtro)
        {
            try
            {
                return BOTipoMotivoAnu.ListarTipoMotivoAnus(pTipoMotivoAnu, pusuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoMotivoAnuService", "ListarTipoMotivoAnu", ex);
                return null;
            }
        }
 

    }
}