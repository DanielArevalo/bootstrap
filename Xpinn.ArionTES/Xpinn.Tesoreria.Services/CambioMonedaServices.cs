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
    public class CambioMonedaServices
    {
        private CambioMonedaBusiness BOCambio;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para AreasCaj
        /// </summary>
        public CambioMonedaServices()
        {
            BOCambio = new CambioMonedaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "40204"; } }


        public CambioMoneda CrearCambioMoneda(CambioMoneda pCambio, Usuario vUsuario)
        {
            try
            {
                return BOCambio.CrearCambioMoneda(pCambio, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CambioMonedaServices", "CrearCambioMoneda", ex);
                return null;
            }
        }


        public CambioMoneda ModificarCambioMoneda(CambioMoneda pCambio, Usuario vUsuario)
        {
            try
            {
                return BOCambio.ModificarCambioMoneda(pCambio, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CambioMonedaServices", "ModificarCambioMoneda", ex);
                return null;
            }
        }


        public CambioMoneda ConsultarCambioMoneda(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOCambio.ConsultarCambioMoneda(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CambioMonedaServices", "ConsultarCambioMoneda", ex);
                return null;
            }
        }


        public List<CambioMoneda> ListarCambioMoneda(string filtro, Usuario vUsuario)
        {
            try
            {
                return BOCambio.ListarCambioMoneda(filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CambioMonedaServices", "ListarCambioMoneda", ex);
                return null;
            }
        }


        public void EliminarCambioMoneda(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOCambio.EliminarCambioMoneda(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CambioMonedaServices", "EliminarCambioMoneda", ex);
            }
        }


    }
}