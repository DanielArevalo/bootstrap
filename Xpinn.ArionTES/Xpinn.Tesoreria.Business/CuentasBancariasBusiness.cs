using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;
using Xpinn.Contabilidad.Business;
using System.Web.UI.WebControls;

namespace Xpinn.Tesoreria.Business
{
    public class CuentasBancariasBusiness : GlobalData
    {

        private CuentasBancariasData DACuentas;

        public CuentasBancariasBusiness()
        {
            DACuentas = new CuentasBancariasData();
        }

        public CuentasBancarias CrearCuentasContables(CuentasBancarias pCuent, Usuario vUsuario, int opcion)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCuent = DACuentas.CrearCuentasContables(pCuent, vUsuario,opcion);
                    ts.Complete();
                }

                return pCuent;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasBancariasBusiness", "CrearCuentasContables", ex);
                return null;
            }
        }


        public void EliminarCuentasBancarias(Int32 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DACuentas.EliminarCuentasBancarias(pId, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasBancariasBusiness", "EliminarCuentasBancarias", ex);                
            }
        }


        public List<CuentasBancarias> ListarCuentasBancarias(string filtro, Usuario pUsuario)
        {
            try
            {
                return DACuentas.ListarCuentasBancarias(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasBancariasBusiness", "ListarCuentasBancarias", ex);
                return null;
            }
        }


        public List<CuentasBancarias> ListarBancos(Usuario pUsuario)
        {
            try
            {
                return DACuentas.ListarBancos( pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasBancariasBusiness", "ListarBancos", ex);
                return null;
            }
        }

        public List<CuentasBancarias> ListarALLBancos(Usuario pUsuario)
        {
            try
            {
                return DACuentas.ListarALLBancos(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasBancariasBusiness", "ListarALLBancos", ex);
                return null;
            }
        }



        public List<CuentasBancarias> ListarNumeroCuentas(Usuario pUsuario)
        {
            try
            {
                return DACuentas.ListarNumeroCuentas(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasBancariasBusiness", "ListarNumeroCuentas", ex);
                return null;
            }
        }



        public CuentasBancarias ConsultarCuentasBancarias(Int32 pId, Usuario vUsuario)
        {
            try
            {
                return DACuentas.ConsultarCuentasBancarias(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasBancariasBusiness", "ConsultarCuentasBancarias", ex);
                return null;
            }
        }

        public List<CuentasBancarias> ListarCuentasBancarias1(Usuario pUsuario)
        {
            try
            {
                return DACuentas.ListarCuentasBancarias1(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasBancariasBusiness", "ListarCuentasBancarias1", ex);
                return null;
            }
        }


        public CuentasBancarias ConsultarCuentasBancariasPorBanco(Int32 pId, string num_cuenta, Usuario vUsuario)
        {
            try
            {
                return DACuentas.ConsultarCuentasBancariasPorBanco(pId,num_cuenta, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasBancariasBusiness", "ConsultarCuentasBancariasPorBanco", ex);
                return null;
            }
        }


    }
}


