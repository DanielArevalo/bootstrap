using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Cartera.Entities;
using Xpinn.Cartera.Data;

namespace Xpinn.Cartera.Business
{
    /// <summary>
    /// Objeto de negocio para Credito
    /// </summary>
    public class CuadreCarteraBusiness : GlobalData
    {
        private CuadreCarteraData DACuadreCartera;

        /// <summary>
        /// Constructor del objeto de negocio para Credito
        /// </summary>
        public CuadreCarteraBusiness()
        {
            DACuadreCartera = new CuadreCarteraData();
        }
        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<CuadreCartera> ConsultaCuadre(CuadreCartera pcuadre, int tipo_deudor, Usuario pUsuario)
        {
            try
            {
                return DACuadreCartera.ConsultaCuadre(pcuadre, tipo_deudor, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuadreCarteraBusiness", "ConsultaCuadre", ex);
                return null;
            }
        }



        public List<CuadreHistorico> ConsultaCuadreHistorico(CuadreCartera pcuadre, Usuario pUsuario)
        {
            try
            {
                return DACuadreCartera.ConsultaCuadreHistorico(pcuadre, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuadreCarteraBusiness", "ConsultaCuadreHistorico", ex);
                return null;
            }
        }

        public CuadreHistorico ConsultarSaldosYDiferenciaCreditos(string fechaFinal, int tipo_deudor, Usuario pUsuario)
        {
            try
            {   
                CuadreHistorico entidad = DACuadreCartera.ConsultarSaldosYDiferenciaCreditos(fechaFinal, tipo_deudor, pUsuario);

                if (entidad.saldo_contable != null && entidad.saldo_operativo != null)
                {
                    entidad.diferencia = entidad.saldo_contable - entidad.saldo_operativo;
                }

                return entidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuadreCarteraBusiness", "ConsultarSaldosYDiferenciaCreditos", ex);
                return null;
            }
        }


        public CuadreHistorico ConsultarSaldosYDiferenciaServicios(string fechaFinal,  Usuario pUsuario)
        {
            try
            {
                CuadreHistorico entidad = DACuadreCartera.ConsultarSaldosYDiferenciaServicios(fechaFinal,  pUsuario);

                if (entidad.saldo_contable != null && entidad.saldo_operativo != null)
                {
                    entidad.diferencia = entidad.saldo_contable - entidad.saldo_operativo;
                }

                return entidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuadreCarteraBusiness", "ConsultarSaldosYDiferenciaServicios", ex);
                return null;
            }
        }


        public CuadreHistorico ConsultarSaldosYDiferenciaAhorroVista(string fechaFinal, Usuario pUsuario)
        {
            try
            {
                CuadreHistorico entidad = DACuadreCartera.ConsultarSaldosYDiferenciaAhorroVista(fechaFinal, pUsuario);

                if (entidad.saldo_contable != null && entidad.saldo_operativo != null)
                {
                    entidad.diferencia = entidad.saldo_contable - entidad.saldo_operativo;
                }

                return entidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuadreCarteraBusiness", "ConsultarSaldosYDiferenciaAhorroVista", ex);
                return null;
            }
        }


        public CuadreHistorico ConsultarSaldosYDiferenciaCDATS(string fechaFinal, Usuario pUsuario)
        {
            try
            {
                CuadreHistorico entidad = DACuadreCartera.ConsultarSaldosYDiferenciaCDATS(fechaFinal, pUsuario);

                if (entidad.saldo_contable != null && entidad.saldo_operativo != null)
                {
                    entidad.diferencia = entidad.saldo_contable - entidad.saldo_operativo;
                }

                return entidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuadreCarteraBusiness", "ConsultarSaldosYDiferenciaCDATS", ex);
                return null;
            }
        }

        public List<CuadreCartera> ConsultarCuadreContablePorComprobante(CuadreCartera cuadre, Usuario usuario)
        {
            try
            {
                return DACuadreCartera.ConsultarCuadreContablePorComprobante(cuadre, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuadreCarteraBusiness", "ConsultarCuadreContablePorComprobante", ex);
                return null;
            }
        }

        public List<CuadreCartera> ConsultarCuadreOperativoPorComprobante(CuadreCartera cuadre, Usuario usuario)
        {
            try
            {
                return DACuadreCartera.ConsultarCuadreOperativoPorComprobante(cuadre, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuadreCarteraBusiness", "ConsultarCuadreOperativoPorComprobante", ex);
                return null;
            }
        }

        public CuadreHistorico ConsultarSaldosYDiferenciaAhorroProgramado(string fechaFinal, Usuario pUsuario)
        {
            try
            {
                CuadreHistorico entidad = DACuadreCartera.ConsultarSaldosYDiferenciaAhorroProgramado(fechaFinal, pUsuario);

                if (entidad.saldo_contable != null && entidad.saldo_operativo != null)
                {
                    entidad.diferencia = entidad.saldo_contable - entidad.saldo_operativo;
                }

                return entidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuadreCarteraBusiness", "ConsultarSaldosYDiferenciaAhorroProgramado", ex);
                return null;
            }
        }


        public CuadreHistorico ConsultarSaldosYDiferenciaAportes(string fechaFinal, Usuario pUsuario)
        {
            try
            {
                CuadreHistorico entidad = DACuadreCartera.ConsultarSaldosYDiferenciaAportes(fechaFinal, pUsuario);

                if (entidad.saldo_contable != null && entidad.saldo_operativo != null)
                {
                    entidad.diferencia = entidad.saldo_contable - entidad.saldo_operativo;
                }

                return entidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuadreCarteraBusiness", "ConsultarSaldosYDiferenciaAportes", ex);
                return null;
            }
        }

        public void ActualizarSaldoCierre(Int64 pTipoProducto, List<CuadreHistorico> lstSaldos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach(CuadreHistorico pCuadre in lstSaldos)
                    {
                        DACuadreCartera.ActualizarSaldoCierre(pTipoProducto, Convert.ToInt64(pCuadre.numero_radicacion), Convert.ToInt64(pCuadre.saldo_operativo), pCuadre.fecha, pUsuario);
                    }
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CierreHistoricoBusiness", "ActualizarSaldoCierre", ex);
            }
        }


        public CuadreHistorico ConsultarSaldosYDiferenciaActivosFijos(string fechaFinal, Usuario pUsuario)
        {
            try
            {
                CuadreHistorico entidad = DACuadreCartera.ConsultarSaldosYDiferenciaActivosFijos(fechaFinal, pUsuario);

                if (entidad.saldo_contable != null && entidad.saldo_operativo != null)
                {
                    entidad.diferencia = entidad.saldo_contable - entidad.saldo_operativo;
                }

                return entidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuadreCarteraBusiness", "ConsultarSaldosYDiferenciaActivosFijos", ex);
                return null;
            }
        }

    }
}