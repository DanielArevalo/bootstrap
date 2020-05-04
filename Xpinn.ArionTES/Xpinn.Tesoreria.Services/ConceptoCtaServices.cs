using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Tesoreria.Entities;
using Xpinn.Tesoreria.Business;

namespace Xpinn.Tesoreria.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ConceptoCtaService
    {

        private ConceptoCtaBusiness BOConceptoCta;
        private ExcepcionBusiness BOExcepcion;

        public ConceptoCtaService()
        {
            BOConceptoCta = new ConceptoCtaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "40701"; } }

        public ConceptoCta CrearConceptoCta(ConceptoCta pConceptoCta, Usuario pusuario)
        {
            try
            {
                pConceptoCta = BOConceptoCta.CrearConceptoCta(pConceptoCta, pusuario);
                return pConceptoCta;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoCtaService", "CrearConceptoCta", ex);
                return null;
            }
        }


        public ConceptoCta ModificarConceptoCta(ConceptoCta pConceptoCta, Usuario pusuario)
        {
            try
            {
                pConceptoCta = BOConceptoCta.ModificarConceptoCta(pConceptoCta, pusuario);
                return pConceptoCta;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoCtaService", "ModificarConceptoCta", ex);
                return null;
            }
        }


        public void EliminarConceptoCta(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOConceptoCta.EliminarConceptoCta(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoCtaService", "EliminarConceptoCta", ex);
            }
        }


        public void EliminarConceptoImpuesto(Int32 pId, Usuario vUsuario)
        {
            try
            {
                BOConceptoCta.EliminarConceptoImpuesto(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoCtaService", "EliminarConceptoImpuesto", ex);
            }
        }

        public ConceptoCta ConsultarConceptoCta(Int64 pId, Usuario pusuario)
        {
            try
            {
                ConceptoCta ConceptoCta = new ConceptoCta();
                ConceptoCta = BOConceptoCta.ConsultarConceptoCta(pId, pusuario);
                return ConceptoCta;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoCtaService", "ConsultarConceptoCta", ex);
                return null;
            }
        }


        public List<ConceptoCta> ListarConceptoCta(ConceptoCta pConceptoCta, Usuario pusuario)
        {
            try
            {
                return BOConceptoCta.ListarConceptoCta(pConceptoCta, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoCtaService", "ListarConceptoCta", ex);
                return null;
            }
        }

        public List<Concepto_CuentasXpagarImp> ListarConceptoImpuesto(Concepto_CuentasXpagarImp pImpuesto, Usuario vUsuario)
        {
            try
            {
                return BOConceptoCta.ListarConceptoImpuesto(pImpuesto, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoCtaService", "ListarConceptoImpuesto", ex);
                return null;
            }
        }

        public List<Concepto_CuentasXpagarImp> ListarConceptoImpuestoDetalle(Concepto_CuentasXpagarImp pImpuesto, Usuario vUsuario)
        {
            try
            {
                return BOConceptoCta.ListarConceptoImpuestoDetalle(pImpuesto, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoCtaService", "ListarConceptoImpuestoDetalle", ex);
                return null;
            }
        }

        public List<Concepto_CuentasXpagarImp> ListarConceptoImpuestoDetalleCxp(Concepto_CuentasXpagarImp pImpuesto, Usuario vUsuario)
        {
            try
            {
                return BOConceptoCta.ListarConceptoImpuestoDetalleCxp(pImpuesto, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoCtaService", "ListarConceptoImpuestoDetalleCxp", ex);
                return null;
            }
        }
        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return BOConceptoCta.ObtenerSiguienteCodigo(pUsuario);
            }
            catch (Exception ex)
            {
                return 0;
            }
        } 

    }
}
