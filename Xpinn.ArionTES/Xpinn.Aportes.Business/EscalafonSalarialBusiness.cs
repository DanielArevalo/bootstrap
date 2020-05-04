using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Aportes.Data;
using Xpinn.Aportes.Entities;
 
namespace Xpinn.Aportes.Business
{
 
        public class EscalafonSalarialBusiness : GlobalBusiness
        {
 
            private EscalafonSalarialData DAEscalafonSalarial;
 
            public EscalafonSalarialBusiness()
            {
                DAEscalafonSalarial = new EscalafonSalarialData();
            }
 
            public EscalafonSalarial CrearEscalafonSalarial(EscalafonSalarial pEscalafonSalarial, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        pEscalafonSalarial = DAEscalafonSalarial.CrearEscalafonSalarial(pEscalafonSalarial, pusuario);
 
                        ts.Complete();
 
                    }
 
                    return pEscalafonSalarial;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("EscalafonSalarialBusiness", "CrearEscalafonSalarial", ex);
                    return null;
                }
            }
 
 
            public EscalafonSalarial ModificarEscalafonSalarial(EscalafonSalarial pEscalafonSalarial, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        pEscalafonSalarial = DAEscalafonSalarial.ModificarEscalafonSalarial(pEscalafonSalarial, pusuario);
 
                        ts.Complete();
 
                    }
 
                    return pEscalafonSalarial;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("EscalafonSalarialBusiness", "ModificarEscalafonSalarial", ex);
                    return null;
                }
            }
 
 
            public void EliminarEscalafonSalarial(Int64 pId, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        DAEscalafonSalarial.EliminarEscalafonSalarial(pId, pusuario);
 
                        ts.Complete();
 
                    }
                }
                catch 
                {
                    return;
                }
            }
 
 
            public EscalafonSalarial ConsultarEscalafonSalarial(Int64 pId, Usuario pusuario)
            {
                try
                {
                    EscalafonSalarial EscalafonSalarial = new EscalafonSalarial();
                    EscalafonSalarial = DAEscalafonSalarial.ConsultarEscalafonSalarial(pId, pusuario);
                    return EscalafonSalarial;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("EscalafonSalarialBusiness", "ConsultarEscalafonSalarial", ex);
                    return null;
                }
            }
 
 
            public List<EscalafonSalarial> ListarEscalafonSalarial(string Filtro,EscalafonSalarial pEscalafonSalarial, Usuario pusuario)
            {
                try
                {
                   return DAEscalafonSalarial.ListarEscalafonSalarial(Filtro,pEscalafonSalarial, pusuario);
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("EscalafonSalarialBusiness", "ListarEscalafonSalarial", ex);
                    return null;
                }
            }
 
 
        }
    }
