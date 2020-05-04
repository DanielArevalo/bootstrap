using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.NIIF.Business;
using Xpinn.NIIF.Entities;

namespace Xpinn.NIIF.Services
{


    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class TipoActivoNIFServices
    {
        private TipoActivoNIFBussines BOTipo;
        private ExcepcionBusiness BOExcepcion;

        
        public TipoActivoNIFServices()
        {
            BOTipo = new TipoActivoNIFBussines();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "210205"; } }


        public TipoActivoNIF CrearTipoActivoNIF(TipoActivoNIF pActivo, Usuario vUsuario, int opcion)
        {
            try
            {
                return BOTipo.CrearTipoActivoNIF(pActivo, vUsuario,opcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoActivoNIFServices", "CrearTipoActivoNIF", ex);
                return null;
            }
        }



        public void EliminarTipoActivo(Int32 pId, Usuario pUsuario)
        {
            try
            {
                BOTipo.EliminarTipoActivo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoActivoNIFServices", "EliminarTipoActivo", ex);
            }
        }


        public TipoActivoNIF ConsultarTipoActivo(Int32 pId, Usuario pUsuario)
        {
            try
            {
                return BOTipo.ConsultarTipoActivo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoActivoNIFServices", "ConsultarTipoActivo", ex);
                return null;
            }
        }



        public List<TipoActivoNIF> ListarTipoActivo(String filtro, Usuario pUsuario)
        {
            try
            {
                return BOTipo.ListarTipoActivo(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoActivoNIFServices", "ListarTipoActivo", ex);
                return null;
            }
        }


    }
}