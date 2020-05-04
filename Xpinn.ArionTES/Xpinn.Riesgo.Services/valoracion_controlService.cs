using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Riesgo.Entities;
using Xpinn.Riesgo.Business;
using Xpinn.Riesgo.Data;

namespace Xpinn.Riesgo.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class valoracion_controlService
    {

        private valoracion_controlBusiness BOvaloracion_control;
        private ExcepcionBusiness BOExcepcion;

        public valoracion_controlService()
        {
            BOvaloracion_control = new valoracion_controlBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "270312"; } }

        public valoracion_control Crearvaloracion_control(valoracion_control pvaloracion_control, Usuario pusuario)
        {
            try
            {
                pvaloracion_control = BOvaloracion_control.Crearvaloracion_control(pvaloracion_control, pusuario);
                return pvaloracion_control;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("valoracion_controlService", "Crearvaloracion_control", ex);
                return null;
            }
        }


        public valoracion_control Modificarvaloracion_control(valoracion_control pvaloracion_control, Usuario pusuario)
        {
            try
            {
                pvaloracion_control = BOvaloracion_control.Modificarvaloracion_control(pvaloracion_control, pusuario);
                return pvaloracion_control;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("valoracion_controlService", "Modificarvaloracion_control", ex);
                return null;
            }
        }


        public void Eliminarvaloracion_control(valoracion_control pvaloracion_control, Usuario vUsuario)
        {
            try
            {
                BOvaloracion_control.Eliminarvaloracion_control(pvaloracion_control, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("valoracion_controlService", "Eliminarvaloracion_control", ex);
            }
        }


        public valoracion_control Consultarvaloracion_control(valoracion_control pvaloracion_control, Usuario vUsuario)
        {
            try
            {

                pvaloracion_control = BOvaloracion_control.Consultarvaloracion_control(pvaloracion_control, vUsuario);
                return pvaloracion_control;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("valoracion_controlService", "Consultarvaloracion_control", ex);
                return null;
            }
        }


        public List<valoracion_control> Listarvaloracion_control(valoracion_control pvaloracion_control, Usuario pusuario)
        {
            try
            {
                return BOvaloracion_control.Listarvaloracion_control(pvaloracion_control, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("valoracion_controlService", "Listarvaloracion_control", ex);
                return null;
            }
        }

        
    }
}

