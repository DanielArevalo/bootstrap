using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Comun.Entities;
using Xpinn.Comun.Business;
using System.Web;

namespace Xpinn.Comun.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class TipoIdentificacionService
    {
        private TipoIdentificacionBusiness BOTipoIdentificacion;
        private TipoIdentificacionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para CierreHistorio
        /// </summary>
        public TipoIdentificacionService()
        {
            BOTipoIdentificacion = new TipoIdentificacionBusiness();
            BOExcepcion = new TipoIdentificacionBusiness();
        }

        public List<TipoIdentificacion> ListarTipoIdentificacion(TipoIdentificacion pTipo, Usuario pUsuario)
        {
            try
            {
                return BOTipoIdentificacion.ListarTipoIdentificacion(pTipo, pUsuario);
            }
            catch
            {
                return null;
            }
        }

        public TipoIdentificacion ConsultarTipoIdentificacion(Int64 pTipo, Usuario pUsuario)
        {
            try
            {
                return BOTipoIdentificacion.ConsultarTipoIdentificacion(pTipo, pUsuario);
            }
            catch
            {
                return null;
            }
        }

        public TipoIdentificacion ConsultarTipoIdentificacion(String pTipo, Usuario pUsuario)
        {
            try
            {
                return BOTipoIdentificacion.ConsultarTipoIdentificacion(pTipo, pUsuario);
            }
            catch
            {
                return null;
            }
        }

    }
}
