using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Contabilidad.Entities;
using Xpinn.Contabilidad.Business;


namespace Xpinn.Contabilidad.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ParametrosCtaServiciosService 
    {
        private ParametroCtaServiciosBusiness BOpar_cue_linser;
        private ExcepcionBusiness BOExcepcion;

        public ParametrosCtaServiciosService()
        {
            BOpar_cue_linser = new ParametroCtaServiciosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "31009"; } }

        public par_cue_linser Crearpar_cue_linser(par_cue_linser ppar_cue_linser, Usuario pusuario)
        {
            try
            {
                ppar_cue_linser = BOpar_cue_linser.Crearpar_cue_linser(ppar_cue_linser, pusuario);
                return ppar_cue_linser;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("par_cue_linserService", "Crearpar_cue_linser", ex);
                return null;
            }
        }


        public par_cue_linser Modificarpar_cue_linser(par_cue_linser ppar_cue_linser, Usuario pusuario)
        {
            try
            {
                ppar_cue_linser = BOpar_cue_linser.Modificarpar_cue_linser(ppar_cue_linser, pusuario);
                return ppar_cue_linser;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("par_cue_linserService", "Modificarpar_cue_linser", ex);
                return null;
            }
        }


        public void Eliminarpar_cue_linser(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOpar_cue_linser.Eliminarpar_cue_linser(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("par_cue_linserService", "Eliminarpar_cue_linser", ex);
            }
        }


        public par_cue_linser Consultarpar_cue_linser(Int64 pId, Usuario pusuario)
        {
            try
            {
                par_cue_linser par_cue_linser = new par_cue_linser();
                par_cue_linser = BOpar_cue_linser.Consultarpar_cue_linser(pId, pusuario);
                return par_cue_linser;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("par_cue_linserService", "Consultarpar_cue_linser", ex);
                return null;
            }
        }


        public List<par_cue_linser> Listarpar_cue_linser(par_cue_linser ppar_cue_linser, Usuario pusuario)
        {
            try
            {
                return BOpar_cue_linser.Listarpar_cue_linser(ppar_cue_linser, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("par_cue_linserService", "Listarpar_cue_linser", ex);
                return null;
            }
        }
    }
}
