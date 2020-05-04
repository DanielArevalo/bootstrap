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


namespace Xpinn.Cartera.Services
{
    /// <summary>
    /// Servicio para Cajero
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ProyeccionCarteraService
    {
        private ProyeccionCarteraBusiness BOProyeccionCartera;
        private ProyeccionCarteraBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para CierreHistorio
        /// </summary>
        public ProyeccionCarteraService()
        {
            BOProyeccionCartera = new ProyeccionCarteraBusiness();
            BOExcepcion = new ProyeccionCarteraBusiness();
        }

        public string CodigoPrograma { get { return "60204"; } }


        /// <summary>
        /// Método para traer datos de la proyección de cartera
        /// </summary>
        /// <returns></returns>
        public List<ProyeccionCartera> listarProyeccionCartera(DateTime pfecha, DateTime pfechafinal, Usuario pUsuario)
        {
            try
            {
                return BOProyeccionCartera.ListarProyeccionCartera(pfecha, pfechafinal, pUsuario);
            }
            catch
            { 
                return null; 
            }
        }

        public Int64 ValidarProyeccionCartera(DateTime pfecha, Usuario pUsuario)
        {
            try
            {
                return BOProyeccionCartera.ValidarProyeccionCartera(pfecha, pUsuario);
            }
            catch
            {
                return 0;
            }
        }

        public void Proyeccion(DateTime pfecha, Usuario pUsuario, ref string serror)
        {
            try
            {
                BOProyeccionCartera.Proyeccion(pfecha, pUsuario, ref serror);
            }
            catch
            {
                return;
            }
        }

    }
}

