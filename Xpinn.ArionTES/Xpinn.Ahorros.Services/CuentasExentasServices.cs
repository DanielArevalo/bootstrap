using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Ahorros.Business;
using Xpinn.Ahorros.Entities;

namespace Xpinn.Ahorros.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CuentasExentasServices
    {
        private CuentasExentasBusiness BOCuentas;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Destinacion
        /// </summary>
        public CuentasExentasServices()
        {
            BOCuentas = new CuentasExentasBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "220109"; } }

        /// <summary>
        /// Servicio para crear Destinacion
        /// </summary>
        /// <param name="pEntity">Entidad Destinacion</param>
        /// <returns>Entidad Destinacion creada</returns>
        public CuentasExenta CrearCuentaExenta(CuentasExenta pExenta, Usuario vUsuario)
        {
            try
            {
                return BOCuentas.CrearCuentaExenta(pExenta, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasExentasServices", "CrearCuentaExenta", ex);
                return null;
            }
        }

        public List<CuentasExenta> ListarProductosControl(int pTipo, string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOCuentas.ListarProductosControl(pTipo, pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasExentasServices", "ListarDropDownNumeroCuenta", ex);
                return null;
            }
        }


        //public CuentasExenta ConsultarCuentaExentaXNumeroCuenta(CuentasExenta pExenta, Usuario vUsuario)
        //{
        //    try
        //    {
        //        return BOCuentas.ConsultarCuentaExentaXNumeroCuenta(pExenta, vUsuario);
        //    }
        //    catch (Exception ex)
        //    {
        //        BOExcepcion.Throw("CuentasExentasServices", "ConsultarCuentaExentaXNumeroCuenta", ex);
        //        return null;
        //    }
        //}

        public void EliminarCuentasExentas(Int32 pId, Usuario vUsuario)
        {
            try
            {
                BOCuentas.EliminarCuentasExentas(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasExentasServices", "EliminarCuentasExentas", ex);
            }
        }

        public void EliminarCuentasExentasNumeroCuenta(Int64 pNumeroCuenta, Usuario vUsuario)
        {
            try
            {
                BOCuentas.EliminarCuentasExentasNumeroCuenta(pNumeroCuenta, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasExentasServices", "EliminarCuentasExentasNumeroCuenta", ex);
            }
        }

        [OperationBehavior(TransactionAutoComplete = true, TransactionScopeRequired = true)]
        [TransactionFlow(TransactionFlowOption.Mandatory)]
        public List<CuentasExenta> ListarCuentaExenta(CuentasExenta pCuenta, Usuario vUsuario)
        {
            try
            {
                return BOCuentas.ListarCuentaExenta(pCuenta, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasExentasServices", "ListarCuentaExenta", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para crear Destinacion
        /// </summary>
        /// <param name="pEntity">Entidad Destinacion</param>
        /// <returns>Entidad Destinacion creada</returns>
        public CuentasExenta CrearCuentaExentApertura(CuentasExenta pExenta, Usuario vUsuario,int opcion)
        {
            try
            {
                return BOCuentas.CrearCuentaExentApertura(pExenta, vUsuario,opcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasExentasServices", "CrearCuentaExentApertura", ex);
                return null;
            }
        }
    }
}