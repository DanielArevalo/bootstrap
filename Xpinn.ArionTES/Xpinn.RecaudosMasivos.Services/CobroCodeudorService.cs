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
    public class CobroCodeudorService
    {

        private CobroCodeudorBusiness BOCobroCodeudor;
        private ExcepcionBusiness BOExcepcion;

        public CobroCodeudorService()
        {
            BOCobroCodeudor = new CobroCodeudorBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "180108"; } }

        public CobroCodeudor CrearCobroCodeudor(CobroCodeudor pCobroCodeudor, Usuario pusuario)
        {
            try
            {
                pCobroCodeudor = BOCobroCodeudor.CrearCobroCodeudor(pCobroCodeudor, pusuario);
                return pCobroCodeudor;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CobroCodeudorService", "CrearCobroCodeudor", ex);
                return null;
            }
        }


        public CobroCodeudor ModificarCobroCodeudor(CobroCodeudor pCobroCodeudor, Usuario pusuario)
        {
            try
            {
                pCobroCodeudor = BOCobroCodeudor.ModificarCobroCodeudor(pCobroCodeudor, pusuario);
                return pCobroCodeudor;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CobroCodeudorService", "ModificarCobroCodeudor", ex);
                return null;
            }
        }


        public void EliminarCobroCodeudor(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOCobroCodeudor.EliminarCobroCodeudor(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CobroCodeudorService", "EliminarCobroCodeudor", ex);
            }
        }


        public CobroCodeudor ConsultarCobroCodeudor(Int64 pId, Usuario pusuario)
        {
            try
            {
                return BOCobroCodeudor.ConsultarCobroCodeudor(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CobroCodeudorService", "ConsultarCobroCodeudor", ex);
                return null;
            }
        }


        public List<CobroCodeudor> ConsultarCodeudoresDeUnCredito(Int64 numeroRadicacion, Usuario pusuario)
        {
            try
            {             
                return BOCobroCodeudor.ConsultarCodeudoresDeUnCredito(numeroRadicacion, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CobroCodeudorService", "ConsultarCodeudoresDeUnCredito", ex);
                return null;
            }
        }


        public bool ConsultarSiCreditoTieneCodeudor(Int64 numeroRadicacion, Usuario pusuario)
        {
            try
            {
                return BOCobroCodeudor.ConsultarSiCreditoTieneCodeudor(numeroRadicacion, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CobroCodeudorService", "ConsultarSiCreditoTieneCodeudor", ex);
                return false;
            }
        }


        public List<EmpresaRecaudo> ListarEmpresaRecaudo(long cod_persona, Usuario pusuario)
        {
            try
            {
                return BOCobroCodeudor.ListarEmpresaRecaudo(cod_persona, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CobroCodeudorService", "ListarEmpresaRecaudo", ex);
                return null;
            }
        }


        public List<CobroCodeudor> ListarCobroCodeudor(string filtro, Usuario pusuario)
        {
            try
            {
                return BOCobroCodeudor.ListarCobroCodeudor(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CobroCodeudorService", "ListarCobroCodeudor", ex);
                return null;
            }
        }


    }
}