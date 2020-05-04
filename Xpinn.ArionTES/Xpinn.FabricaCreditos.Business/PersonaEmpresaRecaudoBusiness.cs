using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Business
{
    /// <summary>
    /// Objeto de negocio para PersonaEmpresaRecaudo
    /// </summary>
    public class PersonaEmpresaRecaudoBusiness : GlobalBusiness
    {
        private PersonaEmpresaRecaudoData DAPersonaEmpresaRecaudo;


        public PersonaEmpresaRecaudoBusiness()
        {
            DAPersonaEmpresaRecaudo = new PersonaEmpresaRecaudoData();
        }


        public PersonaEmpresaRecaudo CrearPersonaEmpresaRecaudo(PersonaEmpresaRecaudo pPersonaEmpresaRecaudo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPersonaEmpresaRecaudo = DAPersonaEmpresaRecaudo.CrearPersonaEmpresaRecaudo(pPersonaEmpresaRecaudo, pUsuario);

                    ts.Complete();
                }

                return pPersonaEmpresaRecaudo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaEmpresaRecaudoBusiness", "CrearPersonaEmpresaRecaudo", ex);
                return null;
            }
        }



        public PersonaEmpresaRecaudo ModificarPersonaEmpresaRecaudo(PersonaEmpresaRecaudo pPersonaEmpresaRecaudo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPersonaEmpresaRecaudo = DAPersonaEmpresaRecaudo.ModificarPersonaEmpresaRecaudo(pPersonaEmpresaRecaudo, pUsuario);

                    ts.Complete();
                }

                return pPersonaEmpresaRecaudo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaEmpresaRecaudoBusiness", "ModificarPersonaEmpresaRecaudo", ex);
                return null;
            }
        }



        public void EliminarPersonaEmpresaRecaudo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAPersonaEmpresaRecaudo.EliminarPersonaEmpresaRecaudo(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaEmpresaRecaudoBusiness", "EliminarPersonaEmpresaRecaudo", ex);
            }
        }


        public PersonaEmpresaRecaudo ConsultarPersonaEmpresaRecaudo(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return DAPersonaEmpresaRecaudo.ConsultarPersonaEmpresaRecaudo(pId, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadPersonaBusiness", "ConsultarActividad", ex);
                return null;
            }
        }



        public List<PersonaEmpresaRecaudo> ListarPersonaEmpresaRecaudo(PersonaEmpresaRecaudo pPersonaEmpresaRecaudo, Usuario pUsuario)
        {
            try
            {
                return DAPersonaEmpresaRecaudo.ListarPersonaEmpresaRecaudo(pPersonaEmpresaRecaudo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaEmpresaRecaudoBusiness", "ListarPersonaEmpresaRecaudo", ex);
                return null;
            }
        }


        public List<PersonaEmpresaRecaudo> ListarPersonaEmpresaRecaudo(Int64 pCodPersona, Usuario pUsuario)
        {
            try
            {
                return DAPersonaEmpresaRecaudo.ListarPersonaEmpresaRecaudo(pCodPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaEmpresaRecaudoBusiness", "ListarPersonaEmpresaRecaudo", ex);
                return null;
            }
        }

        public List<PersonaEmpresaRecaudo> ListarEmpresaRecaudo(Usuario vUsuario)
        {
            return ListarEmpresaRecaudo(false, vUsuario);
        }

        public List<PersonaEmpresaRecaudo> ListarEmpresaRecaudo(bool alfabetico, Usuario pUsuario)
        {
            try
            {
                return DAPersonaEmpresaRecaudo.ListarEmpresaRecaudo(alfabetico, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaEmpresaRecaudoBusiness", "ListarEmpresaRecaudo", ex);
                return null;
            }
        }



    }
}