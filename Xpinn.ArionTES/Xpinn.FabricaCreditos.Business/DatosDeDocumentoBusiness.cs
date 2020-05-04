using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;


namespace Xpinn.FabricaCreditos.Business
{
    /// <summary>
    /// Objeto de negocio para DatosDeDocumento
    /// </summary>
    public class DatosDeDocumentoBusiness : GlobalData
    {
        private DatosDeDocumentoData DADatosDeDocumento;

        /// <summary>
        /// Constructor del objeto de negocio para DatosDeDocumento
        /// </summary>
        public DatosDeDocumentoBusiness()
        {
            DADatosDeDocumento = new DatosDeDocumentoData();
        }


        /// <summary>
        /// Obtiene DatosDeDocumento
        /// </summary>

        public List<DatosDeDocumento> ListarDatosDeDocumento(Int64 numero_radicacion, Usuario pUsuario)
        {
            try
            {
                return DADatosDeDocumento.ListarDatosDeDocumento(numero_radicacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosDeDocumentoBusiness", "ConsultarDatosDeDocumento", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene DatosDeFormatoSolicituddeCredito
        /// </summary>
        public List<DatosDeDocumento> ListarDatosDeDocumentoFormato(Int64 numero_radicacion, Int64 tipo_documento, Usuario pUsuario)
        {
            try
            {
                return DADatosDeDocumento.ListarDatosDeDocumentoFormato(numero_radicacion, tipo_documento, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosDeDocumentoBusiness", "ListarDatosDeDocumentoFormato", ex);
                return null;
            }
        }

        public List<FabricaCreditos.Entities.DatosDeDocumento> ListarDatosDeDocumentoReporteCDAT(Int64 Codigo_CDAT, Usuario pUsuario)
        {
            try
            {
                return DADatosDeDocumento.ListarDatosDeDocumentoReporteCDAT(Codigo_CDAT, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosDeDocumentoBusiness", "ConsultarDatosDeDocumento", ex);
                return null;
            }
        }

        public List<DatosDeDocumento> ListarDatosDeDocumentoFormatoCartasMasivas(Int64 numero_radicacion, Usuario pUsuario)
        {
            try
            {
                return DADatosDeDocumento.ListarDatosDeDocumentoFormatoCartasMasivas(numero_radicacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosDeDocumentoBusiness", "ListarDatosDeDocumentoFormatoCartasMasivas", ex);
                return null;
            }
        }

        public List<DatosDeDocumento> ListarDatosDeDocumentoFormatoCartasMasivasCodeudor(Int64 numero_radicacion, Usuario pUsuario, Int64 Codeudor)
        {
            try
            {
                return DADatosDeDocumento.ListarDatosDeDocumentoFormatoCartasMasivasCodeudor(numero_radicacion, pUsuario, Codeudor);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosDeDocumentoBusiness", "ListarDatosDeDocumentoFormatoCartasMasivas", ex);
                return null;
            }
        }
        public Documento ConsultarSolicitud(Usuario vUsuario)
        {
            try
            {
                return DADatosDeDocumento.ConsultarSolicitud(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientoBusiness", "EliminarAhorroVista", ex);
                return null;
            }
        }
        public Documento CrearDocSolicitud(Documento pEntidad, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEntidad = DADatosDeDocumento.CrearDocSolicitud(pEntidad, pUsuario);

                    ts.Complete();
                }

                return pEntidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosSolicitudBusiness", "CrearSolicitud", ex);
                return null;
            }
        }
        public List<DatosDeDocumento> ListarVariables(Usuario pUsuario)
        {
            try
            {
                return DADatosDeDocumento.ListarVariables(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosDeDocumentoBusiness", "ListarVariables", ex);
                return null;
            }
        }
    }
}