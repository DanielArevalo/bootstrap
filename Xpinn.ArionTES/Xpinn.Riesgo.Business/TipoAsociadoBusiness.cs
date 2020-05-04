using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Riesgo.Data;
using Xpinn.Riesgo.Entities;

namespace Xpinn.Riesgo.Business
{
   public class TipoAsociadoBusiness : GlobalBusiness
    {
        TipoAsociadoData DATipoAsociado;

        public TipoAsociadoBusiness()
        {
            DATipoAsociado = new TipoAsociadoData();
        }

        public List<TipoAsociado> ListarTipoAsociado(TipoAsociado pTipoAsociado, Usuario usuario)
        {
            try
            {
                return DATipoAsociado.ListarTipoAsociado(pTipoAsociado, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoAsociadoBusiness", "ListarTipoAsociado", ex);
                return null;
            }
        }

        public TipoAsociado CrearTipoAsociado(TipoAsociado pTipoAsociado, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    //pTipoAsociado.cod_usua = vUsuario.codusuario;
                    pTipoAsociado = DATipoAsociado.CrearTipoAsociado(pTipoAsociado, vUsuario);
                    ts.Complete();
                    return pTipoAsociado;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadEcoBusiness", "CrearTipoAsociado", ex);
                return null;
            }
        }

        public TipoAsociado ModificarTipoAsociado(TipoAsociado pTipoAsociado, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    //pTipoAsociado.cod_usua = vUsuario.codusuario;
                    pTipoAsociado = DATipoAsociado.ModificarTipoAsociado(pTipoAsociado, vUsuario);
                    ts.Complete();
                    return pTipoAsociado;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoAsociadoBusiness", "ModificarTipoAsociado", ex);
                return null;
            }
        }

        public void EliminarTipoAsociado(TipoAsociado pTipoAsociado, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DATipoAsociado.EliminarTipoAsociado(pTipoAsociado, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoAsociadoBusiness", "EliminarTipoAsociado", ex);
            }
        }

        public TipoAsociado ConsultarTipoAsociado(TipoAsociado pTipoAsociado, Usuario vUsuario)
        {
            try
            {
                pTipoAsociado = DATipoAsociado.ConsultarTipoAsociado(pTipoAsociado, vUsuario);
                return pTipoAsociado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoAsociadoBusiness", "ConsultarTipoAsociado", ex);
                return null;
            }
        }
    }
}
