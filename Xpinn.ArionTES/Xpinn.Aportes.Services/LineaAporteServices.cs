using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Aportes.Business;
using Xpinn.Aportes.Entities;

namespace Xpinn.Aportes.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class LineaAporteServices
    {
        private LineaAporteBusiness BOLineaAporte;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para LineaAporte
        /// </summary>
        public LineaAporteServices()
        {
            BOLineaAporte = new LineaAporteBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "170202"; } }

        /// <summary>
        /// Servicio para crear LineaAporte
        /// </summary>
        /// <param name="pEntity">Entidad LineaAporte</param>
        /// <returns>Entidad LineaAporte creada</returns>
        public LineaAporte CrearLineaAporte(LineaAporte vLineaAporte, Usuario pUsuario)
        {
            try
            {
                return BOLineaAporte.CrearLineaAporte(vLineaAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAporteService", "CrearLineaAporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar LineaAporte
        /// </summary>
        /// <param name="pLineaAporte">Entidad LineaAporte</param>
        /// <returns>Entidad LineaAporte modificada</returns>
        public LineaAporte ModificarLineaAporte(LineaAporte vLineaAporte, Usuario pUsuario)
        {
            try
            {
                return BOLineaAporte.ModificarLineaAporte(vLineaAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAporteService", "ModificarLineaAporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar LineaAporte
        /// </summary>
        /// <param name="pId">identificador de LineaAporte</param>
        public void EliminarLineaAporte(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOLineaAporte.EliminarLineaAporte(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarLineaAporte", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener LineaAporte
        /// </summary>
        /// <param name="pId">identificador de LineaAporte</param>
        /// <returns>Entidad LineaAporte</returns>
        public LineaAporte ConsultarLineaAporte(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOLineaAporte.ConsultarLineaAporte(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAporteService", "ConsultarLineaAporte", ex);
                return null;
            }
        }

        public List<LineaAporte> ListarLineaAporte(LineaAporte pLineaAporte, Usuario pUsuario)
        {
            try
            {
                return BOLineaAporte.ListarLineaAporte(pLineaAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAporteService", "ListarLineaAporte", ex);
                return null;
            }
        }

      

    }
}