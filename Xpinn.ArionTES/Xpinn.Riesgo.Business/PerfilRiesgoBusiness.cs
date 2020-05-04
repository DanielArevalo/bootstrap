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
   public class PerfilRiesgoBusiness : GlobalBusiness
    {
        PerfilRiesgoData DAPerfilRiesgo;

        public PerfilRiesgoBusiness()
        {
            DAPerfilRiesgo = new PerfilRiesgoData();
        }

        public List<PerfilRiesgo> ListarPerfilRiesgo(PerfilRiesgo pPerfilRiesgo, Usuario usuario)
        {
            try
            {
                return DAPerfilRiesgo.ListarPerfilRiesgo(pPerfilRiesgo, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PerfilRiesgoBusiness", "ListarPerfilRiesgo", ex);
                return null;
            }
        }

        public PerfilRiesgo CrearPerfilRiesgo(PerfilRiesgo pPerfilRiesgo, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    //pPerfilRiesgo.cod_usua = vUsuario.codusuario;
                    pPerfilRiesgo = DAPerfilRiesgo.CrearPerfilRiesgo(pPerfilRiesgo, vUsuario);
                    ts.Complete();
                    return pPerfilRiesgo;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadEcoBusiness", "CrearActividades", ex);
                return null;
            }
        }

        public PerfilRiesgo ModificarPerfilRiesgo(PerfilRiesgo pPerfilRiesgo, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    //pPerfilRiesgo.cod_usua = vUsuario.codusuario;
                    pPerfilRiesgo = DAPerfilRiesgo.ModificarPerfilRiesgo(pPerfilRiesgo, vUsuario);
                    ts.Complete();
                    return pPerfilRiesgo;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PerfilRiesgoBusiness", "ModificarActividades", ex);
                return null;
            }
        }

        public void EliminarPerfilRiesgo(PerfilRiesgo pPerfilRiesgo, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAPerfilRiesgo.EliminarPerfilRiesgo(pPerfilRiesgo, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PerfilRiesgoBusiness", "EliminarActividades", ex);
            }
        }

        public PerfilRiesgo ConsultarPerfilRiesgo(PerfilRiesgo pPerfilRiesgo, Usuario vUsuario)
        {
            try
            {
                pPerfilRiesgo = DAPerfilRiesgo.ConsultarPerfilRiesgo(pPerfilRiesgo, vUsuario);
                return pPerfilRiesgo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PerfilRiesgoBusiness", "ConsultarPerfilRiesgo", ex);
                return null;
            }
        }
    }
}
