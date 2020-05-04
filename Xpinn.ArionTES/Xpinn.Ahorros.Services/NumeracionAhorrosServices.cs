using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Ahorros.Business;
using Xpinn.Ahorros.Entities;
using Xpinn.Comun.Entities;

namespace Xpinn.Ahorros.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class NumeracionAhorrosServices
    {
        private NumeracionAhorrosBusiness BOParametrizacion;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para NumeracionAhorros
        /// </summary>
        public NumeracionAhorrosServices()
        {
            BOParametrizacion = new NumeracionAhorrosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "220108"; } }
       
        
        /// <summary>
        /// Servicio para crear NumeracionAhorros
        /// </summary>
        /// <param name="pEntity">Entidad NumeracionAhorros</param>
        /// <returns>Entidad NumeracionAhorros creada</returns>
        public NumeracionAhorros CrearNumeracionAhorros(NumeracionAhorros vNumeracionAhorros, Usuario pUsuario)
        {
            try
            {
                return BOParametrizacion.CrearNumeracionAhorros(vNumeracionAhorros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("NumeracionAhorrosservice", "CrearNumeracionAhorros", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar NumeracionAhorros
        /// </summary>
        /// <param name="pNumeracionAhorros">Entidad NumeracionAhorros</param>
        /// <returns>Entidad NumeracionAhorros modificada</returns>
        public NumeracionAhorros ModificarNumeracionAhorros(NumeracionAhorros vNumeracionAhorros, Usuario pUsuario)
        {
            try
            {
                return BOParametrizacion.ModificarNumeracionAhorros(vNumeracionAhorros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("NumeracionAhorrosservice", "ModificarNumeracionAhorros", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar NumeracionAhorros
        /// </summary>
        /// <param name="pId">identificador de NumeracionAhorros</param>
        public void EliminarNumeracionAhorros(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOParametrizacion.EliminarNumeracionAhorros(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarNumeracionAhorros", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener NumeracionAhorros
        /// </summary>
        /// <param name="pId">identificador de NumeracionAhorros</param>
        /// <returns>Entidad NumeracionAhorros</returns>
        public NumeracionAhorros ConsultarNumeracionAhorros(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOParametrizacion.ConsultarNumeracionAhorros(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("NumeracionAhorrosservice", "ConsultarNumeracionAhorros", ex);
                return null;
            }
        }

        public List<NumeracionAhorros> ListarNumeracionAhorros(NumeracionAhorros pNumeracionAhorros, Usuario pUsuario)
        {
            try
            {
                return BOParametrizacion.ListarNumeracionAhorros(pNumeracionAhorros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("NumeracionAhorrosservice", "ListarNumeracionAhorros", ex);
                return null;
            }
        }

        public List<NumeracionAhorros> ListarNumeracionAhorrosLiquidacion(DateTime pFechaLiquidacion, NumeracionAhorros pNumeracionAhorros, Usuario pUsuario)
        {
            try
            {
                return BOParametrizacion.ListarNumeracionAhorros(pNumeracionAhorros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("NumeracionAhorrosservice", "ListarNumeracionAhorros", ex);
                return null;
            }
        }

        public bool LiquidacionNumeracionAhorros(DateTime pFechaLiquidacion, List<NumeracionAhorros> pNumeracionAhorros, Int64 pcod_proceso, ref Int64 pnum_comp, ref Int64 ptipo_comp, ref string Error, ref Int64 CodOpe, Usuario pUsuario)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("NumeracionAhorrosservice", "ListarNumeracionAhorros", ex);
                return false;
            }
        }

        public Boolean GeneraNumeroCuenta(Usuario pUsuario)
        {
            return BOParametrizacion.GeneraNumeroCuenta(pUsuario);
        }
       
    }
}