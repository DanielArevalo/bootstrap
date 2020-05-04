using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Business;
using System.ServiceModel;


namespace Xpinn.Aportes.Services
{

    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class Par_Cue_LinApoervices
    {
        private ParametrosAporteBusiness BOActividad;
        private ExcepcionBusiness BOExcepcion;

        public string CodigoProgramaCargo { get { return "20201"; } }
        public string CodigoProgramaEstaCiv { get { return "20203"; } }
        public string CodigoProgramaParent { get { return "20204"; } }
        public string CodigoProgramaTipoCont { get { return "20205"; } }
        public string CodigoProgramaTipoIden { get { return "20206"; } }
        public string CodigoProgramaActivi { get { return "20207"; } }
        public string CodigoProgramaNivEsco { get { return "20208"; } }
        public string CodigoProgramaTipoDoc { get { return "20210"; } }

        public Par_Cue_LinApoervices()
        {
            BOActividad = new ParametrosAporteBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }


        public ParametrosAporte CrearPar_Cue_LinApo(ParametrosAporte pParametro, Usuario vUsuario, int opcion)
        {
            try
            {
                return BOActividad.CrearPar_Cue_LinApo(pParametro, vUsuario, opcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinApoervices", "CrearPar_Cue_LinApo", ex);
                return null;
            }
        }

        
        public List<ParametrosAporte> ListarParametrosAporte(string filtro, string orden, Usuario vUsuario)
        {
            try
            {
                return BOActividad.ListarParametrosAporte(filtro, orden, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinApoervices", "ListarParametrosAporte", ex);
                return null;
            }
        }


        public void EliminarParametroAporte(string pId, int opcion, Usuario vUsuario)
        {
            try
            {
                BOActividad.EliminarParametroAporte(pId,opcion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinApoervices", "EliminarParametroAporte", ex);
            }
        }

        public Int32 ObtenerSiguienteCodigo(int opcion, Usuario pUsuario)
        {
            try
            {
                return BOActividad.ObtenerSiguienteCodigo(opcion, pUsuario);
            }
            catch
            {
                return 1;
            }
        }


        public ParametrosAporte ConsultarParametrosAporte(string pId, int opcion, Usuario vUsuario)
        {
            try
            {
                return BOActividad.ConsultarParametrosAporte(pId, opcion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinApoervices", "ConsultarParametrosAporte", ex);
                return null;
            }
        }


    }
}
