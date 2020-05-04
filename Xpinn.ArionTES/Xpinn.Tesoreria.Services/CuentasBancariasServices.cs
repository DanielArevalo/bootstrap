using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Tesoreria.Business;
using Xpinn.Tesoreria.Entities;
using System.Web.UI.WebControls;

namespace Xpinn.Tesoreria.Services
{
    public class CuentasBancariasServices
    {
        private CuentasBancariasBusiness BOCuentas;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Acceso
        /// </summary>
        public CuentasBancariasServices()
        {
            BOCuentas = new CuentasBancariasBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "40202"; } } // Falta adicionar la opcion


        public CuentasBancarias CrearCuentasContables(CuentasBancarias pCuent, Usuario vUsuario,int opcion)
        {
            try
            {
                return BOCuentas.CrearCuentasContables(pCuent, vUsuario, opcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasBancariasServices", "CrearCuentasContables", ex);
                return null;
            }
        }


        public void EliminarCuentasBancarias(Int32 pId, Usuario vUsuario)
        {
            try
            {
                BOCuentas.EliminarCuentasBancarias(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasBancariasServices", "CrearCuentasContables", ex);                
            }
        }


        public List<CuentasBancarias> ListarCuentasBancarias(string filtro, Usuario pUsuario)
        {
            try
            {
                return BOCuentas.ListarCuentasBancarias(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasBancariasServices", "ListarCuentasBancarias", ex);
                return null;
            }
        }


        public List<CuentasBancarias> ListarBancos(Usuario pUsuario)
        {
            try
            {
                return BOCuentas.ListarBancos(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasBancariasServices", "ListarBancos", ex);
                return null;
            }
        }

        public List<CuentasBancarias> ListarALLBancos(Usuario pUsuario)
        {
            try
            {
                return BOCuentas.ListarALLBancos(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasBancariasServices", "ListarALLBancos", ex);
                return null;
            }
        }


        public List<CuentasBancarias> ListarNumeroCuentas(Usuario pUsuario)
        {
            try
            {
                return BOCuentas.ListarNumeroCuentas(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasBancariasServices", "ListarNumeroCuentas", ex);
                return null;
            }
        }


        public CuentasBancarias ConsultarCuentasBancarias(Int32 pId, Usuario vUsuario)
        {
            try
            {
                return BOCuentas.ConsultarCuentasBancarias(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasBancariasServices", "ConsultarCuentasBancarias", ex);
                return null;
            }
        }


        public List<CuentasBancarias> ListarCuentasBancarias1(Usuario pUsuario)
        {
            try
            {
                return BOCuentas.ListarCuentasBancarias1(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasBancariasServices", "ListarCuentasBancarias1", ex);
                return null;
            }
        }

        public CuentasBancarias ConsultarCuentasBancariasPorBanco(Int32 pId, string num_cuenta, Usuario vUsuario)
        {
            try
            {
                return BOCuentas.ConsultarCuentasBancariasPorBanco(pId,num_cuenta, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasBancariasServices", "ConsultarCuentasBancariasPorBanco", ex);
                return null;
            }
        }

    }
}
