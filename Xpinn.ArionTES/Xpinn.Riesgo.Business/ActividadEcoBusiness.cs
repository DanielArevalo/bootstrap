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
    public class ActividadEcoBusiness : GlobalBusiness
    {
        ActividadEcoData DAActividades;


        public ActividadEcoBusiness()
        {
            DAActividades = new ActividadEcoData();
        }

        public List<ActividadEco> ListarActividadEco(ActividadEco pActividades, Usuario usuario)
        {
            try
            {
                return DAActividades.ListarActividadEco(pActividades, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadEcoBusiness", "ListarActividadEco", ex);
                return null;
            }
        }

        public ActividadEco CrearActividades(ActividadEco pActividades, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pActividades.cod_usua = vUsuario.codusuario;
                    pActividades = DAActividades.CrearActividad(pActividades, vUsuario);
                    ts.Complete();
                    return pActividades;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadEcoBusiness", "CrearActividades", ex);
                return null;
            }
        }

        public ActividadEco ModificarActividades(ActividadEco pActividad, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pActividad.cod_usua = vUsuario.codusuario;
                    pActividad = DAActividades.ModificarActividad(pActividad, vUsuario);
                    ts.Complete();
                    return pActividad;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadEcoBusiness", "ModificarActividades", ex);
                return null;
            }
        }

        public void EliminarActividades(ActividadEco pActividad, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAActividades.EliminarActividades(pActividad, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadEcoBusiness", "EliminarActividades", ex);
            }
        }

        public ActividadEco ConsultarActividades(ActividadEco pActividad, Usuario vUsuario)
        {
            try
            {
                pActividad = DAActividades.ConsultarActividades(pActividad, vUsuario);
                return pActividad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadEcoBusiness", "ConsultarActividades", ex);
                return null;
            }
        }
    }
}
