using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Business;
using System.ServiceModel;

namespace Xpinn.Nomina.Services
{

    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]

    public class ConceptoNominaService
    {
        private ConceptoNominaBusiness BOConceptoNomina;
        private ExcepcionBusiness BOExcepcion;

        public ConceptoNominaService()
        {
            BOConceptoNomina = new ConceptoNominaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }
        public string CodigoPrograma { get { return "250607"; } }
        public ConceptosNomina CrearConceptoNomina(ConceptosNomina pConceptoNomina, Usuario vUsuario)
        {
            try
            {
                pConceptoNomina = BOConceptoNomina.CrearConceptoNomina(pConceptoNomina, vUsuario);
                return pConceptoNomina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoNominaServices", "CrearConceptoNomina", ex);
                return null;
            }
        }
        public ConceptosNomina ConsultarCodigoMaximoConceptoNomina(Usuario pUsuario)
        {
            try
            {
                return BOConceptoNomina.ConsultarCodigoMaximoConceptoNomina(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoNominaServices", "ConsultarCodigoMaximoConceptoNomina", ex);
                return null;
            }
        }
        public List<ConceptosNomina> ListarConceptosNomina(string filtro,Usuario pUsuario)
        {
            try
            {
                return BOConceptoNomina.ListarConceptosNomin(filtro,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoService", "ConsultarMaxTiposDocumento", ex);
                return null;
            }
        }
        public ConceptosNomina ModificarConceptoNomina(ConceptosNomina pConceptoNomina, Usuario vUsuario)
        {
            try
            {
                pConceptoNomina = BOConceptoNomina.modificarConceptoNomina(pConceptoNomina, vUsuario);
                return pConceptoNomina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoNominaServices", "ModificarConceptoNomina", ex);
                return null;
            }
        }
        public ConceptosNomina EliminarConceptoNomina(ConceptosNomina pConceptoNomina, Usuario vUsuario)
        {
            try
            {
                pConceptoNomina = BOConceptoNomina.EliminarConceptoNomina(pConceptoNomina, vUsuario);
                return pConceptoNomina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoNominaServices", "EliminarConceptoNomina", ex);
                return null;
            }
        }
        public ConceptosNomina ConsultarConceptoNomina(Usuario pUsuario,string idObjeto)
        {
            try
            {
                return BOConceptoNomina.ConsultarConceptoNomina(pUsuario,idObjeto);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoNominaServices", "ConsultarConceptoNomina", ex);
                return null;
            }
        }

        public List<ConceptosNomina> ConsultarConceptosNominaConfiltro(Usuario pUsuario, string filtro)
        {
            try
            {
                return BOConceptoNomina.ConsultarConceptosNominaConfiltro(pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoNominaServices", "ConsultarConceptosNominaConfiltro", ex);
                return null;
            }
        }


    }
}
