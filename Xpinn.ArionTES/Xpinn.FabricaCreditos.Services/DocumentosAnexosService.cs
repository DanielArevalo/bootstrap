using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;
using System.Data.Common;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class DocumentosAnexosService
    {
        private DocumentosAnexosBusiness BODocumentosAnexos;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para DocumentosAnexos
        /// </summary>
        public DocumentosAnexosService()
        {
            BODocumentosAnexos = new DocumentosAnexosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100102"; } } //100108
        public string CodigoProgram { get { return " 100163"; } }

        /// <summary>
        /// Servicio para crear DocumentosAnexos
        /// </summary>
        /// <param name="pEntity">Entidad DocumentosAnexos</param>
        /// <returns>Entidad DocumentosAnexos creada</returns>
        public DocumentosAnexos CrearDocumentosAnexos(DocumentosAnexos pDocumentosAnexos, Usuario pUsuario)
        {
            try
            {
                return BODocumentosAnexos.CrearDocumentosAnexos(pDocumentosAnexos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentosAnexosService", "CrearDocumentosAnexos", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para crear DocumentosAnexos
        /// </summary>
        /// <param name="pEntity">Entidad DocumentosAnexos</param>
        /// <returns>Entidad DocumentosAnexos creada</returns>
        public DocumentosAnexos CrearDocAnexos(DocumentosAnexos pDocumentosAnexos, Usuario pUsuario)
        {
            try
            {
                return BODocumentosAnexos.CrearDocAnexos(pDocumentosAnexos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentosAnexosService", "CrearDocAnexos", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar DocumentosAnexos
        /// </summary>
        /// <param name="pDocumentosAnexos">Entidad DocumentosAnexos</param>
        /// <returns>Entidad DocumentosAnexos modificada</returns>
        public DocumentosAnexos ModificarDocumentosAnexos(DocumentosAnexos pDocumentosAnexos, Usuario pUsuario)
        {
            try
            {
                return BODocumentosAnexos.ModificarDocumentosAnexos(pDocumentosAnexos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentosAnexosService", "ModificarDocumentosAnexos", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para modificar DocumentosAnexos
        /// </summary>
        /// <param name="pDocumentosAnexos">Entidad DocumentosAnexos</param>
        /// <returns>Entidad DocumentosAnexos modificada</returns>
        public DocumentosAnexos ModificarDocAnexos(DocumentosAnexos pDocumentosAnexos, Int64 pdocumento,Usuario pUsuario)
        {
            try
            {
                return BODocumentosAnexos.ModificarDocAnexos(pDocumentosAnexos,pdocumento, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentosAnexosService", "ModificarDocAnexos", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar DocumentosAnexos
        /// </summary>
        /// <param name="pId">identificador de DocumentosAnexos</param>
        public void EliminarDocumentosAnexos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BODocumentosAnexos.EliminarDocumentosAnexos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarDocumentosAnexos", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener DocumentosAnexos
        /// </summary>
        /// <param name="pId">identificador de DocumentosAnexos</param>
        /// <returns>Entidad DocumentosAnexos</returns>
        public DocumentosAnexos ConsultarDocumentosAnexos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BODocumentosAnexos.ConsultarDocumentosAnexos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentosAnexosService", "ConsultarDocumentosAnexos", ex);
                return null;
            }
        }


        public DocumentosAnexos ConsultarDocumentosAnexosConFiltro(string filtro, Usuario pUsuario)
        {
            try
            {
                return BODocumentosAnexos.ConsultarDocumentosAnexosConFiltro(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentosAnexosService", "ConsultarDocumentosAnexosConFiltro", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de DocumentosAnexoss a partir de unos filtros
        /// </summary>
        /// <param name="pDocumentosAnexos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de DocumentosAnexos obtenidos</returns>
        public List<DocumentosAnexos> ListarDocumentosAnexos(DocumentosAnexos pDocumentosAnexos, int pTipo, Usuario pUsuario)
        {
            try
            {
                return BODocumentosAnexos.ListarDocumentosAnexos(pDocumentosAnexos, pTipo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentosAnexosService", "ListarDocumentosAnexos", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener lista de DocumentosAnexoss a partir de unos filtros
        /// </summary>
        /// <param name="pDocumentosAnexos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de DocumentosAnexos obtenidos</returns>
        public List<DocumentosAnexos> ListarDocAnexos(DocumentosAnexos pDocumentosAnexos, int pTipo, Usuario pUsuario)
        {
            try
            {
                return BODocumentosAnexos.ListarDocAnexos(pDocumentosAnexos, pTipo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentosAnexosService", "ListarDocAnexos", ex);
                return null;
            }
        }




        public List<DocumentosAnexos> Handler(DocumentosAnexos vDocumentosAnexos, Usuario pUsuario)
        {
            try
            {
                return BODocumentosAnexos.Handler(vDocumentosAnexos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentosAnexosService", "Handler", ex);
                return null;
            }
        }


        public List<DocumentosAnexos> ListadoControlDocumentos(DateTime pFechaAper,String pFiltro, Usuario pUsuario) 
        {
            try
            {
                return BODocumentosAnexos.ListadoControlDocumentos(pFechaAper, pFiltro, pUsuario);
            }
            catch 
            { 
                return null;
            }
        }
    }
}