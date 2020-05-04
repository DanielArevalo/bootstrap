using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Servicios.Entities;
using Xpinn.Servicios.Business;
using Xpinn.Util;

namespace Xpinn.Servicios.Services
{
    public class DesembolsoServiciosServices
    {
        public string CodigoPrograma { get { return "80103"; } }

        DesembolsoServiciosBusiness BODesembolso;
        ExcepcionBusiness BOExcepcion;

        public DesembolsoServiciosServices()
        {
            BODesembolso = new DesembolsoServiciosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }


        public DesembosoServicios CrearTransaccionDesembolso(DesembosoServicios pDesembolso, Xpinn.Tesoreria.Entities.Operacion pOperacion, Usuario vUsuario)
        {
            try
            {
                return BODesembolso.CrearTransaccionDesembolso(pDesembolso,pOperacion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DesembolsoServiciosServices", "CrearTransaccionDesembolso", ex);
                return null;
            }
        }


        public DateTime? ObtenerFechaInicioServicio(DateTime pFechaActual, int pFormaPago, Int64 pCodEmpresa, string pPeriodicidad, Usuario vUsuario)
        {
            try
            {
                return BODesembolso.ObtenerFechaInicioServicio(pFechaActual, pFormaPago, pCodEmpresa, pPeriodicidad, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DesembolsoServiciosBusiness", "ObtenerFechaInicioServicio", ex);
                return null;
            }
        }

    }
}
