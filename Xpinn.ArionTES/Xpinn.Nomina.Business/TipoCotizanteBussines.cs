using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Nomina.Data;
using Xpinn.Nomina.Entities;
 
namespace Xpinn.Nomina.Business
{

    public class TipoCotizanteBusiness : GlobalBusiness
    {

        private TipoCotizanteNominaData DATipoCotizante;

        public TipoCotizanteBusiness()
        {
            DATipoCotizante = new TipoCotizanteNominaData();
        }

        public TipoCotizante CrearTipoCotizante(TipoCotizante pTipoCotizante, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoCotizante = DATipoCotizante.CrearTipoCotizante(pTipoCotizante, pusuario);

                    ts.Complete();

                }

                return pTipoCotizante;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoCotizanteBusiness", "CrearTipoCotizante", ex);
                return null;
            }
        }


        public TipoCotizante ModificarTipoCotizante(TipoCotizante pTipoCotizante, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoCotizante = DATipoCotizante.ModificarTipoCotizante(pTipoCotizante, pusuario);

                    ts.Complete();

                }

                return pTipoCotizante;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoCotizanteBusiness", "ModificarTipoCotizante", ex);
                return null;
            }
        }



        public TipoCotizante ConsultarTipoCotizante(Int64 pId, Usuario pusuario)
        {
            try
            {
                TipoCotizante TipoCotizante = new TipoCotizante();
                TipoCotizante = DATipoCotizante.ConsultarTipoCotizante(pId, pusuario);
                return TipoCotizante;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoCotizanteBusiness", "ConsultarTipoCotizante", ex);
                return null;
            }
        }

        
        public List<TipoCotizante> ListarTipoCotizante(string pid, Usuario pusuario)
        {
            try
            {
                return DATipoCotizante.ListarTipoCotizante(pid, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoCotizanteBusiness", "ListarTipoCotizante", ex);
                return null;
            }
        }


    }
}
