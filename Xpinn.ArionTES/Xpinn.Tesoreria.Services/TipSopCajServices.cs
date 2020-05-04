using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Tesoreria.Business;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class TipSopCajService
    {
        private TipSopCajBusiness BOTipSopCaj;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para TipSopCaj
        /// </summary>
        public TipSopCajService()
        {
            BOTipSopCaj = new TipSopCajBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "40601"; } }

        /// <summary>
        /// Servicio para crear TipSopCaj
        /// </summary>
        /// <param name="pEntity">Entidad TipSopCaj</param>
        /// <returns>Entidad TipSopCaj creada</returns>
        public TipSopCaj CrearTipSopCaj(TipSopCaj vTipSopCaj, Usuario pUsuario)
        {
            try
            {
                return BOTipSopCaj.CrearTipSopCaj(vTipSopCaj, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipSopCajService", "CrearTipSopCaj", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar TipSopCaj
        /// </summary>
        /// <param name="pTipSopCaj">Entidad TipSopCaj</param>
        /// <returns>Entidad TipSopCaj modificada</returns>
        public TipSopCaj ModificarTipSopCaj(TipSopCaj vTipSopCaj, Usuario pUsuario)
        {
            try
            {
                return BOTipSopCaj.ModificarTipSopCaj(vTipSopCaj, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipSopCajService", "ModificarTipSopCaj", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar TipSopCaj
        /// </summary>
        /// <param name="pId">identificador de TipSopCaj</param>
        public void EliminarTipSopCaj(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOTipSopCaj.EliminarTipSopCaj(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarTipSopCaj", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener TipSopCaj
        /// </summary>
        /// <param name="pId">identificador de TipSopCaj</param>
        /// <returns>Entidad TipSopCaj</returns>
        public TipSopCaj ConsultarTipSopCaj(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOTipSopCaj.ConsultarTipSopCaj(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipSopCajService", "ConsultarTipSopCaj", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de TipSopCaj 
        /// </summary>
        /// <param name="pTipSopCaj">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipSopCaj obtenidos</returns>
        public List<TipSopCaj> ListarTipSopCaj(TipSopCaj vTipSopCaj, Usuario pUsuario)
        {
            try
            {
                return BOTipSopCaj.ListarTipSopCaj(vTipSopCaj, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipSopCajService", "ListarTipSopCaj", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtener el siguiente código
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return BOTipSopCaj.ObtenerSiguienteCodigo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipSopCajService", "ObtenerSiguienteCodigo", ex);
                return 0;
            }
        }

    }
}