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
    public class ActividadesServices
    {
        private ActividadesBusiness BOActividad;
        private ExcepcionBusiness BOExcepcion;


        public string CodigoPrograma { get { return "170104"; } }
        
        public string ProgramaAperturaAporte { get { return "170101"; } }

        public string CodigoProgramaActividadEconomica { get { return "100213"; } }


        public ActividadesServices()
        {
            BOActividad = new ActividadesBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }


        public Actividades CrearActividadesPersona(Actividades pActividad, Usuario pUsuario)
        {
            try
            {
                return BOActividad.CrearActividadesPersona(pActividad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadPersonaService", "CrearActividadesPersona", ex);
                return null;
            }
        }


        public Actividades ModificarActividadesPersona(Actividades pActividad, Usuario vUsuario)
        {
            try
            {
                return BOActividad.ModificarActividadesPersona(pActividad, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadPersonaService", "ModificarActividadesPersona", ex);
                return null;
            }
        }


        public List<Actividades> ListarActividadesPersona(Actividades pActividad, Usuario pUsuario)
        {
            try
            {
                return BOActividad.ListarActividadesPersona(pActividad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadPersonaService", "ListarActividadesPersona", ex);
                return null;
            }
        }



        public List<Actividades> ConsultarActividad(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOActividad.ConsultarActividad(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadPersonaService", "ConsultarActividad", ex);
                return null;
            }
        }

      

        public void EliminarActividadPersona(Int64 pIdActividad, Usuario vUsuario)
        {
            try
            {
                BOActividad.EliminarActividadPersona(pIdActividad, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadPersonaService", "EliminarActividadPersona", ex);
            }
        }


        public List<Actividades> ConsultarActividadesEconomicasSecundarias(Int64 pCodPersona, Usuario vUsuario)
        {
            List<Actividades> lstActividadCIIU = null;
            try
            {
                lstActividadCIIU = BOActividad.ConsultarActividadesEconomicasSecundarias(pCodPersona, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadPersonaService", "ConsultarActividadesEconomicasSecundarias", ex);
                return null;
            }
            return lstActividadCIIU;
        }

        #region PERSONA INFORMACION FINANCIERA


        public List<CuentasBancarias> ConsultarCuentasBancarias(Int64 pId,string filtro, Usuario vUsuario)
        {
            try
            {
                return BOActividad.ConsultarCuentasBancarias(pId,filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPersonaService", "ConsultarCuentasBancarias", ex);
                return null;
            }
        }

        public void EliminarCuentasBancarias(Int64 pIdCuentabancaria, Usuario vUsuario)
        {
            try
            {
                BOActividad.EliminarCuentasBancarias(pIdCuentabancaria, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPersonaService", "EliminarCuentasBancarias", ex);
            }
           
        }



        #endregion


        #region ACTIVIDAD ECONOMICA

        public List<Actividades> ConsultarActividadEconomica(Actividades Actividades, Usuario vUsuario)
        {
            try
            {

                return BOActividad.ConsultarActividadEconomica(Actividades, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadService", "ConsultarActividadEconomica", ex);
                return null;
            }
        }

        public Actividades ConsultarActividadEconomicaId(Int64 pId, Usuario vUsuario)
        {
            try
            {
                  return BOActividad.ConsultarActividadEconomicaId(pId, vUsuario);
              
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadService", "ConsultarActividadEconomicaId", ex);
                return null;
            }
        }

        public Actividades CrearActividadEconomica(Actividades pActividad, Usuario pUsuario)
        {
            try
            {
                return BOActividad.CrearActividadEconomica(pActividad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadesService", "CrearActividadEconomica", ex);
                return null;
            }
        }

        public Actividades ModificarActividadEconomica(String pCodactividad, Actividades pActividad, Usuario vUsuario)
        {
            try
            {
                return BOActividad.ModificarActividadEconomica(pCodactividad,pActividad, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadesService", "ModificarActividadEconomica", ex);
                return null;
            }
        }

        public void EliminarActividadEconomica(String pCodactividad, Usuario vUsuario)
        {
            try
            {
                BOActividad.EliminarActividadEconomica(pCodactividad, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadesService", "EliminarActividadEconomica", ex);
            }
        }

        public List<Actividades> listarTemasInteres(Usuario pUsuario)
        {
            try
            {
                return BOActividad.listarTemasInteres(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadesService", "listarTemasInteres", ex);
                return null;
            }
        }

        #endregion

    }
}
