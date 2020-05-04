using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Contabilidad.Data;
using Xpinn.Contabilidad.Entities;


namespace Xpinn.Contabilidad.Business
{
    public class ProcesoContableBusiness : GlobalBusiness
    {
        private ProcesoContableData DAProceso;        

        /// <summary>
        /// Constructor del objeto de negocio para Usuario
        /// </summary>
        public ProcesoContableBusiness()
        {
            DAProceso = new ProcesoContableData();
        }

        /// <summary>
        /// Método apra crear el proceso contable
        /// </summary>
        /// <param name="pProceso"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public ProcesoContable CrearProcesoContable(ProcesoContable pProceso, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pProceso = DAProceso.CrearProcesoContable(pProceso, vUsuario);

                    ts.Complete();
                }

                return pProceso;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoContableBusiness", "CrearProcesoContable", ex);
                return null;
            }
        }

        public ProcesoContable ModificarProcesoContable(ProcesoContable pProceso, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pProceso = DAProceso.ModificarProcesoContable(pProceso, vUsuario);

                    ts.Complete();
                }

                return pProceso;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoContableBusiness", "ModificarProcesoContable", ex);
                return null;
            }
        }

        public List<ProcesoContable> ListarProcesoContable(ProcesoContable pProcesoContable, Usuario vUsuario)
        {
            try
            {
                return DAProceso.ListarProcesoContable(pProcesoContable, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("conceptoBusiness", "ListarProcesoContable", ex);
                return null;
            }
        }

        public ProcesoContable ConsultarProcesoContable(Int64 pId, Usuario vUsuario)
        {
            try
            {
                ProcesoContable procesoContable = new ProcesoContable();

                procesoContable = DAProceso.ConsultarProcesoContable(pId, vUsuario);

                return procesoContable;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("conceptoBusiness", "ConsultarProcesoContable", ex);
                return null;
            }
        }

        public ProcesoContable ConsultarProcesoContableOperacion(Int64 pId, Usuario vUsuario)
        {
            try
            {
                ProcesoContable procesoContable = new ProcesoContable();

                procesoContable = DAProceso.ConsultarProcesoContableOperacion(pId, vUsuario);

                return procesoContable;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("conceptoBusiness", "ConsultarProcesoContableOperacion", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return DAProceso.ObtenerSiguienteCodigo(pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("conceptoBusiness", "ObtenerSiguienteCodigo", ex);
                return 0;
            }
        }

    }
}
