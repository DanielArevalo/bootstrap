using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Servicios.Data;
using Xpinn.Servicios.Entities;

namespace Xpinn.Servicios.Business
{

    public class SolicitudCredServRecogidosBusiness : GlobalBusiness
    {

        private SolicitudCredServRecogidosData DASolicitudCredServRecogidos;

        public SolicitudCredServRecogidosBusiness()
        {
            DASolicitudCredServRecogidos = new SolicitudCredServRecogidosData();
        }

        public SolicitudCredServRecogidos CrearSolicitudCredServRecogidos(SolicitudCredServRecogidos pSolicitudCredServRecogidos, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pSolicitudCredServRecogidos = DASolicitudCredServRecogidos.CrearSolicitudCredServRecogidos(pSolicitudCredServRecogidos, pusuario);

                    ts.Complete();

                }

                return pSolicitudCredServRecogidos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCredServRecogidosBusiness", "CrearSolicitudCredServRecogidos", ex);
                return null;
            }
        }


        public SolicitudCredServRecogidos ModificarSolicitudCredServRecogidos(SolicitudCredServRecogidos pSolicitudCredServRecogidos, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pSolicitudCredServRecogidos = DASolicitudCredServRecogidos.ModificarSolicitudCredServRecogidos(pSolicitudCredServRecogidos, pusuario);

                    ts.Complete();

                }

                return pSolicitudCredServRecogidos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCredServRecogidosBusiness", "ModificarSolicitudCredServRecogidos", ex);
                return null;
            }
        }


        public void EliminarSolicitudCredServRecogidos(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DASolicitudCredServRecogidos.EliminarSolicitudCredServRecogidos(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCredServRecogidosBusiness", "EliminarSolicitudCredServRecogidos", ex);
            }
        }


        public SolicitudCredServRecogidos ConsultarSolicitudCredServRecogidos(Int64 pId, Usuario pusuario)
        {
            try
            {
                SolicitudCredServRecogidos SolicitudCredServRecogidos = new SolicitudCredServRecogidos();
                SolicitudCredServRecogidos = DASolicitudCredServRecogidos.ConsultarSolicitudCredServRecogidos(pId, pusuario);
                return SolicitudCredServRecogidos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCredServRecogidosBusiness", "ConsultarSolicitudCredServRecogidos", ex);
                return null;
            }
        }


        public List<SolicitudCredServRecogidos> ListarSolicitudCredServRecogidos(long numeroCredito, Usuario pusuario)
        {
            try
            {
                return DASolicitudCredServRecogidos.ListarSolicitudCredServRecogidos(numeroCredito, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCredServRecogidosBusiness", "ListarSolicitudCredServRecogidos", ex);
                return null;
            }
        }

        public List<SolicitudCredServRecogidos> ListarSolicitudCredServRecogidosActualizado(long numeroCredito, Usuario usuario)
        {
            try
            {
                return DASolicitudCredServRecogidos.ListarSolicitudCredServRecogidosActualizado(numeroCredito, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCredServRecogidosBusiness", "ListarSolicitudCredServRecogidosActualizado", ex);
                return null;
            }
        }
    }
}