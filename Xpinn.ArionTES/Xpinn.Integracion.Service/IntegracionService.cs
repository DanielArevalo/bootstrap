using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Integracion.Business;
using Xpinn.Integracion.Entities;
using Xpinn.Util;
using System.Data;
using System.ServiceModel;

namespace Xpinn.Integracion.Services
{
    public class IntegracionService
    {

        private IntegracionBusiness BOIntegracion;
        private ExcepcionBusiness BOExcepcion;

        public IntegracionService()
        {
            BOIntegracion = new IntegracionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }              
       
        public Entities.Integracion consultarConvenioIntegracion(int id, Usuario pUsuario)
        {
            try
            {
                return BOIntegracion.consultarConvenioIntegracion(id, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AuthBusiness", "consultarConvenioIntegracion", ex);
                return null;
            }
        }

    }
}