using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;
using Xpinn.Comun.Entities;

namespace Xpinn.Asesores.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class TiposDocCobranzasServices
    {
        private TiposDocCobranzasBusiness BOTiposDocumento;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para TiposDocumento
        /// </summary>
        public TiposDocCobranzasServices()
        {
            BOTiposDocumento = new TiposDocCobranzasBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "110207"; } }
        public string CodigoProgramaFormatoCorreo { get { return "110208"; } }

        /// <summary>
        /// Servicio para crear TiposDocumento
        /// </summary>
        /// <param name="pEntity">Entidad TiposDocumento</param>
        /// <returns>Entidad TiposDocumento creada</returns>
        public TiposDocCobranzas CrearTiposDocumento(TiposDocCobranzas pTiposDocumento, Usuario pUsuario)
        {
            try
            {
                return BOTiposDocumento.CrearTiposDocumento(pTiposDocumento, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoService", "CrearTiposDocumento", ex);
                return null;
            }
        }

        public TiposDocCobranzas CrearFormatoDocumentoCorreo(TiposDocCobranzas pTiposDocumento, Usuario pUsuario)
        {
            try
            {
                return BOTiposDocumento.CrearFormatoDocumentoCorreo(pTiposDocumento, pUsuario);
            }
            catch(Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoService", "CrearFormatoDocumentoCorreo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar TiposDocumento
        /// </summary>
        /// <param name="pTiposDocumento">Entidad TiposDocumento</param>
        /// <returns>Entidad TiposDocumento modificada</returns>
        public TiposDocCobranzas ModificarTiposDocumento(TiposDocCobranzas pTiposDocumento, Usuario pUsuario)
        {
            try
            {
                return BOTiposDocumento.ModificarTiposDocumento(pTiposDocumento, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoService", "ModificarTiposDocumento", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar TiposDocumento
        /// </summary>
        /// <param name="pId">identificador de TiposDocumento</param>
        public void EliminarTiposDocumento(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOTiposDocumento.EliminarTiposDocumento(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarTiposDocumento", ex);
            }
        }

        public TiposDocCobranzas ModificarFormatoDocumentoCorreo(TiposDocCobranzas ptipoDocumento, Usuario usuario)
        {
            try
            {
                return BOTiposDocumento.ModificarFormatoDocumentoCorreo(ptipoDocumento, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoService", "ModificarFormatoDocumentoCorreo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener TiposDocumento
        /// </summary>
        /// <param name="pId">identificador de TiposDocumento</param>
        /// <returns>Entidad TiposDocumento</returns>
        public TiposDocCobranzas ConsultarTiposDocumento(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOTiposDocumento.ConsultarTiposDocumento(pId, pUsuario);
            }
            catch
            {
                return null;
            }
        }



        public TiposDocCobranzas ConsultarMaxTiposDocumento( Usuario pUsuario)
        {
            try
            {

                return BOTiposDocumento.ConsultarMaxTiposDocumento( pUsuario);
            }
            catch
            {
                return null;
            }
        }


        public TiposDocCobranzas ConsultarFormatoDocumentoCorreo(Int64 pId, Usuario pUsuario, bool verificarSoloSiExiste = false)
        {
            try
            {

                return BOTiposDocumento.ConsultarFormatoDocumentoCorreo(pId, pUsuario, verificarSoloSiExiste);
            }
            catch
            {
                return null;
            }
        }

        public Xpinn.Asesores.Entities.Persona ConsultarCorreoPersona(Int64 pId, Usuario pUsuario, string identificacion = null)
        {
            try
            {

                return BOTiposDocumento.ConsultarCorreoPersona(pId, pUsuario, identificacion);
            }
            catch
            {
                return null;
            }
        }

        public Empresa ConsultarCorreoEmpresa(Int64 pId, Usuario pUsuario)
        {
            try
            {

                return BOTiposDocumento.ConsultarCorreoEmpresa(pId, pUsuario);
            }
            catch
            {
                return null;
            }
        }


        public string GenerarDocumentos(TiposDocCobranzas pTiposDocumento, Usuario pUsuario)
        {
            try
            {
                return BOTiposDocumento.GenerarDocumentos(pTiposDocumento, pUsuario);
            }
            catch 
            {
               
                return null;
            }
        }
        public List<TiposDocCobranzas> ListarTiposDocumentoCobranzas(TiposDocCobranzas pTiposDocumento, Usuario pUsuario)
        {
            try
            {
                return BOTiposDocumento.ListarTiposDocumentoCobranzas(pTiposDocumento, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoService", "ListarTiposDocumento", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener lista de TiposDocumentos a partir de unos filtros
        /// </summary>
        /// <param name="pTiposDocumento">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TiposDocumento obtenidos</returns>
        public List<TiposDocCobranzas> ListarTiposDocumento(TiposDocCobranzas pTiposDocumento, Usuario pUsuario)
        {
            try
            {
                return BOTiposDocumento.ListarTiposDocumento(pTiposDocumento, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoService", "ListarTiposDocumento", ex);
                return null;
            }
        }
    }
}
