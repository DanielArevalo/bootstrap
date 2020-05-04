using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;
 
namespace Xpinn.FabricaCreditos.Business
{
 
        public class AmortizaCreBusiness : GlobalBusiness
        {
 
            private AmortizaCreData DAAmortizaCre;
 
            public AmortizaCreBusiness()
            {
                DAAmortizaCre = new AmortizaCreData();
            }
 
            public AmortizaCre CrearAmortizaCre(AmortizaCre pAmortizaCre, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        pAmortizaCre = DAAmortizaCre.CrearAmortizaCre(pAmortizaCre, pusuario);
 
                        ts.Complete();
 
                    }
 
                    return pAmortizaCre;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("AmortizaCreBusiness", "CrearAmortizaCre", ex);
                    return null;
                }
            }
 
 
            public AmortizaCre ModificarAmortizaCre(AmortizaCre pAmortizaCre, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        pAmortizaCre = DAAmortizaCre.ModificarAmortizaCre(pAmortizaCre, pusuario);
 
                        ts.Complete();
 
                    }
 
                    return pAmortizaCre;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("AmortizaCreBusiness", "ModificarAmortizaCre", ex);
                    return null;
                }
            }
 
 
            public void EliminarAmortizaCre(Int64 pId, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        DAAmortizaCre.EliminarAmortizaCre(pId, pusuario);
 
                        ts.Complete();
 
                    }
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("AmortizaCreBusiness", "EliminarAmortizaCre", ex);
                }
            }
 
 
            public AmortizaCre ConsultarAmortizaCre(Int64 pId, Usuario pusuario)
            {
                try
                {
                    AmortizaCre AmortizaCre = new AmortizaCre();
                    AmortizaCre = DAAmortizaCre.ConsultarAmortizaCre(pId, pusuario);
                    return AmortizaCre;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("AmortizaCreBusiness", "ConsultarAmortizaCre", ex);
                    return null;
                }
            }
 
 
            public List<AmortizaCre> ListarAmortizaCre(AmortizaCre pAmortizaCre, Usuario pusuario)
            {
                try
                {
                   return DAAmortizaCre.ListarAmortizaCre(pAmortizaCre, pusuario);
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("AmortizaCreBusiness", "ListarAmortizaCre", ex);
                    return null;
                }
            }
 
 
        }

}