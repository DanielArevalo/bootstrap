using Xpinn.Util;
using Xpinn.ConciliacionBancaria.Data;
using Xpinn.ConciliacionBancaria.Entities;
using System.Transactions;
using System;
using System.Collections.Generic;

namespace Xpinn.ConciliacionBancaria.Business
{

    public class conceptobancarioBusiness : GlobalBusiness
    {

        private ConceptoBancarioData DAconceptobancario;

        public conceptobancarioBusiness()
        {
            DAconceptobancario = new ConceptoBancarioData();
        }

        public ConceptoBancario Crearconceptobancario(ConceptoBancario pconceptobancario, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pconceptobancario = DAconceptobancario.CrearConceptoBancario(pconceptobancario, pusuario);

                    ts.Complete();

                }

                return pconceptobancario;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("conceptobancarioBusiness", "Crearconceptobancario", ex);
                return null;
            }
        }


        public ConceptoBancario Modificarconceptobancario(ConceptoBancario pconceptobancario, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pconceptobancario = DAconceptobancario.ModificarConceptoBancario(pconceptobancario, pusuario);

                    ts.Complete();

                }

                return pconceptobancario;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("conceptobancarioBusiness", "Modificarconceptobancario", ex);
                return null;
            }
        }


        public void Eliminarconceptobancario(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAconceptobancario.EliminarConceptoBancario(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("conceptobancarioBusiness", "Eliminarconceptobancario", ex);
            }
        }


        public ConceptoBancario Consultarconceptobancario(Int64 pId, Usuario pusuario)
        {
            try
            {
                ConceptoBancario conceptobancario = new ConceptoBancario();
                conceptobancario = DAconceptobancario.ConsultarConceptoBancario(pId, pusuario);
                return conceptobancario;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("conceptobancarioBusiness", "Eliminarconceptobancario", ex);
                return null;
            }

        }


        public List<ConceptoBancario> Listarconceptobancario(String filtro,ConceptoBancario pconceptobancario, Usuario pusuario)
        {
            try
            {
                return DAconceptobancario.ListarConceptoBancario(filtro,pconceptobancario, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("conceptobancarioBusiness", "Listarconceptobancario", ex);
                return null;
            }
        }

    }
}