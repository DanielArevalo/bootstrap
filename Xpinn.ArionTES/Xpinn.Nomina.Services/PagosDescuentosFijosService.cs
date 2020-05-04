using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Business;

namespace Xpinn.Nomina.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class PagosDescuentosFijosService
    {

        private PagosDescuentosFijosBusiness BOPagosDescuentosFijos;
        private ExcepcionBusiness BOExcepcion;

        public PagosDescuentosFijosService()
        {
            BOPagosDescuentosFijos = new PagosDescuentosFijosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "250303"; } }

        public PagosDescuentosFijos CrearPagosDescuentosFijos(PagosDescuentosFijos pPagosDescuentosFijos, Usuario pusuario)
        {
            try
            {
                pPagosDescuentosFijos = BOPagosDescuentosFijos.CrearPagosDescuentosFijos(pPagosDescuentosFijos, pusuario);
                return pPagosDescuentosFijos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PagosDescuentosFijosService", "CrearPagosDescuentosFijos", ex);
                return null;
            }
        }


        public PagosDescuentosFijos ModificarPagosDescuentosFijos(PagosDescuentosFijos pPagosDescuentosFijos, Usuario pusuario)
        {
            try
            {
                pPagosDescuentosFijos = BOPagosDescuentosFijos.ModificarPagosDescuentosFijos(pPagosDescuentosFijos, pusuario);
                return pPagosDescuentosFijos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PagosDescuentosFijosService", "ModificarPagosDescuentosFijos", ex);
                return null;
            }
        }


        public void EliminarPagosDescuentosFijos(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOPagosDescuentosFijos.EliminarPagosDescuentosFijos(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PagosDescuentosFijosService", "EliminarPagosDescuentosFijos", ex);
            }
        }


        public PagosDescuentosFijos ConsultarPagosDescuentosFijos(Int64 pId, Usuario pusuario)
        {
            try
            {
                PagosDescuentosFijos PagosDescuentosFijos = new PagosDescuentosFijos();
                PagosDescuentosFijos = BOPagosDescuentosFijos.ConsultarPagosDescuentosFijos(pId, pusuario);
                return PagosDescuentosFijos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PagosDescuentosFijosService", "ConsultarPagosDescuentosFijos", ex);
                return null;
            }
        }


        public List<PagosDescuentosFijos> ListarPagosDescuentosFijos(string filtro, Usuario pusuario)
        {
            try
            {
                return BOPagosDescuentosFijos.ListarPagosDescuentosFijos(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PagosDescuentosFijosService", "ListarPagosDescuentosFijos", ex);
                return null;
            }
        }

        public List<PagosDescuentosFijos> ListarConceptosNomina(string filtro, Usuario pusuario)
        {
            try
            {
                return BOPagosDescuentosFijos.ListarConceptosNomina(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PagosDescuentosFijosService", "ListarConceptosNomina", ex);
                return null;
            }
        }

        public PagosDescuentosFijos ConsultarTipoConceptosNomina(string filtro, Usuario pusuario)
        {
            try
            {
                return BOPagosDescuentosFijos.ConsultarTipoConceptosNomina(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PagosDescuentosFijosService", "ConsultarTipoConceptosNomina", ex);
                return null;
            }
        }


        public List<PagosDescuentosFijos> ListarProveedorDescuentos(Usuario pusuario)
        {
            try
            {
                return BOPagosDescuentosFijos.ListarProveedorDescuentos( pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PagosDescuentosFijosService", "ListarProveedorDescuentos", ex);
                return null;
            }
        }


        public List<PagosDescuentosFijos> ListarDescuentosFijosReporte(string filtro, Usuario pusuario)
        {
            try
            {
                return BOPagosDescuentosFijos.ListarDescuentosFijosReporte(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PagosDescuentosFijosService", "ListarDescuentosFijosReporte", ex);
                return null;
            }
        }

    }
}