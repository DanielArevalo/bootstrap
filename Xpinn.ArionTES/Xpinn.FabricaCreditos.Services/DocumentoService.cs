using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
    public class DocumentoService
    {
        private DocumentoBusiness BODocumento;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Documento
        /// </summary>
        public DocumentoService()
        {
            BODocumento = new DocumentoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "CRE"; } }

        /// <summary>
        /// Servicio para crear Documento
        /// </summary>
        /// <param name="pEntity">Entidad Documento</param>
        /// <returns>Entidad Documento creada</returns>
        public Documento CrearDocumentoGenerado(Documento pDocumento, Int64 numero_radicacion, Usuario pUsuario)
        {
            try
            {
                return BODocumento.CrearDocumentoGenerado(pDocumento, numero_radicacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentoService", "CrearDocumentoGenerado", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Documento
        /// </summary>
        /// <param name="pDocumento">Entidad Documento</param>
        /// <returns>Entidad Documento modificada</returns>
        public Documento ModificarDocumento(Documento pDocumento, Usuario pUsuario)
        {
            try
            {
                return BODocumento.ModificarDocumento(pDocumento, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentoService", "ModificarDocumento", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Documento
        /// </summary>
        /// <param name="pId">identificador de Documento</param>
        public void EliminarDocumento(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BODocumento.EliminarDocumento(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarDocumento", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Documento
        /// </summary>
        /// <param name="pId">identificador de Documento</param>
        /// <returns>Entidad Documento</returns>
        public Documento ConsultarDocumento(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BODocumento.ConsultarDocumento(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentoService", "ConsultarDocumento", ex);
                return null;
            }
        }

        public Documento ConsultarDocumentos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BODocumento.ConsultarDocumentos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentoService", "ConsultarDocumento", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener Documento
        /// </summary>
        /// <param name="pId">identificador de Documento</param>
        /// <returns>Entidad Documento</returns>
        public Documento ConsultarDocumentoAprobacion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BODocumento.ConsultarDocumentoAprobacion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentoService", "ConsultarDocumentoAprobacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Documentos a partir de unos filtros
        /// </summary>
        /// <param name="pDocumento">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Documento obtenidos</returns>
        public List<Documento> ListarDocumentoAGenerar(Documento pDocumento, Usuario pUsuario)
        {
            try
            {
                return BODocumento.ListarDocumentoAGenerar(pDocumento, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentoService", "ListarDocumentoAGenerar", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener lista de Documentos a partir de unos filtros
        /// </summary>
        /// <param name="pDocumento">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Documento obtenidos</returns>
        public List<Documento> ListarCartaAprobacion(Documento pDocumento, Usuario pUsuario)
        {
            try
            {
                return BODocumento.ListarCartaAprobacion(pDocumento, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentoService", "ListarCartaAprobacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Documentos a partir de unos filtros
        /// </summary>
        /// <param name="pDocumento">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Documento obtenidos</returns>
        public List<Documento> ListarDocumentoGenerado(Documento pDocumento, Usuario pUsuario)
        {
            try
            {
                return BODocumento.ListarDocumentoGenerado(pDocumento, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentoService", "ListarDocumentoGenerado", ex);
                return null;
            }
          

        }

       
        /// <summary>
        /// Servicio para obtener lista de Documentos a partir de unos filtros
        /// </summary>
        /// <param name="pDocumento">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Documento obtenidos</returns>
        public List<Documento> ListarCartaAprobacionGenerado(Documento pDocumento, Usuario pUsuario)
        {
            try
            {
                return BODocumento.ListarCartaAprobacionGenerado(pDocumento, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentoService", "ListarCartaAprobacionGenerado", ex);
                return null;
            }


        }
        public String Listarconsecutivo(string tipo, Usuario pUsuario)
        {
            try
            {
                return BODocumento.Listarconsecutivo(tipo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentoService", "ListarDocumentoGenerado", ex);
                return null;
            }
          
        }

        public bool CrearDocGarantias(Credito credito, Usuario pUsuario)
        {
            try
            {
                return BODocumento.CrearDocGarantias(credito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentoService", "CrearDocGarantias", ex);
                return false;
            }
        }
    }
}