using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Nomina.Data;
using Xpinn.Nomina.Entities;
 
namespace Xpinn.Nomina.Business
{

    public class RangosRetencionBussiness : GlobalBusiness
    {

        private RangosRetencionData DARangosRetencion;

        public RangosRetencionBussiness()
        {
            DARangosRetencion= new RangosRetencionData();
        }

        public RangosRetencion CrearRangosRetencion(RangosRetencion pRangosRetencion, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pRangosRetencion = DARangosRetencion.CrearRangosRetencion(pRangosRetencion, pusuario);

                    ts.Complete();

                }

                return pRangosRetencion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RangosRetencionBussiness", "CrearRangosRetencion", ex);
                return null;
            }
        }


        public RangosRetencion ModificarRangosRetencion(RangosRetencion pRangosRetencion, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pRangosRetencion = DARangosRetencion.ModificarRangosRetencion(pRangosRetencion, pusuario);

                    ts.Complete();

                }

                return pRangosRetencion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RangosRetencionBussiness", "ModificarRangosRetencion", ex);
                return null;
            }
        }



        public RangosRetencion ConsultarRangosRetencion(Int64 pId, Usuario pusuario)
        {
            try
            {
                RangosRetencion RangosRetencion = new RangosRetencion();
                RangosRetencion = DARangosRetencion.ConsultarRangosRetencion(pId, pusuario);
                return RangosRetencion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RangosRetencionBussiness", "ConsultarRangosRetencion", ex);
                return null;
            }
        }

        
        public List<RangosRetencion> ListarRangosRetencion(string pid, Usuario pusuario)
        {
            try
            {
                return DARangosRetencion.ListarRangosRetencion(pid, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RangosRetencionBussiness", "ListarRangosRetencion", ex);
                return null;
            }
        }


    }
}
