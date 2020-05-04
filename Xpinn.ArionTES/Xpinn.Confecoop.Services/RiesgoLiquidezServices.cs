using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Confecoop.Entities;
using Xpinn.Confecoop.Business;
using Xpinn.Util;

namespace Xpinn.Confecoop.Services
{
    public class RiesgoLiquidezServices
    {
        RiesgoLiquidezBusiness BORiesgo;
        private ExcepcionBusiness BOExcepcion;

        public RiesgoLiquidezServices()
        {
            BORiesgo = new RiesgoLiquidezBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoProgramaRiesgoLiquidez { get { return "170418"; } }

        public Boolean CrearRiesgoLiquidez(List<RiesgoLiquidez> lstRiesgoLiquidez, ref string pError, Usuario vUsuario)
        {
            return BORiesgo.CrearRiesgoLiquidez(lstRiesgoLiquidez, ref pError, vUsuario);
        }


        public List<RiesgoLiquidez> ListarRiesgoLiquidez(DateTime pFechaCorte, bool pNiif, Usuario vUsuario)
        {
            return BORiesgo.ListarRiesgoLiquidez(pFechaCorte, pNiif, vUsuario);
        }

        public List<RiesgoLiquidez> ListarProyeccionRiesgoLiquidez(RiesgoLiquidez riesgo, TipoProyeccionRiesgoLiquidez tipoProyeccion, Usuario usuario)
        {
            return BORiesgo.ListarProyeccionRiesgoLiquidez(riesgo, tipoProyeccion, usuario);
        }
    }
}
