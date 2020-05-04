using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Aportes.Business;
using Xpinn.Aportes.Entities;
//using System.Web.UI.WebControls;

namespace Xpinn.Aportes.Services
{
    public class OperacionApoServices
    {
        private OperacionApoBusiness BOOperacionBusiness;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Acceso
        /// </summary>
        public OperacionApoServices()
        {
            BOOperacionBusiness = new OperacionApoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "40801"; } }
       

       
        /// <summary>
        /// Contabilizar las operaciones
        /// </summary>
        /// <param name="poperacion"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<OperacionApo> ContabilizarOperacion(List<OperacionApo> pLstOperacion, ref string pError, Usuario pUsuario)
        {
            try
            {
                return BOOperacionBusiness.ContabilizarOperacion(pLstOperacion, ref pError, pUsuario);
            }
            catch
            {
                return null;
            }
        }

    

    }
}
