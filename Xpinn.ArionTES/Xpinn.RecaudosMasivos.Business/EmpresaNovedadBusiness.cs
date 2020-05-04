using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;
using Xpinn.FabricaCreditos.Entities;
using System.Web;
using System.IO;

namespace Xpinn.Tesoreria.Business
{
    public class EmpresaNovedadBusiness : GlobalBusiness
    {
        private EmpresaNovedadData DAEmpresaNovedad;
        //private StreamReader strReader;

        const string quote = "\"";

        public EmpresaNovedadBusiness()
        {
            DAEmpresaNovedad = new EmpresaNovedadData();
        }

        public List<EmpresaNovedad> ListarRecaudo(EmpresaNovedad pRecaudosMasivos, string pOrden, Usuario pUsuario)
        {
            try
            {
                return DAEmpresaNovedad.ListarRecaudo(pRecaudosMasivos, pOrden, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadBusiness", "ListarRecaudo", ex);
                return null;
            }
        }

        public EmpresaNovedad ConsultarRecaudo(string pNumeroRecaudo, Usuario pUsuario)
        {
            try
            {
                return DAEmpresaNovedad.ConsultarRecaudo(pNumeroRecaudo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadBusiness", "ConsultarRecaudo", ex);
                return null;
            }
        }

        public List<EmpresaNovedad> ListarDetalleGeneracion(EmpresaNovedad pRecaudo, Usuario pUsuario)
        {
            try
            {
                return DAEmpresaNovedad.ListarDetalleGeneracion(pRecaudo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadBusiness", "ListarDetalleGeneracion", ex);
                return null;
            }
        }

        public List<EmpresaNovedad> ActualizarDetalleGeneracion(EmpresaNovedad pRecaudos, Usuario vUsuario)
        {
            try
            {
                return DAEmpresaNovedad.ActualizarDetalleGeneracion(pRecaudos, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadBusiness", "ActualizarDetalleGeneracion", ex);
                return null;
            }
        }

        public List<EmpresaNovedad> ListarNovedadPersona(string cod_persona, Usuario usuario)
        {
            try
            {
                return DAEmpresaNovedad.ListarNovedadPersona(cod_persona, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadBusiness", "ListarNovedadPersona", ex);
                return null;
            }
        }

        public EmpresaNovedad ConsultarVacaciones(string codVacaciones, Usuario usuario)
        {
            try
            {
                return DAEmpresaNovedad.ConsultarVacaciones(codVacaciones, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadBusiness", "ConsultarVacaciones", ex);
                return null;
            }
        }

        public EmpresaNovedad ConsultarPersonaVacaciones(string pFiltro, Usuario usuario)
        {
            try
            {
                return DAEmpresaNovedad.ConsultarPersonaVacaciones(pFiltro, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadBusiness", "ConsultarPersonaVacaciones", ex);
                return null;
            }
        }

        public void EliminarVacaciones(long idBorrar, Usuario usuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAEmpresaNovedad.EliminarVacaciones(idBorrar, usuario);
                    ts.Complete();
                    return;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadBusiness", "EliminarVacaciones", ex);
            }
        }

        public List<EmpresaNovedad> ListarEmpresaNovedad(string filtro, Usuario pUsuario)
        {
            try
            {
                return DAEmpresaNovedad.ListarEmpresaNovedad(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadBusiness", "ListarEmpresaNovedad", ex);
                return null;
            }
        }

        public List<EmpresaNovedad> ListarDetalleGeneracionNuevas(EmpresaNovedad pRecaudos, Usuario vUsuario)
        {
            try
            {
                return DAEmpresaNovedad.ListarDetalleGeneracionNuevas(pRecaudos, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadBusiness", "ListarDetalleGeneracionNuevas", ex);
                return null;
            }
        }

        public void GenerarNovedades(EmpresaNovedad recaudosmasivos, ref string pError, Usuario pUsuario)
        {
            try
            {
                TransactionOptions tranopc = new TransactionOptions();
                tranopc.Timeout = TimeSpan.MaxValue;
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, tranopc))
                {
                    DAEmpresaNovedad.GenerarNovedades(recaudosmasivos, ref pError, pUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                pError = ex.Message;
            }
        }

        public void GenerarNovedadesNuevas(EmpresaNovedad recaudosmasivos, ref string pError, Usuario pUsuario)
        {
            try
            {
                DAEmpresaNovedad.GenerarNovedadesNuevas(recaudosmasivos, ref pError, pUsuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
            }
        }

        public List<EmpresaNovedad> ListarTempNovedades(EmpresaNovedad recaudo, Usuario pUsuario)
        {
            try
            {
                return DAEmpresaNovedad.ListarTempNovedades(recaudo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadBusiness", "ListarTempNovedades", ex);
                return null;
            }
        }

        public Vacaciones ModificarVacaciones(Vacaciones vacaciones, Usuario usuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    vacaciones = DAEmpresaNovedad.ModificarVacaciones(vacaciones, usuario);
                    ts.Complete();
                    return vacaciones;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadBusiness", "ModificarVacaciones", ex);
                return null;
            }
        }

        public List<EmpresaNovedad> ListarTempNovedadesNuevas(EmpresaNovedad Recaudo, Usuario vUsuario)
        {
            try
            {
                return DAEmpresaNovedad.ListarTempNovedadesNuevas(Recaudo, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadBusiness", "ListarTempNovedades", ex);
                return null;
            }
        }

        public EmpresaNovedad CrearRecaudosGeneracion(EmpresaNovedad recaudosmasivos, Usuario pUsuario)
        {
            try
            {
                TransactionOptions transactionOptions = new TransactionOptions();
                transactionOptions.Timeout = TimeSpan.MaxValue;
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
                {
                    recaudosmasivos = DAEmpresaNovedad.CrearRecaudosGeneracion(recaudosmasivos, pUsuario);

                    Int64 cod;
                    cod = Convert.ToInt64(recaudosmasivos.numero_novedad);

                    if (recaudosmasivos.lstTemp != null)
                    {
                        /*int num = 0;
                        foreach (EmpresaNovedad eRecau in recaudosmasivos.lstTemp)
                        {
                            EmpresaNovedad nRecaudos = new EmpresaNovedad();
                            eRecau.numero_novedad = cod;
                            eRecau.nombre = eRecau.nombres + ", " + eRecau.apellidos;
                            nRecaudos = DAEmpresaNovedad.CrearDetRecaudosGeneracion(eRecau, pUsuario);
                            num += 1;
                        }*/
                        List<EmpresaNovedad> lstRecaudos = recaudosmasivos.lstTemp;
                        if (!DAEmpresaNovedad.CrearDetRecaudosGeneracion(cod, ref lstRecaudos, pUsuario))
                            return null;
                        recaudosmasivos.lstTemp = lstRecaudos;
                    }

                    if (recaudosmasivos.lstTempNuevos != null && recaudosmasivos.lstTempNuevos.Count > 0)
                    {
                        foreach (EmpresaNovedad eRecaNew in recaudosmasivos.lstTempNuevos)
                        {
                            eRecaNew.numero_novedad = cod;
                            eRecaNew.nombre = eRecaNew.nombres + "," + eRecaNew.apellidos;
                            EmpresaNovedad pEntNew = new EmpresaNovedad();
                            pEntNew = DAEmpresaNovedad.CrearDetRecaudosGeneracionNEW(eRecaNew, pUsuario);
                        }
                    }

                    ts.Complete();
                    return recaudosmasivos;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadBusiness", "CrearRecaudosGeneracion", ex);
                return null;
            }
        }

        public EmpresaNovedad ModificarRecaudosGeneracion(EmpresaNovedad recaudosmasivos, Usuario pUsuario,int opcion,int opcionNew)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    recaudosmasivos = DAEmpresaNovedad.ModificarRecaudosGeneracion(recaudosmasivos, pUsuario);
                    Int64 Cod;
                    Cod = Convert.ToInt64(recaudosmasivos.numero_novedad);

                    if (recaudosmasivos.lstTemp != null)
                    {
                        int num = 0;
                        foreach (EmpresaNovedad eRecau in recaudosmasivos.lstTemp)
                        {
                            eRecau.numero_novedad = Cod;
                            eRecau.nombre = eRecau.nombres + "," + eRecau.apellidos;
                            EmpresaNovedad nRecaudos = new EmpresaNovedad();
                            if (opcion == 1)
                                nRecaudos = DAEmpresaNovedad.CrearDetRecaudosGeneracion(eRecau, pUsuario);
                            else
                            {
                                if (eRecau.iddetalle == 0)
                                    nRecaudos = DAEmpresaNovedad.CrearDetRecaudosGeneracion(eRecau, pUsuario);
                                else
                                    nRecaudos = DAEmpresaNovedad.ModificarDetRecaudosGeneracion(eRecau, pUsuario);
                            }
                            num += 1;
                        }
                    }

                    if (recaudosmasivos.lstTempNuevos != null && recaudosmasivos.lstTempNuevos.Count > 0)
                    {
                        foreach (EmpresaNovedad eRecaNew in recaudosmasivos.lstTempNuevos)
                        {
                            eRecaNew.numero_novedad = Cod;
                            eRecaNew.nombre = eRecaNew.nombres + "," + eRecaNew.apellidos;
                            EmpresaNovedad pEntNew = new EmpresaNovedad();
                            if (opcionNew == 1)
                                pEntNew = DAEmpresaNovedad.CrearDetRecaudosGeneracionNEW(eRecaNew, pUsuario);
                            else
                            {
                                if (eRecaNew.iddetalle == 0)
                                    pEntNew = DAEmpresaNovedad.CrearDetRecaudosGeneracionNEW(eRecaNew, pUsuario);
                                else
                                    pEntNew = DAEmpresaNovedad.ModificarDetRecaudosGeneracionNEW(eRecaNew, pUsuario);
                            }
                        }
                    }

                    ts.Complete();
                    return recaudosmasivos;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadBusiness", "ModificarRecaudosGeneracion", ex);
                return null;
            }
        }

        public Vacaciones InsertarVacaciones(Vacaciones vacaciones, Usuario usuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    vacaciones = DAEmpresaNovedad.InsertarVacaciones(vacaciones, usuario);
                    ts.Complete();
                    return vacaciones;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadBusiness", "InsertarVacaciones", ex);
                return null;
            }
        }

        public EmpresaNovedad CrearDetRecaudosGeneracion(EmpresaNovedad recaudosmasivos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    recaudosmasivos = DAEmpresaNovedad.CrearDetRecaudosGeneracion(recaudosmasivos, pUsuario);
                    ts.Complete();
                    return recaudosmasivos;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadBusiness", "CrearDetRecaudosGeneracion", ex);
                return null;
            }
        }


        public EmpresaNovedad ModificarDetRecaudosGeneracion(EmpresaNovedad recaudosmasivos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    recaudosmasivos = DAEmpresaNovedad.ModificarDetRecaudosGeneracion(recaudosmasivos, pUsuario);
                    ts.Complete();
                    return recaudosmasivos;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadBusiness", "ModificarDetRecaudosGeneracion", ex);
                return null;
            }
        }

        public EmpresaNovedad CrearDetRecaudosGeneracionNEW(EmpresaNovedad recaudosmasivos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    recaudosmasivos = DAEmpresaNovedad.CrearDetRecaudosGeneracionNEW(recaudosmasivos, pUsuario);
                    ts.Complete();
                    return recaudosmasivos;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadBusiness", "CrearDetRecaudosGeneracionNEW", ex);
                return null;
            }
        }


        public EmpresaNovedad ModificarDetRecaudosGeneracionNEW(EmpresaNovedad recaudosmasivos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    recaudosmasivos = DAEmpresaNovedad.ModificarDetRecaudosGeneracionNEW(recaudosmasivos, pUsuario);
                    ts.Complete();
                    return recaudosmasivos;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadBusiness", "ModificarDetRecaudosGeneracionNEW", ex);
                return null;
            }
        }

        public void EliminarDetRecaudosGeneracionNew(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAEmpresaNovedad.EliminarDetRecaudosGeneracionNew(pId, vUsuario);
                    ts.Complete();
                    return;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadBusiness", "EliminarDetRecaudosGeneracionNew", ex);
                return;
            }
        }

        public void EliminarDetRecaudosGeneracion(Int64 pId, Usuario vUsuario, int opcion)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAEmpresaNovedad.EliminarDetRecaudosGeneracion(pId, vUsuario, opcion);
                    ts.Complete();
                    return;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadBusiness", "EliminarDetRecaudosGeneracion", ex);
                return;
            }
        }

        public EmpresaNovedad CREAR_TEMP_RECAUDO(EmpresaNovedad pTemp, Usuario vUsuario)
        {
            try
            {
                return DAEmpresaNovedad.CREAR_TEMP_RECAUDO(pTemp, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadBusiness", "CREAR_TEMP_RECAUDO", ex);
                return null;
            }
        }


        public EmpresaNovedad MODIFICAR_TEMP_RECAUDO(EmpresaNovedad pTemp, Usuario vUsuario)
        {
            try
            {
                return DAEmpresaNovedad.MODIFICAR_TEMP_RECAUDO(pTemp, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadBusiness", "MODIFICAR_TEMP_RECAUDO", ex);
                return null;
            }
        }

        public EmpresaNovedad CREAR_TEMP_RECAUDO_NUEVO(EmpresaNovedad pTemp, Usuario vUsuario)
        {
            try
            {
                return DAEmpresaNovedad.CREAR_TEMP_RECAUDO_NUEVO(pTemp, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadBusiness", "CREAR_TEMP_RECAUDO_NUEVO", ex);
                return null;
            }
        }


        public EmpresaNovedad MODIFICAR_TEMP_RECAUDO_NUEVO(EmpresaNovedad pTemp, Usuario vUsuario)
        {
            try
            {
                return DAEmpresaNovedad.MODIFICAR_TEMP_RECAUDO_NUEVO(pTemp, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadBusiness", "MODIFICAR_TEMP_RECAUDO_NUEVO", ex);
                return null;
            }
        }

        public List<EmpresaNovedad> ListarDetalleRecaudo(int pNumeroRecaudo, Usuario pUsuario)
        {
            try
            {
                return DAEmpresaNovedad.ListarDetalleRecaudo(pNumeroRecaudo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadBusiness", "ListarDetalleRecaudo", ex);
                return null;
            }
        }


        public List<EmpresaNovedad> ListarEstructuraXempresa(Int32 pId, Usuario pUsuario)
        {
            try
            {
                return DAEmpresaNovedad.ListarEstructuraXempresa(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadBusiness", "ListarEstructuraXempresa", ex);
                return null;
            }
        }

        public Boolean ConsultarConcepto(Int64 pCodEmpresa, Int64 pTipoProducto, string pCodLinea, ref Int64 prioridad, ref String concepto, Usuario pUsuario)
        {
            try
            {               
                return DAEmpresaNovedad.ConsultarConcepto(pCodEmpresa, pTipoProducto, pCodLinea, ref prioridad, ref concepto, pUsuario);
            }
            catch 
            {                
                return false;
            }
        }


        public int? ConsultarTipoLista(string pTipoLista, Usuario pUsuario)
        {
            return DAEmpresaNovedad.ConsultarTipoLista(pTipoLista, pUsuario);
        }

        public string ConsultarEstadoPersonaAfiliacion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAEmpresaNovedad.ConsultarEstadoPersonaAfiliacion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaNovedadBussiness", "ConsultarEstadoPersonaAfiliacion", ex);
                return null;
            }
        }



    }
}
