using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Reporteador.Data;
using Xpinn.Reporteador.Entities;

namespace Xpinn.Reporteador.Business
{
    
    public class UIAFBusiness : GlobalBusiness
    {
        private UIAFData DAReporte;


        public UIAFBusiness()
        {
            DAReporte = new UIAFData();
        }

        public UIAF CrearUiafREporte(UIAF pUIAF, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pUIAF = DAReporte.CrearUiafREporte(pUIAF, vUsuario);
                    int cod = pUIAF.idreporte;

                    if (pUIAF.lstProductos != null && pUIAF.lstProductos.Count > 0)
                    {
                        int num = 0;
                        foreach (UIAFDetalle eUiaf in pUIAF.lstProductos)
                        {
                            UIAFDetalle nReporte = new UIAFDetalle();
                            eUiaf.idreporte = cod;
                            eUiaf.tipo_producto = eUiaf.tipo_producto_vista.ToString();
                            eUiaf.departamento = eUiaf.departamento.ToString();
                            eUiaf.tipo_identificacion1 = eUiaf.tipo_identificacion1_vista.ToString();
                            eUiaf.tipo_identificacion2 = eUiaf.tipo_identificacion2_vista.ToString();
                            nReporte = DAReporte.Crear_Mod_UIAFProductos(eUiaf, vUsuario, 1);//CREAR
                            num += 1;
                        }
                    }                                        

                    ts.Complete();
                }
                return pUIAF;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAFBusiness", "CrearUiafREporte", ex);
                return null;
            }
        }


        public UIAF ModificarUiafREporte(UIAF pUIAF, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pUIAF = DAReporte.ModificarUiafREporte(pUIAF, vUsuario);
                    int cod = pUIAF.idreporte;
                    if (pUIAF.lstProductos != null && pUIAF.lstProductos.Count > 0)
                    {
                        foreach (UIAFDetalle eUiaf in pUIAF.lstProductos)
                        {
                            UIAFDetalle nReporte = new UIAFDetalle();
                            eUiaf.idreporte = cod;
                            if(eUiaf.idreporteproductos <= 0 && eUiaf.idreporteproductos == null)
                                nReporte = DAReporte.Crear_Mod_UIAFProductos(eUiaf, vUsuario, 1);
                            else
                                nReporte = DAReporte.Crear_Mod_UIAFProductos(eUiaf, vUsuario, 2);
                        }
                    }

                    ts.Complete();
                }
                return pUIAF;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAFBusiness", "ModificarUiafREporte", ex);
                return null;
            }
        }


        public void EliminarReporteUIAF(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAReporte.EliminarReporteUIAF(pId, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAFBusiness", "EliminarReporteUIAF", ex);
            }
        }



        public void EliminarUIAFProducto(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAReporte.EliminarUIAFProducto(pId, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAFBusiness", "EliminarUIAFProducto", ex);
            }
        }



        public List<UIAF> ListarReporteUIAF(String filtro, DateTime pFechaIni, DateTime pFechaFin, Usuario vUsuario)
        {
            try
            {
                return DAReporte.ListarReporteUIAF(filtro,pFechaIni,pFechaFin, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAFBusiness", "ListarReporteUIAF", ex);
                return null;
            }
        }


        public List<UIAFDetalle> ListarVistaProductos(String filtro, DateTime pFechaIni, DateTime pFechaFin, Usuario vUsuario)
        {
            try
            {
                return DAReporte.ListarVistaProductos(filtro, pFechaIni, pFechaFin, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAFBusiness", "ListarVistaProductos", ex);
                return null;
            }
        }


        public List<UIAFDetalle> ListarVistaProductosAll(String filtro, Usuario vUsuario)
        {
            try
            {
                return DAReporte.ListarVistaProductosAll(filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAFBusiness", "ListarVistaProductosAll", ex);
                return null;
            }
        }

        public List<UIAFDetalle> ListarUIAFProductos(String filtro, Usuario vUsuario)// DateTime pFechaIni, DateTime pFechaFin,
        {
            try
            {
                return DAReporte.ListarUIAFProductos(filtro,  vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAFBusiness", "ListarUIAFProductos", ex);
                return null;
            }
        }


        public UIAF ConsultarReporteUIAF(Int32 pId, Usuario pUsuario)
        {
            try
            {
                return DAReporte.ConsultarReporteUIAF(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAFBusiness", "ConsultarReporteUIAF", ex);
                return null;
            }
        }
        

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return DAReporte.ObtenerSiguienteCodigo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAFBusiness", "ObtenerSiguienteCodigo", ex);
                return 0;
            }
        }


        public List<UIAFTarjetas> ListarTransaccionesTarjeta(String filtro, DateTime pFechaIni, DateTime pFechaFin, Usuario vUsuario)
        {
            try
            {
                return DAReporte.ListarTransaccionesTarjeta(filtro, pFechaIni, pFechaFin, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAFBusiness", "ListarTransaccionesTarjeta", ex);
                return null;
            }
        }

    }
}