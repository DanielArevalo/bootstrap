using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.ActivosFijos.Business;
using Xpinn.ActivosFijos.Entities;

namespace Xpinn.ActivosFijos.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class TipoArticuloservices
    {
        private TipoArticuloBusiness BOTipoArticulo;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para ActivoFijo
        /// </summary>
        public TipoArticuloservices()
        {
            BOTipoArticulo = new TipoArticuloBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "50206"; } }

        /// <summary>
        /// Servicio para crear ActivoFijo
        /// </summary>
        /// <param name="pEntity">Entidad ActivoFijo</param>
        /// <returns>Entidad ActivoFijo creada</returns>
        public TipoArticulo CrearTipoArticulo(TipoArticulo vTipoArticulo, Usuario pUsuario)
        {
            try
            {
                return BOTipoArticulo .CrearTipoArticulo(vTipoArticulo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijoservice", "CrearActivoFijo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar ActivoFijo
        /// </summary>
        /// <param name="pActivoFijo">Entidad ActivoFijo</param>
        /// <returns>Entidad ActivoFijo modificada</returns>
        public TipoArticulo ModificarTipoArticulo(TipoArticulo  vTipoArticulo, Usuario pUsuario)
        {
            try
            {
                return BOTipoArticulo.ModificarTipoArticulo(vTipoArticulo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijoservice", "ModificarActivoFijo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar ActivoFijo
        /// </summary>
        /// <param name="pId">identificador de ActivoFijo</param>
        public void EliminarTipoArticulo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOTipoArticulo.EliminarTipoArticulo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarActivoFijo", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener ActivoFijo
        /// </summary>
        /// <param name="pId">identificador de ActivoFijo</param>
        /// <returns>Entidad ActivoFijo</returns>
        public TipoArticulo  ConsultarTipoArticulo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOTipoArticulo.ConsultarTipoArticulo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijoservice", "ConsultarActivoFijo", ex);
                return null;
            }
        }

        public List<TipoArticulo> ListarTipoArticulo(TipoArticulo pTipoArticulo, Usuario pUsuario)
        {
            try
            {
                return BOTipoArticulo.ListarTipoArticulo(pTipoArticulo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijoservice", "ListarActivoFijo", ex);
                return null;
            }
        }

       

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return BOTipoArticulo  .ObtenerSiguienteCodigo(pUsuario);
            }
            catch
            {
                return 1;
            }
        }

     


    }
}