using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Business
{

    /// <summary>
    /// Objeto de negocio para TomadorPoliza
    /// </summary>
    /// 
    public class TomadorPolizaBusiness : GlobalData
    {

        private TomadorPolizasData DATomadorPoliza;


        /// <summary>
        /// Constructor del objeto de negocio para PolizasSeguros
        /// </summary>
        public TomadorPolizaBusiness()
        {
            DATomadorPoliza = new TomadorPolizasData();
        }


        /// <summary>
        /// Obtiene el dato del Tomador de la poliza
        /// </summary>
        /// <param name="pId">identificador del TomadorPoliza</param>
        /// <returns>Familiares consultada</returns>
        public TomadorPoliza ConsultarTomadorPoliza( Usuario pUsuario)
        {
            try
            {
                return DATomadorPoliza.ConsultarDatosTomadorPoliza(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TomadorPolizaBusiness", "ConsultarTomadorPoliza", ex);
                return null;
            }
        }

    }
}