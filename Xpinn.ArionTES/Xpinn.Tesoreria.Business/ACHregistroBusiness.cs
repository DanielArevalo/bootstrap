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
    /// Objeto de negocio para ACHregistro
    /// </summary>
    public class ACHregistroBusiness : GlobalBusiness
    {
        private ACHregistroData DAACHregistro;
        private ACHcampoData DAACHcampo;

        /// <summary>
        /// Constructor del objeto de negocio para ACHregistro
        /// </summary>
        public ACHregistroBusiness()
        {
            DAACHregistro = new ACHregistroData();
            DAACHcampo = new ACHcampoData();
        }

        /// <summary>
        /// Crea un ACHregistro
        /// </summary>
        /// <param name="pACHregistro">Entidad ACHregistro</param>
        /// <returns>Entidad ACHregistro creada</returns>
        public ACHregistro CrearACHregistro(ACHregistro pACHregistro, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pACHregistro = DAACHregistro.CrearACHregistro(pACHregistro, pUsuario);

                    if (pACHregistro.LstCampos.Count > 0 && pACHregistro.LstCampos != null)
                    {
                        foreach (ACHcampo rCampo in pACHregistro.LstCampos)
                        {
                            ACHdet_reg entidad = new ACHdet_reg();
                            entidad.registro = Convert.ToInt32(pACHregistro.codigo);
                            entidad.campo = Convert.ToInt32(rCampo.codigo);
                            entidad.orden = Convert.ToInt32(rCampo.orden);

                            ACHdet_reg entidad2 = new ACHdet_reg();
                            if (entidad.campo > 0)
                                entidad2 = DAACHregistro.CrearModACHdet_reg(entidad, pUsuario);
                        }
                    }
                    ts.Complete();
                }

                return pACHregistro;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHregistroBusiness", "CrearACHregistro", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un ACHregistro
        /// </summary>
        /// <param name="pACHregistro">Entidad ACHregistro</param>
        /// <returns>Entidad ACHregistro modificada</returns>
        public ACHregistro ModificarACHregistro(ACHregistro pACHregistro, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pACHregistro = DAACHregistro.ModificarACHregistro(pACHregistro, pUsuario);

                    if (pACHregistro.LstCampos.Count > 0 && pACHregistro.LstCampos != null)
                    {
                        foreach (ACHcampo rCampo in pACHregistro.LstCampos)
                        {
                            ACHdet_reg entidad = new ACHdet_reg();
                            entidad.registro = Convert.ToInt32(pACHregistro.codigo);
                            entidad.campo = Convert.ToInt32(rCampo.codigo);
                            entidad.orden = Convert.ToInt32(rCampo.orden);

                            ACHdet_reg entidad2 = new ACHdet_reg();
                            if (entidad.campo > 0)
                                entidad2 = DAACHregistro.CrearModACHdet_reg(entidad, pUsuario);
                        }
                    }

                    ts.Complete();
                }

                return pACHregistro;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHregistroBusiness", "ModificarACHregistro", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un ACHregistro
        /// </summary>
        /// <param name="pId">Identificador de ACHregistro</param>
        public void EliminarCampoXACHregistro(Int64 pRegistro,Int64 pCampo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAACHregistro.EliminarCampoXACHregistro(pRegistro, pCampo, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHregistroBusiness", "EliminarCampoXACHregistro", ex);
            }
        }

        /// <summary>
        /// Obtiene un ACHregistro
        /// </summary>
        /// <param name="pId">Identificador de ACHregistro</param>
        /// <returns>Entidad ACHregistro</returns>
        public ACHregistro ConsultarACHregistro(Int64 pId, Usuario vUsuario)
        {
            try
            {
                ACHregistro pACHregistro = new ACHregistro();

                pACHregistro = DAACHregistro.ConsultarACHregistro(pId, vUsuario);
                pACHregistro.LstCampos = DAACHcampo.ListarACHcampo(pId, vUsuario);

                return pACHregistro;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHregistroBusiness", "ConsultarACHregistro", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pACHregistro">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ACHregistro obtenidos</returns>
        public List<ACHregistro> ListarACHregistro(ACHregistro pACHregistro, Usuario pUsuario)
        {
            try
            {
                return DAACHregistro.ListarACHregistro(pACHregistro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHregistroBusiness", "ListarACHregistro", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return DAACHregistro.ObtenerSiguienteCodigo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHregistroBusiness", "ObtenerSiguienteCodigo", ex);
                return 0;
            }
        }


    }
}