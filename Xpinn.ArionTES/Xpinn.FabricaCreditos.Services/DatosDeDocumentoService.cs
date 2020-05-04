using System;
using System.Collections.Generic;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class DatosDeDocumentoService
    {
        private DatosDeDocumentoBusiness BODatosDeDocumento;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para DatosDeDocumento
        /// </summary>
        public DatosDeDocumentoService()
        {
            BODatosDeDocumento = new DatosDeDocumentoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "CRE"; } }

        /// <summary>
        /// Servicio para crear DatosDeDocumento
        /// </summary>
        /// <param name="pEntity">Entidad DatosDeDocumento</param>
        /// <returns>Entidad DatosDeDocumento creada</returns>
        public List<DatosDeDocumento> ListarDatosDeDocumentoGenerado(Int64 numero_radicacion, Usuario pUsuario)
        {
            try
            {
                return BODatosDeDocumento.ListarDatosDeDocumento(numero_radicacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosDeDocumentoService", "CrearDatosDeDocumentoGenerado", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para crear DatosDeDocumentoFormatoSolictud
        /// </summary>
        /// <param name="pEntity">Entidad DatosDeDocumento</param>
        /// <returns>Entidad DatosDeDocumento creada</returns>
        public List<DatosDeDocumento> ListarDatosDeDocumentoFormato(Int64 numero_radicacion, Int64 tipo_documento, Usuario pUsuario)
        {
            try
            {
                return BODatosDeDocumento.ListarDatosDeDocumentoFormato(numero_radicacion, tipo_documento, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosDeDocumentoService", "ListarDatosDeDocumentoFormato", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para crear DatosDeDocumentoFormatoSolictud
        /// </summary>
        /// <param name="pEntity">Entidad DatosDeDocumento</param>
        /// <returns>Entidad DatosDeDocumento creada</returns>
        public List<DatosDeDocumento> ListarDatosDeDocumentoFormatoCartasMasivas(Int64 numero_radicacion, Usuario pUsuario)
        {
            try
            {
                return BODatosDeDocumento.ListarDatosDeDocumentoFormatoCartasMasivas(numero_radicacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosDeDocumentoService", "ListarDatosDeDocumentoFormatoCartasMasivas", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para crear DatosDeDocumentoFormatoSolictudCodeudores
        /// </summary>
        /// <param name="pEntity">Entidad DatosDeDocumento</param>
        /// <returns>Entidad DatosDeDocumento creada</returns>
        public List<DatosDeDocumento> ListarDatosDeDocumentoFormatoCartasMasivasCodeudor(Int64 numero_radicacion, Usuario pUsuario, Int64 Codeudor)
        {
            try
            {
                return BODatosDeDocumento.ListarDatosDeDocumentoFormatoCartasMasivasCodeudor(numero_radicacion, pUsuario, Codeudor);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosDeDocumentoService", "ListarDatosDeDocumentoFormatoCartasMasivas", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para crear DatosDeDocumento de un CDAT
        /// </summary>
        /// <param name="pCodigoCDAT">Codigo de CDAT</param>
        /// <returns>Entidad DatosDeDocumento creada</returns>
        public List<DatosDeDocumento> ListarDatosDeDocumentoCDAT(Int64 pCodigoCDAT, Usuario pUsuario)
        {
            try
            {
                return BODatosDeDocumento.ListarDatosDeDocumentoReporteCDAT(pCodigoCDAT, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosDeDocumentoService", "CrearDatosDeDocumentoGenerado", ex);
                return null;
            }
        }
        public Documento ConsultarSolicitud(Usuario vUsuario)
        {
            try
            {
                return BODatosDeDocumento.ConsultarSolicitud(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientoServices", "ConsultarAhorroVista", ex);
                return null;
            }
        }
        public Documento CrearDocSolicitud(Documento pEntidad, Usuario pUsuario)
        {
            try
            {
                return BODatosDeDocumento.CrearDocSolicitud(pEntidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudService", "CrearSolicitud", ex);
                return null;
            }

        }
        public List<DatosDeDocumento> ListarVariables(Usuario pUsuario)
        {
            try
            {
                return BODatosDeDocumento.ListarVariables(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosDeDocumentoService", "ListarVariables", ex);
                return null;
            }
        }
    }
}