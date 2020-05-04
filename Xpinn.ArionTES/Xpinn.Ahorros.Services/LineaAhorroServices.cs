using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Ahorros.Business;
using Xpinn.Ahorros.Entities;

namespace Xpinn.Ahorros.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class LineaAhorroServices
    {
        private LineaAhorroBusiness BOLineaAhorro;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para LineaAhorro
        /// </summary>
        public LineaAhorroServices()
        {
            BOLineaAhorro = new LineaAhorroBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "220101"; } }

        /// <summary>
        /// Servicio para crear LineaAhorro
        /// </summary>
        /// <param name="pEntity">Entidad LineaAhorro</param>
        /// <returns>Entidad LineaAhorro creada</returns>
        public LineaAhorro CrearLineaAhorro(LineaAhorro vLineaAhorro, Usuario pUsuario)
        {
            try
            {
                return BOLineaAhorro.CrearLineaAhorro(vLineaAhorro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAhorroservice", "CrearLineaAhorro", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar LineaAhorro
        /// </summary>
        /// <param name="pLineaAhorro">Entidad LineaAhorro</param>
        /// <returns>Entidad LineaAhorro modificada</returns>
        public LineaAhorro ModificarLineaAhorro(LineaAhorro vLineaAhorro, Usuario pUsuario)
        {
            try
            {
                return BOLineaAhorro.ModificarLineaAhorro(vLineaAhorro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAhorroservice", "ModificarLineaAhorro", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar LineaAhorro
        /// </summary>
        /// <param name="pId">identificador de LineaAhorro</param>
        public void EliminarLineaAhorro(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOLineaAhorro.EliminarLineaAhorro(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarLineaAhorro", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener LineaAhorro
        /// </summary>
        /// <param name="pId">identificador de LineaAhorro</param>
        /// <returns>Entidad LineaAhorro</returns>
        public LineaAhorro ConsultarLineaAhorro(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOLineaAhorro.ConsultarLineaAhorro(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAhorroservice", "ConsultarLineaAhorro", ex);
                return null;
            }
        }

        public List<LineaAhorro> ListarLineaAhorro(LineaAhorro pLineaAhorro, Usuario pUsuario)
        {
            try
            {
                return BOLineaAhorro.ListarLineaAhorro(pLineaAhorro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAhorroservice", "ListarLineaAhorro", ex);
                return null;
            }
        }


    }
}