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
    public class JurisdiccionDepaBusiness : GlobalBusiness
    {

        JurisdiccionDepaData DAJurisdiccionDepa;

        public JurisdiccionDepaBusiness()
        {
            DAJurisdiccionDepa = new JurisdiccionDepaData();
        }

        public List<JurisdiccionDepa> ListarJurisdiccionDepa(JurisdiccionDepa pJurisdiccionDepa, Usuario usuario)
        {
            try
            {
                return DAJurisdiccionDepa.ListarJurisdiccionDepa(pJurisdiccionDepa, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("JurisdiccionDepaBusiness", "ListarJurisdiccionDepa", ex);
                return null;
            }
        }

        public JurisdiccionDepa CrearJurisdiccionDepa(JurisdiccionDepa pJurisdiccionDepa, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pJurisdiccionDepa.cod_usua = vUsuario.codusuario;
                    pJurisdiccionDepa = DAJurisdiccionDepa.CrearJurisdiccionDepa(pJurisdiccionDepa, vUsuario);
                    ts.Complete();
                    return pJurisdiccionDepa;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("JurisdiccionDepaBusiness", "CrearJurisdiccionDepa", ex);
                return null;
            }
        }

        public JurisdiccionDepa ModificarJurisdiccionDepa(JurisdiccionDepa pJurisdiccionDepa, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pJurisdiccionDepa.cod_usua = vUsuario.codusuario;
                    pJurisdiccionDepa = DAJurisdiccionDepa.ModificarJurisdiccionDepa(pJurisdiccionDepa, vUsuario);
                    ts.Complete();
                    return pJurisdiccionDepa;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("JurisdiccionDepaBusiness", "ModificarJurisdiccionDepa", ex);
                return null;
            }
        }

        public void EliminarJurisdiccionDepa(JurisdiccionDepa pJurisdiccionDepa, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAJurisdiccionDepa.EliminarJurisdiccionDepa(pJurisdiccionDepa, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("JurisdiccionDepaBusiness", "EliminarJurisdiccionDepa", ex);
            }
        }

        public JurisdiccionDepa ConsultarJurisdiccionDepa(JurisdiccionDepa pJurisdiccionDepa, Usuario vUsuario)
        {
            try
            {
                pJurisdiccionDepa = DAJurisdiccionDepa.ConsultarJurisdiccionDepa(pJurisdiccionDepa, vUsuario);
                return pJurisdiccionDepa;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("JurisdiccionDepaBusiness", "ConsultarJurisdiccionDepa", ex);
                return null;
            }
        }

        public List<JurisdiccionDepa> ListasDesplegables(Usuario pUsuario)
        {
            try
            {
                return DAJurisdiccionDepa.ListasDesplegables(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("JurisdiccionDepaBusiness", "ListasDesplegables", ex);
                return null;
            }
        }


    }
}
