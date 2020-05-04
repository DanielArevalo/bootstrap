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
    /// Objeto de negocio para Documento
    /// </summary>
    public class DocumentoBusiness : GlobalData
    {
        private DocumentoData DADocumento;

        /// <summary>
        /// Constructor del objeto de negocio para Documento
        /// </summary>
        public DocumentoBusiness()
        {
            DADocumento = new DocumentoData();
        }

        /// <summary>
        /// Crea un Documento
        /// </summary>
        /// <param name="pDocumento">Entidad Documento</param>
        /// <returns>Entidad Documento creada</returns>
        public Documento CrearDocumentoGenerado(Documento pDocumento, Int64 numero_radicacion, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pDocumento = DADocumento.CrearDocumentoGenerado(pDocumento,numero_radicacion, pUsuario);

                    ts.Complete();
                }

                return pDocumento;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentoBusiness", "CrearDocumentoGenerado", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Documento
        /// </summary>
        /// <param name="pDocumento">Entidad Documento</param>
        /// <returns>Entidad Documento modificada</returns>
        public Documento ModificarDocumento(Documento pDocumento, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pDocumento = DADocumento.ModificarDocumento(pDocumento, pUsuario);

                    ts.Complete();
                }

                return pDocumento;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentoBusiness", "ModificarDocumento", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un Documento
        /// </summary>
        /// <param name="pId">Identificador de Documento</param>
        public void EliminarDocumento(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DADocumento.EliminarDocumento(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentoBusiness", "EliminarDocumento", ex);
            }
        }

        /// <summary>
        /// Obtiene un Documento
        /// </summary>
        /// <param name="pId">Identificador de Documento</param>
        /// <returns>Entidad Documento</returns>
        public Documento ConsultarDocumento(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DADocumento.ConsultarDocumento(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentoBusiness", "ConsultarDocumento", ex);
                return null;
            }
        }

        public Documento ConsultarDocumentos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DADocumento.ConsultarDocumentos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentoBusiness", "ConsultarDocumento", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene un Documento
        /// </summary>
        /// <param name="pId">Identificador de Documento</param>
        /// <returns>Entidad Documento</returns>
        public Documento ConsultarDocumentoAprobacion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DADocumento.ConsultarDocumentoAprobacion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentoBusiness", "ConsultarDocumentoAprobacion", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pDocumento">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Documento obtenidos</returns>
        public List<Documento> ListarDocumentoAGenerar(Documento pDocumento, Usuario pUsuario)
        {
            try
            {
                return DADocumento.ListarDocumentoAGenerar(pDocumento, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentoBusiness", "ListarDocumentoAGenerar", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pDocumento">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Documento obtenidos</returns>
        public List<Documento> ListarCartaAprobacion(Documento pDocumento, Usuario pUsuario)
        {
            try
            {
                return DADocumento.ListarCartaAprobacion(pDocumento, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentoBusiness", "ListarCartaAprobacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pDocumento">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Documento obtenidos</returns>
        public List<Documento> ListarDocumentoGenerado(Documento pDocumento, Usuario pUsuario)
        {
            try
            {
                return DADocumento.ListarDocumentoGenerado(pDocumento, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentoBusiness", "ListarDocumentoGenerado", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pDocumento">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Documento obtenidos</returns>
        public List<Documento> ListarCartaAprobacionGenerado(Documento pDocumento, Usuario pUsuario)
        {
            try
            {
                return DADocumento.ListarCartaAprobacionGenerado(pDocumento, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentoBusiness", "ListarCartaAprobacionGenerado", ex);
                return null;
            }
        }

        public String Listarconsecutivo(string tipo, Usuario pUsuario)
        {
            try
            {
                return DADocumento.Listarconsecutivo(tipo, pUsuario);
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
                foreach (Documento item in credito.lstDocumentos)
                {
                    DADocumento.CrearDocumentoGenerado(item, credito.numero_radicacion, pUsuario);
                }
                return true;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentoService", "CrearDocGarantias", ex);
                return false;
            }
        }
    }
}