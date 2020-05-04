using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Business
{
    /// <summary>
    /// Objeto de negocio para TipoListaRecaudo
    /// </summary>
    public class TipoListaRecaudoBusiness : GlobalBusiness
    {
        private TipoListaRecaudoData DATipoListaRecaudo;
        private TipoListaRecaudoDetalleData DATipoListaRecaudoDetalle;

        /// <summary>
        /// Constructor del objeto de negocio para TipoListaRecaudo
        /// </summary>
        public TipoListaRecaudoBusiness()
        {
            DATipoListaRecaudo = new TipoListaRecaudoData();
            DATipoListaRecaudoDetalle = new TipoListaRecaudoDetalleData();
        }

        /// <summary>
        /// Crea un TipoListaRecaudo
        /// </summary>
        /// <param name="pTipoListaRecaudo">Entidad TipoListaRecaudo</param>
        /// <returns>Entidad TipoListaRecaudo creada</returns>
        public TipoListaRecaudo CrearTipoListaRecaudo(TipoListaRecaudo pTipoListaRecaudo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoListaRecaudo = DATipoListaRecaudo.CrearTipoListaRecaudo(pTipoListaRecaudo, pUsuario);
                    int? cod;
                    cod = pTipoListaRecaudo.idtipo_lista;

                    if (pTipoListaRecaudo.lstDetalle != null)
                    {
                        int num = 0;
                        foreach (TipoListaRecaudoDetalle eEstruc in pTipoListaRecaudo.lstDetalle)
                        {
                            TipoListaRecaudoDetalle nEstructura = new TipoListaRecaudoDetalle();
                            eEstruc.idtipo_lista = cod;
                            nEstructura = DATipoListaRecaudoDetalle.CrearTipoListaRecaudoDetalle(eEstruc, pUsuario);
                            num += 1;
                        }
                    }
                    ts.Complete();
                }

                return pTipoListaRecaudo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoListaRecaudoBusiness", "CrearTipoListaRecaudo", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un TipoListaRecaudo
        /// </summary>
        /// <param name="pTipoListaRecaudo">Entidad TipoListaRecaudo</param>
        /// <returns>Entidad TipoListaRecaudo modificada</returns>
        public TipoListaRecaudo ModificarTipoListaRecaudo(TipoListaRecaudo pTipoListaRecaudo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoListaRecaudo = DATipoListaRecaudo.ModificarTipoListaRecaudo(pTipoListaRecaudo, pUsuario);
                    if (pTipoListaRecaudo.lstDetalle != null)
                    {
                        int num = 0;
                        foreach (TipoListaRecaudoDetalle eEstruc in pTipoListaRecaudo.lstDetalle)
                        {
                            eEstruc.idtipo_lista = pTipoListaRecaudo.idtipo_lista;
                            TipoListaRecaudoDetalle nEstructura = new TipoListaRecaudoDetalle();
                            if (eEstruc.codtipo_lista_detalle <= 0)
                            {
                                if (eEstruc.tipo_producto != 0)
                                    nEstructura = DATipoListaRecaudoDetalle.CrearTipoListaRecaudoDetalle(eEstruc, pUsuario);
                            }
                            else
                            {
                                nEstructura = DATipoListaRecaudoDetalle.ModificarTipoListaRecaudoDetalle(eEstruc, pUsuario);
                            }
                            num += 1;
                        }
                    }

                    ts.Complete();
                }

                return pTipoListaRecaudo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoListaRecaudoBusiness", "ModificarTipoListaRecaudo", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un TipoListaRecaudo
        /// </summary>
        /// <param name="pId">Identificador de TipoListaRecaudo</param>
        public void EliminarTipoListaRecaudo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DATipoListaRecaudo.EliminarTipoListaRecaudo(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoListaRecaudoBusiness", "EliminarTipoListaRecaudo", ex);
            }
        }

        /// <summary>
        /// Obtiene un TipoListaRecaudo
        /// </summary>
        /// <param name="pId">Identificador de TipoListaRecaudo</param>
        /// <returns>Entidad TipoListaRecaudo</returns>
        public TipoListaRecaudo ConsultarTipoListaRecaudo(Int64 pId, Usuario vUsuario)
        {
            try
            {
                TipoListaRecaudo TipoListaRecaudo = new TipoListaRecaudo();
                TipoListaRecaudo = DATipoListaRecaudo.ConsultarTipoListaRecaudo(pId, vUsuario);
                TipoListaRecaudo.lstDetalle = new List<TipoListaRecaudoDetalle>();

                TipoListaRecaudoDetalle pDetalle = new TipoListaRecaudoDetalle();
                pDetalle.idtipo_lista = TipoListaRecaudo.idtipo_lista;
                TipoListaRecaudo.lstDetalle = DATipoListaRecaudoDetalle.ListarTipoListaRecaudoDetalle(pDetalle, vUsuario);

                return TipoListaRecaudo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoListaRecaudoBusiness", "ConsultarTipoListaRecaudo", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pTipoListaRecaudo">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoListaRecaudo obtenidos</returns>
        public List<TipoListaRecaudo> ListarTipoListaRecaudo(TipoListaRecaudo pTipoListaRecaudo, Usuario pUsuario)
        {
            try
            {
                return DATipoListaRecaudo.ListarTipoListaRecaudo(pTipoListaRecaudo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoListaRecaudoBusiness", "ListarTipoListaRecaudo", ex);
                return null;
            }
        }

        public void EliminarTipoListaRecaudoDetalle(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DATipoListaRecaudoDetalle.EliminarTipoListaRecaudoDetalle(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoListaRecaudoBusiness", "EliminarTipoListaRecaudoDetalle", ex);
            }
        }

    }
}