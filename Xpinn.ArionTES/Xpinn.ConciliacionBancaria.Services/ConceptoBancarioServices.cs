using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.ConciliacionBancaria.Entities;
using Xpinn.ConciliacionBancaria.Business;

namespace Xpinn.ConciliacionBancaria.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ConciliacionBancariaService
    {

        private conceptobancarioBusiness BOConciliacionBancaria;
        private ExcepcionBusiness BOExcepcion;

        public ConciliacionBancariaService()
        {
            BOConciliacionBancaria = new conceptobancarioBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "40807"; } }

        public ConceptoBancario CrearConciliacionBancaria(ConceptoBancario pConciliacionBancaria, Usuario pusuario)
        {
            try
            {
                pConciliacionBancaria = BOConciliacionBancaria.Crearconceptobancario(pConciliacionBancaria, pusuario);
                return pConciliacionBancaria;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaService", "CrearConciliacionBancaria", ex);
                return null;
            }
        }


        public ConceptoBancario ModificarConciliacionBancaria(ConceptoBancario pConciliacionBancaria, Usuario pusuario)
        {
            try
            {
                pConciliacionBancaria = BOConciliacionBancaria.Modificarconceptobancario(pConciliacionBancaria, pusuario);
                return pConciliacionBancaria;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaService", "ModificarConciliacionBancaria", ex);
                return null;
            }
        }


        public void EliminarConciliacionBancaria(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOConciliacionBancaria.Eliminarconceptobancario(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaService", "EliminarConciliacionBancaria", ex);
            }
        }


        public ConceptoBancario ConsultarConciliacionBancaria(Int64 pId, Usuario pusuario)
        {
            try
            {
                ConceptoBancario ConciliacionBancaria = new ConceptoBancario();
                ConciliacionBancaria = BOConciliacionBancaria.Consultarconceptobancario(pId, pusuario);
                return ConciliacionBancaria;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaService", "ConsultarConciliacionBancaria", ex);
                return null;
            }
        }


        public List<ConceptoBancario> Listarconceptobancario(String filtro,ConceptoBancario pconceptobancario, Usuario pusuario)
        {
            try
            {
                return BOConciliacionBancaria.Listarconceptobancario(filtro,pconceptobancario, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("conceptobancarioBusiness", "Listarconceptobancario", ex);
                return null;
            }
        }

    }
}
