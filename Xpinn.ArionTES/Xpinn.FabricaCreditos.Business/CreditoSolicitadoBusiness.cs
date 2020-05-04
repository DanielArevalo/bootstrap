using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Business
{
    public class CreditoSolicitadoBusiness : GlobalData
    {
        private CreditoSolicitadoData DACredito;
        private LineasCreditoData DADescuentos;
        private CuotasExtrasData DACuotasExtras;
        CreditoData DACreditoData1 ;
        /// <summary>
        /// Constructor del objeto de negocio para Caja
        /// </summary>
        public CreditoSolicitadoBusiness()
        {
            DACredito = new CreditoSolicitadoData();
            DADescuentos = new LineasCreditoData(); ;
            DACreditoData1 = new CreditoData();
            DACuotasExtras = new CuotasExtrasData();
        }

        /// <summary>
        /// Obtiene la lista de creditos solicitados
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de creditos obtenidos</returns>
        public List<CreditoSolicitado> ListarCreditos(CreditoSolicitado pCredito, Usuario pUsuario, String filtro = "")
        {
            try
            {
                return DACredito.ListarCreditos(pCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoBusiness", "ListarCreditos", ex);
                return null;
            }
        }

        public List<CreditoSolicitado> ListaCreditosFiltradosEstadoV(CreditoSolicitado pCredito, Usuario pUsuario, String filtro = "")
        {
            try
            {
                return DACredito.ListaCreditosFiltradosEstadoV(pCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoBusiness", "ListaCreditosFiltradosEstadoV", ex);
                return null;
            }
        }

        /// <summary> 
        /// Obtiene la lista de creditos Rotativos
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de creditos obtenidos</returns>
        public List<CreditoSolicitado> ListarCreditosRotativos(CreditoSolicitado pCredito, DateTime pFecha, Usuario pUsuario, String filtro)
        {
            try
            {
                return DACredito.ListarCreditosRotativos(pCredito, pFecha, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoBusiness", "ListarCreditosRotativos", ex);
                return null;
            }
        }

        public List<CreditoSolicitado> ListarCreditosRotativosSolicitados(CreditoSolicitado pCredito, Usuario pUsuario, String filtro)
        {
            try
            {
                return DACredito.ListarCreditosRotativosSolicitados(pCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoBusiness", "ListarCreditosRotativos", ex);
                return null;
            }
        }



        public CreditoSolicitado ConsultarCreditosRotativos(CreditoSolicitado pCredito, Usuario pUsuario)
        {
            try
            {
                return DACredito.ConsultarCreditosRotativos(pCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoBusiness", "ConsultarCreditosRotativos", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene los datos de un Aprobador
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de Aprobadores obtenidos</returns>
        public CreditoSolicitado ConsultarCredito(CreditoSolicitado pCredito, Usuario pUsuario)
        {
            try
            {
                CreditoSolicitado eCredito = new CreditoSolicitado();
                eCredito = DACredito.ConsultarCredito(pCredito, pUsuario);
                if (eCredito != null)
                {
                    if (eCredito.NumeroCredito != 0)
                    {
                        // Traer los atributos descontados del crédito
                        eCredito.lstDescuentosCredito = new List<DescuentosCredito>();
                        DescuentosCredito eDescCred = new DescuentosCredito();
                        eDescCred.numero_radicacion = eCredito.NumeroCredito;
                        eDescCred.cod_linea = Convert.ToInt32(eCredito.cod_linea_credito);
                        eCredito.lstDescuentosCredito = DACredito.ListarDescuentosCredito(eDescCred, pUsuario);
                        // Traer los atributos financiados del crédito
                        eCredito.lstAtributosCredito = new List<AtributosCredito>();
                        AtributosCredito eAtrCred = new AtributosCredito();
                        eAtrCred.numero_radicacion = eCredito.NumeroCredito;
                        eCredito.lstAtributosCredito = DACredito.ListarAtributosCredito(eAtrCred, pUsuario);
                    }
                }
                return eCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoBusiness", "ConsultarCredito", ex);
                return null;
            }
        }


        public Xpinn.FabricaCreditos.Entities.Imagenes ObtenerSoporte(long pId, Usuario pUsuario)
        {
            try
            {
                return DACredito.ObtenerSoporte(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoBusiness", "ObtenerSoporte", ex);
                return null;
            }
        }


        public CreditoSolicitado ConsultarParamAprobacion(CreditoSolicitado pCredito, Usuario pUsuario)
        {
            try
            {
                CreditoSolicitado eCredito = new CreditoSolicitado();
                eCredito = DACredito.ConsultarParamAprobacion(pCredito, pUsuario);
               
                return eCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoBusiness", "ConsultarParamAprobacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Aprueba el credito solicitado
        /// </summary>
        /// <param name="pEntity">Entidad CreditoSolicitado</param>
        /// <returns>Entidad creada</returns>
        public CreditoSolicitado AprobarCredito(CreditoSolicitado pCredito, Usuario pUsuario, ref string sError)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    codeudoresData DACodeudores = new codeudoresData();
                    if (pCredito.lstCodeudores.Count > 0)
                    {
                        if (DACodeudores.BorrarCodeudoresCredito(pCredito.NumeroCredito, pUsuario) == true)
                        {
                            foreach (Persona1 gItem in pCredito.lstCodeudores)
                            {
                                if (gItem.cod_persona != 0)
                                {
                                    codeudores eCodeudores = new codeudores();
                                    eCodeudores.idcodeud = 0;
                                    eCodeudores.numero_radicacion = pCredito.NumeroCredito;
                                    eCodeudores.codpersona = gItem.cod_persona;
                                    eCodeudores.tipo_codeudor = "C";
                                    eCodeudores.parentesco = 1;
                                    eCodeudores.opinion = "B";
                                    eCodeudores.responsabilidad = "S";
                                    eCodeudores = DACodeudores.CrearCodeudoresCredito(eCodeudores, pUsuario);
                                }
                            }
                        }
                        else
                        {
                            sError = "No pudo borrar los codeuores del crédito";
                        }
                    }
                    CuotasExtrasData DACuoExt = new CuotasExtrasData();
                    if (pCredito.lstCuoExt.Count > 0)
                    {
                        foreach (CuotasExtras gItem in pCredito.lstCuoExt)
                        {
                            if (gItem.valor != 0 && gItem.valor != null)
                            {
                                gItem.cod_cuota = 0;
                                gItem.numero_radicacion = pCredito.NumeroCredito;
                                gItem.saldo_capital = 0;
                                gItem.saldo_interes = 0;
                                gItem.valor_capital = gItem.valor;
                                gItem.valor_interes = 0;
                               // DACuoExt.CrearCuotasExtras(gItem, pUsuario);
                            }
                        }
                    }
                    pCredito = DACredito.AprobarCredito(pCredito, pUsuario, ref sError);
                    ts.Complete();
                }
                return pCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoBusiness", "AprobarCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Aprueba el credito solicitado
        /// </summary>
        /// <param name="pEntity">Entidad CreditoSolicitado</param>
        /// <returns>Entidad creada</returns>
        public CreditoSolicitado AprobarCreditoModificando(CreditoSolicitado pCredito, bool rptaDistribucion, List<Credito_Giro> lstDistribucion, int pOpcion, CreditoOrdenServicio pCredOrden, Usuario pUsuario,
            ref string sError, List<CreditoRecoger> lstCreditoRecoger = null, List<CreditoEmpresaRecaudo> lstDetalle = null, bool cambioTasa = false, List<CreditoSolicitado> lstCambiTasa=null)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DescuentosCredito DADescuentos = new DescuentosCredito();
                    codeudoresData DACodeudores = new codeudoresData();
                    CreditoData DACreditoData = new CreditoData();
                    CreditoRecogerData DACreditoRecoger = new CreditoRecogerData();

                    if (lstCreditoRecoger != null && lstCreditoRecoger.Count > 0)
                    {
                        foreach (var creditoRecoger in lstCreditoRecoger)
                        {
                            if (creditoRecoger.solo_borrar)
                            {
                                DACreditoRecoger.EliminarCreditoRecoger(creditoRecoger.numero_credito, pUsuario); 
                            }
                            else
                            {
                                DACreditoRecoger.EliminarCreditoRecoger(creditoRecoger.numero_credito, pUsuario);
                                DACreditoRecoger.CrearCreditoRecoger(creditoRecoger, pUsuario);
                            }
                        }
                    }

                    DACreditoData.cambiolinea(pCredito.NumeroCredito, pCredito.cod_linea_credito.ToString(), pUsuario);

                    if (lstDetalle != null && lstDetalle.Count > 0)
                    {
                        foreach (CreditoEmpresaRecaudo rEmpre in lstDetalle)
                        {
                            if (rEmpre.porcentaje != 0)
                            {
                                rEmpre.numero_radicacion = pCredito.NumeroCredito;
                                DACreditoData.CrearModEmpresa_Recaudo(rEmpre, pUsuario, 1);
                            }
                        }
                    }
                    
                    if (lstCambiTasa != null && lstCambiTasa.Count > 0)
                    { 
                        foreach (CreditoSolicitado rTasa in lstCambiTasa)
                        {
                            if (cambioTasa)
                            {
                                rTasa.NumeroCredito = pCredito.NumeroCredito;
                                DACreditoData.cambiotasaFija(rTasa, pUsuario);
                            }
                        }
                    }

                    if (pCredito.lstCodeudores.Count > 0)
                    {
                        if (DACodeudores.BorrarCodeudoresCredito(pCredito.NumeroCredito, pUsuario) == true)
                        {
                            codeudores eCodeudores;
                            foreach (Persona1 gItem in pCredito.lstCodeudores)
                            {
                                if (gItem.cod_persona != 0)
                                {
                                    eCodeudores = new codeudores();
                                    eCodeudores.idcodeud = 0;
                                    eCodeudores.numero_radicacion = pCredito.NumeroCredito;
                                    eCodeudores.codpersona = gItem.cod_persona;
                                    eCodeudores.tipo_codeudor = "C";
                                    eCodeudores.parentesco = 1;
                                    eCodeudores.opinion = "B";
                                    eCodeudores.responsabilidad = "S";
                                    eCodeudores.orden = gItem.orden;
                                    eCodeudores = DACodeudores.CrearCodeudoresCredito(eCodeudores, pUsuario);
                                }
                            }
                        }
                        else
                        {
                            sError = "No pudo borrar los codeuores del crédito";
                        }
                    }
                    CuotasExtrasData DACuoExt = new CuotasExtrasData();
                    if (pCredito.lstCuoExt != null)
                    { 
                        if (pCredito.lstCuoExt.Count > 0)
                        {
                            foreach (CuotasExtras gItem in pCredito.lstCuoExt)
                            {
                                if (gItem.valor != 0 && gItem.valor != null)
                                {
                                    gItem.cod_cuota = 0;
                                    gItem.numero_radicacion = pCredito.NumeroCredito;
                                    gItem.saldo_capital = 0;
                                    gItem.saldo_interes = 0;
                                    gItem.valor_capital = gItem.valor;
                                    gItem.valor_interes = 0;
                                   // DACuoExt.CrearCuotasExtras(gItem, pUsuario); //se cambio para que no re inserte las cuotas extras
                                }
                            }
                        }
                    }
                    if (rptaDistribucion == true)
                    {
                        if (lstDistribucion.Count > 0)
                        {
                            Credito_GiroData DACreditoGiro = new Credito_GiroData();
                            foreach (Credito_Giro nCredito in lstDistribucion)
                            {
                                Credito_Giro pEntidad = new Credito_Giro();
                                nCredito.numero_radicacion = pCredito.NumeroCredito;
                                pEntidad = DACreditoGiro.CrearCredito_giro(nCredito, pUsuario);
                            }
                        }
                    }
                    if (pCredito.lstDescuentosCredito != null)
                    {
                        foreach (DescuentosCredito variable in pCredito.lstDescuentosCredito)
                        {
                            variable.numero_radicacion = Convert.ToInt64(pCredito.NumeroCredito.ToString().Trim());

                            DADescuentos = DACredito.modificardeduccionesCredito(variable, pUsuario);
                        }
                    }


                    pCredito = DACredito.AprobarCreditoModificando(pCredito, pUsuario);

                    CreditoData DACreditoGeneral = new CreditoData();
                    CreditoOrdenServicio pOrden = new CreditoOrdenServicio();
                    if (pOpcion == 1)
                    {//CREAR
                        pOrden = DACreditoGeneral.CrearCreditoOrdenServicio(pCredOrden, pUsuario);
                    }
                    else if (pOpcion == 2)
                    {//MODIFICAR
                        pOrden = DACreditoGeneral.ModificarCreditoOrdenServicio(pCredOrden, pUsuario);
                    }
                    else if (pOpcion == 3)
                    {//ELIMINAR
                        DACreditoGeneral.EliminarCreditoOrdenServicio(pCredOrden.idordenservicio, pCredOrden.numero_radicacion, pUsuario);
                    }

                    ts.Complete();
                }
                return pCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoBusiness", "AprobarCreditoModificando", ex);
                sError = ex.Message;
                return null;
            }
        }

        public SolicitudCreditoAAC CrearSolicitudCreditoProveedor(SolicitudCreditoAAC pSolicitudCreditoAAC, Usuario vUsuario, List<DocumentosAnexos> lstDocumentos = null)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pSolicitudCreditoAAC = DACredito.CrearSolicitudCreditoProveedor(pSolicitudCreditoAAC, vUsuario);
                    long pNumSolicitud = pSolicitudCreditoAAC.numerosolicitud;
                    if (lstDocumentos != null)
                    {
                        if (lstDocumentos.Count > 0)
                        {
                            Xpinn.Imagenes.Data.ImagenesORAData DADocumento = new Imagenes.Data.ImagenesORAData();
                            foreach (DocumentosAnexos nDocument in lstDocumentos)
                            {
                                nDocument.numerosolicitud = pNumSolicitud;
                                DocumentosAnexos pEntidad = new DocumentosAnexos();
                                pEntidad = DADocumento.CrearDocumentosAnexos(nDocument, vUsuario);
                            }
                        }
                    }
                    ts.Complete();
                }
                return pSolicitudCreditoAAC;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoBusiness", "CrearSolicitudCreditoProveedor", ex);
                return null;
            }
        }


        /// <summary>
        /// Aplaza el credito solicitado
        /// </summary>
        /// <param name="pEntity">Entidad CreditoSolicitado</param>
        /// <returns>Entidad creada</returns>
        public CreditoSolicitado AplazarCredito(CreditoSolicitado pCredito, Motivo motivo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCredito = DACredito.AplazarCredito(pCredito, motivo, pUsuario);
                    ts.Complete();
                }
                return pCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoBusiness", "AplazarCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Niega el credito solicitado
        /// </summary>
        /// <param name="pEntity">Entidad CreditoSolicitado</param>
        /// <returns>Entidad creada</returns>
        public CreditoSolicitado NegarCredito(CreditoSolicitado pCredito, Motivo motivo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCredito = DACredito.NegarCredito(pCredito, motivo, pUsuario);
                    ts.Complete();
                }
                return pCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoBusiness", "NegarCredito", ex);
                return null;
            }
        }



        /// <returns>Entidad creada</returns>
        public CreditoSolicitado AprobarCreditoRotativo(CreditoSolicitado pCredito, Usuario pUsuario, ref string sError)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DescuentosCredito pdescuentos = new DescuentosCredito();
                    codeudoresData DACodeudores = new codeudoresData();
                    if (pCredito.lstCodeudores.Count > 0)
                    {
                        if (DACodeudores.BorrarCodeudoresCredito(pCredito.NumeroCredito, pUsuario) == true)
                        {
                            foreach (Persona1 gItem in pCredito.lstCodeudores)
                            {
                                if (gItem.cod_persona != 0)
                                {
                                    codeudores eCodeudores = new codeudores();
                                    eCodeudores.idcodeud = 0;
                                    eCodeudores.numero_radicacion = pCredito.NumeroCredito;
                                    eCodeudores.codpersona = gItem.cod_persona;
                                    eCodeudores.tipo_codeudor = "C";
                                    eCodeudores.parentesco = 1;
                                    eCodeudores.opinion = "B";
                                    eCodeudores.responsabilidad = "S";
                                    eCodeudores = DACodeudores.CrearCodeudoresCredito(eCodeudores, pUsuario);
                                }
                            }
                        }
                        else
                        {
                            sError = "No pudo borrar los codeuores del crédito";
                        }

                        if (pCredito.lstDescuentosCredito != null)
                        {
                            foreach (DescuentosCredito variable in pCredito.lstDescuentosCredito)
                            {
                                variable.numero_radicacion = Convert.ToInt64(pCredito.NumeroCredito.ToString().Trim());

                                pdescuentos = DACredito.modificardeduccionesCredito(variable, pUsuario);
                            }
                        }
                    }

                    pCredito = DACredito.AprobarCreditoModificando(pCredito, pUsuario);
                    ts.Complete();
                }
                return pCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoBusiness", "AprobarCreditoRotativo", ex);
                sError = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de estados de creditos
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de creditos obtenidos</returns>
        public List<CreditoSolicitado> ListarEstadosCredito(Usuario pUsuario)
        {
            try
            {
                return DACredito.ListarEstadosCredito(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoBusiness", "ListarEstadosCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene los datos de un Aprobador
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de Aprobadores obtenidos</returns>
        public CreditoSolicitado ConsultarCodigodelProceso(CreditoSolicitado pCredito, Usuario pUsuario)
        {
            try
            {
                CreditoSolicitado eCredito = new CreditoSolicitado();
                eCredito = DACredito.ConsultarCodigodelProceso(pCredito, pUsuario);

                return eCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoBusiness", "ConsultarCodigodelProceso", ex);
                return null;
            }
        }
        //Agregado para consultar proceso anterior
        public ControlTiempos ConsultarProcesoAnterior(string estado, Usuario pUsuario)
        {
            try
            {
                return DACredito.ConsultarProcesoAnterior(estado, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoBusiness", "ConsultarProcesoAnterior", ex);
                return null;
            }
        }



        public DescuentosCredito modificardeduccionesCredito(DescuentosCredito entidad, Usuario pUsuario)
        {
            try
            {
                return DACredito.modificardeduccionesCredito(entidad, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoBusiness", "modificardeduccionesCredito", ex);
                return null;
            }
        }

        #region METODO DE ATENCION AL CLIENTE

        public SolicitudCreditoAAC CrearSolicitudCreditoAAC(SolicitudCreditoAAC pSolicitudCreditoAAC, Usuario vUsuario, Int32 pOpcion, List<DocumentosAnexos> lstDocumentos = null, List<CuotasExtras> lstCuotasExtras = null)
        {
            Int64 pNumSolicitud = 0;
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
                {
                    pSolicitudCreditoAAC = DACredito.CrearSolicitudCreditoAAC(pSolicitudCreditoAAC, vUsuario, pOpcion);
                    pNumSolicitud = pSolicitudCreditoAAC.numerosolicitud;
                    if (lstDocumentos != null)
                    {
                        if (lstDocumentos.Count > 0)
                        {
                            Xpinn.Imagenes.Data.ImagenesORAData DADocumento = new Imagenes.Data.ImagenesORAData();
                            foreach (DocumentosAnexos nDocument in lstDocumentos)
                            {
                                nDocument.numerosolicitud = pNumSolicitud;
                                DocumentosAnexos pEntidad = new DocumentosAnexos();
                                pEntidad = DADocumento.CrearDocumentosAnexos(nDocument, vUsuario);
                            }
                        }
                    }
                    if (lstCuotasExtras != null)
                    {
                        if (lstCuotasExtras.Count > 0)
                        {
                            CuotasExtras entidad = null;
                            foreach (CuotasExtras pCuotasExtras in lstCuotasExtras)
                            {
                                pCuotasExtras.numero_radicacion = pNumSolicitud;
                                entidad = DACuotasExtras.CrearSolicitudCuotasExtras(pCuotasExtras, vUsuario);
                            }
                        }
                    }
                    ts.Complete();
                }
                return pSolicitudCreditoAAC;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoBusiness", "CrearSolicitudCreditoAAC", ex);
                return null;
            }
        }


        public Int64 ConfirmarSolicitudCreditoAutomatico(SolicitudCreditoAAC pSolicitudCreditoAAC, Usuario vUsuario)
        {
            Int64 pNumRadicado = 0;
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pNumRadicado = DACredito.ConfirmarSolicitudCreditoAutomatico(pSolicitudCreditoAAC, vUsuario);                    
                    ts.Complete();
                }
                return pNumRadicado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoBusiness", "ConfirmarSolicitudCreditoautomatico", ex);
                return 0;
            }
        }
        #endregion

        public List<Xpinn.FabricaCreditos.Entities.Imagenes> ListaDocumentosAnexos(int pTipoReferencia, Int64 pNumeroSolicitud,int tipoProducto, Usuario pUsuario)
        {
            try
            {
                Xpinn.Imagenes.Data.ImagenesORAData DADocumento = new Imagenes.Data.ImagenesORAData();
                return DADocumento.ListaDocumentosAnexos(pTipoReferencia, pNumeroSolicitud, tipoProducto, pUsuario);                 
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoBusiness", "ListaDocumentosAnexos", ex);
                return null;
            }
        }

        public byte[] ConsultarDocAnexo(Int64 pIdDocumento, Usuario pUsuario)
        {
            try
            {
                Xpinn.Imagenes.Data.ImagenesORAData DADocumento = new Imagenes.Data.ImagenesORAData();
                return DADocumento.ConsultarDocAnexo(pIdDocumento, pUsuario);
            }
            catch (Exception ex)
            {             
                return null;
            }
        }

        public List<DescuentosCredito> ListarDescuentosCredito(DescuentosCredito pDescuentoscredito, Usuario pUsuario)
        {
            try
            {
                return DACredito.ListarDescuentosCredito(pDescuentoscredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoBusiness", "ListarDescuentosCredito", ex);
                return null;
            }
        }


    }
}
