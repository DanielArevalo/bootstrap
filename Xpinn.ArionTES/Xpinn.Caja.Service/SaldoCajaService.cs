using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Caja.Business;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class SaldoCajaService
    {
        private SaldoCajaBusiness BOSaldoCaja;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para SaldoCaja
        /// </summary>
        public SaldoCajaService()
        {
            BOSaldoCaja = new SaldoCajaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "SG040"; } }

        /// <summary>
        /// Servicio para crear SaldoCaja
        /// </summary>
        /// <param name="pEntity">Entidad SaldoCaja</param>
        /// <returns>Entidad SaldoCaja creada</returns>
        public SaldoCaja CrearSaldoCaja(SaldoCaja pSaldoCaja, Usuario pUsuario)
        {
            try
            {
                return BOSaldoCaja.CrearSaldoCaja(pSaldoCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SaldoCajaService", "CrearSaldoCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar SaldoCaja
        /// </summary>
        /// <param name="pSaldoCaja">Entidad SaldoCaja</param>
        /// <returns>Entidad SaldoCaja modificada</returns>
        public SaldoCaja ModificarSaldoCaja(SaldoCaja pSaldoCaja, Usuario pUsuario)
        {
            try
            {
                return BOSaldoCaja.ModificarSaldoCaja(pSaldoCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SaldoCajaService", "ModificarSaldoCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar SaldoCaja
        /// </summary>
        /// <param name="pId">identificador de SaldoCaja</param>
        public void EliminarSaldoCaja(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOSaldoCaja.EliminarSaldoCaja(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarSaldoCaja", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener SaldoCaja
        /// </summary>
        /// <param name="pId">identificador de SaldoCaja</param>
        /// <returns>Entidad SaldoCaja</returns>
        public SaldoCaja ConsultarSaldoCaja(SaldoCaja pSaldoCaja, Usuario pUsuario)
        {
            try
            {
                return BOSaldoCaja.ConsultarSaldoCaja(pSaldoCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SaldoCajaService", "ConsultarSaldoCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de SaldoCajas a partir de unos filtros
        /// </summary>
        /// <param name="pSaldoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de SaldoCaja obtenidos</returns>
        public List<SaldoCaja> ListarSaldoCaja(SaldoCaja pSaldoCaja, Usuario pUsuario)
        {
            try
            {
                return BOSaldoCaja.ListarSaldoCaja(pSaldoCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SaldoCajaService", "ListarSaldoCaja", ex);
                return null;
            }
        }

        public SaldoCaja ConsultarSaldoTesoreriaConsig(SaldoCaja saldo, Usuario usuario)
        {
            try
            {
                return BOSaldoCaja.ConsultarSaldoTesoreriaConsig(saldo, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SaldoCajaService", "ConsultarSaldoTesoreriaConsig", ex);
                return null;
            }
        }
    }
}