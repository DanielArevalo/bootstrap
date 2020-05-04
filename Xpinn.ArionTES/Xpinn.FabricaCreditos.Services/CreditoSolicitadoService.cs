using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;
using System.Data;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicio para Aprobador
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CreditoSolicitadoService
    {
        private CreditoSolicitadoBusiness BOCredito;
        private ExcepcionBusiness BOExcepcion;


        /// <summary>
        /// Constructor del servicio para Caja
        /// </summary>
        public CreditoSolicitadoService()
        {
            BOCredito = new CreditoSolicitadoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100137"; } }
        public string CodigoProgramaRotativo { get { return "100506"; } }
        public string CodigoProgramaRotativoAprobar { get { return "100508"; } }
        public string CodigoProgramaRotativoReporte { get { return "100509"; } }
        public string CodigoProgramaRotativoExtracto { get { return "100513"; } }

        // Codigo de programa para analisis de credito
        public string CodigoProgramaAnalisisCredito { get { return "100165"; } }

        /// <summary>
        /// Obtiene la lista de creditos solicitados
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de creditos obtenidos</returns>
        public List<CreditoSolicitado> ListarCreditos(CreditoSolicitado pCredito, Usuario pUsuario, String filtro = "")
        {
            try
            {
                return BOCredito.ListarCreditos(pCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoService", "ListarCreditos", ex);
                return null;
            }
        }

        public List<CreditoSolicitado> ListaCreditosFiltradosEstadoV(CreditoSolicitado pCredito, Usuario pUsuario, String filtro = "")
        {
            try
            {
                return BOCredito.ListaCreditosFiltradosEstadoV(pCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoService", "ListaCreditosFiltradosEstadoV", ex);
                return null;
            }
        }

        
        /// <summary>
        /// Obtiene la lista de creditos rotativos
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de creditos obtenidos</returns>
        public List<CreditoSolicitado>ListarCreditosRotativos(CreditoSolicitado pCredito,DateTime pFecha, Usuario pUsuario, String filtro)
        {
            try
            {
                return BOCredito.ListarCreditosRotativos(pCredito, pFecha,pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoService", "ListarCreditosRotativos", ex);
                return null;
            }
        }

        public List<CreditoSolicitado> ListarCreditosRotativosSolicitados(CreditoSolicitado pCredito, Usuario pUsuario, String filtro)
        {
            try
            {
                return BOCredito.ListarCreditosRotativosSolicitados(pCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoService", "ListarCreditosRotativosSolicitados", ex);
                return null;
            }
        }
        public CreditoSolicitado ConsultarCreditosRotativos(CreditoSolicitado pCredito,  Usuario pUsuario)
        {
            try
            {
                return BOCredito.ConsultarCreditosRotativos(pCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoService", "ConsultarCreditosRotativos", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de aprobadores
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de Aprobadores obtenidos</returns>
        public CreditoSolicitado ConsultarCredito(CreditoSolicitado pCredito, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ConsultarCredito(pCredito, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoService", "ConsultarCredito", ex);
                return null;
            }
        }

        public Imagenes ObtenerSoporte(long pId, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ObtenerSoporte(pId, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoService", "ObtenerSoporte", ex);
                return null;
            }
        }

        public CreditoSolicitado ConsultarParamAprobacion(CreditoSolicitado pCredito, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ConsultarParamAprobacion(pCredito, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoService", "ConsultarParamAprobacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Aprueba el credito solicitado
        /// </summary>
        /// <param name="pEntity">Entidad Credito solicitado</param>
        /// <returns>Entidad creada</returns>
        public CreditoSolicitado AprobarCredito(CreditoSolicitado pCredito, Usuario pUsuario, ref string sError)
        {
            try
            {
                return BOCredito.AprobarCredito(pCredito, pUsuario, ref sError);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoService", "AprobarCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Aprueba el credito solicitado
        /// </summary>
        /// <param name="pEntity">Entidad Credito solicitado</param>
        /// <returns>Entidad creada</returns>
        public CreditoSolicitado AprobarCreditoModificando(CreditoSolicitado pCredito, bool rptaDistribucion, List<Credito_Giro> lstDistribucion, int pOpcion, CreditoOrdenServicio pCredOrden, Usuario pUsuario, 
            ref string sError, List<CreditoRecoger> lstCreditoRecoger = null, List<CreditoEmpresaRecaudo> lstDetalle = null, bool cambioTasa = false, List<CreditoSolicitado> lstCambiTasa=null)
        {
            try
            {
                return BOCredito.AprobarCreditoModificando(pCredito, rptaDistribucion, lstDistribucion, pOpcion, pCredOrden, pUsuario, ref sError, lstCreditoRecoger, lstDetalle, cambioTasa, lstCambiTasa);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoService", "AprobarCreditoModificando", ex);
                sError = ex.Message;
                return null;
            }
        }
       

        /// <summary>
        /// Aplaza el credito solicitado
        /// </summary>
        /// <param name="pEntity">Entidad Credito solicitado</param>
        /// <returns>Entidad creada</returns>
        public CreditoSolicitado AplazarCredito(CreditoSolicitado pCredito, Motivo motivo, Usuario pUsuario)
        {
            try
            {
                return BOCredito.AplazarCredito(pCredito, motivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoService", "AplazarCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Aplaza el credito solicitado
        /// </summary>
        /// <param name="pEntity">Entidad Credito solicitado</param>
        /// <returns>Entidad creada</returns>
        public CreditoSolicitado AprobarCreditoRotativo(CreditoSolicitado pCredito,Usuario pUsuario,ref string sError)
        {
            try
            {
                return BOCredito.AprobarCreditoRotativo(pCredito, pUsuario, ref sError);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoService", "AprobarCreditoRotativo", ex);
               sError = ex.Message;
                return null;
            }
            
        }

        /// <summary>
        /// Niega el credito solicitado
        /// </summary>
        /// <param name="pEntity">Entidad Credito solicitado</param>
        /// <returns>Entidad creada</returns>
        public CreditoSolicitado NegarCredito(CreditoSolicitado pCredito, Motivo motivo, Usuario pUsuario)
        {
            try
            {
                return BOCredito.NegarCredito(pCredito, motivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoService", "NegarCredito", ex);
                return null;
            }
        }


        public List<CreditoSolicitado> ListarEstadosCredito(Usuario pUsuario)
        {
            try
            {
                return BOCredito.ListarEstadosCredito(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoService", "ListarEstadosCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de aprobadores
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de Aprobadores obtenidos</returns>
        public CreditoSolicitado ConsultarCodigodelProceso(CreditoSolicitado pCredito, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ConsultarCodigodelProceso(pCredito, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoService", "ConsultarCodigodelProceso", ex);
                return null;
            }
        }
        //Agregado para consultar proceso anterior
        public ControlTiempos ConsultarProcesoAnterior(string estado, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ConsultarProcesoAnterior(estado, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoService", "ConsultarProcesoAnterior", ex);
                return null;
            }
        }


        public DescuentosCredito modificardeduccionesCredito(DescuentosCredito entidad, Usuario pUsuario)
        {
            try
            {
                return BOCredito.modificardeduccionesCredito(entidad, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoService", "modificardeduccionesCredito", ex);
                return null;
            }
        }

        #region METODO DE ATENCION AL CLIENTE

        public SolicitudCreditoAAC CrearSolicitudCreditoAAC(SolicitudCreditoAAC pSolicitudCreditoAAC, Usuario vUsuario, Int32 pOpcion, List<DocumentosAnexos> lstDocumentos = null, List<CuotasExtras> lstCuotasExtras = null)
        {
            try
            {
                return BOCredito.CrearSolicitudCreditoAAC(pSolicitudCreditoAAC, vUsuario, pOpcion, lstDocumentos, lstCuotasExtras);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoService", "CrearSolicitudCreditoAAC", ex);
                return null;
            }
        }


        public Int64 ConfirmarSolicitudCreditoAutomatico(SolicitudCreditoAAC pSolicitudCreditoAAC, Usuario vUsuario)
        {
            try
            {
                return BOCredito.ConfirmarSolicitudCreditoAutomatico(pSolicitudCreditoAAC, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoService", "ConfirmarSolicitudCreditoAutomatico", ex);
                return 0;
            }
        }

        public SolicitudCreditoAAC CrearSolicitudCreditoProveedor(SolicitudCreditoAAC pSolicitudCreditoAAC, Usuario vUsuario, List<DocumentosAnexos> lstDocumentos = null)
        {
            try
            {
                return BOCredito.CrearSolicitudCreditoProveedor(pSolicitudCreditoAAC, vUsuario, lstDocumentos);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoService", "CrearSolicitudCreditoProveedor", ex);
                return null;
            }
        }


        #endregion

        public List<Xpinn.FabricaCreditos.Entities.Imagenes> ListaDocumentosAnexos(int pTipoReferencia, Int64 pNumeroSolicitud, int tipoProducto, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ListaDocumentosAnexos(pTipoReferencia, pNumeroSolicitud, tipoProducto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoService", "ListaDocumentosAnexos", ex);
                return null;
            }
        }

        public byte[] ConsultarDocAnexo(Int64 pIdDocumento, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ConsultarDocAnexo(pIdDocumento, pUsuario);
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
                return BOCredito.ListarDescuentosCredito(pDescuentoscredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoSolicitadoService", "ListarDescuentosCredito", ex);
                return null;
            }
        }


    }
}
