using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Obligaciones.Business;
using Xpinn.Obligaciones.Entities;

namespace Xpinn.Obligaciones.Services
{
    /// <summary>
    /// Servicios para Tipo Liquidacion
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class LineaObligacionService
    {
        private LineaObligacionBusiness BOLineaObligacion;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para LineaObligacion
        /// </summary>
        public LineaObligacionService()
        {
            BOLineaObligacion = new LineaObligacionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "130201"; } }
        
        /// <summary>
        /// Servicio para obtener lista de Tipo de Liquidacion a partir de unos filtros
        /// </summary>
        /// <param name="pSolicitud">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de LineaObligacion obtenidos</returns>
        public List<LineaObligacion> ListarLineaObligacion(LineaObligacion pTipLiq, Usuario pUsuario)
        {
            try
            {
                return BOLineaObligacion.ListarLineaObligacion(pTipLiq, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaObligacionService", "ListarLineaObligacion", ex);
                return null;
            }
        }


        /// <summary>
        /// Elimina una Linea de Obligacion
        /// </summary>
        /// <param name="pId">identificador de la oficina</param>
        public void EliminarLineaOb(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOLineaObligacion.EliminarLineaOb(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaObligacionService", "EliminarLineaOb", ex);
            }
        }



        /// <summary>
        /// Crea una LineaObligacion
        /// </summary>
        /// <param name="pEntity">Entidad oficina</param>
        /// <returns>Entidad creada</returns>
        public LineaObligacion CrearLineaOb(LineaObligacion pLineaObligacion, Usuario pUsuario)
        {
            try
            {
                return BOLineaObligacion.CrearLineaOb(pLineaObligacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaObligacionService", "CrearLineaOb", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica una LineaObligacion
        /// </summary>
        /// <param name="pEntity">Entidad LineaObligacion</param>
        /// <returns>Entidad modificada</returns>
        public LineaObligacion ModificarLineaObligacion(LineaObligacion pLineaObligacion, Usuario pUsuario)
        {
            try
            {
                return BOLineaObligacion.ModificarLineaOb(pLineaObligacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaObligacionService", "ModificarLineaOb", ex);
                return null;
            }

        }

        /// <summary>
        /// Obtiene una Linea de Obligacion
        /// </summary>
        /// <param name="pId">identificador de la Linea</param>
        /// <returns>Linea consultada</returns>
        public LineaObligacion ConsultarLineaOb(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOLineaObligacion.ConsultarLineaOb(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaObligacionService", "ConsultarLineaOb", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene el conteo de usuarios asociados a la oficina especifica 
        /// </summary>
        /// <param name="pId">identificador de la TipoLiquidacion</param>
        /// <returns>TipoLiquidacion consultada</returns>
        public LineaObligacion ConsultarObligacionXLineaObligacion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOLineaObligacion.ConsultarObligacionXLineaObligacion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaObligacionService", "ConsultarObligacionXLineaObligacion", ex);
                return null;
            }
        }

    }
}
