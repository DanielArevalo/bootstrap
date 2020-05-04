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
    public class ParametroCtasAportesService
    {
        private ParametroCtasAportesBusiness BOParametro;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para PlanCuentas
        /// </summary>
        public ParametroCtasAportesService()
        {
            BOParametro = new ParametroCtasAportesBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "31004"; } }

        public List<Par_Cue_LinApo> ListarPar_Cue_LinApo(string filtro, Usuario vUsuario)
        {
            try
            {
                return BOParametro.ListarPar_Cue_LinApo(filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinApoService", "ListarPar_Cue_LinApo", ex);
                return null;
            }
        }


        public Par_Cue_LinApo ConsultarPar_Cue_LinApo(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOParametro.ConsultarPar_Cue_LinApo(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinApoService", "ConsultarPar_Cue_LinApo", ex);
                return null;
            }
        }

        public Par_Cue_LinApo CrearParametroAporte(Par_Cue_LinApo pParam, Usuario vUsuario,int opcion)
        {
            try
            {
                return BOParametro.CrearParametroAporte(pParam, vUsuario, opcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinApoService", "CrearParametroAporte", ex);
                return null;
            }
        }


        public void EliminarParametroAporte(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOParametro.EliminarParametroAporte(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinApoService", "EliminarParametroAporte", ex);
            }
        }


    }
}