using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Cartera.Entities;
using Xpinn.Cartera.Business;
using System.Web;
using Xpinn.Util;

namespace Xpinn.Cartera.Services
{
    /// <summary>
    /// Servicio para CuadreCartera
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CuadreCarteraService
    {
        private CuadreCarteraBusiness BOCuadreCartera;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para CierreHistorio
        /// </summary>
        public CuadreCarteraService()
        {
            BOCuadreCartera = new CuadreCarteraBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "60207"; } }
        public string CodigoProgramaHistorico { get { return "60208"; } }

        public List<CuadreCartera> ConsultaCuadre(CuadreCartera pcuadre, int tipo_deudor, Usuario pUsuario)
        {
            return BOCuadreCartera.ConsultaCuadre(pcuadre, tipo_deudor, pUsuario);
        }

        public List<CuadreHistorico> ConsultaCuadreHistorico(CuadreCartera pcuadre, Usuario pUsuario)
        {
            try
            {
                return BOCuadreCartera.ConsultaCuadreHistorico(pcuadre, pUsuario);
            }
            catch (Exception ex)
            {                
                return null;
            }
        }

        public CuadreHistorico ConsultarSaldosYDiferenciaCreditos(string fechaFinal, int tipo_deudor, Usuario pUsuario)
        {
            try
            {
                return BOCuadreCartera.ConsultarSaldosYDiferenciaCreditos(fechaFinal, tipo_deudor, pUsuario);
            }
            catch (Exception)
            {
                return null;
            }
        }



        public CuadreHistorico ConsultarSaldosYDiferenciaServicios(string fechaFinal,  Usuario pUsuario)
        {
            try
            {
                return BOCuadreCartera.ConsultarSaldosYDiferenciaServicios(fechaFinal,  pUsuario);
            }
            catch (Exception)
            {
                return null;
            }
        }


        public CuadreHistorico ConsultarSaldosYDiferenciaAhorroVista(string fechaFinal, Usuario pUsuario)
        {
            try
            {
                return BOCuadreCartera.ConsultarSaldosYDiferenciaAhorroVista(fechaFinal, pUsuario);
            }
            catch (Exception)
            {
                return null;
            }
        }


        public CuadreHistorico ConsultarSaldosYDiferenciaCDATS(string fechaFinal, Usuario pUsuario)
        {
            try
            {
                return BOCuadreCartera.ConsultarSaldosYDiferenciaCDATS(fechaFinal, pUsuario);
            }
            catch (Exception)
            {
                return null;
            }
        }


        public CuadreHistorico ConsultarSaldosYDiferenciaAhorroProgramado(string fechaFinal, Usuario pUsuario)
        {
            try
            {
                return BOCuadreCartera.ConsultarSaldosYDiferenciaAhorroProgramado(fechaFinal, pUsuario);
            }
            catch (Exception)
            {
                return null;
            }
        }


        public CuadreHistorico ConsultarSaldosYDiferenciaAportes(string fechaFinal, Usuario pUsuario)
        {
            try
            {
                return BOCuadreCartera.ConsultarSaldosYDiferenciaAportes(fechaFinal, pUsuario);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<CuadreCartera> ConsultarCuadreContablePorComprobante(CuadreCartera cuadre, Usuario usuario)
        {
            try
            {
                return BOCuadreCartera.ConsultarCuadreContablePorComprobante(cuadre, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuadreCarteraService", "ConsultarCuadreContablePorComprobante", ex);
                return null;
            }
        }

        public List<CuadreCartera> ConsultarCuadreOperativoPorComprobante(CuadreCartera cuadre, Usuario usuario)
        {
            try
            {
                return BOCuadreCartera.ConsultarCuadreOperativoPorComprobante(cuadre, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuadreCarteraService", "ConsultarCuadreOperativoPorComprobante", ex);
                return null;
            }
        }




        public void ActualizarSaldoCierre(Int64 pTipoProducto, List<CuadreHistorico> lstSaldos, Usuario pUsuario)
        {
            try
            {
                BOCuadreCartera.ActualizarSaldoCierre(pTipoProducto, lstSaldos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuadreCarteraService", "ActualizarSaldoCierre", ex);
            }
        }



        public CuadreHistorico ConsultarSaldosYDiferenciaActivosFijos(string fechaFinal, Usuario pUsuario)
        {
            try
            {
                return BOCuadreCartera.ConsultarSaldosYDiferenciaActivosFijos(fechaFinal, pUsuario);
            }
            catch (Exception)
            {
                return null;
            }
        }


    }
}

