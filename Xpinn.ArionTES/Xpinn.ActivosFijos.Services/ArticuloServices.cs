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
    public class Articuloservices
    {
        private ArticuloBusiness BOArticulo;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para ActivoFijo
        /// </summary>
        public Articuloservices()
        {
            BOArticulo = new ArticuloBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "50207"; } }

        /// <summary>
        /// Servicio para crear ActivoFijo
        /// </summary>
        /// <param name="pEntity">Entidad ActivoFijo</param>
        /// <returns>Entidad ActivoFijo creada</returns>
        public Articulo CrearTipoArticulo(Articulo vArticulo, Usuario pUsuario)
        {
            try
            {
                return BOArticulo.CrearArticulo(vArticulo, pUsuario);
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
        public Articulo ModificarArticulo(Articulo vArticulo, Usuario pUsuario)
        {
            try
            {
                return BOArticulo.ModificarArticulo(vArticulo, pUsuario);
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
        public void EliminarArticulo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOArticulo.EliminarArticulo(pId, pUsuario);
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
        public Articulo  ConsultarArticulo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOArticulo.ConsultarArticulo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijoservice", "ConsultarActivoFijo", ex);
                return null;
            }
        }

        public List<Articulo> ListarArticulo(Articulo pArticulo, Usuario pUsuario)
        {
            try
            {
                return BOArticulo.ListarArticulo(pArticulo, pUsuario);
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
                return BOArticulo  .ObtenerSiguienteCodigo(pUsuario);
            }
            catch
            {
                return 1;
            }
        }

     


    }
}