using System;
using System.Collections.Generic;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Business;
using System.IO;

namespace Xpinn.Aportes.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class PlanesTelefonicosService
    {
        private PlanesTelefonicosBusiness BOPlanesTelef;
        private ExcepcionBusiness BOExcepcion;

        public PlanesTelefonicosService()
        {
            BOPlanesTelef = new PlanesTelefonicosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "170801"; } }

        //Crear Plan Telefonico
        public PlanTelefonico CrearPlanTelefonico(PlanTelefonico pPlanTel, Usuario pusuario)
        {
            try
            {
                pPlanTel = BOPlanesTelef.CrearPlanTelefonico(pPlanTel, pusuario);
                return pPlanTel;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosService", "CrearPlanTelefonico", ex);
                return null;
            }
        }

        //Modificar plan Telefonico
        public PlanTelefonico ModificarPlanTelefonico(PlanTelefonico pPlanTel, Usuario pusuario)
        {
            try
            {
                pPlanTel = BOPlanesTelef.ModificarPlanTelefonico(pPlanTel, pusuario);
                return pPlanTel;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosService", "ModificarPlanTelefonico", ex);
                return null;
            }
        }

        //Eliminar Plan Telefonico
        public void EliminarPlanTelefonico(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOPlanesTelef.EliminarPlanTelefonico(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosService", "EliminarPlanTelefonico", ex);
            }
        }

        //Consultar Plan Telefonico
        public PlanTelefonico ConsultarPlanTelefonico(Int64 pId, Usuario pusuario)
        {
            try
            {
                PlanTelefonico PlanTelefonico = new PlanTelefonico();
                PlanTelefonico = BOPlanesTelef.ConsultarPlanTelefonico(pId, pusuario);
                return PlanTelefonico;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosService", "ConsultarPlanTelefonico", ex);
                return null;
            }
        }

        //Consultar Todos lo planes
        public List<PlanTelefonico> ListarPlanTelefonico(PlanTelefonico pPlanTel, Usuario pusuario)
        {
            try
            {
                return BOPlanesTelef.ListarPlanTelefonico(pPlanTel, pusuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosService", "ListarPlanTelefonico", ex);
                return null;
            }
        }

        //Listar Proveedores        
        public List<PlanTelefonico> ListarProveedores(Usuario pusuario)
        {
            try
            {
                return BOPlanesTelef.ListarProveedores(pusuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosService", "ListarProveedores", ex);
                return null;
            }
        }
        
        //Crear Linea Telefonica
        public PlanTelefonico CrearLineaTelefonica(ref Int64 vCod_Ope, Xpinn.Tesoreria.Entities.Operacion pOperacion, PlanTelefonico pPlanTel, Usuario pusuario)
        {
            try
            {
                pPlanTel = BOPlanesTelef.CrearLineaTelefonica(ref vCod_Ope, pOperacion, pPlanTel, pusuario);
                return pPlanTel;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosService", "CrearLineaTelefonica", ex);
                return null;
            }
        }
        
        //Modificar Linea Telefonico
        public PlanTelefonico ModificarLineaTelefonica(PlanTelefonico pPlanTel, Usuario pusuario)
        {
            try
            {
                pPlanTel = BOPlanesTelef.ModificarLineaTelefonica(pPlanTel, pusuario);
                return pPlanTel;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosService", "ModificarLineaTelefonica", ex);
                return null;
            }
        }

        //Consultar todas las lineas
        public List<PlanTelefonico> ListarLineasTelefonicas(PlanTelefonico plineafiltro , Usuario pusuario)
        {
            try
            {
                return BOPlanesTelef.ListarLineasTelefonicas(plineafiltro, pusuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosService", "ListarLineasTelefonicas", ex);
                return null;
            }
        }

        //Consultar Linea Telefonica
        public PlanTelefonico ConsultarLineaTelefonica(string pId, Usuario pusuario)
        {
            try
            {
                PlanTelefonico PlanTelefonico = new PlanTelefonico();
                PlanTelefonico = BOPlanesTelef.ConsultarLineaTelefonica(pId, pusuario);
                return PlanTelefonico;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosService", "ConsultarPlanTelefonico", ex);
                return null;
            }
        }

        public bool ConsultarLineasExistentes(Usuario vUsuario)
        {
            try
            {
                bool resultado = BOPlanesTelef.ConsultarLineasExistentes(vUsuario);
                return resultado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosService", "ConsultarLineasExistentes", ex);
                return false;
            }
        }

        //carga masiva 
        public void CargaAdicionales(ref string pError, Stream pstream, ref List<PlanTelefonico> lstAdicionales, ref List<ErroresCargaAportes> plstErrores, Usuario pUsuario)
        {
            try
            {
                BOPlanesTelef.CargaAdicionales(ref pError, pstream, ref lstAdicionales, ref plstErrores, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosService", "CargaAportes", ex);
            }
        }

        //Anderson Guardar Adicionales Masivo
        public void CrearImportacion(ref string pError, List<PlanTelefonico> lstAdicionales, ref Xpinn.Tesoreria.Entities.Operacion vOpe, Usuario pUsuario, ref List<Int64> lst_Num_Lin)
        {
            try
            {
                BOPlanesTelef.CrearImportacion(ref pError, lstAdicionales, ref vOpe, pUsuario, ref lst_Num_Lin);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosService", "CrearAporteImportacion", ex);
            }
        }

        //Estado de cuenta
        public List<PlanTelefonico> ListarLineas(PlanTelefonico pLinea, DateTime pFecha, Usuario vUsuario, string filtro)
        {
            try
            {
                return BOPlanesTelef.ListarLineas(pLinea, pFecha, vUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosService", "ListarLineas", ex);
                return null;
            }
        }
        public List<PlanTelefonico> ListarLineasAtencionWeb( Usuario vUsuario, string filtro)
        {
            try
            {
                return BOPlanesTelef.ListarLineasAtencionWeb(vUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosService", "ListarLineasAtencionWeb", ex);
                return null;
            }
        }
        //Masivo carga
        public void CargarLineasMasivo(ref string pError, Stream pstream, ref List<PlanTelefonico> lstCreditos, ref List<ErroresCargaAportes> plstErrores, Usuario pUsuario)
        {
            try
            {
                BOPlanesTelef.CargarLineasMasivo(ref pError, pstream, ref lstCreditos, ref plstErrores, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosService", "CargarLineasMasivo", ex);
            }
        }

        //Masivo Crear Lineas
        public void CrearMasivoLineas(List<PlanTelefonico> lstLineas, ref string pError, ref List<PlanTelefonico> lst_num_lin, Xpinn.Tesoreria.Entities.Operacion vOpe, Usuario pUsuario)
        {
            try
            {
                BOPlanesTelef.CrearMasivoLineas(lstLineas, ref pError, ref lst_num_lin, vOpe, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosService", "CrearMasivoLineas", ex);
            }
        }

        //Traspaso
        public PlanTelefonico Traspaso(ref Int64 vCod_Ope, Xpinn.Tesoreria.Entities.Operacion pOperacion, PlanTelefonico pPlanTel, Usuario pusuario)
        {
            try
            {
                pPlanTel = BOPlanesTelef.Traspaso(ref vCod_Ope, pOperacion, pPlanTel, pusuario);
                return pPlanTel;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosService", "Traspaso", ex);
                return null;
            }
        }

        //Reposicion
        public PlanTelefonico Reposicion(PlanTelefonico pPlanTel, Usuario pusuario)
        {
            try
            {
                pPlanTel = BOPlanesTelef.Reposicion(pPlanTel, pusuario);
                return pPlanTel;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosService", "Reposicion", ex);
                return null;
            }
        }

        //CANCELACION
        public PlanTelefonico Cancelacion(PlanTelefonico pPlanTel, Usuario pusuario)
        {
            try
            {
                pPlanTel = BOPlanesTelef.Cancelacion(pPlanTel, pusuario);
                return pPlanTel;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosService", "Cancelacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Método de activación de líneas telefónicas.
        /// </summary>
        /// <param name="pPlanTel"></param>
        /// <param name="pusuario"></param>
        /// <returns></returns>
        public PlanTelefonico ActivacionDeLineasTelefonica(ref string pError, ref Xpinn.Tesoreria.Entities.Operacion vOpe, PlanTelefonico pPlanTel, Usuario pusuario)
        {
            try
            {
                pPlanTel = BOPlanesTelef.ActivacionDeLineasTelefonica(ref pError, ref vOpe, pPlanTel, pusuario);
                return pPlanTel;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivacionDeLineasTelefonica", "CrearPlanTelefonico", ex);
                return null;
            }
        }

    }
}
