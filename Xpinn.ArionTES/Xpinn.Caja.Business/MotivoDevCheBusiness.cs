using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Caja.Data;
using Xpinn.Caja.Entities;
 
namespace Xpinn.Caja.Business
{
    public class MotivoDevCheBusiness : GlobalBusiness
    {
 
        private MotivoDevCheData DAMotivoDevChe;
 
        public MotivoDevCheBusiness()
        {
            DAMotivoDevChe = new MotivoDevCheData();
        }
 
        public MotivoDevChe CrearMotivoDevChe(MotivoDevChe pMotivoDevChe, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pMotivoDevChe = DAMotivoDevChe.CrearMotivoDevChe(pMotivoDevChe, pusuario);
 
                    ts.Complete();
 
                }
 
                return pMotivoDevChe;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoDevCheBusiness", "CrearMotivoDevChe", ex);
                return null;
            }
        }
 
 
        public MotivoDevChe ModificarMotivoDevChe(MotivoDevChe pMotivoDevChe, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pMotivoDevChe = DAMotivoDevChe.ModificarMotivoDevChe(pMotivoDevChe, pusuario);
 
                    ts.Complete();
 
                }
 
                return pMotivoDevChe;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoDevCheBusiness", "ModificarMotivoDevChe", ex);
                return null;
            }
        }
 
 
        public void EliminarMotivoDevChe(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAMotivoDevChe.EliminarMotivoDevChe(pId, pusuario);
 
                    ts.Complete();
 
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoDevCheBusiness", "EliminarMotivoDevChe", ex);
            }
        }
 
 
        public MotivoDevChe ConsultarMotivoDevChe(Int64 pId, Usuario pusuario)
        {
            try
            {
                MotivoDevChe MotivoDevChe = new MotivoDevChe();
                MotivoDevChe = DAMotivoDevChe.ConsultarMotivoDevChe(pId, pusuario);
                return MotivoDevChe;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoDevCheBusiness", "ConsultarMotivoDevChe", ex);
                return null;
            }
        }
 
 
        public List<MotivoDevChe> ListarMotivoDevChe(MotivoDevChe pMotivoDevChe, Usuario pusuario)
        {
            try
            {
                return DAMotivoDevChe.ListarMotivoDevChe(pMotivoDevChe, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoDevCheBusiness", "ListarMotivoDevChe", ex);
                return null;
            }
        }
 
 
    }
    
}
