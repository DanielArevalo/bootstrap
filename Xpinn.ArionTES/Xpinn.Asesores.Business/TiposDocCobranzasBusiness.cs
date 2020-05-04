using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Asesores.Data;
using Xpinn.Asesores.Entities;
using Xpinn.Comun.Entities;

namespace Xpinn.Asesores.Business
{
    public class TiposDocCobranzasBusiness : GlobalData
    {
        private TiposDocCobranzasData DATiposDocumento;

        /// <summary>
        /// Constructor del objeto de negocio para TiposDocumento
        /// </summary>
        public TiposDocCobranzasBusiness()
        {
            DATiposDocumento = new TiposDocCobranzasData();

        }

        /// <summary>
        /// Crea un TiposDocumento
        /// </summary>
        /// <param name="pTiposDocumento">Entidad TiposDocumento</param>
        /// <returns>Entidad TiposDocumento creada</returns>
        public TiposDocCobranzas CrearTiposDocumento(TiposDocCobranzas pTiposDocumento, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTiposDocumento = DATiposDocumento.CrearTiposDocumento(pTiposDocumento, pUsuario);

                    ts.Complete();
                }

                return pTiposDocumento;
            }
            catch(Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoBusiness", "CrearTiposDocumento", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un TiposDocumento
        /// </summary>
        /// <param name="pTiposDocumento">Entidad TiposDocumento</param>
        /// <returns>Entidad TiposDocumento modificada</returns>
        public TiposDocCobranzas ModificarTiposDocumento(TiposDocCobranzas pTiposDocumento, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTiposDocumento = DATiposDocumento.ModificarTiposDocumento(pTiposDocumento, pUsuario);

                    ts.Complete();
                }

                return pTiposDocumento;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoBusiness", "ModificarTiposDocumento", ex);
                return null;
            }
        }

        public TiposDocCobranzas CrearFormatoDocumentoCorreo(TiposDocCobranzas pTiposDocumento, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTiposDocumento = DATiposDocumento.CrearFormatoDocumentoCorreo(pTiposDocumento, pUsuario);

                    ts.Complete();
                }

                return pTiposDocumento;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoBusiness", "CrearFormatoDocumentoCorreo", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un TiposDocumento
        /// </summary>
        /// <param name="pId">Identificador de TiposDocumento</param>
        public void EliminarTiposDocumento(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DATiposDocumento.EliminarTiposDocumento(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoBusiness", "EliminarTiposDocumento", ex);
            }
        }

        public TiposDocCobranzas ModificarFormatoDocumentoCorreo(TiposDocCobranzas pTiposDocumento, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTiposDocumento = DATiposDocumento.ModificarFormatoDocumentoCorreo(pTiposDocumento, pUsuario);

                    ts.Complete();
                }

                return pTiposDocumento;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoBusiness", "ModificarFormatoDocumentoCorreo", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene un TiposDocumento
        /// </summary>
        /// <param name="pId">Identificador de TiposDocumento</param>
        /// <returns>Entidad TiposDocumento</returns>
        public TiposDocCobranzas ConsultarTiposDocumento(Int64 pId, Usuario pUsuario)
        {
            try
            {
               
                return DATiposDocumento.ConsultarTiposDocumento(pId, pUsuario);
            }
            catch 
            {
                return null;
            }
        }

        public TiposDocCobranzas ConsultarMaxTiposDocumento(Usuario pUsuario)
        {
            try
            {

                return DATiposDocumento.ConsultarMaxTipoDocumento(pUsuario);
            }
            catch
            {
                return null;
            }
        }

        public Xpinn.Asesores.Entities.Persona ConsultarCorreoPersona(long pId, Usuario pUsuario, string identificacion = null)
        {
            try
            {
                return DATiposDocumento.ConsultarCorreoPersona(pId, pUsuario, identificacion);
            }
            catch
            {
                return null;
            }
        }

        public Empresa ConsultarCorreoEmpresa(long pId, Usuario pUsuario)
        {
            try
            {
                return DATiposDocumento.ConsultarCorreoEmpresa(pId, pUsuario);
            }
            catch
            {
                return null;
            }
        }

        public TiposDocCobranzas ConsultarFormatoDocumentoCorreo(long pId, Usuario pUsuario, bool verificarSoloSiExiste = false)
        {
            try
            {
                return DATiposDocumento.ConsultarFormatoDocumentoCorreo(pId, pUsuario, verificarSoloSiExiste);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pTiposDocumento">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TiposDocumento obtenidos</returns>
        public List<TiposDocCobranzas> ListarTiposDocumento(TiposDocCobranzas pTiposDocumento, Usuario pUsuario)
        {
            try
            {
                return DATiposDocumento.ListarTiposDocumento(pTiposDocumento, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoBusiness", "ListarTiposDocumento", ex);
                return null;
            }
        }
        public List<TiposDocCobranzas> ListarTiposDocumentoCobranzas(TiposDocCobranzas pTiposDocumento, Usuario pUsuario)
        {
            try
            {
                return DATiposDocumento.ListarTiposDocumentoCobranzas(pTiposDocumento, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoBusiness", "ListarTiposDocumento", ex);
                return null;
            }
        }


        public string GenerarDocumentos(TiposDocCobranzas pTiposDocumento, Usuario pUsuario)
        {
            try
            {
                
                TiposDocCobranzas entidad = new TiposDocCobranzas();
                TiposDocCobranzas entidaddocumento = new TiposDocCobranzas();
                entidaddocumento = DATiposDocumento.ConsultarTiposDocumento(pTiposDocumento.tipo_documento, pUsuario);
                pTiposDocumento.lstVariables = DATiposDocumento.GenerarDocumento(pTiposDocumento, pUsuario);
                string texto = entidaddocumento.texto;

                // Validar que exista el texto
                if (texto.Trim().Length <= 0)
                    return texto;

                foreach (TiposDocCobranzas dFila in pTiposDocumento.lstVariables)
                {                    
                    string cCampo;
                    // Validar el campo o variable
                    if (dFila.campo == null)
                    {
                        cCampo = " ";
                    }
                    else
                    {
                        cCampo = dFila.campo.ToString().Trim();
                    }
                    // Validar el valor
                    string cValor = "";
                    if (dFila.valor != null)
                        cValor = dFila.valor.ToString().Trim();
                    else
                        cValor = " ";
                    // Reemplazar el valor de la variable en el texto
                    texto = texto.Replace(cCampo, cValor);
                    entidad.texto = texto;             
                 }

                return texto;
            }
            catch 
            {
                
                return null;
            }
        }

    }
}




