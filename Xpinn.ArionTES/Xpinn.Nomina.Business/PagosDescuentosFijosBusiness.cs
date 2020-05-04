using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Nomina.Data;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Business
{

    public class PagosDescuentosFijosBusiness : GlobalBusiness
    {

        private PagosDescuentosFijosData DAPagosDescuentosFijos;

        public PagosDescuentosFijosBusiness()
        {
            DAPagosDescuentosFijos = new PagosDescuentosFijosData();
        }

        public PagosDescuentosFijos CrearPagosDescuentosFijos(PagosDescuentosFijos pPagosDescuentosFijos, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPagosDescuentosFijos = DAPagosDescuentosFijos.CrearPagosDescuentosFijos(pPagosDescuentosFijos, pusuario);

                    ts.Complete();

                }

                return pPagosDescuentosFijos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PagosDescuentosFijosBusiness", "CrearPagosDescuentosFijos", ex);
                return null;
            }
        }


        public PagosDescuentosFijos ModificarPagosDescuentosFijos(PagosDescuentosFijos pPagosDescuentosFijos, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPagosDescuentosFijos = DAPagosDescuentosFijos.ModificarPagosDescuentosFijos(pPagosDescuentosFijos, pusuario);

                    ts.Complete();

                }

                return pPagosDescuentosFijos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PagosDescuentosFijosBusiness", "ModificarPagosDescuentosFijos", ex);
                return null;
            }
        }


        public void EliminarPagosDescuentosFijos(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAPagosDescuentosFijos.EliminarPagosDescuentosFijos(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PagosDescuentosFijosBusiness", "EliminarPagosDescuentosFijos", ex);
            }
        }


        public PagosDescuentosFijos ConsultarPagosDescuentosFijos(Int64 pId, Usuario pusuario)
        {
            try
            {
                PagosDescuentosFijos PagosDescuentosFijos = new PagosDescuentosFijos();
                PagosDescuentosFijos = DAPagosDescuentosFijos.ConsultarPagosDescuentosFijos(pId, pusuario);
                return PagosDescuentosFijos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PagosDescuentosFijosBusiness", "ConsultarPagosDescuentosFijos", ex);
                return null;
            }
        }


        public List<PagosDescuentosFijos> ListarPagosDescuentosFijos(string filtro, Usuario pusuario)
        {
            try
            {
                return DAPagosDescuentosFijos.ListarPagosDescuentosFijos(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PagosDescuentosFijosBusiness", "ListarPagosDescuentosFijos", ex);
                return null;
            }
        }

        public List<PagosDescuentosFijos> ListarDescuentosFijosReporte(string filtro, Usuario pusuario)
        {
            try
            {
                return DAPagosDescuentosFijos.ListarDescuentosFijosReporte(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PagosDescuentosFijosBusiness", "ListarDescuentosFijosReporte", ex);
                return null;
            }
        }

        public List<PagosDescuentosFijos> ListarConceptosNomina(string filtro, Usuario pusuario)
        {
            try
            {
                return DAPagosDescuentosFijos.ListarConceptosNomina(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PagosDescuentosFijosBusiness", "ListarConceptosNomina", ex);
                return null;
            }
        }

        public PagosDescuentosFijos ConsultarTipoConceptosNomina(string filtro, Usuario pusuario)
        {
            try
            {
                return DAPagosDescuentosFijos.ConsultarTipoConceptosNomina(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PagosDescuentosFijosBusiness", "ConsultarTipoConceptosNomina", ex);
                return null;
            }
        }


        public List<PagosDescuentosFijos> ListarProveedorDescuentos( Usuario pusuario)
        {
            try
            {
                return DAPagosDescuentosFijos.ListarProveedorDescuentos( pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PagosDescuentosFijosBusiness", "ListarProveedorDescuentos", ex);
                return null;
            }
        }


    }
}
