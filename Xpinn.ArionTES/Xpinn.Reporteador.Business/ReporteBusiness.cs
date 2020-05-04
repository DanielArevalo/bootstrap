using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Reporteador.Data;
using Xpinn.Reporteador.Entities;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.Reporteador.Business
{
    /// <summary>
    /// Objeto de negocio para Presupuesto
    /// </summary>
    public class ReporteBusiness : GlobalBusiness
    {
        private ReporteData DAReporte;

        /// <summary>
        /// Constructor del objeto de negocio para Presupuesto
        /// </summary>
        public ReporteBusiness()
        {
            DAReporte = new ReporteData();
        }


        public DataTable GenerarReporte(int pCodReporte, DateTime pFecha, List<Parametros> lstParametros, ref string[] aColumnas, ref System.Type[] aTipos, ref int numerocolumnas, ref string pError, Usuario pReporte)
        {
            try
            {
                return DAReporte.GenerarReporte(pCodReporte, pFecha, lstParametros, ref aColumnas, ref aTipos, ref numerocolumnas, ref pError, pReporte);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "GenerarReporte", ex);
                return null;
            }
        }

        public DataTable GenerarLista(string pSentencia, ref string pError, Usuario pUsuario)
        {
            try
            {
                return DAReporte.GenerarLista(pSentencia, ref pError, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "GenerarReporte", ex);
                return null;
            }
        }


        /// <summary>
        /// Crea un Reporte
        /// </summary>
        /// <param name="pReporte">Entidad Reporte</param>
        /// <returns>Entidad Reporte creada</returns>
        public Reporte CrearReporte(Reporte pReporte, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pReporte = DAReporte.CrearReporte(pReporte, vUsuario);
                    if (pReporte.lstTablas != null)
                    {
                        foreach (Tabla eTabla in pReporte.lstTablas)
                        {
                            eTabla.idreporte = pReporte.idreporte;
                            DAReporte.CrearTabla(eTabla, vUsuario);
                        }
                    }
                    if (pReporte.lstEncadenamiento != null)
                    {
                        foreach (Encadenamiento eEncadenamiento in pReporte.lstEncadenamiento)
                        {
                            eEncadenamiento.idreporte = pReporte.idreporte;
                            if (eEncadenamiento.idencadenamiento != -1)
                                DAReporte.CrearEncadenamiento(eEncadenamiento, vUsuario);
                        }
                    }
                    if (pReporte.lstCondicion != null)
                    {
                        foreach (Condicion eCondicion in pReporte.lstCondicion)
                        {
                            eCondicion.idreporte = pReporte.idreporte;
                            if (eCondicion.idcondicion != -1)
                                DAReporte.CrearCondicion(eCondicion, vUsuario);
                        }
                    }
                    if (pReporte.lstColumnaReporte != null)
                    {
                        foreach (ColumnaReporte eColumnaReporte in pReporte.lstColumnaReporte)
                        {
                            eColumnaReporte.idreporte = pReporte.idreporte;
                            if (eColumnaReporte.idcolumna != -1)
                                DAReporte.CrearColumna(eColumnaReporte, vUsuario);
                        }
                    }
                    if (pReporte.lstOrden != null)
                    {
                        foreach (Orden eOrden in pReporte.lstOrden)
                        {
                            eOrden.idreporte = pReporte.idreporte;
                            if (eOrden.idorden != -1)
                                DAReporte.CrearOrden(eOrden, vUsuario);
                        }
                    }
                    if (pReporte.lstGrupo != null)
                    {
                        foreach (Grupo eGrupo in pReporte.lstGrupo)
                        {
                            eGrupo.idreporte = pReporte.idreporte;
                            if (eGrupo.idgrupo != -1)
                                DAReporte.CrearGrupo(eGrupo, vUsuario);
                        }
                    }
                    if (pReporte.lstUsuarios != null)
                    {
                        foreach (UsuariosReporte eUsuario in pReporte.lstUsuarios)
                        {
                            eUsuario.idreporte = pReporte.idreporte;
                            if (eUsuario.autorizar == true)
                                DAReporte.CrearUsuario(eUsuario, vUsuario);
                        }
                    }
                    if (pReporte.lstPerfil != null)
                    {
                        foreach (PerfilReporte ePerfil in pReporte.lstPerfil)
                        {
                            ePerfil.idreporte = pReporte.idreporte;
                            if (ePerfil.autorizar == true)
                                DAReporte.CrearPerfil(ePerfil, vUsuario);
                        }
                    }
                    if (pReporte.lstParametros != null)
                    {
                        foreach (Parametros eParametros in pReporte.lstParametros)
                        {
                            eParametros.idreporte = pReporte.idreporte;
                            DAReporte.CrearParametro(eParametros, vUsuario);
                        }
                    }
                    if (pReporte.lstPlantilla != null)
                    {
                        foreach (Plantilla ePlantilla in pReporte.lstPlantilla)
                        {
                            ePlantilla.idreporte = pReporte.idreporte;
                            if (ePlantilla.idplantilla != -1)
                                DAReporte.CrearPlantilla(ePlantilla, vUsuario);
                        }
                    }

                    ts.Complete();
                }

                return pReporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "CrearReporte", ex);
                return null;
            }
        }

        

        public DataTable ConsultarCanal_GarantiasComunitarias(Usuario usuario)
        {
            try
            { 
                return DAReporte.ConsultarCanal_GarantiasComunitarias(usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ConsultarReporteConsolidadoCierre", ex);
                return null;
            }
        }

        public DataTable ConsultarProductos_GarantiasComunitarias(DateTime pFechaCorte, Usuario usuario)
        {
            try
            {
                return DAReporte.ConsultarProductos_GarantiasComunitarias(pFechaCorte, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ConsultarReporteConsolidadoCierre", ex);
                return null;
            }
        }

        public DataTable ConsultarTransacciones_GarantiasComunitarias(DateTime pFechaCorte, Usuario usuario)
        {
            try
            {
                return DAReporte.ConsultarTransacciones_GarantiasComunitarias(pFechaCorte, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ConsultarReporteConsolidadoCierre", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Reporte
        /// </summary>
        /// <param name="pReporte">Entidad Reporte</param>
        /// <returns>Entidad Reporte modificada</returns>
        public Reporte ModificarReporte(Reporte pReporte, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pReporte = DAReporte.ModificarReporte(pReporte, vUsuario);
                    DAReporte.InicializaReporte(pReporte.idreporte, vUsuario);
                    if (pReporte.lstTablas != null)
                    {
                        foreach (Tabla eTabla in pReporte.lstTablas)
                        {
                            eTabla.idreporte = pReporte.idreporte;
                            DAReporte.CrearTabla(eTabla, vUsuario);
                        }
                    }
                    if (pReporte.lstEncadenamiento != null)
                    {
                        foreach (Encadenamiento eEncadenamiento in pReporte.lstEncadenamiento)
                        {
                            eEncadenamiento.idreporte = pReporte.idreporte;
                            if (eEncadenamiento.idencadenamiento != -1)
                                DAReporte.CrearEncadenamiento(eEncadenamiento, vUsuario);
                        }
                    }
                    if (pReporte.lstCondicion != null)
                    {
                        foreach (Condicion eCondicion in pReporte.lstCondicion)
                        {
                            eCondicion.idreporte = pReporte.idreporte;
                            if (eCondicion.idcondicion != -1)
                                DAReporte.CrearCondicion(eCondicion, vUsuario);
                        }
                    }
                    if (pReporte.lstColumnaReporte != null)
                    {
                        foreach (ColumnaReporte eColumnaReporte in pReporte.lstColumnaReporte)
                        {
                            eColumnaReporte.idreporte = pReporte.idreporte;
                            if (eColumnaReporte.idcolumna != -1)
                                DAReporte.CrearColumna(eColumnaReporte, vUsuario);
                        }
                    }
                    if (pReporte.lstOrden != null)
                    {
                        foreach (Orden eOrden in pReporte.lstOrden)
                        {
                            eOrden.idreporte = pReporte.idreporte;
                            if (eOrden.idorden != -1)
                                DAReporte.CrearOrden(eOrden, vUsuario);
                        }
                    }
                    if (pReporte.lstGrupo != null)
                    {
                        foreach (Grupo eGrupo in pReporte.lstGrupo)
                        {
                            eGrupo.idreporte = pReporte.idreporte;
                            if (eGrupo.idgrupo != -1)
                                DAReporte.CrearGrupo(eGrupo, vUsuario);
                        }
                    }
                    if (pReporte.lstUsuarios != null)
                    {
                        foreach (UsuariosReporte eUsuario in pReporte.lstUsuarios)
                        {
                            eUsuario.idreporte = pReporte.idreporte;
                            if (eUsuario.autorizar == true)
                                DAReporte.CrearUsuario(eUsuario, vUsuario);
                        }
                    }
                    if (pReporte.lstPerfil != null)
                    {
                        foreach (PerfilReporte ePerfil in pReporte.lstPerfil)
                        {
                            ePerfil.idreporte = pReporte.idreporte;
                            if (ePerfil.autorizar == true)
                                DAReporte.CrearPerfil(ePerfil, vUsuario);
                        }
                    }
                    if (pReporte.lstParametros != null)
                    {
                        foreach (Parametros eParametros in pReporte.lstParametros)
                        {
                            eParametros.idreporte = pReporte.idreporte;
                            DAReporte.CrearParametro(eParametros, vUsuario);
                        }
                    }
                    if (pReporte.lstPlantilla != null)
                    {
                        foreach (Plantilla ePlantilla in pReporte.lstPlantilla)
                        {
                            ePlantilla.idreporte = pReporte.idreporte;
                            if (ePlantilla.idplantilla != -1)
                                DAReporte.CrearPlantilla(ePlantilla, vUsuario);
                        }
                    }

                    ts.Complete();
                }

                return pReporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ModificarReporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un Reporte
        /// </summary>
        /// <param name="pId">Identificador de Reporte</param>
        public void EliminarReporte(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAReporte.EliminarReporte(pId, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "EliminarReporte", ex);
            }
        }

        /// <summary>
        /// Obtiene un Reporte
        /// </summary>
        /// <param name="pId">Identificador de Reporte</param>
        /// <returns>Entidad Reporte</returns>
        public Reporte ConsultarReporte(Int64 pId, Usuario vUsuario)
        {
            try
            {
                Reporte reporte = new Reporte();

                reporte = DAReporte.ConsultarReporte(pId, vUsuario);
                Tabla pTabla = new Tabla();
                pTabla.idreporte = reporte.idreporte;
                reporte.lstTablas = DAReporte.ListarTabla(pTabla, vUsuario);
                Encadenamiento pEncadenamiento = new Encadenamiento();
                pEncadenamiento.idreporte = reporte.idreporte;
                reporte.lstEncadenamiento = DAReporte.ListarEncadenamiento(pEncadenamiento, vUsuario);
                Condicion pCondicion = new Condicion();
                pCondicion.idreporte = reporte.idreporte;
                reporte.lstCondicion = DAReporte.ListarCondicion(pCondicion, vUsuario);
                ColumnaReporte pColumnaReporte = new ColumnaReporte();
                pColumnaReporte.idreporte = reporte.idreporte;
                reporte.lstColumnaReporte = DAReporte.ListarColumna(pColumnaReporte, vUsuario);
                Orden pOrden = new Orden();
                pOrden.idreporte = reporte.idreporte;
                reporte.lstOrden = DAReporte.ListarOrden(pOrden, vUsuario);
                Grupo pGrupo = new Grupo();
                pGrupo.idreporte = reporte.idreporte;
                reporte.lstGrupo = DAReporte.ListarGrupo(pGrupo, vUsuario);
                Plantilla pPlantilla = new Plantilla();
                pPlantilla.idreporte = reporte.idreporte;
                reporte.lstPlantilla = DAReporte.ListarPlantilla(pPlantilla, vUsuario);

                return reporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ConsultarReporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pReporte">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Usuario obtenidos</returns>
        public List<Reporte> ListarReporte(Reporte pReporte, Usuario vUsuario)
        {
            try
            {
                return DAReporte.ListarReporte(pReporte, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ListarReporte", ex);
                return null;
            }
        }

        public List<Reporte> ListarReporteUsuario(Usuario vUsuario)
        {
            try
            {
                return DAReporte.ListarReporteUsuario(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ListarReporteUsuario", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return DAReporte.ObtenerSiguienteCodigo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ObtenerSiguienteCodigo", ex);
                return 0;
            }
        }

        public List<Formato> ListarFormato(Formato pFormato, Usuario vUsuario)
        {
            try
            {
                return DAReporte.ListarFormato(pFormato, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ListarFormato", ex);
                return null;
            }
        }

        public Parametros CrearParametro(Parametros pParametros, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pParametros = DAReporte.CrearParametro(pParametros, vUsuario);

                    ts.Complete();
                }

                return pParametros;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "CrearParametro", ex);
                return null;
            }
        }

        public void EliminarParametro(Int64 pIdReporte, Int64 pIdParametro, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAReporte.EliminarParametro(pIdReporte, pIdParametro, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "EliminarParametro", ex);
            }
        }

        public Parametros ModificarParametro(Parametros pParametros, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pParametros = DAReporte.ModificarParametro(pParametros, vUsuario);

                    ts.Complete();
                }

                return pParametros;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ModificarParametro", ex);
                return null;
            }
        }

        public List<Parametros> ListarParametro(Parametros pParametros, Usuario vUsuario)
        {
            try
            {
                return DAReporte.ListarParametro(pParametros, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ListarParametro", ex);
                return null;
            }
        }

        public List<UsuariosReporte> ListarUsuarios(UsuariosReporte pUsuario, Usuario vUsuario)
        {
            try
            {
                return DAReporte.ListarUsuarios(pUsuario, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ListarUsuarios", ex);
                return null;
            }
        }

        public List<PerfilReporte> ListarPerfil(PerfilReporte pPerfil, Usuario vUsuario)
        {
            try
            {
                return DAReporte.ListarPerfil(pPerfil, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ListarPerfil", ex);
                return null;
            }
        }

        public UsuariosReporte CrearUsuario(UsuariosReporte pUsuariosReporte, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pUsuariosReporte = DAReporte.CrearUsuario(pUsuariosReporte, vUsuario);

                    ts.Complete();
                }

                return pUsuariosReporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "CrearUsuario", ex);
                return null;
            }
        }

        public void EliminarUsuario(Int64 pIdReporte, Int64 pIdUsuario, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAReporte.EliminarUsuario(pIdReporte, pIdUsuario, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "EliminarUsuario", ex);
            }
        }

        public List<Tabla> ListarTablaBase(Tabla pTabla, Usuario vUsuario)
        {
            try
            {
                return DAReporte.ListarTablaBase(pTabla, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ListarTablaBase", ex);
                return null;
            }
        }

        public List<Columna> ListarColumnaBase(string pTabla, Columna pColumna, Usuario vUsuario)
        {
            try
            {
                return DAReporte.ListarColumnaBase(pTabla, pColumna, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ListarColumna", ex);
                return null;
            }
        }

        public Columna ConsultarColumna(string pTabla, string pColumna, Usuario vUsuario)
        {
            try
            {
                return DAReporte.ConsultarColumna(pTabla, pColumna, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ConsultarColumna", ex);
                return null;
            }
        }

        public List<Xpinn.Reporteador.Entities.Lista> ListarLista(Xpinn.Reporteador.Entities.Lista pLista, Usuario vUsuario)
        {
            try
            {
                return DAReporte.ListarLista(pLista, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ListarLista", ex);
                return null;
            }
        }

        public List<Xpinn.Reporteador.Entities.Lista> ListarReporteLista(string filtro,Xpinn.Reporteador.Entities.Lista pLista, Usuario vUsuario)
        {
            try
            {
                return DAReporte.ListarReporteLista(filtro, pLista, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ListarLista", ex);
                return null;
            }
        }

        

        public Lista ConsultarLista(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return DAReporte.ConsultarLista(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ConsultarLista", ex);
                return null;
            }
        }

        public List<Plantilla> ListarPlantilla(Plantilla pPlantilla, Usuario vUsuario)
        {
            try
            {
                return DAReporte.ListarPlantilla(pPlantilla, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ListarPlantilla", ex);
                return null;
            }
        }







        public Lista CrearReporeLista(Lista pLista, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pLista = DAReporte.CrearReporteLista(pLista, pusuario);

                    ts.Complete();

                }

                return pLista;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ListaBusiness", "CrearLista", ex);
                return null;
            }
        }


        public Lista ModificarReporteLista(Lista pLista, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pLista = DAReporte.ModificarReporteLista(pLista, pusuario);

                    ts.Complete();

                }

                return pLista;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ListaBusiness", "ModificarLista", ex);
                return null;
            }
        }


        public void EliminarReporteLista(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAReporte.EliminarReporteLista(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ListaBusiness", "EliminarLista", ex);
            }
        }


        public Lista ConsultarReporteLista(Int64 pId, Usuario pusuario)
        {
            try
            {
                Lista Lista = new Lista();
                Lista = DAReporte.ConsultarReporteLista(pId, pusuario);
                return Lista;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ListaBusiness", "ConsultarLista", ex);
                return null;
            }
        }


        public List<TransaccionEfectivo> ListarfechaCierrHist(Usuario vUsuario)
        {
            // Listas fechas de períodos ya cerrados
            List<TransaccionEfectivo> lstFecha = new List<TransaccionEfectivo>();
            lstFecha = DAReporte.ListarfechaCierrHist(vUsuario); 
            return lstFecha;
        }


        
        public List<Credito> ListarCreditosDesembolsados(Credito pCredito, Usuario pUsuario, String filtro)
        {
            try
            {
                return DAReporte.ListarCreditosDesembolsados(pCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ListarCreditos", ex);
                return null;
            }
        }



    }
}