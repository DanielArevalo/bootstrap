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
    public class TranAporteServices
    {
        private TranAporteBusiness BOTranAporte;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para TranAporte
        /// </summary>
        public TranAporteServices()
        {
            BOTranAporte = new TranAporteBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "170202"; } }

        /// <summary>
        /// Servicio para crear TranAporte
        /// </summary>
        /// <param name="pEntity">Entidad TranAporte</param>
        /// <returns>Entidad TranAporte creada</returns>
        public TranAporte CrearTranAporte(TranAporte vTranAporte, Usuario pUsuario)
        {
            try
            {
                return BOTranAporte.CrearTranAporte(vTranAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TranAporteService", "CrearTranAporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar TranAporte
        /// </summary>
        /// <param name="pTranAporte">Entidad TranAporte</param>
        /// <returns>Entidad TranAporte modificada</returns>
        public TranAporte ModificarTranAporte(TranAporte vTranAporte, Usuario pUsuario)
        {
            try
            {
                return BOTranAporte.ModificarTranAporte(vTranAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TranAporteService", "ModificarTranAporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar TranAporte
        /// </summary>
        /// <param name="pId">identificador de TranAporte</param>
        public void EliminarTranAporte(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOTranAporte.EliminarTranAporte(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarTranAporte", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener TranAporte
        /// </summary>
        /// <param name="pId">identificador de TranAporte</param>
        /// <returns>Entidad TranAporte</returns>
        public TranAporte ConsultarTranAporte(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOTranAporte.ConsultarTranAporte(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TranAporteService", "ConsultarTranAporte", ex);
                return null;
            }
        }

        public List<TranAporte> ListarTipoComp(TranAporte pTranAporte, Usuario pUsuario)
        {
            try
            {
                return BOTranAporte.ListarTranAporte(pTranAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TranAporteService", "ListarTipoComp", ex);
                return null;
            }
        }

    }
}