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
    /// Objeto de negocio para AreasCaj
    /// </summary>
    public class AreasCajBusiness : GlobalBusiness
    {
        private AreasCajData DAAreasCaj;

        /// <summary>
        /// Constructor del objeto de negocio para AreasCaj
        /// </summary>
        public AreasCajBusiness()
        {
            DAAreasCaj = new AreasCajData();
        }

        /// <summary>
        /// Crea un AreasCaj
        /// </summary>
        /// <param name="pAreasCaj">Entidad AreasCaj</param>
        /// <returns>Entidad AreasCaj creada</returns>
        public AreasCaj CrearAreasCaj(AreasCaj pAreasCaj, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAreasCaj = DAAreasCaj.CrearAreasCaj(pAreasCaj, pUsuario);

                    ts.Complete();
                }

                return pAreasCaj;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreasCajBusiness", "CrearAreasCaj", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un AreasCaj
        /// </summary>
        /// <param name="pAreasCaj">Entidad AreasCaj</param>
        /// <returns>Entidad AreasCaj modificada</returns>
        public AreasCaj ModificarAreasCaj(AreasCaj pAreasCaj, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAreasCaj = DAAreasCaj.ModificarAreasCaj(pAreasCaj, pUsuario);

                    ts.Complete();
                }

                return pAreasCaj;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreasCajBusiness", "ModificarAreasCaj", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un AreasCaj
        /// </summary>
        /// <param name="pId">Identificador de AreasCaj</param>
        public void EliminarAreasCaj(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAAreasCaj.EliminarAreasCaj(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreasCajBusiness", "EliminarAreasCaj", ex);
            }
        }

        /// <summary>
        /// Obtiene un AreasCaj
        /// </summary>
        /// <param name="pId">Identificador de AreasCaj</param>
        /// <returns>Entidad AreasCaj</returns>
        public AreasCaj ConsultarAreasCaj(Int64 pId, Usuario vUsuario)
        {
            try
            {
                AreasCaj AreasCaj = new AreasCaj();

                AreasCaj = DAAreasCaj.ConsultarAreasCaj(pId, vUsuario);

                return AreasCaj;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreasCajBusiness", "ConsultarAreasCaj", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pAreasCaj">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de AreasCaj obtenidos</returns>
        public List<AreasCaj> ListarAreasCaj(AreasCaj pAreasCaj, Usuario pUsuario)
        {
            try
            {
                return DAAreasCaj.ListarAreasCaj(pAreasCaj, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreasCajBusiness", "ListarAreasCaj", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return DAAreasCaj.ObtenerSiguienteCodigo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreasCajBusiness", "ObtenerSiguienteCodigo", ex);
                return 0;
            }
        }

        //Creado para arqueo de caja menor


        /// <summary>
        /// Crea un ArqueoDetalle
        /// </summary>
        /// <param name="pArqueoDetalle">Lista de tipo ArqueoDetalle</param>
        /// <param name="pArqueo">Entidad ArqueoCaj</param>
        /// <returns>Entidad ArqueoDetalle creada</returns>
        public ArqueoDetalle CrearDetalleArqueo(List<ArqueoDetalle> pArqueoDetalle, ArqueoCaj pArqueo, Usuario pUsuario)
        {
            try
            {
                ArqueoDetalle pArqueoD = new ArqueoDetalle();
                ArqueoDetalle ArqueoD = new ArqueoDetalle();
                //ArqueoCaj pArqueoCaj = new ArqueoCaj();

                using (TransactionScope t = new TransactionScope(TransactionScopeOption.Required))
                {
                    pArqueo = DAAreasCaj.CrearArqueoCajaMenor(pArqueo, pUsuario);
                    t.Complete();
                }

                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    //ArqueoDetalle ArqueoD = new ArqueoDetalle();
                    foreach (ArqueoDetalle arqueoD in pArqueoDetalle)
                    {
                         ArqueoD = new ArqueoDetalle()
                        {
                            id_det_arqueo = null,
                            id_arqueo = pArqueo.id_arqueo,
                            tipo_efectivo = arqueoD.tipo_efectivo.ToString(),
                            denominacion = Convert.ToInt64(arqueoD.denominacion),
                            cantidad = Convert.ToInt64(arqueoD.cantidad),
                            total = Convert.ToInt64(arqueoD.total)
                        };
                        ArqueoD = DAAreasCaj.CrearArqueoCajaDetalle(ArqueoD, pUsuario);                        
                    }
                    ts.Complete();
                }

                return ArqueoD;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreasCajBusiness", "CrearArqueoCajaMenor", ex);
                return null;
            }
        }

        //Consultar codigo de caja menor
        /// <summary>
        /// Obtiene un AreasCaj
        /// </summary>
        /// <param name="pId">Identificador de AreasCaj</param>
        /// <returns>Entidad AreasCaj</returns>
        public AreasCaj ConsultarCajaMenor(int codusuario, Usuario vUsuario)
        {
            try
            {
                AreasCaj AreasCaj = new AreasCaj();

                AreasCaj = DAAreasCaj.ConsultarCajaMenor(codusuario, vUsuario);
                return AreasCaj;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreasCajBusiness", "ConsultarAreasCaj", ex);
                return null;
            }
        }

        public void ModificarArqueoCaja(Int64? id_arqueo, Int64 total_arqueo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAAreasCaj.ModificarArqueoCaja(id_arqueo,total_arqueo, pUsuario);
                    ts.Complete();
                }                
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreasCajBusiness", "ModificarAreasCaj", ex);
            }
        }

    }
}