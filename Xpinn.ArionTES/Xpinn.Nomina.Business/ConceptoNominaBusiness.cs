using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Transactions;
using Xpinn.Nomina.Data;
using Xpinn.Nomina.Entities;
using Xpinn.Util;

namespace Xpinn.Nomina.Business
{
    public class ConceptoNominaBusiness : GlobalBusiness 
    {
        private ConceptoNominaData DAConceptoNomina;

        public ConceptoNominaBusiness()
        {
            DAConceptoNomina = new ConceptoNominaData();
        }
        public ConceptosNomina CrearConceptoNomina(ConceptosNomina pConceptoNomina, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pConceptoNomina = DAConceptoNomina.CrearConceptoNomina(pConceptoNomina, vUsuario);



                    ts.Complete();
                }

                return pConceptoNomina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoNomina", "CrearConceptoNomina", ex);
                return null;
            }
        }
        public ConceptosNomina ConsultarCodigoMaximoConceptoNomina(Usuario pUsuario)
        {
            try
            {
                return DAConceptoNomina.ConsultarCodigoMaximoConceptoNomina(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoNomina", "ConsultarCodigoMaximoConceptoNomina", ex);
                return null;
            }
        }
        public List<ConceptosNomina> ListarConceptosNomin(string filtro,Usuario pUsuario)
        {
            try
            {
                return DAConceptoNomina.ListarConceptosNomina(filtro,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoNomina", "listarConceptosNomina", ex);
                return null;
            }
        }
        public ConceptosNomina modificarConceptoNomina(ConceptosNomina pConceptoNomina, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pConceptoNomina = DAConceptoNomina.ModificarConceptoNomina(pConceptoNomina, vUsuario);



                    ts.Complete();
                }

                return pConceptoNomina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoNomina", "modificarConceptoNomina", ex);
                return null;
            }
        }
        public ConceptosNomina EliminarConceptoNomina(ConceptosNomina pConceptoNomina, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pConceptoNomina = DAConceptoNomina.EliminarConceptoNomina(pConceptoNomina, vUsuario);



                    ts.Complete();
                }

                return pConceptoNomina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoNomina", "EliminarConceptoNomina", ex);
                return null;
            }
        }
        public ConceptosNomina ConsultarConceptoNomina(Usuario pUsuario,string idObjeto)
        {
            try
            {
                return DAConceptoNomina.ConsultarConceptoNomina(pUsuario,idObjeto);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoNomina", "ConsultarConceptoNomina", ex);
                return null;
            }
        }


        public List<ConceptosNomina> ConsultarConceptosNominaConfiltro(Usuario pUsuario, string filtro)
        {
            try
            {
                return DAConceptoNomina.ConsultarConceptosNominaConfiltro(pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoNomina", "ConsultarConceptosNominaConfiltro", ex);
                return null;
            }
        }


    }
}
