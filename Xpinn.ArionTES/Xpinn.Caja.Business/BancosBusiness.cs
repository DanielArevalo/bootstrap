using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Caja.Data;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Business
{
    /// <summary>
    /// Objeto de negocio para Bancos
    /// </summary>
    public class BancosBusiness : GlobalData
    {
        private BancosData DABancos;

        /// <summary>
        /// Constructor del objeto de negocio para Bancos
        /// </summary>
        public BancosBusiness()
        {
            DABancos = new BancosData();
        }

        /// <summary>
        /// Obtiene la lista de Bancos dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Bancos obtenidos</returns>
        public List<Bancos> ListarBancos(Bancos pBancos, Usuario pUsuario)
        {
            try
            {
                return DABancos.ListarBancos(pBancos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BancosBusiness", "ListarBancos", ex);
                return null;
            }
        }

        public List<Bancos> ListarBancos(Bancos pBancos, int pOrden, Usuario pUsuario)
        {
            try
            {
                return DABancos.ListarBancos(pBancos, pOrden, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BancosBusiness", "ListarBancos", ex);
                return null;
            }
        }

        public List<Bancos> ListarBancosegre(Usuario pUsuario)
        {
            try
            {
                return DABancos.ListarBancosegre(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BancosBusiness", "ListarBancos", ex);
                return null;
            }
        }


        public List<Bancos> ListarCuentaBancaria_Bancos(Usuario pUsuario)
        {
            try
            {
                return DABancos.ListarCuentaBancaria_Bancos(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BancosBusiness", "ListarCuentaBancaria_Bancos", ex);
                return null;
            }
        }


        public List<Bancos> ListarBancosegrecuentas(string codigo, Usuario pUsuario)
        {
            try
            {
                return DABancos.ListarBancosegrecuentas(codigo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BancosBusiness", "ListarBancos", ex);
                return null;
            }
        }

        public string soporte(string codigo, Usuario pUsuario)
        {
            try
            {
                return DABancos.soporte(codigo, pUsuario);
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
                return DABancos.Ruta_Cheque(codigo, pUsuario);
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
                return DABancos.ConsultaChequera(codigo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BancosService", "ConsultaChequera", ex);
                return null;
            }
        }

        public List<Bancos> ListarBancosEntidad(Bancos pBancos, Usuario pUsuario)
        {
            try
            {
                return DABancos.ListarBancosEntidad(pBancos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BancosBusiness", "ListarBancosEntidad", ex);
                return null;
            }
        }


        public List<CuentaBancaria> ListarCuentaBancos(Int64 pCodBanco, Usuario pUsuario)
        {
            try
            {
                return DABancos.ListarCuentaBancos(pCodBanco, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BancosBusiness", "ListarCuentaBancos", ex);
                return null;
            }
        }


        public int? ConsultarTipoCuenta(string numeroCuenta, Usuario vusuario)
        {
            try
            {
                return DABancos.ConsultarTipoCuenta(numeroCuenta, vusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BancosBusiness", "ConsultarTipoCuenta", ex);
                return null;
            }
        }


        public Bancos ConsultarBancos(Int64 pId, Usuario vusuario)
        {
            try
            {
                return DABancos.ConsultarBancos(pId, vusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BancosBusiness", "ConsultarBancos", ex);
                return null;
            }
        }

        public Bancos CrearBancos(Bancos pBancos, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pBancos = DABancos.CrearBancos(pBancos, vUsuario);

                    ts.Complete();
                }

                return pBancos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BancosBusiness", "CrearBancos", ex);
                return null;
            }
        }

        public Bancos ModificarBancos(Bancos pBancos, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pBancos = DABancos.ModificarBancos(pBancos, vUsuario);

                    ts.Complete();
                }

                return pBancos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BancosBusiness", "ModificarBancos", ex);
                return null;
            }
        }

        public void EliminarBancos(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DABancos.EliminarBancos(pId, vUsuario);

                    ts.Complete();
                }

                return;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BancosBusiness", "ModificarBancos", ex);
                return;
            }
        }

        public string ConsultaBancoPersona(string codigo, Usuario pUsuario)
        {
            try
            {
                return DABancos.ConsultaBancoPersona(codigo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BancosBusiness", "ConsultaBancoPersona", ex);
                return null;
            }
        }


    }
}
