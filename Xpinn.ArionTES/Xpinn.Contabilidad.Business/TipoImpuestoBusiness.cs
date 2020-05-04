using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Contabilidad.Data;
using Xpinn.Contabilidad.Entities;
 
namespace Xpinn.Contabilidad.Business
{
    public class TipoImpuestoBusiness : GlobalBusiness
    {
 
        private TipoImpuestoData DATipoImpuesto;
 
        public TipoImpuestoBusiness()
        {
            DATipoImpuesto = new TipoImpuestoData();
        }
 
        public TipoImpuesto CrearTipoImpuesto(TipoImpuesto pTipoImpuesto, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoImpuesto = DATipoImpuesto.CrearTipoImpuesto(pTipoImpuesto, pusuario);
 
                    ts.Complete();
 
                }
 
                return pTipoImpuesto;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoImpuestoBusiness", "CrearTipoImpuesto", ex);
                return null;
            }
        }
 
 
        public TipoImpuesto ModificarTipoImpuesto(TipoImpuesto pTipoImpuesto, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoImpuesto = DATipoImpuesto.ModificarTipoImpuesto(pTipoImpuesto, pusuario);
 
                    ts.Complete();
 
                }
 
                return pTipoImpuesto;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoImpuestoBusiness", "ModificarTipoImpuesto", ex);
                return null;
            }
        }
 
 
        public void EliminarTipoImpuesto(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DATipoImpuesto.EliminarTipoImpuesto(pId, pusuario);
 
                    ts.Complete();
 
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoImpuestoBusiness", "EliminarTipoImpuesto", ex);
            }
        }
 
 
        public TipoImpuesto ConsultarTipoImpuesto(Int64 pId, Usuario pusuario)
        {
            try
            {
                TipoImpuesto TipoImpuesto = new TipoImpuesto();
                TipoImpuesto = DATipoImpuesto.ConsultarTipoImpuesto(pId, pusuario);
                return TipoImpuesto;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoImpuestoBusiness", "ConsultarTipoImpuesto", ex);
                return null;
            }
        }
 
 
        public List<TipoImpuesto> ListarTipoImpuesto(TipoImpuesto pTipoImpuesto, Usuario pusuario)
        {
            try
            {
                return DATipoImpuesto.ListarTipoImpuesto(pTipoImpuesto, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoImpuestoBusiness", "ListarTipoImpuesto", ex);
                return null;
            }
        }
 
 
    }
    
}