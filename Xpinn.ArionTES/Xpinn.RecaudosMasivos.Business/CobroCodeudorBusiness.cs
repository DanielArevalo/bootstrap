using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Business
{

    public class CobroCodeudorBusiness : GlobalBusiness
    {

        private CobroCodeudorData DACobroCodeudor;

        public CobroCodeudorBusiness()
        {
            DACobroCodeudor = new CobroCodeudorData();
        }

        public CobroCodeudor CrearCobroCodeudor(CobroCodeudor pCobroCodeudor, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCobroCodeudor = DACobroCodeudor.CrearCobroCodeudor(pCobroCodeudor, pusuario);

                    ts.Complete();

                }

                return pCobroCodeudor;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CobroCodeudorBusiness", "CrearCobroCodeudor", ex);
                return null;
            }
        }


        public CobroCodeudor ModificarCobroCodeudor(CobroCodeudor pCobroCodeudor, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCobroCodeudor = DACobroCodeudor.ModificarCobroCodeudor(pCobroCodeudor, pusuario);

                    ts.Complete();

                }

                return pCobroCodeudor;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CobroCodeudorBusiness", "ModificarCobroCodeudor", ex);
                return null;
            }
        }


        public void EliminarCobroCodeudor(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DACobroCodeudor.EliminarCobroCodeudor(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CobroCodeudorBusiness", "EliminarCobroCodeudor", ex);
            }
        }


        public CobroCodeudor ConsultarCobroCodeudor(Int64 pId, Usuario pusuario)
        {
            try
            {
                CobroCodeudor CobroCodeudor = new CobroCodeudor();
                CobroCodeudor = DACobroCodeudor.ConsultarCobroCodeudor(pId, pusuario);
                return CobroCodeudor;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CobroCodeudorBusiness", "ConsultarCobroCodeudor", ex);
                return null;
            }
        }

        public List<EmpresaRecaudo> ListarEmpresaRecaudo(long cod_persona, Usuario pusuario)
        {
            try
            {
                return DACobroCodeudor.ListarEmpresaRecaudo(cod_persona, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CobroCodeudorBusiness", "ListarEmpresaRecaudo", ex);
                return null;
            }
        }

        public List<CobroCodeudor> ConsultarCodeudoresDeUnCredito(long numeroRadicacion, Usuario pusuario)
        {
            try
            {
                return DACobroCodeudor.ConsultarCodeudoresDeUnCredito(numeroRadicacion, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CobroCodeudorBusiness", "ConsultarCodeudoresDeUnCredito", ex);
                return null;
            }
        }


        public bool ConsultarSiCreditoTieneCodeudor(long numeroRadicacion, Usuario pusuario)
        {
            try
            {
                return DACobroCodeudor.ConsultarSiCreditoTieneCodeudor(numeroRadicacion, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CobroCodeudorBusiness", "ConsultarSiCreditoTieneCodeudor", ex);
                return false;
            }
        }


        public List<CobroCodeudor> ListarCobroCodeudor(string filtro, Usuario pusuario)
        {
            try
            {
                return DACobroCodeudor.ListarCobroCodeudor(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CobroCodeudorBusiness", "ListarCobroCodeudor", ex);
                return null;
            }
        }


    }
}
