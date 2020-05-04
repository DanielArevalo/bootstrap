using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Ahorros.Data;
using Xpinn.Ahorros.Entities;

namespace Xpinn.Ahorros.Business
{
    /// <summary>
    /// Objeto de negocio para Destinacion
    /// </summary>
    public class CuentasExentasBusiness : GlobalBusiness
    {
        private CuentasExentasData DACuentas;

        /// <summary>
        /// Constructor del objeto de negocio para Destinacion
        /// </summary>
        public CuentasExentasBusiness()
        {
            DACuentas = new CuentasExentasData();
        }


        public CuentasExenta CrearCuentaExenta(CuentasExenta pExenta, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
                {
                    if (pExenta.lstCuentas != null && pExenta.lstCuentas.Count > 0)
                    {
                        foreach (CuentasExenta nCuent in pExenta.lstCuentas)
                        {
                            CuentasExenta xCtaExenta = new CuentasExenta();
                            if (nCuent.idexenta > 0) //MODIFICAR
                                xCtaExenta = DACuentas.CrearCuentaExenta(nCuent, vUsuario, 2);
                            else  //CREAR
                            {
                                nCuent.idexenta = Convert.ToInt32(DACuentas.ObtenerSiguienteCodigo(vUsuario));
                                xCtaExenta = DACuentas.CrearCuentaExenta(nCuent, vUsuario, 1);
                            }
                        }
                    }                  
                    ts.Complete();
                }

                return pExenta;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasExentasBusiness", "CrearDestinacion", ex);
                return null;
            }
        }

        public List<CuentasExenta> ListarProductosControl(int pTipo, string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DACuentas.ListarProductosControl(pTipo, pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasExentasBusiness", "ListarDropDownNumeroCuenta", ex);
                return null;
            }
        }

        public CuentasExenta ConsultarCuentaExentaXNumeroCuenta(CuentasExenta pExenta, Usuario vUsuario)
        {
            try
            {
                return DACuentas.ConsultarCuentaExentaXNumeroCuenta(pExenta, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasExentasBusiness", "ConsultarCuentaExentaXNumeroCuenta", ex);
                return null;
            }
        }


        public void EliminarCuentasExentas(Int32 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DACuentas.EliminarCuentasExentas(pId, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasExentasBusiness", "EliminarCuentasExentas", ex);
            }
        }

        public void EliminarCuentasExentasNumeroCuenta(Int64 pNumeroCuenta, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DACuentas.EliminarCuentasExentasNumeroCuenta(pNumeroCuenta, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasExentasBusiness", "EliminarCuentasExentasNumeroCuenta", ex);
            }
        }

        public List<CuentasExenta> ListarCuentaExenta(CuentasExenta pCuenta, Usuario vUsuario)
        {
            try
            {
                return DACuentas.ListarCuentaExenta(pCuenta, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasExentasBusiness", "ListarCuentaExenta", ex);
                return null;
            }
        }

        public CuentasExenta CrearCuentaExentApertura(CuentasExenta pExenta, Usuario vUsuario,int opcion)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    if (opcion==1)
                    {
                        pExenta.idexenta = Convert.ToInt32(DACuentas.ObtenerSiguienteCodigo(vUsuario));
                    }
                  
                    pExenta = DACuentas.CrearCuentaExenta(pExenta, vUsuario, opcion);
                            
                        
                    
                    ts.Complete();
                }

                return pExenta;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasExentasBusiness", "CrearCuentaExentApertura", ex);
                return null;
            }
        }

                
    }
}