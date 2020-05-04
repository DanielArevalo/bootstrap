using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.ConciliacionBancaria.Data;
using Xpinn.ConciliacionBancaria.Entities;

using System.IO;

namespace Xpinn.ConciliacionBancaria.Business
{
    /// <summary>
    /// Objeto de negocio para ExtractoBancario
    /// </summary>
    public class ExtractoBancarioBusiness : GlobalBusiness
    {
        private ExtractoBancarioData DAExtractoBancario;
        private DetExtractoBancarioData DADetalle;

        /// <summary>
        /// Constructor del objeto de negocio para ExtractoBancario
        /// </summary>
        public ExtractoBancarioBusiness()
        {
            DAExtractoBancario = new ExtractoBancarioData();
            DADetalle = new DetExtractoBancarioData();
        }

        /// <summary>
        /// Crea un ExtractoBancario
        /// </summary>
        /// <param name="pExtractoBancario">Entidad ExtractoBancario</param>
        /// <returns>Entidad ExtractoBancario creada</returns>
        public ExtractoBancario CrearExtractoBancario(ExtractoBancario pExtractoBancario, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pExtractoBancario = DAExtractoBancario.CrearExtractoBancario(pExtractoBancario, pUsuario);
                    Int64 cod = pExtractoBancario.idextracto;
                    if (pExtractoBancario.lstDetalle != null && pExtractoBancario.lstDetalle.Count > 0)
                    {
                        foreach (DetExtractoBancario rDeta in pExtractoBancario.lstDetalle)
                        {
                            DetExtractoBancario nDetalle = new DetExtractoBancario();
                            rDeta.idextracto = cod;
                            nDetalle = DADetalle.CrearDetExtractoBancario(rDeta, pUsuario);
                        }
                    }

                    ts.Complete();
                }

                return pExtractoBancario;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExtractoBancarioBusiness", "CrearExtractoBancario", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un ExtractoBancario
        /// </summary>
        /// <param name="pExtractoBancario">Entidad ExtractoBancario</param>
        /// <returns>Entidad ExtractoBancario modificada</returns>
        public ExtractoBancario ModificarExtractoBancario(ExtractoBancario pExtractoBancario, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pExtractoBancario = DAExtractoBancario.ModificarExtractoBancario(pExtractoBancario, pUsuario);
                    Int64 cod = pExtractoBancario.idextracto;
                    if (pExtractoBancario.lstDetalle != null && pExtractoBancario.lstDetalle.Count > 0)
                    {
                        foreach (DetExtractoBancario rDeta in pExtractoBancario.lstDetalle)
                        {
                            DetExtractoBancario nDetalle = new DetExtractoBancario();
                            rDeta.idextracto = cod;
                            if(rDeta.iddetalle > 0 && rDeta.iddetalle != null)
                                nDetalle = DADetalle.ModificarDetExtractoBancario(rDeta, pUsuario);
                            else
                                nDetalle = DADetalle.CrearDetExtractoBancario(rDeta, pUsuario);
                        }
                    }
                    ts.Complete();
                }

                return pExtractoBancario;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExtractoBancarioBusiness", "ModificarExtractoBancario", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un ExtractoBancario
        /// </summary>
        /// <param name="pId">Identificador de ExtractoBancario</param>
        public void EliminarExtractoBancario(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAExtractoBancario.EliminarExtractoBancario(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExtractoBancarioBusiness", "EliminarExtractoBancario", ex);
            }
        }

        /// <summary>
        /// Obtiene un ExtractoBancario
        /// </summary>
        /// <param name="pId">Identificador de ExtractoBancario</param>
        /// <returns>Entidad ExtractoBancario</returns>
        public ExtractoBancario ConsultarExtractoBancario(Int64 pId, Usuario vUsuario)
        {
            try
            {
                ExtractoBancario ExtractoBancario = new ExtractoBancario();

                ExtractoBancario = DAExtractoBancario.ConsultarExtractoBancario(pId, vUsuario);

                return ExtractoBancario;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExtractoBancarioBusiness", "ConsultarExtractoBancario", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pExtractoBancario">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ExtractoBancario obtenidos</returns>
        public List<ExtractoBancario> ListarExtractoBancario(string pFiltro, Usuario pUsuario)
        {
            try
            {
                return DAExtractoBancario.ListarExtractoBancario(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExtractoBancarioBusiness", "ListarExtractoBancario", ex);
                return null;
            }
        }



        public List<DetExtractoBancario> ListarDetExtractoBancario(Int32 pId, Usuario pUsuario)
        {
            try
            {
                return DADetalle.ListarDetExtractoBancario(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExtractoBancarioBusiness", "ListarDetExtractoBancario", ex);
                return null;
            }
        }

        public List<DetExtractoBancario> ListarConceptos_Bancarios(Usuario pUsuario)
        {
            try
            {
                return DADetalle.ListarConceptos_Bancarios(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExtractoBancarioBusiness", "ListarConceptos_Bancarios", ex);
                return null;
            }
        }


        public void EliminarDetExtractoBancario(Int32 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DADetalle.EliminarDetExtractoBancario(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExtractoBancarioBusiness", "EliminarDetExtractoBancario", ex);
            }
        }
        

    }
}