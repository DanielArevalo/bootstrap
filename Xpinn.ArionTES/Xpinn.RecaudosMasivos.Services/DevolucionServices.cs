using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Tesoreria.Business;
using Xpinn.Tesoreria.Entities;
using Xpinn.Caja.Entities;
using System.Web;
using System.Web.UI.WebControls;


namespace Xpinn.Tesoreria.Services
{
    public class DevolucionServices
    {
        private DevolucionBusiness BODevolucion;
        private ExcepcionBusiness BOExcepcion;


        public DevolucionServices()
        {
            BODevolucion = new DevolucionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "180302"; } }


        public Devolucion Crear_Mod_Devolucion(Devolucion pDevol, Usuario vUsuario, int opcion)
        {
            try
            {
                return BODevolucion.Crear_Mod_Devolucion(pDevol, vUsuario, opcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DevolucionServices", "Crear_Mod_Devolucion", ex);
                return null;
            }
        }


        public Int64 EliminarDevolucion(Int64 pId, decimal pValor, Usuario pUsuario)
        {
            try
            {
                return BODevolucion.EliminarDevolucion(pId, pValor, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DevolucionServices", "EliminarDevolucion", ex);
                return 0;
            }
        }


        public List<Devolucion> ListarDevolucion(Devolucion pDevolucion, DateTime pFecha, Usuario vUsuario, string filtro)
        {
            try
            {
                return BODevolucion.ListarDevolucion(pDevolucion, pFecha, vUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DevolucionServices", "ListarDevolucion", ex);
                return null;
            }
        }


        public Devolucion ConsultarDevolucion(int pId, Usuario vUsuario)
        {
            try
            {
                return BODevolucion.ConsultarDevolucion(pId, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DevolucionServices", "ConsultarDevolucion", ex);
                return null;
            }
        }


        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return BODevolucion.ObtenerSiguienteCodigo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DevolucionServices", "ObtenerSiguienteCodigo", ex);
                return 0;
            }
        }


        public Devolucion ConsultarDetalleRecaudo(int pId, Usuario vUsuario)
        {
            try
            {
                return BODevolucion.ConsultarDetalleRecaudo(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DevolucionServices", "ConsultarDetalleRecaudo", ex);
                return null;
            }
        }

        public TransaccionCaja AplicarDevoluciones(TransaccionCaja pTransaccionCaja, GridView gvTransacciones, List<Devolucion> lstDevolucion, string pObservacion, Usuario pUsuario, ref string Error)
        {
            try
            {
                PersonaTransaccion perTran = new PersonaTransaccion();
                return BODevolucion.AplicarDevoluciones(pTransaccionCaja, perTran, gvTransacciones, lstDevolucion, pObservacion, pUsuario, ref Error);
            }
            catch (Exception ex)
            {
                Error = Error + ex.Message;
                return null;
            }
        }

        public TransaccionCaja AplicarDevoluciones(TransaccionCaja pTransaccionCaja, PersonaTransaccion perTran, GridView gvTransacciones, List<Devolucion> lstDevolucion, string pObservacion, Usuario pUsuario, ref string Error)
        {
            try
            {
                return BODevolucion.AplicarDevoluciones(pTransaccionCaja, perTran,  gvTransacciones, lstDevolucion, pObservacion, pUsuario, ref Error);
            }
            catch (Exception ex)
            {
                Error = Error + ex.Message;
                return null;
            }
        }

        public List<Devolucion> ConsultarDevolucionDetalle(int pId, Usuario vUsuario)
        {
            try
            {
                return BODevolucion.ConsultarDevolucionDetalle(pId, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DevolucionServices", "ConsultarDevolucionDetalle", ex);
                return null;
            }
        }


    }
}
