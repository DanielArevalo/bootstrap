using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Business
{
    /// <summary>
    /// Objeto de negocio para AreasCaj
    /// </summary>
    public class AprobacionCtasPorPagarBusiness : GlobalBusiness
    {
        private AprobacionCtasPorPagarData DAAprobacion;

        /// <summary>
        /// Constructor del objeto de negocio para AreasCaj
        /// </summary>
        public AprobacionCtasPorPagarBusiness()
        {
            DAAprobacion = new AprobacionCtasPorPagarData();
        }


        public List<AprobacionCtasPorPagar> ListarCuentasXpagar(String filtro, String Orden, DateTime pFechaIng, DateTime pFechaVencIni, DateTime pFechaVencFin, Usuario vUsuario)
        {
            try
            {
                return DAAprobacion.ListarCuentasXpagar(filtro, Orden, pFechaIng, pFechaVencIni, pFechaVencFin, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionGirosBusiness", "ListarCuentasXpagar", ex);
                return null;
            }
        }

        public void AprobarCuentaXpagar(List<Xpinn.Tesoreria.Entities.Giro> lstGiro, Usuario vUsuario)
        {
            int contador = 0;
            int idcuentabancaria = 0;
            int cod_banco = 0;
            String num_cuenta = "";
            int tipo_cuenta = 0;
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (lstGiro != null && lstGiro.Count > 0)
                    {
                        foreach (Xpinn.Tesoreria.Entities.Giro nGiro in lstGiro)
                        {
                            Xpinn.Tesoreria.Data.AprobacionCtasPorPagarData GiroData = new Xpinn.Tesoreria.Data.AprobacionCtasPorPagarData();
                            Xpinn.Tesoreria.Entities.Giro vGiroEntidad = new Xpinn.Tesoreria.Entities.Giro();

                            Xpinn.Tesoreria.Data.GiroData GiroData1 = new Xpinn.Tesoreria.Data.GiroData();
                            Xpinn.Tesoreria.Entities.Giro vGiroEntidad1 = new Xpinn.Tesoreria.Entities.Giro();

                            if (contador == 0)
                            {
                                if (nGiro.cod_ope > 0)
                                {
                                    vGiroEntidad = GiroData.ModificarGiroXCod_ope(nGiro, vUsuario);

                                }
                            }
                            else
                            {
                                if (nGiro.cod_ope > 0)
                                {
                                       nGiro.tipo_acto = 10;
                                       vGiroEntidad = GiroData1.CrearGiro(nGiro, vUsuario,1);
                                }
                            }

                            DAAprobacion.ActualizarEstadoCuentaXpagar(nGiro.codpagofac, vUsuario);
                            idcuentabancaria = Convert.ToInt32(nGiro.idctabancaria);
                            cod_banco = Convert.ToInt32(nGiro.cod_banco);
                            num_cuenta = Convert.ToString(nGiro.num_cuenta) == null ? "0" : Convert.ToString(nGiro.num_cuenta);
                            tipo_cuenta = Convert.ToInt32(nGiro.tipo_cuenta);
                            DAAprobacion.ActualizarGiroAprobCuentaXpagar(Convert.ToInt32(nGiro.cod_ope), idcuentabancaria, cod_banco, num_cuenta, tipo_cuenta, vUsuario);
                            contador++;
                        }
                    }
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionGirosBusiness", "AprobarCuentaXpagar", ex);
            }
        }



    }
}