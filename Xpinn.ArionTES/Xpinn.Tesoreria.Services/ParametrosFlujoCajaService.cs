using System;
using System.Collections.Generic;
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
    public class ParametrosFlujoCajaService
    {
        private ParametrosFlujoCajaBusiness BOParametrosFlujoCaja;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para SoporteCaj
        /// </summary>
        public ParametrosFlujoCajaService()
        {
            BOParametrosFlujoCaja = new ParametrosFlujoCajaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }
        public string CodigoPrograma { get { return "40901"; } }

        public string CodigoProgramaR { get { return "40902"; } }


        /// <summary>
        /// Servicio para crear Concepto y Cuentas
        /// </summary>
        /// <param name="ParametrosFlujoCaj">Entidad ParametrosFlujoCaj</param>
        /// <returns>Entidad ParametrosFlujoCaj creada</returns>
        public ParametrosFlujoCaja CrearConceptoCuenta(ParametrosFlujoCaja pFlujoCajaConcepto, List<ParametrosFlujoCaja> lstConceptoCuenta, Usuario pUsuario)
        {
            try
            {
                return BOParametrosFlujoCaja.CrearConceptoCuenta(pFlujoCajaConcepto, lstConceptoCuenta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosFlujoCajaService", "CrearConceptoCuenta", ex);
                return null;
            }
        }

        public ParametrosFlujoCaja ModificarConceptoCuenta(ParametrosFlujoCaja pFlujoCajaConcepto, List<ParametrosFlujoCaja> lstConceptoCuenta, Usuario pUsuario)
        {
            try
            {
                return BOParametrosFlujoCaja.ModificarConceptoCuenta(pFlujoCajaConcepto, lstConceptoCuenta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosFlujoCajaService", "CrearConceptoCuenta", ex);
                return null;
            }
        }

        public ParametrosFlujoCaja ConsultarConceptoCuenta(Int64 cod_concepto, Usuario pUsuario)
        {
            try
            {
                return BOParametrosFlujoCaja.ConsultarConceptoCuenta(cod_concepto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosFlujoCajaService", "ConsultarConceptoCuenta", ex);
                return null;
            }
        }

        public List<ParametrosFlujoCaja> ListarConceptos(ParametrosFlujoCaja pConcepto, Usuario pUsuario)
        {
            try
            {
                return BOParametrosFlujoCaja.ListarConceptos(pConcepto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosFlujoCajaService", "ListarConceptos", ex);
                return null;
            }
        }

        public List<ParametrosFlujoCaja> ListarCuentas(Int64 cod_concepto, Usuario pUsuario)
        {
            try
            {
                return BOParametrosFlujoCaja.ListarCuentas(cod_concepto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosFlujoCajaService", "ListarCuentas", ex);
                return null;
            }
        }

        public void EliminarConcepto(Int64 cod_concepto, Usuario pUsuario)
        {
            try
            {
                BOParametrosFlujoCaja.EliminarConcepto(cod_concepto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosFlujoCajaService", "EliminarConcepto", ex);
            }
        }

        public void EliminarConceptoCuenta(Int64 cod_cuenta_con, Usuario pUsuario)
        {
            try
            {
                BOParametrosFlujoCaja.EliminarConceptoCuenta(cod_cuenta_con, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosFlujoCajaService", "EliminarConceptoCuenta", ex);
            }
        }

        public List<ParametrosFlujoCaja> ListarConceptosReporte(DateTime fecha_inicial, DateTime fecha_final, Usuario pUsuario)
        {
            try
            {
                return BOParametrosFlujoCaja.ListarConceptosReporte(fecha_inicial, fecha_final, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosFlujoCajaService", "ListarConceptosReporte", ex);
                return null;
            }
        }

        public List<string> ListarTitulos(DateTime fecha_inicial, DateTime fecha_final, Usuario pUsuario)
        {
            try
            {
                return BOParametrosFlujoCaja.ListarTitulos(fecha_inicial, fecha_final, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosFlujoCajaBusiness", "ListarTitulos", ex);
                return null;
            }
        }
    }
}
