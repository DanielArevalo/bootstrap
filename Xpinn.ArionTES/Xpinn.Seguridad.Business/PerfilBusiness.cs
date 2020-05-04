using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Seguridad.Data;
using Xpinn.Seguridad.Entities;

namespace Xpinn.Seguridad.Business
{
    /// <summary>
    /// Objeto de negocio para Perfil
    /// </summary>
    public class PerfilBusiness : GlobalBusiness
    {
        private PerfilData DAPerfil;
        private AccesoData DAPerfilAcceso;

        /// <summary>
        /// Constructor del objeto de negocio para Perfil
        /// </summary>
        public PerfilBusiness()
        {
            DAPerfil = new PerfilData();
            DAPerfilAcceso = new AccesoData();
        }

        /// <summary>
        /// Crea un Perfil
        /// </summary>
        /// <param name="pPerfil">Entidad Perfil</param>
        /// <returns>Entidad Perfil creada</returns>
        /// 
        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario) 
        {
            try
            {
                return DAPerfil.ObtenerSiguienteCodigo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PerfilBusiness", "CrearPerfil", ex);
                return -1;
            }
        }

        public Perfil CrearPerfil(Perfil pPerfil, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPerfil = DAPerfil.CrearPerfil(pPerfil, pUsuario);

                    ts.Complete();
                }

                return pPerfil;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PerfilBusiness", "CrearPerfil", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Perfil
        /// </summary>
        /// <param name="pPerfil">Entidad Perfil</param>
        /// <returns>Entidad Perfil modificada</returns>
        public Perfil ModificarPerfil(Perfil pPerfil, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPerfil = DAPerfil.ModificarPerfil(pPerfil, pUsuario);
                    if (pPerfil.lstAccesos != null)
                    {
                        foreach (Acceso rFila in pPerfil.lstAccesos)
                        {
                            rFila.codigoperfil = pPerfil.codperfil;
                            rFila.codacceso = DAPerfilAcceso.ExisteAcceso(rFila, pUsuario);
                            if (rFila.codacceso != 0)
                                DAPerfilAcceso.ModificarAcceso(rFila, pUsuario);
                            else
                                DAPerfilAcceso.CrearAcceso(rFila, pUsuario);
                        }
                    }
                    ts.Complete();
                }

                return pPerfil;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PerfilBusiness", "ModificarPerfil", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un Perfil
        /// </summary>
        /// <param name="pId">Identificador de Perfil</param>
        public void EliminarPerfil(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAPerfil.EliminarPerfil(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PerfilBusiness", "EliminarPerfil", ex);
            }
        }

        /// <summary>
        /// Obtiene un Perfil
        /// </summary>
        /// <param name="pId">Identificador de Perfil</param>
        /// <returns>Entidad Perfil</returns>
        public Perfil ConsultarPerfil(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAPerfil.ConsultarPerfil(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PerfilBusiness", "ConsultarPerfil", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pPerfil">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Perfil obtenidos</returns>
        public List<Perfil> ListarPerfil(Perfil pPerfil, Usuario pUsuario)
        {
            try
            {
                return DAPerfil.ListarPerfil(pPerfil, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PerfilBusiness", "ListarPerfil", ex);
                return null;
            }
        }

        public List<Acceso> ListarOpciones(Int64 IdPerfil, Int64 CodModulo, Usuario pUsuario)
        {
            try
            {
                return DAPerfil.ListarOpciones(IdPerfil, CodModulo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PerfilBusiness", "ListarOpciones", ex);
                return null;
            }
        }

        public List<CamposPermiso> ConsultarCamposPerfil(CamposPermiso cdPerfil,  Usuario pUsuario)
        {
            try
            {
                return DAPerfil.ConsultarCamposPerfil(cdPerfil, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PerfilBusiness", "ConsultarCamposPerfil", ex);
                return null;
            }
        }

        public bool CrearCamposPerfil(CamposPermiso pPerfil, Usuario pUsuario)
        {
            try
            {
                return DAPerfil.CrearCamposPerfil(pPerfil, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PerfilBusiness", "CrearCamposPerfil", ex);
                return false;
            }
        }

        public bool EliminarCamposPerfil(CamposPermiso pPerfil, Usuario pUsuario)
        {
            try
            {
                return DAPerfil.EliminarCamposPerfil(pPerfil, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PerfilBusiness", "EliminarCamposPerfil", ex);
                return false;
            }
        }
    }
}