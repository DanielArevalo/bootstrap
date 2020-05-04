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
    /// Objeto de negocio para codeudores
    /// </summary>
    public class TipoCupoBusiness : GlobalBusiness
    {
        private TipoCupoData DATipoCupo;

        /// <summary>
        /// Constructor del objeto de negocio para codeudores
        /// </summary>
        public TipoCupoBusiness()
        {
            DATipoCupo = new TipoCupoData();
        }

        /// <summary>
        /// Crea un tipo de tasa
        /// </summary>
        /// <param name="pUsuario">Entidad TipoCupo</param>
        /// <returns>Entidad TipoCupo creada</returns>
        public TipoCupo CrearTipoCupo(TipoCupo pTipoCupo, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoCupo = DATipoCupo.CrearTipoCupo(pTipoCupo, vUsuario);
                    if (pTipoCupo.LstVariables != null)
                    {
                        foreach (DetTipoCupo item in pTipoCupo.LstVariables)
                        {
                            item.tipo_cupo = Convert.ToInt64(pTipoCupo.tipo_cupo);          
                            item.iddetalle = DATipoCupo.CrearDetTipoCupo(item, vUsuario);
                        }
                    }

                    ts.Complete();
                }

                return pTipoCupo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoCupoBusiness", "CrearTipoCupo", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un tipo de tasa
        /// </summary>
        /// <param name="pUsuario">Entidad tipo de tasa</param>
        /// <returns>Entidad tipo tasa modificada</returns>
        public TipoCupo ModificarTipoCupo(TipoCupo pTipoCupo, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoCupo = DATipoCupo.ModificarTipoCupo(pTipoCupo, vUsuario);
                    if (pTipoCupo.LstVariables != null)
                    {
                        foreach (DetTipoCupo item in pTipoCupo.LstVariables)
                        {
                            item.tipo_cupo = Convert.ToInt64(pTipoCupo.tipo_cupo);
                            item.iddetalle = DATipoCupo.CrearDetTipoCupo(item, vUsuario);
                        }
                    }

                    ts.Complete();
                }

                return pTipoCupo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoCupoBusiness", "ModificarTipoCupo", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un tipo de tasa
        /// </summary>
        /// <param name="pId">Identificador de tipo de tasa</param>
        public void EliminarTipoCupo(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DATipoCupo.EliminarTipoCupo(pId, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoCupoBusiness", "EliminarTipoCupo", ex);
            }
        }

        /// <summary>
        /// Obtiene un Usuario
        /// </summary>
        /// <param name="pId">Identificador de Usuario</param>
        /// <returns>Entidad Usuario</returns>
        public TipoCupo ConsultarTipoCupo(Int64 pId, Usuario vUsuario)
        {
            try
            {
                TipoCupo TipoCupo = new TipoCupo();

                TipoCupo = DATipoCupo.ConsultarTipoCupo(pId, vUsuario);

                return TipoCupo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoCupoBusiness", "ConsultarTipoCupo", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pUsuario">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de tipo de tasa obtenidos</returns>
        public List<TipoCupo> ListarTipoCupo(TipoCupo pTipoCupo, Usuario vUsuario)
        {
            try
            {
                return DATipoCupo.ListarTipoCupo(pTipoCupo, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoCupoBusiness", "ListarUsuario", ex);
                return null;
            }
        }

        public TipoCupo ConsultarTipoCupo(TipoCupo pTipoCupo, Usuario pUsuario)
        {
            try
            {
                TipoCupo TipoCupo = new TipoCupo();

                TipoCupo = DATipoCupo.ConsultarTipoCupo(pTipoCupo, pUsuario);

                return TipoCupo;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<DetTipoCupo> ListarDetTipoCupo(int pTipoCupo, Usuario vUsuario)
        {
            try
            {
                return DATipoCupo.ListarDetTipoCupo(pTipoCupo, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoCupoBusiness", "ListarDetTipoCupo", ex);
                return null;
            }
        }

        public Int64 CrearDetTipoCupo(DetTipoCupo pDetTipoCupo, Usuario pusuario)
        {
            Int64 _iddetalle = 0;
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    _iddetalle = DATipoCupo.CrearDetTipoCupo(pDetTipoCupo, pusuario);

                    ts.Complete();

                }

                return _iddetalle;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DetTipoCupoBusiness", "CrearDetTipoCupo", ex);
                return 0;
            }
        }


        public DetTipoCupo ModificarDetTipoCupo(DetTipoCupo pDetTipoCupo, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pDetTipoCupo = DATipoCupo.ModificarDetTipoCupo(pDetTipoCupo, pusuario);

                    ts.Complete();

                }

                return pDetTipoCupo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DetTipoCupoBusiness", "ModificarDetTipoCupo", ex);
                return null;
            }
        }


        public void EliminarDetTipoCupo(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DATipoCupo.EliminarDetTipoCupo(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DetTipoCupoBusiness", "EliminarDetTipoCupo", ex);
            }
        }


        public DetTipoCupo ConsultarDetTipoCupo(Int64 pId, Usuario pusuario)
        {
            try
            {
                DetTipoCupo DetTipoCupo = new DetTipoCupo();
                DetTipoCupo = DATipoCupo.ConsultarDetTipoCupo(pId, pusuario);
                return DetTipoCupo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DetTipoCupoBusiness", "ConsultarDetTipoCupo", ex);
                return null;
            }
        }






    }
}