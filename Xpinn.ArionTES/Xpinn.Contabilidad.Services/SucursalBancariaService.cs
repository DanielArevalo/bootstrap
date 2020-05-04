using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Contabilidad.Business;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class SucursalBancariaService
    {
        private SucursalBancariaBusiness BOSucursalBancaria;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para SucursalBancaria
        /// </summary>
        public SucursalBancariaService()
        {
            BOSucursalBancaria = new SucursalBancariaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "30705"; } }

        /// <summary>
        /// Servicio para crear SucursalBancaria
        /// </summary>
        /// <param name="pEntity">Entidad SucursalBancaria</param>
        /// <returns>Entidad SucursalBancaria creada</returns>
        public SucursalBancaria CrearSucursalBancaria(SucursalBancaria vSucursalBancaria, Usuario pUsuario)
        {
            try
            {
                return BOSucursalBancaria.CrearSucursalBancaria(vSucursalBancaria, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SucursalBancariaService", "CrearSucursalBancaria", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar SucursalBancaria
        /// </summary>
        /// <param name="pSucursalBancaria">Entidad SucursalBancaria</param>
        /// <returns>Entidad SucursalBancaria modificada</returns>
        public SucursalBancaria ModificarSucursalBancaria(SucursalBancaria vSucursalBancaria, Usuario pUsuario)
        {
            try
            {
                return BOSucursalBancaria.ModificarSucursalBancaria(vSucursalBancaria, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SucursalBancariaService", "ModificarSucursalBancaria", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar SucursalBancaria
        /// </summary>
        /// <param name="pId">identificador de SucursalBancaria</param>
        public void EliminarSucursalBancaria(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOSucursalBancaria.EliminarSucursalBancaria(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarSucursalBancaria", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener SucursalBancaria
        /// </summary>
        /// <param name="pId">identificador de SucursalBancaria</param>
        /// <returns>Entidad SucursalBancaria</returns>
        public SucursalBancaria ConsultarSucursalBancaria(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOSucursalBancaria.ConsultarSucursalBancaria(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SucursalBancariaService", "ConsultarSucursalBancaria", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de SucursalBancarias a partir de unos filtros
        /// </summary>
        /// <param name="pSucursalBancaria">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de SucursalBancaria obtenidos</returns>
        public List<SucursalBancaria> ListarSucursalBancaria(SucursalBancaria vSucursalBancaria, Usuario pUsuario)
        {
            try
            {
                return BOSucursalBancaria.ListarSucursalBancaria(vSucursalBancaria, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SucursalBancariaService", "ListarSucursalBancaria", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario vusuario)
        {
            try
            {
                return BOSucursalBancaria.ObtenerSiguienteCodigo(vusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SucursalBancariaService", "ObtenerSiguienteCodigo", ex);
                return Int64.MinValue;
            }
        }

    }
}