using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Tesoreria.Business;
using Xpinn.Tesoreria.Entities;
using System.Web;
using System.IO;
using Xpinn.FabricaCreditos.Entities;
using System.Web.UI.WebControls;

namespace Xpinn.Tesoreria.Services
{
    public class EmpresaNovedadService
    {
        private EmpresaNovedadBusiness BOEmpresaNovedad;
        private ExcepcionBusiness BOExcepcion;

        public EmpresaNovedadService()
        {
            BOEmpresaNovedad = new EmpresaNovedadBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "180104"; } }
        public string CodigoProgramaVacaciones { get { return "180110"; } }

        public List<EmpresaNovedad> ListarRecaudo(EmpresaNovedad pRecaudosMasivos, string pOrden, Usuario pUsuario)
        {
            try
            {
                return BOEmpresaNovedad.ListarRecaudo(pRecaudosMasivos, pOrden, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadServices", "ListarRecaudo", ex);
                return null;
            }
        }

        public EmpresaNovedad ConsultarRecaudo(string pNumeroRecaudo, Usuario pUsuario)
        {
            try
            {
                return BOEmpresaNovedad.ConsultarRecaudo(pNumeroRecaudo, pUsuario);                
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadServices", "ConsultarRecaudo", ex);
                return null;
            }
        }

        public List<EmpresaNovedad> ListarDetalleGeneracion(EmpresaNovedad pRecaudo, Usuario pUsuario)
        {
            try
            {
                return BOEmpresaNovedad.ListarDetalleGeneracion(pRecaudo, pUsuario);                
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadServices", "ListarDetalleGeneracion", ex);
                return null;
            }
        }

        public List<EmpresaNovedad> ListarNovedadPersona(string cod_persona, Usuario usuario)
        {
            try
            {
                return BOEmpresaNovedad.ListarNovedadPersona(cod_persona, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadServices", "ListarNovedadPersona", ex);
                return null;
            }
        }

        public EmpresaNovedad ConsultarVacaciones(string codVacaciones, Usuario usuario)
        {
            try
            {
                return BOEmpresaNovedad.ConsultarVacaciones(codVacaciones, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadServices", "ConsultarVacaciones", ex);
                return null;
            }
        }

        public EmpresaNovedad ConsultarPersonaVacaciones(string pFiltro, Usuario usuario)
        {
            try
            {
                return BOEmpresaNovedad.ConsultarPersonaVacaciones(pFiltro, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadServices", "ConsultarPersonaVacaciones", ex);
                return null;
            }
        }

        public void EliminarVacaciones(long idBorrar, Usuario usuario)
        {
            try
            {
                BOEmpresaNovedad.EliminarVacaciones(idBorrar, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadServices", "EliminarVacaciones", ex);
            }
        }

        public List<EmpresaNovedad> ListarEmpresaNovedad(string filtro, Usuario pUsuario)
        {
            try
            {
                return BOEmpresaNovedad.ListarEmpresaNovedad(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadServices", "ListarEmpresaNovedad", ex);
                return null;
            }
        }

        public List<EmpresaNovedad> ActualizarDetalleGeneracion(EmpresaNovedad pRecaudos, Usuario vUsuario)
        {
            try
            {
                return BOEmpresaNovedad.ActualizarDetalleGeneracion(pRecaudos, vUsuario);                
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadServices", "ActualizarDetalleGeneracion", ex);
                return null;
            }
        }

        public List<EmpresaNovedad> ListarDetalleGeneracionNuevas(EmpresaNovedad pRecaudos, Usuario vUsuario)
        {
            try
            {
                return BOEmpresaNovedad.ListarDetalleGeneracionNuevas(pRecaudos, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadServices", "ListarDetalleGeneracionNuevas", ex);
                return null;
            }
        }

        public void GenerarNovedades(EmpresaNovedad recaudosmasivos,ref string pError, Usuario pUsuario)
        {
            try
            {
                BOEmpresaNovedad.GenerarNovedades(recaudosmasivos,ref pError, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadServices", "GenerarNovedades", ex);
            }
        }

        public Vacaciones ModificarVacaciones(Vacaciones vacaciones, Usuario usuario)
        {
            try
            {
                return BOEmpresaNovedad.ModificarVacaciones(vacaciones, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadServices", "ModificarVacaciones", ex);
                return null;
            }
        }

        public void GenerarNovedadesNuevas(EmpresaNovedad recaudosmasivos, ref string pError, Usuario pUsuario)
        {
            try
            {
                BOEmpresaNovedad.GenerarNovedadesNuevas(recaudosmasivos, ref pError, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadServices", "GenerarNovedadesNuevas", ex);
            }
        }

        public List<EmpresaNovedad> ListarTempNovedades(EmpresaNovedad recaudo, Usuario pUsuario)
        {
            try
            {
                return BOEmpresaNovedad.ListarTempNovedades(recaudo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadServices", "ListarTempNovedades", ex);
                return null;
            }
        }

        public List<EmpresaNovedad> ListarTempNovedadesNuevas(EmpresaNovedad Recaudo, Usuario vUsuario)
        {
            try
            {
                return BOEmpresaNovedad.ListarTempNovedadesNuevas(Recaudo, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadServices", "ListarTempNovedades", ex);
                return null;
            }
        }

        public void Eliminar_1_Encabezado_2_Detalle_RECAUDO(Int64 pId, Usuario vUsuario, int opcion)
        {
            try
            {
                BOEmpresaNovedad.EliminarDetRecaudosGeneracion(pId, vUsuario, opcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadServices", "EliminarDetRecaudosGeneracion", ex);
            }
        }

        public EmpresaNovedad CrearRecaudosGeneracion(EmpresaNovedad recaudosmasivos, Usuario pUsuario)
        {
            try
            {
                return BOEmpresaNovedad.CrearRecaudosGeneracion(recaudosmasivos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadServices", "CrearRecaudosGeneracion", ex);
                return null;
            }
        }

        public EmpresaNovedad ModificarRecaudosGeneracion(EmpresaNovedad recaudosmasivos, Usuario pUsuario, int opcion, int opcionNew)
        {
            try
            {
                return BOEmpresaNovedad.ModificarRecaudosGeneracion(recaudosmasivos, pUsuario, opcion, opcionNew);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadServices", "ModificarRecaudosGeneracion", ex);
                return null;
            }
        }

        public EmpresaNovedad CrearDetRecaudosGeneracion(EmpresaNovedad recaudosmasivos, Usuario pUsuario)
        {
            try
            {
                return BOEmpresaNovedad.CrearDetRecaudosGeneracion(recaudosmasivos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadServices", "CrearDetRecaudosGeneracion", ex);
                return null;
            }
        }

        public Vacaciones InsertarVacaciones(Vacaciones vacaciones, Usuario usuario)
        {
            try
            {
                return BOEmpresaNovedad.InsertarVacaciones(vacaciones, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadServices", "InsertarVacaciones", ex);
                return null;
            }
        }


        public EmpresaNovedad ModificarDetRecaudosGeneracion(EmpresaNovedad recaudosmasivos, Usuario pUsuario)
        {
            try
            {
                return BOEmpresaNovedad.ModificarDetRecaudosGeneracion(recaudosmasivos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadServices", "ModificarDetRecaudosGeneracion", ex);
                return null;
            }
        }

        public EmpresaNovedad CrearDetRecaudosGeneracionNEW(EmpresaNovedad recaudosmasivos, Usuario pUsuario)
        {
            try
            {
                return BOEmpresaNovedad.CrearDetRecaudosGeneracionNEW(recaudosmasivos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadServices", "CrearDetRecaudosGeneracionNEW", ex);
                return null;
            }
        }

        public EmpresaNovedad ModificarDetRecaudosGeneracionNEW(EmpresaNovedad recaudosmasivos, Usuario pUsuario)
        {
            try
            {
                return BOEmpresaNovedad.ModificarDetRecaudosGeneracionNEW(recaudosmasivos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadServices", "ModificarDetRecaudosGeneracionNEW", ex);
                return null;
            }
        }

        public void EliminarDetRecaudosGeneracionNew(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOEmpresaNovedad.EliminarDetRecaudosGeneracionNew(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadServices", "EliminarDetRecaudosGeneracionNew", ex);
            }
        }


        public EmpresaNovedad CREAR_TEMP_RECAUDO(EmpresaNovedad pTemp, Usuario vUsuario)
        {
            try
            {
                return BOEmpresaNovedad.CREAR_TEMP_RECAUDO(pTemp, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadServices", "CREAR_TEMP_RECAUDO", ex);
                return null;
            }
        }

        public EmpresaNovedad MODIFICAR_TEMP_RECAUDO(EmpresaNovedad pTemp, Usuario vUsuario)
        {
            try
            {
                return BOEmpresaNovedad.MODIFICAR_TEMP_RECAUDO(pTemp, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadServices", "MODIFICAR_TEMP_RECAUDO", ex);
                return null;
            }
        }

        public EmpresaNovedad CREAR_TEMP_RECAUDO_NUEVO(EmpresaNovedad pTemp, Usuario vUsuario)
        {
            try
            {
                return BOEmpresaNovedad.CREAR_TEMP_RECAUDO_NUEVO(pTemp, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadServices", "CREAR_TEMP_RECAUDO_NUEVO", ex);
                return null;
            }
        }

        public EmpresaNovedad MODIFICAR_TEMP_RECAUDO_NUEVO(EmpresaNovedad pTemp, Usuario vUsuario)
        {
            try
            {
                return BOEmpresaNovedad.MODIFICAR_TEMP_RECAUDO_NUEVO(pTemp, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadServices", "MODIFICAR_TEMP_RECAUDO_NUEVO", ex);
                return null;
            }
        }

        public List<EmpresaNovedad> ListarDetalleRecaudo(int pNumeroRecaudo, Usuario pUsuario)
        {
            try
            {
                return BOEmpresaNovedad.ListarDetalleRecaudo(pNumeroRecaudo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadServices", "ListarDetalleRecaudo", ex);
                return null;
            }
        }


        public List<EmpresaNovedad> ListarEstructuraXempresa(Int32 pId, Usuario pUsuario)
        {
            try
            {
                return BOEmpresaNovedad.ListarEstructuraXempresa(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadServices", "ListarEstructuraXempresa", ex);
                return null;
            }
        }

        public Boolean ConsultarConcepto(Int64 pCodEmpresa, Int64 pTipoProducto, string pCodLinea, ref Int64 prioridad, ref String concepto, Usuario pUsuario)
        {
            try
            {
                return BOEmpresaNovedad.ConsultarConcepto(pCodEmpresa, pTipoProducto, pCodLinea, ref prioridad, ref concepto, pUsuario);
            }
            catch
            {
                return false;
            }
        }

        public int? ConsultarTipoLista(string pTipoLista, Usuario pUsuario)
        {
            return BOEmpresaNovedad.ConsultarTipoLista(pTipoLista, pUsuario);
        }

        public string ConsultarEstadoPersonaAfiliacion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOEmpresaNovedad.ConsultarEstadoPersonaAfiliacion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadServices", "ConsultarEstadoPersonaAfiliacion", ex);
                return null;
            }
        }



    }
}
