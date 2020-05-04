using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Contabilidad.Data;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Business
{

    public class ParametroCtasAuxiliosBusiness : GlobalBusiness
        {
 
            private ParametroCtasAuxiliosData DAPar_Cue_LinAux;
 
            public ParametroCtasAuxiliosBusiness()
            {
                DAPar_Cue_LinAux = new ParametroCtasAuxiliosData();
            }
 
            public Par_Cue_LinAux CrearPar_Cue_LinAux(Par_Cue_LinAux pPar_Cue_LinAux, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        pPar_Cue_LinAux = DAPar_Cue_LinAux.CrearPar_Cue_LinAux(pPar_Cue_LinAux, pusuario);
 
                        ts.Complete();
 
                    }
 
                    return pPar_Cue_LinAux;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("Par_Cue_LinAuxBusiness", "CrearPar_Cue_LinAux", ex);
                    return null;
                }
            }
 
 
            public Par_Cue_LinAux ModificarPar_Cue_LinAux(Par_Cue_LinAux pPar_Cue_LinAux, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        pPar_Cue_LinAux = DAPar_Cue_LinAux.ModificarPar_Cue_LinAux(pPar_Cue_LinAux, pusuario);
 
                        ts.Complete();
 
                    }
 
                    return pPar_Cue_LinAux;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("Par_Cue_LinAuxBusiness", "ModificarPar_Cue_LinAux", ex);
                    return null;
                }
            }
 
 
            public void EliminarPar_Cue_LinAux(Int64 pId, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        DAPar_Cue_LinAux.EliminarPar_Cue_LinAux(pId, pusuario);
 
                        ts.Complete();
 
                    }
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("Par_Cue_LinAuxBusiness", "EliminarPar_Cue_LinAux", ex);
                }
            }
 
 
            public Par_Cue_LinAux ConsultarPar_Cue_LinAux(Int64 pId, Usuario pusuario)
            {
                try
                {
                    Par_Cue_LinAux Par_Cue_LinAux = new Par_Cue_LinAux();
                    Par_Cue_LinAux = DAPar_Cue_LinAux.ConsultarPar_Cue_LinAux(pId, pusuario);
                    return Par_Cue_LinAux;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("Par_Cue_LinAuxBusiness", "ConsultarPar_Cue_LinAux", ex);
                    return null;
                }
            }
 
 
            public List<Par_Cue_LinAux> ListarPar_Cue_LinAux(Par_Cue_LinAux pPar_Cue_LinAux, Usuario pusuario)
            {
                try
                {
                   return DAPar_Cue_LinAux.ListarPar_Cue_LinAux(pPar_Cue_LinAux, pusuario);
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("Par_Cue_LinAuxBusiness", "ListarPar_Cue_LinAux", ex);
                    return null;
                }
            }
 
 
        }

}


