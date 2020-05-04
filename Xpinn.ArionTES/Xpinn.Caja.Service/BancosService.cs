using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Caja.Business;
using Xpinn.Caja.Entities;
using Xpinn.Util;

namespace Xpinn.Caja.Services
{
    /// <summary>
    /// Servicio para Bancos
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class BancosService
    {
        private BancosBusiness BOBancos;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Bancos
        /// </summary>
        public BancosService()
        {
            BOBancos = new BancosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "30703"; } }
        public string CodigoBancos;

        /// <summary>
        /// Obtiene la lista de Bancoses dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Bancoses obtenidos</returns>
        public List<Bancos> ListarBancos(Bancos pBancos, Usuario pUsuario)
        {
            try
            {
                return BOBancos.ListarBancos(pBancos, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BancosService", "ListarBancos", ex);
                return null;
            }
        }

        public List<Bancos> ListarBancos(Bancos pBancos, int pOrden, Usuario pUsuario)
        {
            try
            {
                return BOBancos.ListarBancos(pBancos, pOrden, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BancosService", "ListarBancos", ex);
                return null;
            }
        }

        public List<Bancos> ListarBancosegre(Usuario pUsuario)
        {
            try
            {
                return BOBancos.ListarBancosegre(pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BancosService", "ListarBancos", ex);
                return null;
            }
        }

        public List<Bancos> ListarCuentaBancaria_Bancos(Usuario pUsuario)
        {
            try
            {
                return BOBancos.ListarCuentaBancaria_Bancos(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BancosService", "ListarCuentaBancaria_Bancos", ex);
                return null;
            }
        }

        public List<Bancos> ListarBancosegrecuentas(string codigo, Usuario pUsuario)
        {
            try
            {
                return BOBancos.ListarBancosegrecuentas(codigo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BancosService", "ListarBancos", ex);
                return null;
            }
        }

        public string soporte(string codigo, Usuario pUsuario)
        {
            try
            {
                return BOBancos.soporte(codigo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BancosService", "ListarBancos", ex);
                return null;
            }
        }


        public string Ruta_Cheque(string codigo, Usuario pUsuario)
        {
            try
            {
                return BOBancos.Ruta_Cheque(codigo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BancosService", "ListarBancos", ex);
                return null;
            }
        }


        public Xpinn.Tesoreria.Entities.Chequera ConsultaChequera(string codigo, Usuario pUsuario)
        {
            try
            {
                return BOBancos.ConsultaChequera(codigo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BancosService", "ConsultaChequera", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Bancoses dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Bancoses obtenidos</returns>
        public List<Bancos> ListarBancosEntidad(Bancos pBancos, Usuario pUsuario)
        {
            try
            {
                return BOBancos.ListarBancosEntidad(pBancos, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BancosService", "ListarBancosEntidad", ex);
                return null;
            }
        }

        public List<CuentaBancaria> ListarCuentaBancos(Int64 pCodBanco, Usuario pUsuario)
        {
            try
            {
                return BOBancos.ListarCuentaBancos(pCodBanco, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BancosService", "ListarCuentaBancos", ex);
                return null;
            }
        }

        public Bancos ConsultarBancos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOBancos.ConsultarBancos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BancosService", "ConsultarBancos", ex);
                return null;
            }
        }

        public int? ConsultarTipoCuenta(string numeroCuenta, Usuario pUsuario)
        {
            try
            {
                return BOBancos.ConsultarTipoCuenta(numeroCuenta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BancosService", "ConsultarTipoCuenta", ex);
                return null;
            }
        }

        public Bancos CrearBancos(Bancos pBancos, Usuario vUsuario)
        {
            try
            {
                return BOBancos.CrearBancos(pBancos, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BancosService", "CrearBancos", ex);
                return null;
            }
        }

        public Bancos ModificarBancos(Bancos pBancos, Usuario vUsuario)
        {
            try
            {
                return BOBancos.ModificarBancos(pBancos, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BancosService", "ModificarBancos", ex);
                return null;
            }
        }

        public void EliminarBancos(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOBancos.EliminarBancos(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BancosService", "EliminarBancos", ex);
                return;
            }
        }

        public string ConsultaBancoPersona(string codigo, Usuario pUsuario)
        {
            try
            {
                return BOBancos.ConsultaBancoPersona(codigo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BancosService", "ConsultaBancoPersona", ex);
                return null;
            }
        }

    }
}
