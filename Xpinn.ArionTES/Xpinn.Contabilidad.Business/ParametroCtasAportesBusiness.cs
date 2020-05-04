using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Contabilidad.Data;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Business
{
    /// <summary>
    /// Objeto de negocio para concepto
    /// </summary>
    public class ParametroCtasAportesBusiness : GlobalBusiness
    {
        private ParametroCtasAportesData DAParametro;

        /// <summary>
        /// Constructor del objeto de negocio para concepto
        /// </summary>
        public ParametroCtasAportesBusiness()
        {
            DAParametro = new ParametroCtasAportesData();
        }

        
        public List<Par_Cue_LinApo> ListarPar_Cue_LinApo(string filtro, Usuario vUsuario)
        {
            try
            {
                return DAParametro.ListarPar_Cue_LinApo(filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinApoBusiness", "ListarPar_Cue_LinApo", ex);
                return null;
            }
        }


        public Par_Cue_LinApo ConsultarPar_Cue_LinApo(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return DAParametro.ConsultarPar_Cue_LinApo(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinApoBusiness", "ConsultarPar_Cue_LinApo", ex);
                return null;
            }
        }


        public Par_Cue_LinApo CrearParametroAporte(Par_Cue_LinApo pParam, Usuario vUsuario, int opcion)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pParam = DAParametro.CrearParametroAporte(pParam, vUsuario, opcion);
                    
                    ts.Complete();
                }
                return pParam;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinApoBusiness", "CrearParametroAporte", ex);
                return null;
            }
        }


        public void EliminarParametroAporte(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAParametro.EliminarParametroAporte(pId, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinApoBusiness", "EliminarParametroAporte", ex);               
            }
        }


    }
}

