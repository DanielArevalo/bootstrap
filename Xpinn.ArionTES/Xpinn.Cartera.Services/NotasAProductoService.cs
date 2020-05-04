using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Cartera.Entities;
using Xpinn.Cartera.Business;
using System.Web;
using Xpinn.Util;
using Xpinn.Caja.Entities;
using System.Web.UI.WebControls;
using Xpinn.Tesoreria.Entities;


namespace Xpinn.Cartera.Services
{
    /// <summary>
    /// Servicio para Castigo
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class NotasAProductoService
    {
        private NotasAProductoBusiness BONotas;
        private CastigoBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para CierreHistorio
        /// </summary>
        public NotasAProductoService()
        {
            BONotas = new NotasAProductoBusiness();
            BOExcepcion = new CastigoBusiness();
        }

        public string CodigoPrograma { get { return "60113"; } }


        public TransaccionCaja AplicarNotasAProductos(Devolucion pDevol, Boolean GeneraDevolucion, TransaccionCaja pOperacion, GridView gvTransacciones, Usuario pUsuario,Boolean Pendientes, ref string Error)
        {
            try
            {
                return BONotas.AplicarNotasAProductos(pDevol, GeneraDevolucion, pOperacion, gvTransacciones, pUsuario,Pendientes, ref Error);
            }
            catch 
            {
                return null;
            }
        }

    }

}
