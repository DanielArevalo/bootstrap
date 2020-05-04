using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Riesgo.Entities;
using Xpinn.Riesgo.Business;
using Xpinn.Riesgo.Data;

namespace Xpinn.Riesgo.Service
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class alertasService
    {
        private alertasBusiness BOalertas;
        private ExcepcionBusiness BOExcepcion;

        public alertasService()
        {
            BOalertas = new alertasBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }
        public string CodigoPrograma { get { return "270207"; } }

        public alertas_ries Crearalertas(alertas_ries palertas, Usuario pusuario)
        {
            try
            {
                palertas = BOalertas.Crearalertas(palertas, pusuario);
                return palertas;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("alertasService", "Crearalertas", ex);
                return null;
            }
        }
        public alertas_ries Modificaralertas(alertas_ries palertas, Usuario pusuario)
        {
            try
            {
                palertas = BOalertas.Modificaralertas(palertas, pusuario);

                return palertas;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("alertasService", "Modificaralertas", ex);
                return null;
            }
        }
        public void Eliminaralertas(alertas_ries palertas, Usuario pusuario)
        {
            try
            {
                BOalertas.Eliminaralertas(palertas, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("alertasService", "Eliminaralertas", ex);
            }
        }
       public alertas_ries Consultaralertas(alertas_ries pAlertas, Usuario vUsuario)
        {
            try
            {
                pAlertas = BOalertas.Consultaralertas(pAlertas, vUsuario);
                return pAlertas;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("alertasService", "Consultaralertas", ex);
                return null;
            }
        }
        public List<alertas_ries> Listaralertas(alertas_ries palertas, Usuario pusuario)
        {
            try
            {
                return BOalertas.Listaralertas(palertas, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("alertasService", "Listaralertas", ex);
                return null;
            }
        }
    }
}