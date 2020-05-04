using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Tesoreria.Business;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Services
{
    
    public class CuentasPorPagarService
    {
        private CuentasPorPagarBusiness BOCuentas;
        private ExcepcionBusiness BOExcepcion;


        public CuentasPorPagarService()
        {
            BOCuentas = new CuentasPorPagarBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "40702"; } }
        public string CodigoProgramaAnticipo { get { return "40704"; } }
        public string CodigoProgramaReporte { get { return "40705"; } }

        public CUENTAXPAGAR_ANTICIPO CrearCUENTAXPAGAR_ANTICIPO(Xpinn.Tesoreria.Entities.Giro pGiro, Xpinn.Tesoreria.Entities.Operacion pOperacion, CUENTAXPAGAR_ANTICIPO pCUENTAXPAGAR_ANTICIPO, Usuario pusuario)
        {
            try
            {
                pCUENTAXPAGAR_ANTICIPO = BOCuentas.CrearCUENTAXPAGAR_ANTICIPO(pGiro,pOperacion,pCUENTAXPAGAR_ANTICIPO, pusuario);
                return pCUENTAXPAGAR_ANTICIPO;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CUENTAXPAGAR_ANTICIPOService", "CrearCUENTAXPAGAR_ANTICIPO", ex);
                return null;
            }
        }


        public CUENTAXPAGAR_ANTICIPO ModificarCUENTAXPAGAR_ANTICIPO(Xpinn.Tesoreria.Entities.Giro pGiro, Xpinn.Tesoreria.Entities.Operacion pOperacion, CUENTAXPAGAR_ANTICIPO pCUENTAXPAGAR_ANTICIPO, Usuario pusuario)
        {
            try
            {
                pCUENTAXPAGAR_ANTICIPO = BOCuentas.ModificarCUENTAXPAGAR_ANTICIPO(pGiro, pOperacion,   pCUENTAXPAGAR_ANTICIPO, pusuario);
                return pCUENTAXPAGAR_ANTICIPO;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CUENTAXPAGAR_ANTICIPOService", "ModificarCUENTAXPAGAR_ANTICIPO", ex);
                return null;
            }
        }


        public void EliminarCUENTAXPAGAR_ANTICIPO(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOCuentas.EliminarCUENTAXPAGAR_ANTICIPO(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CUENTAXPAGAR_ANTICIPOService", "EliminarCUENTAXPAGAR_ANTICIPO", ex);
            }
        }


        public CUENTAXPAGAR_ANTICIPO ConsultarCUENTAXPAGAR_ANTICIPO(Int64 pId, Usuario pusuario)
        {
            try
            {
                CUENTAXPAGAR_ANTICIPO CUENTAXPAGAR_ANTICIPO = new CUENTAXPAGAR_ANTICIPO();
                CUENTAXPAGAR_ANTICIPO = BOCuentas.ConsultarCUENTAXPAGAR_ANTICIPO(pId, pusuario);
                return CUENTAXPAGAR_ANTICIPO;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CUENTAXPAGAR_ANTICIPOService", "ConsultarCUENTAXPAGAR_ANTICIPO", ex);
                return null;
            }
        }


        public List<CUENTAXPAGAR_ANTICIPO> ListarCUENTAXPAGAR_ANTICIPO(CUENTAXPAGAR_ANTICIPO pCUENTAXPAGAR_ANTICIPO, Usuario pusuario)
        {
            try
            {
                return BOCuentas.ListarCUENTAXPAGAR_ANTICIPO(pCUENTAXPAGAR_ANTICIPO, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CUENTAXPAGAR_ANTICIPOService", "ListarCUENTAXPAGAR_ANTICIPO", ex);
                return null;
            }
        }


        public CuentasPorPagar CrearCuentasXpagar(CuentasPorPagar pCuentas, Operacion pOperacion, bool opcion, long formadesembolso, Giro pGiro, Usuario vUsuario)
        {
            try
            {
                return BOCuentas.CrearCuentasXpagar(pCuentas, pOperacion, opcion, formadesembolso, pGiro,vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPorPagarService", "CrearCuentasXpagar", ex);
                return null;
            }
        }


        public CuentasPorPagar ModificarCuentasXpagar(CuentasPorPagar pCuentas, Operacion pOperacion, Usuario vUsuario)
        {
            try
            {
                return BOCuentas.ModificarCuentasXpagar(pCuentas, pOperacion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPorPagarService", "ModificarCuentasXpagar", ex);
                return null;
            }
        }


        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return BOCuentas.ObtenerSiguienteCodigo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPorPagarService", "ObtenerSiguienteCodigo", ex);
                return 0;
            }
        }

        public Int64 ObtenerSiguienteEquivalente(Usuario pUsuario)
        {
            try
            {
                return BOCuentas.ObtenerSiguienteEquivalente(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPorPagarService", "ObtenerSiguienteEquivalente", ex);
                return 0;
            }
        }

        public void EliminarCuentasXpagar(Int32 pId, Usuario vUsuario)
        {
            try
            {
                BOCuentas.EliminarCuentasXpagar(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPorPagarService", "EliminarCuentasXpagar", ex);
            }
        }

        public List<CuentasPorPagar> ListarAnticipos(CuentasPorPagar pCuentas, DateTime pFechaIni, DateTime pFechaFin, Usuario vUsuario, String filtro)
        {
            try
            {
                return BOCuentas.ListarAnticipos(pCuentas, pFechaIni, pFechaFin, vUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPorPagarService", "ListarCuentasXpagar", ex);
                return null;
            }
        }

        public List<CuentasPorPagar> ListarCuentasXpagar(CuentasPorPagar pCuentas, DateTime pFechaIni, DateTime pFechaFin, DateTime pVencIni, DateTime pVencFin, Usuario vUsuario, String filtro)
        {
            try
            {
                return BOCuentas.ListarCuentasXpagar(pCuentas,pFechaIni,pFechaFin,pVencIni,pVencFin, vUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPorPagarService", "ListarCuentasXpagar", ex);
                return null;
            }
        }


        public List<CuentaXpagar_Detalle> ConsultarDetalleCuentasXpagar(CuentaXpagar_Detalle pCuentas, Usuario vUsuario)
        {
            try
            {
                return BOCuentas.ConsultarDetalleCuentasXpagar(pCuentas, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPorPagarService", "ConsultarDetalleCuentasXpagar", ex);
                return null;
            }
        }


        public List<CuentaXpagar_Pago> ConsultarDetalleFormaPago(CuentaXpagar_Pago pCuentas, Usuario vUsuario)
        {
            try
            {
                return BOCuentas.ConsultarDetalleFormaPago(pCuentas, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPorPagarService", "ConsultarDetalleFormaPago", ex);
                return null;
            }
        }

        public CuentasPorPagar CONSULTARANTICIPOS(CuentasPorPagar pCuentas, Usuario vUsuario)
        {
            try
            {
               
                    return BOCuentas.CONSULTARANTICIPOS(pCuentas, vUsuario);

                 
                

             
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPorPagarBusiness", "ConsultarCuentasXpagar", ex);
                return null;
            }
        }

        public CuentasPorPagar ConsultarCuentasXpagar(CuentasPorPagar pCuentas, Usuario vUsuario)
        {
            try
            {
                return BOCuentas.ConsultarCuentasXpagar(pCuentas, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPorPagarService", "ConsultarCuentasXpagar", ex);
                return null;
            }
        }


        public CuentasPorPagar ConsultarDatosReporte(CuentasPorPagar pCuentas, Usuario vUsuario)
        {
            try
            {
                return BOCuentas.ConsultarDatosReporte(pCuentas, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPorPagarService", "ConsultarDatosReporte", ex);
                return null;
            }
        }


        public void EliminarCuentasXpagarDetalles(Int32 pId, Usuario vUsuario, int opcion)
        {
            try
            {
                BOCuentas.EliminarCuentasXpagarDetalles(pId, vUsuario,opcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPorPagarService", "EliminarCuentasXpagarDetalles", ex);
            }
        }


        public List<CuentasXpagarImpuesto> ConsultarDetImpuestosXConcepto(CuentasXpagarImpuesto pImpuesto, Usuario vUsuario)
        {
            try
            {
                return BOCuentas.ConsultarDetImpuestosXConcepto(pImpuesto, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPorPagarService", "ConsultarDetImpuestosXConcepto", ex);
                return null;
            }
        }



        public CuentasPorPagar ConsultarGiro(CuentasPorPagar pOperacion, Usuario vUsuario)
        {
            try
            {
                return BOCuentas.ConsultarGiro(pOperacion,vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPorPagarService", "ConsultarGiro", ex);
                return null;
            }
        }

        public CuentasPorPagar ConsultarGiroXfactura(CuentasPorPagar pOperacion, Usuario vUsuario)
        {
            try
            {
                return BOCuentas.ConsultarGiroXfactura(pOperacion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPorPagarService", "ConsultarGiroXfactura", ex);
                return null;
            }
        }



    }
}