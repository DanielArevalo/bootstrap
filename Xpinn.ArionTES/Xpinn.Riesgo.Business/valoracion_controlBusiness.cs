using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Riesgo.Data;
using Xpinn.Riesgo.Entities;

namespace Xpinn.Riesgo.Data
{

    public class valoracion_controlBusiness : GlobalBusiness
    {

        private valoracion_controlRiesgoData BOvaloracion_control;

        public valoracion_controlBusiness()
        {
            BOvaloracion_control = new valoracion_controlRiesgoData();
        }

        public valoracion_control Crearvaloracion_control(valoracion_control pvaloracion_control, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pvaloracion_control = BOvaloracion_control.Crearvaloracion_control(pvaloracion_control, vUsuario);
                    ts.Complete();
                    return pvaloracion_control;
                }

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("valoracion_controlBusiness", "Crearvaloracion_control", ex);
                return null;
            }
        }


        public valoracion_control Modificarvaloracion_control(valoracion_control pvaloracion_control, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pvaloracion_control = BOvaloracion_control.Modificarvaloracion_control(pvaloracion_control, vUsuario);
                    ts.Complete();
                    return pvaloracion_control;
                }

                
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("valoracion_controlBusiness", "Modificarvaloracion_control", ex);
                return null;
            }
        }


        public void Eliminarvaloracion_control(valoracion_control pvaloracion_control, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    BOvaloracion_control.Eliminarvaloracion_control(pvaloracion_control, vUsuario);
                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("valoracion_controlBusiness", "Eliminarvaloracion_control", ex);
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
                BOExcepcion.Throw("valoracion_controlBusiness", "Consultarvaloracion_control", ex);
                return null;
            }
        }


        public List<valoracion_control> Listarvaloracion_control(valoracion_control pvaloracion_control, Usuario usuario)
        {
            try
            {
                return BOvaloracion_control.Listarvaloracion_control(pvaloracion_control, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("valoracion_controlBusiness", "Listarvaloracion_control", ex);
                return null;
            }
        }


    }
}



