using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Integracion.Entities;
using Xpinn.Integracion.Data;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Xpinn.Integracion.Business
{
    public class IntegracionBusiness : GlobalData
    {
        private IntegracionData BOIntegracionData;
        private Xpinn.Integracion.Entities.Auth token;

        public IntegracionBusiness()
        {
            BOIntegracionData = new IntegracionData();
        }

        public Entities.Integracion consultarConvenioIntegracion(int id, Usuario pUsuario)
        {
            try
            {
                Entities.Integracion entidad = BOIntegracionData.ObtenerIntegracion(id,pUsuario);

                if(entidad != null)
                {                   
                    return entidad;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AuthBusiness", "consultarConvenioIntegracion", ex);
                return null;
            }
        }
    }
}


