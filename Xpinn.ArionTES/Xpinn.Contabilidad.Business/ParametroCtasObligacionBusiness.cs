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
    public class ParametroCtasObligacionBusiness : GlobalBusiness
    {
        private ParametroCtasObligacionData DAParametro;

        /// <summary>
        /// Constructor del objeto de negocio para concepto
        /// </summary>
        public ParametroCtasObligacionBusiness()
        {
            DAParametro = new ParametroCtasObligacionData();
        }


        public List<Par_Cue_Obligacion> ListarParametrosCtasOBLI(string filtro, Usuario vUsuario)
        {
            try
            {
                return DAParametro.ListarParametrosCtasOBLI(filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroCtasObligacionBusiness", "ListarParametrosCtasOBLI", ex);
                return null;
            }
        }


        public Par_Cue_Obligacion ConsultarParametroCtasOBLI(Int32 pId, Usuario vUsuario)
        {
            try
            {
                return DAParametro.ConsultarParametroCtasOBLI(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroCtasObligacionBusiness", "ConsultarParametroCtasOBLI", ex);
                return null;
            }
        }


        public Par_Cue_Obligacion CrearParamCtasObligacion(Par_Cue_Obligacion pParam, Usuario vUsuario, int opcion)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pParam = DAParametro.CrearParamCtasObligacion(pParam, vUsuario, opcion);
                    
                    ts.Complete();
                }
                return pParam;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroCtasObligacionBusiness", "CrearParamCtasObligacion", ex);
                return null;
            }
        }


        public void EliminarParametroCtasOBLI(Int32 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAParametro.EliminarParametroCtasOBLI(pId, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroCtasObligacionBusiness", "EliminarParametroCtasOBLI", ex);               
            }
        }


    }
}

