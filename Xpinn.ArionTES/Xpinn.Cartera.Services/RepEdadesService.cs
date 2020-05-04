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
using Xpinn.Comun.Entities;

namespace Xpinn.Cartera.Services
{
    /// <summary>
    /// Servicio para Cajero
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class RepEdadesService
    {
        private RepEdadesBusiness BORepEdades;
        private RepEdadesBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para CierreHistorio
        /// </summary>
        public RepEdadesService()
        {
            BORepEdades = new RepEdadesBusiness();
            BOExcepcion = new RepEdadesBusiness();
        }

        public string CodigoPrograma { get { return "60201"; } }

        public List<Cierea> ListarFechaCierre(Usuario pUsuario)
        {
            try
            {
                return BORepEdades.ListarFechaCierre(pUsuario);
            }
            catch 
            {
                return null;
            }
        }

        public List<EdadMora> ListarRangos(Usuario pUsuario)
        {
            try
            {
                return BORepEdades.ListarRangos(pUsuario);
            }
            catch
            {
                return null;
            }
        }

        public List<RepEdades> ListarCredito(DateTime pFecha, Usuario pUsuario, String sfiltro)
        {
            try
            {
                return BORepEdades.ListarCredito(pFecha, pUsuario, sfiltro);
            }
            catch
            { 
                return null; 
            }
        }

    }

}
