using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Contabilidad.Business;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class Par_Cue_OtrosService
    {
        private Par_Cue_OtrosBusiness BOParametro;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para PlanCuentas
        /// </summary>
        public Par_Cue_OtrosService()
        {
            BOParametro = new Par_Cue_OtrosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "31006"; } }
        public string codigoProgramaParametro { get { return "31007"; } } 

        public List<Par_Cue_Otros> ListarPar_Cue_Otros(string filtro, Usuario vUsuario)
        {
            try
            {
                return BOParametro.ListarPar_Cue_Otros(filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_OtrosService", "ListarPar_Cue_Otros", ex);
                return null;
            }
        }


        public Par_Cue_Otros ConsultarParametroCtasOtros(Int32 pId, Usuario vUsuario)
        {
            try
            {
                return BOParametro.ConsultarParametroCtasOtros(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_OtrosService", "ConsultarParametroCtasOtros", ex);
                return null;
            }
        }

        public Par_Cue_Otros CrearPar_Cue_Otros(Par_Cue_Otros pParam, Usuario vUsuario, int opcion)
        {
            try
            {
                return BOParametro.CrearPar_Cue_Otros(pParam, vUsuario, opcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_OtrosService", "CrearPar_Cue_Otros", ex);
                return null;
            }
        }


        public void EliminarParametroCtasOtros(Int32 pId, Usuario vUsuario)
        {
            try
            {
                BOParametro.EliminarParametroCtasOtros(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_OtrosService", "EliminarParametroCtasOtros", ex);
            }
        }

        public List<Par_Cue_LinAho> getListParametrosServices(Usuario pusuario, String pfiltro) 
        {
            try
            {
                return BOParametro.getListParametrosBusiness(pusuario, pfiltro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_OtrosService", "EliminarParametroCtasOtros", ex);
                return null;
            }
        }

        public void EliminarParametroServices(Usuario pUsuario, Int64 idcodigo) 
        {
            try
            {
                BOParametro.EliminarParametroBusiness(pUsuario, idcodigo);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_OtrosService", "EliminarParametroCtasOtros", ex);
            }
        }

        public void insertarParametroServices(Usuario pUsuario, Par_Cue_LinAho pEntidadParametro) 
        {
            try
            {
                BOParametro.insertarParametroBusiness(pUsuario, pEntidadParametro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_OtrosService", "insertarParametroServices", ex);
            }
        }

        public Par_Cue_LinAho getParametroByIdServices(Usuario pusuario, Int64 idParametro) 
        {
            try
            {
                return BOParametro.getParametroByIdBusiness(pusuario, idParametro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_OtrosService", "EliminarParametroCtasOtros", ex);
                return null;
            }
        }

        public void updateParametroServices(Usuario pUsuario, Par_Cue_LinAho entidadcrea) 
        {
            try
            {
                BOParametro.updateParametroBusinness(pUsuario, entidadcrea);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_OtrosService", "updateParametroServices", ex);
            }
        }

        public Int64 ObtenerSiguienteCodigo_ParCue_LinAHO(Usuario vUsuario)
        {
            try
            {
                return BOParametro.ObtenerSiguienteCodigo_ParCue_LinAHO(vUsuario);
            }
            catch
            {
                return 1;
            }
        }

    }
}