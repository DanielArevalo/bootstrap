using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Business;
using System.ServiceModel;


namespace Xpinn.FabricaCreditos.Services
{

    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class InformacionAdicionalServices
    {
        private InformacionAdicionalBusiness BOInformacion;
        private ExcepcionBusiness BOExcepcion;



        public string CodigoProgramaParametros { get { return "170204"; } }


        public InformacionAdicionalServices()
        {
            BOInformacion = new InformacionAdicionalBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }
        

        #region PERSONA INFORMACION ADICIONAL

        public InformacionAdicional CrearTipo_InforAdicional(InformacionAdicional pAdicional, Usuario vUsuario)
        {
            try
            {
                return BOInformacion.CrearTipo_InforAdicional(pAdicional, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionAdicionalServices", "CrearTipo_InforAdicional", ex);
                return null;
            }
        }


        public InformacionAdicional ModificarTipo_InforAdicional(InformacionAdicional pAdicional, Usuario vUsuario)
        {
            try
            {
                return BOInformacion.ModificarTipo_InforAdicional(pAdicional, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionAdicionalServices", "ModificarTipo_InforAdicional", ex);
                return null;
            }
        }


        public void EliminarInformacionAdicional(Int64 pIdActividad, Usuario vUsuario)
        {
            try
            {
                BOInformacion.EliminarInformacionAdicional(pIdActividad, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionAdicionalServices", "EliminarInformacionAdicional", ex);               
            }
        }

        
        #endregion



        public List<InformacionAdicional> ListarInformacionAdicional(InformacionAdicional pInformacion, string tipo, Usuario vUsuario)
        {
            try
            {
                return BOInformacion.ListarInformacionAdicional(pInformacion,tipo, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionAdicionalBusiness", "ListarInformacionAdicional", ex);
                return null;
            }
        }


        public List<InformacionAdicional> ListarInformacionAdicionalGeneral(InformacionAdicional pInformacion, Usuario vUsuario)
        {
            try
            {
                return BOInformacion.ListarInformacionAdicionalGeneral(pInformacion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionAdicionalBusiness", "ListarInformacionAdicionalGeneral", ex);
                return null;
            }
        }



        public List<InformacionAdicional> ConsultarInformacionAdicional(InformacionAdicional pInfo, Usuario vUsuario)
        {
            try
            {
                return BOInformacion.ConsultarInformacionAdicional(pInfo, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionAdicionalBusiness", "ConsultarInformacionAdicional", ex);
                return null;
            }
        }


        public List<InformacionAdicional> ListarPersonaInformacion(Int64 pCod, string tipo_persona, Usuario vUsuario)
        {
            try
            {
                return BOInformacion.ListarPersonaInformacion(pCod,tipo_persona, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionAdicionalBusiness", "ListarPersonaInformacion", ex);
                return null;
            }
        }
        public bool ActualizacionMasiva(List<InformacionAdicional> pAdicional, Usuario usuario,List<ErroresCarga> pError)
        {
            try
            {
              return  BOInformacion.ActualizacionMasiva(pAdicional, usuario, pError);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadPersonaService", "ActualizacionMasiva", ex);
                return false;
            }
        }

        public void EliminarActividadPersona(Int64 pIdActividad, Usuario vUsuario)
        {
            try
            {
                BOInformacion.EliminarActividadPersona(pIdActividad, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadPersonaService", "EliminarActividadPersona", ex);
            }
        }

        public InformacionAdicional CrearPersona_InfoAdicional(InformacionAdicional pAdicional, Usuario usuario)
        {
            try
            {
                return BOInformacion.CrearPersona_InfoAdicional(pAdicional, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadPersonaService", "CrearPersona_InfoAdicional", ex);
                return null;
            }
        }

        public string ConsultarInformacionPersonalDeUnaPersona(long codigoPersona, long codigoTipoInformacion, Usuario vUsuario)
        {
            try
            {
                return BOInformacion.ConsultarInformacionPersonalDeUnaPersona(codigoPersona, codigoTipoInformacion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadPersonaService", "ConsultarInformacionPersonalDeUnaPersona", ex);
                return null;
            }
        }

        public void ModificarPersona_InfoAdicional(InformacionAdicional pAdicional, Usuario usuario)
        {
            try
            {
                BOInformacion.ModificarPersona_InfoAdicional(pAdicional, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadPersonaService", "ModificarPersona_InfoAdicional", ex);
            }
        }
    }
}
