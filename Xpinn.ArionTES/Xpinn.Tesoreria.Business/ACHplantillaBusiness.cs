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
    /// Objeto de negocio para ACHplantilla
    /// </summary>
    public class ACHplantillaBusiness : GlobalBusiness
    {
        private ACHplantillaData DAACHplantilla;
        private ACHregistroData DAACHregistro;

        /// <summary>
        /// Constructor del objeto de negocio para ACHplantilla
        /// </summary>
        public ACHplantillaBusiness()
        {
            DAACHplantilla = new ACHplantillaData();
            DAACHregistro = new ACHregistroData();
        }

        /// <summary>
        /// Crea un ACHplantilla
        /// </summary>
        /// <param name="pACHplantilla">Entidad ACHplantilla</param>
        /// <returns>Entidad ACHplantilla creada</returns>
        public ACHplantilla CrearACHplantilla(ACHplantilla pACHplantilla, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pACHplantilla = DAACHplantilla.CrearACHplantilla(pACHplantilla, pUsuario);
                    if (pACHplantilla.LstRegistros != null)
                    {
                        foreach (ACHregistro rItem in pACHplantilla.LstRegistros)
                        {
                            if (rItem.codigo > 0)
                            {
                                rItem.plantilla = pACHplantilla.codigo;
                                DAACHplantilla.CrearACHRegistro(rItem, pUsuario);
                            }
                        }
                    }
                    ts.Complete();
                }

                return pACHplantilla;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHplantillaBusiness", "CrearACHplantilla", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un ACHplantilla
        /// </summary>
        /// <param name="pACHplantilla">Entidad ACHplantilla</param>
        /// <returns>Entidad ACHplantilla modificada</returns>
        public ACHplantilla ModificarACHplantilla(ACHplantilla pACHplantilla, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pACHplantilla = DAACHplantilla.ModificarACHplantilla(pACHplantilla, pUsuario);
                    if (pACHplantilla.LstRegistros != null)
                    {
                        foreach (ACHregistro rItem in pACHplantilla.LstRegistros)
                        {

                            ACHregistro entidad = new ACHregistro();
                            entidad = DAACHplantilla.ConsultarRegisPlantilla(pACHplantilla.codigo, rItem.codigo, pUsuario);
                            if (entidad.codigo <= 0 && rItem.codigo > 0)
                            {
                                rItem.plantilla = pACHplantilla.codigo;
                                entidad = DAACHplantilla.CrearACHRegistro(rItem, pUsuario);
                            }
                        }
                    }
                    ts.Complete();
                }

                return pACHplantilla;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHplantillaBusiness", "ModificarACHplantilla", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un ACHplantilla
        /// </summary>
        /// <param name="pId">Identificador de ACHplantilla</param>
        public void EliminarACHplantilla(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAACHplantilla.EliminarACHplantilla(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHplantillaBusiness", "EliminarACHplantilla", ex);
            }
        }

        /// <summary>
        /// Obtiene un ACHplantilla
        /// </summary>
        /// <param name="pId">Identificador de ACHplantilla</param>
        /// <returns>Entidad ACHplantilla</returns>
        public ACHplantilla ConsultarACHplantilla(Int64 pId, Usuario vUsuario)
        {
            try
            {
                ACHplantilla pACHplantilla = new ACHplantilla();

                pACHplantilla = DAACHplantilla.ConsultarACHplantilla(pId, vUsuario);
                pACHplantilla.LstRegistros = DAACHregistro.ListarACHregistro(pId, vUsuario);

                return pACHplantilla;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHplantillaBusiness", "ConsultarACHplantilla", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pACHplantilla">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ACHplantilla obtenidos</returns>
        public List<ACHplantilla> ListarACHplantilla(ACHplantilla pACHplantilla, Usuario pUsuario)
        {
            try
            {
                return DAACHplantilla.ListarACHplantilla(pACHplantilla, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHplantillaBusiness", "ListarACHplantilla", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return DAACHplantilla.ObtenerSiguienteCodigo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHplantillaBusiness", "ObtenerSiguienteCodigo", ex);
                return 0;
            }
        }


        public void EliminarACH_PLANTILLA(Int64 pPlantilla, Int32 pRegistro, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAACHplantilla.EliminarACH_PLANTILLA(pPlantilla, pRegistro, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHplantillaBusiness", "EliminarACH_PLANTILLA", ex);
            }
        }


        public ACHregistro ConsultarRegisPlantilla(Int64 pPlantilla, Int64 pRegistro, Usuario vUsuario)
        {
            try
            {
                ACHregistro pACHplantilla = new ACHregistro();
                pACHplantilla = DAACHplantilla.ConsultarRegisPlantilla(pPlantilla,pRegistro, vUsuario);

                return pACHplantilla;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHplantillaBusiness", "ConsultarRegisPlantilla", ex);
                return null;
            }
        }


    }
}