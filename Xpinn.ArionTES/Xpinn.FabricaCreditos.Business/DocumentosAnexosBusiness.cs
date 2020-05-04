using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Imagenes.Data;
using System.Data.Common;

namespace Xpinn.FabricaCreditos.Business
{
    /// <summary>
    /// Objeto de negocio para DocumentosAnexos
    /// </summary>
    public class DocumentosAnexosBusiness : GlobalBusiness
    {
        private DocumentosAnexosData DADocumentosAnexos;
        private ImagenesORAData DAImagenes;

        /// <summary>
        /// Constructor del objeto de negocio para DocumentosAnexos
        /// </summary>
        public DocumentosAnexosBusiness()
        {
            DADocumentosAnexos = new DocumentosAnexosData();
            DAImagenes = new ImagenesORAData();
        }

        /// <summary>
        /// Crea un DocumentosAnexos
        /// </summary>
        /// <param name="pDocumentosAnexos">Entidad DocumentosAnexos</param>
        /// <returns>Entidad DocumentosAnexos creada</returns>
        public DocumentosAnexos CrearDocumentosAnexos(DocumentosAnexos pDocumentosAnexos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pDocumentosAnexos = DAImagenes.CrearDocumentosAnexos(pDocumentosAnexos, pUsuario);

                    ts.Complete();
                }

                return pDocumentosAnexos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentosAnexosBusiness", "CrearDocumentosAnexos", ex);
                return null;
            }
        }


        /// <summary>
        /// Crea un DocumentosAnexos
        /// </summary>
        /// <param name="pDocumentosAnexos">Entidad DocumentosAnexos</param>
        /// <returns>Entidad DocumentosAnexos creada</returns>
        public DocumentosAnexos CrearDocAnexos(DocumentosAnexos pDocumentosAnexos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (pDocumentosAnexos.imagen != null)
                    {
                        Xpinn.Imagenes.Data.ImagenesORAData DAImagenes = new Xpinn.Imagenes.Data.ImagenesORAData();
                        pDocumentosAnexos = DAImagenes.CrearDocAnexosImagen(pDocumentosAnexos, pUsuario);
                    }
                    else
                    { 
                        pDocumentosAnexos = DADocumentosAnexos.CrearDocAnexos(pDocumentosAnexos, pUsuario);
                    }
                    ts.Complete();
                }

                return pDocumentosAnexos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentosAnexosBusiness", "CrearDocAnexos", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un DocumentosAnexos
        /// </summary>
        /// <param name="pDocumentosAnexos">Entidad DocumentosAnexos</param>
        /// <returns>Entidad DocumentosAnexos modificada</returns>
        public DocumentosAnexos ModificarDocumentosAnexos(DocumentosAnexos pDocumentosAnexos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pDocumentosAnexos = DAImagenes.ModificarDocumentosAnexos(pDocumentosAnexos, pUsuario);

                    ts.Complete();
                }

                return pDocumentosAnexos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentosAnexosBusiness", "ModificarDocumentosAnexos", ex);
                return null;
            }
        }


        /// <summary>
        /// Modifica un DocumentosAnexos
        /// </summary>
        /// <param name="pDocumentosAnexos">Entidad DocumentosAnexos</param>
        /// <returns>Entidad DocumentosAnexos modificada</returns>
        public DocumentosAnexos ModificarDocAnexos(DocumentosAnexos pDocumentosAnexos,Int64 pdocumento, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (pDocumentosAnexos.imagen != null)
                    {
                        pDocumentosAnexos = DAImagenes.ModificarDocAnexosImagen(pDocumentosAnexos, pUsuario);
                    }
                    else
                    {
                        pDocumentosAnexos = DAImagenes.ModificarDocumentosAnexos(pDocumentosAnexos, pUsuario);
                    }


                    ts.Complete();
                }

                return pDocumentosAnexos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentosAnexosBusiness", "ModificarDocAnexos", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un DocumentosAnexos
        /// </summary>
        /// <param name="pId">Identificador de DocumentosAnexos</param>
        public void EliminarDocumentosAnexos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DADocumentosAnexos.EliminarDocumentosAnexos(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentosAnexosBusiness", "EliminarDocumentosAnexos", ex);
            }
        }

        /// <summary>
        /// Obtiene un DocumentosAnexos
        /// </summary>
        /// <param name="pId">Identificador de DocumentosAnexos</param>
        /// <returns>Entidad DocumentosAnexos</returns>
        public DocumentosAnexos ConsultarDocumentosAnexos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DADocumentosAnexos.ConsultarDocumentosAnexos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentosAnexosBusiness", "ConsultarDocumentosAnexos", ex);
                return null;
            }
        }


        public DocumentosAnexos ConsultarDocumentosAnexosConFiltro(string filtro, Usuario pUsuario)
        {
            try
            {
                return DADocumentosAnexos.ConsultarDocumentosAnexosConFiltro(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentosAnexosBusiness", "ConsultarDocumentosAnexosConFiltro", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pDocumentosAnexos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de DocumentosAnexos obtenidos</returns>
        public List<DocumentosAnexos> ListarDocumentosAnexos(DocumentosAnexos pDocumentosAnexos, int pTipo, Usuario pUsuario)
        {
            try
            {
                return DADocumentosAnexos.ListarDocumentosAnexos(pDocumentosAnexos, pTipo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentosAnexosBusiness", "ListarDocumentosAnexos", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pDocumentosAnexos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de DocumentosAnexos obtenidos</returns>
        public List<DocumentosAnexos> ListarDocAnexos(DocumentosAnexos pDocumentosAnexos, int pTipo, Usuario pUsuario)
        {
            try
            {
                return DADocumentosAnexos.ListarDocAnexos(pDocumentosAnexos, pTipo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentosAnexosBusiness", "ListarDocAnexos", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pDocumentosAnexos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de DocumentosAnexos obtenidos</returns>
        public List<DocumentosAnexos> Handler(DocumentosAnexos vDocumentosAnexos, Usuario pUsuario)
        {
            try
            {
                return DADocumentosAnexos.Handler(vDocumentosAnexos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentosAnexosBusiness", "ListarDocumentosAnexos", ex);
                return null;
            }
        }

         public List<DocumentosAnexos> ListadoControlDocumentos(DateTime pFechaAper, String pFiltro, Usuario pUsuario)
        {
            try
            {
               return DADocumentosAnexos.ListadoControlDocumentos(pFechaAper,pFiltro, pUsuario);
            }
            catch 
            { 
                return null;
            }
        }

    }
}