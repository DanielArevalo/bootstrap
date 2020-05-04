using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Caja.Data;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Business
{
    /// <summary>
    /// Objeto de negocio para TipoMotivoAnu
    /// </summary>
    public class TipoMotivoAnuBusiness : GlobalBusiness
    {
        private TipoMotivoAnuData DATipoMotivoAnu;
         
        /// <summary>
        /// Constructor del objeto de negocio para TipoMotivoAnu
        /// </summary>
        public TipoMotivoAnuBusiness()
        {
            DATipoMotivoAnu = new TipoMotivoAnuData();
        }

        /// <summary>
        /// Crea un TipoMotivoAnu
        /// </summary>
        /// <param name="pTipoMotivoAnu">Entidad TipoMotivoAnu</param>
        /// <returns>Entidad TipoMotivoAnu creada</returns>
        public TipoMotivoAnu CrearTipoMotivoAnu(TipoMotivoAnu pTipoMotivoAnu, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoMotivoAnu = DATipoMotivoAnu.CrearTipoMotivoAnu(pTipoMotivoAnu, pUsuario);

                    ts.Complete();
                }

                return pTipoMotivoAnu;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoMotivoAnuBusiness", "CrearTipoMotivoAnu", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un TipoMotivoAnu
        /// </summary>
        /// <param name="pTipoMotivoAnu">Entidad TipoMotivoAnu</param>
        /// <returns>Entidad TipoMotivoAnu modificada</returns>
        public TipoMotivoAnu ModificarTipoMotivoAnu(TipoMotivoAnu pTipoMotivoAnu, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoMotivoAnu = DATipoMotivoAnu.ModificarTipoMotivoAnu(pTipoMotivoAnu, pUsuario);

                    ts.Complete();
                }

                return pTipoMotivoAnu;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoMotivoAnuBusiness", "ModificarTipoMotivoAnu", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un TipoMotivoAnu
        /// </summary>
        /// <param name="pId">Identificador de TipoMotivoAnu</param>
        public void EliminarTipoMotivoAnu(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DATipoMotivoAnu.EliminarTipoMotivoAnu(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoMotivoAnuBusiness", "EliminarTipoMotivoAnu", ex);
            }
        }

        /// <summary>
        /// Obtiene un TipoMotivoAnu
        /// </summary>
        /// <param name="pId">Identificador de TipoMotivoAnu</param>
        /// <returns>Entidad TipoMotivoAnu</returns>
        public TipoMotivoAnu ConsultarTipoMotivoAnu(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DATipoMotivoAnu.ConsultarTipoMotivoAnu(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoMotivoAnuBusiness", "ConsultarTipoMotivoAnu", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pTipoMotivoAnu">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoMotivoAnu obtenidos</returns>
        public List<TipoMotivoAnu> ListarTipoMotivoAnu(TipoMotivoAnu pTipoMotivoAnu, Usuario pUsuario)
        {
            try
            {
                return DATipoMotivoAnu.ListarTipoMotivoAnu(pTipoMotivoAnu, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoMotivoAnuBusiness", "ListarTipoMotivoAnu", ex);
                return null;
            }
        }

           public TipoMotivoAnu CrearTipoMotivoAnus(TipoMotivoAnu pTipoMotivoAnu, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        pTipoMotivoAnu = DATipoMotivoAnu.CrearTipoMotivoAnus(pTipoMotivoAnu, pusuario);
 
                        ts.Complete();
 
                    }
 
                    return pTipoMotivoAnu;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("TipoMotivoAnuBusiness", "CrearTipoMotivoAnu", ex);
                    return null;
                }
            }
 
 
            public TipoMotivoAnu ModificarTipoMotivoAnus(TipoMotivoAnu pTipoMotivoAnu, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        pTipoMotivoAnu = DATipoMotivoAnu.ModificarTipoMotivoAnus(pTipoMotivoAnu, pusuario);
 
                        ts.Complete();
 
                    }
 
                    return pTipoMotivoAnu;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("TipoMotivoAnuBusiness", "ModificarTipoMotivoAnu", ex);
                    return null;
                }
            }
 
 
            public void EliminarTipoMotivoAnus(Int64 pId, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        DATipoMotivoAnu.EliminarTipoMotivoAnus(pId, pusuario);
 
                        ts.Complete();
 
                    }
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("TipoMotivoAnuBusiness", "EliminarTipoMotivoAnu", ex);
                }
            }
 
 
            public TipoMotivoAnu ConsultarTipoMotivoAnun(Int64 pId, Usuario pusuario)
            {
                try
                {
                    TipoMotivoAnu TipoMotivoAnu = new TipoMotivoAnu();
                    TipoMotivoAnu = DATipoMotivoAnu.ConsultarTipoMotivoAnun(pId, pusuario);
                    return TipoMotivoAnu;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("TipoMotivoAnuBusiness", "ConsultarTipoMotivoAnu", ex);
                    return null;
                }
            }


            public List<TipoMotivoAnu> ListarTipoMotivoAnus(TipoMotivoAnu pTipoMotivoAnu, Usuario pusuario, String filtro)
            {
                try
                {
                    return DATipoMotivoAnu.ListarTipoMotivoAnus(pTipoMotivoAnu, pusuario, filtro);
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("TipoMotivoAnuBusiness", "ListarTipoMotivoAnu", ex);
                    return null;
                }
            }
 
 
        }


    }
