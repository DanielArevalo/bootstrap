using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Cartera.Data;
using Xpinn.Cartera.Entities;
using System.Web;

namespace Xpinn.Cartera.Business
{
    public class ReliquidacionBusiness : GlobalData
    {

        private ReliquidacionData DAReliquidacion;

        /// <summary>
        /// Constructor del objeto de negocio para cierre histórico
        /// </summary>
        public ReliquidacionBusiness()
        {
            DAReliquidacion = new ReliquidacionData();
        }

        public List<Xpinn.FabricaCreditos.Entities.Credito> ListarCredito(Usuario pUsuario, String filtro)
        {
            try
            {
                return DAReliquidacion.ListarCredito(pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReliquidacionBusiness", "ListarCredito", ex);
                return null;
            }
        }


        public List<Xpinn.FabricaCreditos.Entities.Credito> ListarCreditoss(Usuario pUsuario, String filtro)
        {
            try
            {
                return DAReliquidacion.ListarCreditoss(pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReliquidacionBusiness", "ListarCredito", ex);
                return null;
            }
        }


        public Reliquidacion CrearReliquidacion(Reliquidacion pReliquidacion, ref string pError, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pReliquidacion = DAReliquidacion.CrearReliquidacion(pReliquidacion, ref pError, pUsuario);
                    // Grabar las cuotas extras
                    if (pReliquidacion.lstCuotasExtras != null && pReliquidacion.numero_radicacion > 0)
                    {
                        Xpinn.FabricaCreditos.Data.CuotasExtrasData DACuoExt = new Xpinn.FabricaCreditos.Data.CuotasExtrasData();
                        DACuoExt.EliminarCuotasExtrasActuales(Convert.ToInt64(pReliquidacion.numero_radicacion), pUsuario);
                        foreach (Xpinn.FabricaCreditos.Entities.CuotasExtras vCuotaExtra in pReliquidacion.lstCuotasExtras)
                        {
                            //ingresa datos en la entity
                            vCuotaExtra.numero_radicacion = pReliquidacion.numero_radicacion;
                            DACuoExt.CrearCuotasExtras(vCuotaExtra, pUsuario);
                        }
                    }
                    ts.Complete();
                    return pReliquidacion;
                }
            }
            catch (Exception ex)
            {
                pError += ex.Message;
                BOExcepcion.Throw("ReliquidacionBusiness", "CrearReliquidacion", ex);
                return null;
            }
        }
        public Xpinn.FabricaCreditos.Entities.Credito CreditoReliquidado(string pNumeroRadicacion, Usuario pUsuario)
        {
            try
            {
                return DAReliquidacion.CreditoReliquidado(pNumeroRadicacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReliquidacionBusiness", "CreditoReliquidado", ex);
                return null;
            }
        }


    }
}
