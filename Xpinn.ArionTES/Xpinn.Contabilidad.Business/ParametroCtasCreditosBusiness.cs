using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Contabilidad.Data;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Business
{
    /// <summary>
    /// Objeto de negocio para Par_Cue_LinCred
    /// </summary>
    public class ParametroCtasCreditosBusiness : GlobalBusiness
    {
        private ParametroCtasCreditosData DAPar_Cue_LinCred;

        /// <summary>
        /// Constructor del objeto de negocio para Par_Cue_LinCred
        /// </summary>
        public ParametroCtasCreditosBusiness()
        {
            DAPar_Cue_LinCred = new ParametroCtasCreditosData();
        }

        /// <summary>
        /// Crea un Par_Cue_LinCred
        /// </summary>
        /// <param name="pPar_Cue_LinCred">Entidad Par_Cue_LinCred</param>
        /// <returns>Entidad Par_Cue_LinCred creada</returns>
        public Par_Cue_LinCred CrearPar_Cue_LinCred(Par_Cue_LinCred pPar_Cue_LinCred, ref string error, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    error = DAPar_Cue_LinCred.ValidarCuentaCredito(pPar_Cue_LinCred, vusuario);
                    if(error == "" || error == null)
                        pPar_Cue_LinCred = DAPar_Cue_LinCred.CrearPar_Cue_LinCred(pPar_Cue_LinCred, vusuario);

                    ts.Complete();
                }

                return pPar_Cue_LinCred;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinCredBusiness", "CrearPar_Cue_LinCred", ex);
                return null;
            }
        }

        public bool CrearParametrizacionLinea(List<Par_Cue_LinCred> lstParametros, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    List<Par_Cue_LinCred> lstParamExiste = new List<Par_Cue_LinCred>();
                    foreach(Par_Cue_LinCred parametro in lstParametros)
                    {
                        PlanCuentasData PlanCuentaData = new PlanCuentasData();
                        PlanCuentas plan = new PlanCuentas();
                        if(parametro.tipo_mov == 0 || parametro.idparametro == 0)
                        {
                            plan = PlanCuentaData.ConsultarPlanCuentas(parametro.cod_cuenta, pUsuario);
                            parametro.tipo_mov = plan.tipo == "C" ? 2 : 1;
                        }
                        DAPar_Cue_LinCred.CrearParametrizacionCuenta(parametro, pUsuario);
                    }                                            

                    ts.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinCredService", "CrearParametrizacionLinea", ex);
                return false;
            }
        }

        /// <summary>
        /// Modifica un Par_Cue_LinCred
        /// </summary>
        /// <param name="pPar_Cue_LinCred">Entidad Par_Cue_LinCred</param>
        /// <returns>Entidad Par_Cue_LinCred modificada</returns>
        public Par_Cue_LinCred ModificarPar_Cue_LinCred(Par_Cue_LinCred pPar_Cue_LinCred, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPar_Cue_LinCred = DAPar_Cue_LinCred.ModificarPar_Cue_LinCred(pPar_Cue_LinCred, vusuario);

                    ts.Complete();
                }

                return pPar_Cue_LinCred;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinCredBusiness", "ModificarPar_Cue_LinCred", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un Par_Cue_LinCred
        /// </summary>
        /// <param name="pId">Identificador de Par_Cue_LinCred</param>
        public void EliminarPar_Cue_LinCred(Int64 pId, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAPar_Cue_LinCred.EliminarPar_Cue_LinCred(pId, vusuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinCredBusiness", "EliminarPar_Cue_LinCred", ex);
            }
        }

        /// <summary>
        /// Obtiene un Par_Cue_LinCred
        /// </summary>
        /// <param name="pId">Identificador de Par_Cue_LinCred</param>
        /// <returns>Entidad Par_Cue_LinCred</returns>
        public Par_Cue_LinCred ConsultarPar_Cue_LinCred(Int64 pId, Usuario vusuario)
        {
            try
            {
                Par_Cue_LinCred Par_Cue_LinCred = new Par_Cue_LinCred();

                Par_Cue_LinCred = DAPar_Cue_LinCred.ConsultarPar_Cue_LinCred(pId, vusuario);

                return Par_Cue_LinCred;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinCredBusiness", "ConsultarPar_Cue_LinCred", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pPar_Cue_LinCred">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Par_Cue_LinCred obtenidos</returns>
        public List<Par_Cue_LinCred> ListarPar_Cue_LinCred(Par_Cue_LinCred pPar_Cue_LinCred, Usuario vUsuario)
        {
            try
            {
                return DAPar_Cue_LinCred.ListarPar_Cue_LinCred(pPar_Cue_LinCred, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinCredBusiness", "ListarPar_Cue_LinCred", ex);
                return null;
            }
        }

        public List<Par_Cue_LinCred> ListarClasificacion(Usuario vUsuario)
        {
            try
            {
                return DAPar_Cue_LinCred.ListarClasificacion(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinCredBusiness", "ListarClasificacion", ex);
                return null;
            }
        }

        public List<Par_Cue_LinCred> ListarTransaccion(Usuario vUsuario)
        {
            try
            {
                return DAPar_Cue_LinCred.ListarTransaccion(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinCredBusiness", "ListarTransaccion", ex);
                return null;
            }
        }

        public List<Par_Cue_LinCred> ListarEstructura(Usuario vUsuario)
        {
            try
            {
                return DAPar_Cue_LinCred.ListarEstructura(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinCredBusiness", "ListarEstructura", ex);
                return null;
            }
        }

        public List<Par_Cue_LinCred> ListarLibranza(Usuario vUsuario)
        {
            try
            {
                return DAPar_Cue_LinCred.ListarLibranza(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinCredBusiness", "ListarLibranza", ex);
                return null;
            }
        }

        public List<Par_Cue_LinCred> ListarGarantia(Usuario vUsuario)
        {
            try
            {
                return DAPar_Cue_LinCred.ListarGarantia(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinCredBusiness", "ListarGarantia", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return DAPar_Cue_LinCred.ObtenerSiguienteCodigo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinCredBusiness", "ObtenerSiguienteCodigo", ex);
                return Int64.MinValue;
            }
        }


        public void CopiarPar_Cue_LinCred(String lineaOrigen, String LineaDestino, int? cod_atr, int? tipo, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {                   
                    DAPar_Cue_LinCred.CopiarPar_Cue_LinCred(lineaOrigen, LineaDestino, cod_atr, tipo, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinCredBusiness", "CopiarPar_Cue_LinCred", ex);
            }
        }

        public Par_Cue_LinCred ConsultarPar_Cue_LinCred2(Par_Cue_LinCred pPar_Cue_LinCred, string pfiltro, Usuario vUsuario)
        {
            try
            {
                Par_Cue_LinCred Par_Cue_LinCred = new Par_Cue_LinCred();

                Par_Cue_LinCred = DAPar_Cue_LinCred.ConsultarPar_Cue_LinCred2(pPar_Cue_LinCred,pfiltro, vUsuario);

                return Par_Cue_LinCred;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinCredBusiness", "ConsultarPar_Cue_LinCred2", ex);
                return null;
            }
        }

        public Par_Cue_LinCred ConsultarPAR_CUE_OTROS(Par_Cue_LinCred pPar_Cue_LinCred, string pfiltro, Usuario vUsuario)
        {
            try
            {
                return DAPar_Cue_LinCred.ConsultarPAR_CUE_OTROS(pPar_Cue_LinCred, pfiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinCredBusiness", "ConsultarPAR_CUE_OTROS", ex);
                return null;
            }
        }

        public Par_Cue_LinCred ConsultarPAR_CUE_LINAPO(Par_Cue_LinCred pPar_Cue_LinCred, string pfiltro, Usuario vUsuario)
        {
            try
            {
                return DAPar_Cue_LinCred.ConsultarPAR_CUE_LINAPO(pPar_Cue_LinCred, pfiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinCredBusiness", "ConsultarPAR_CUE_LINAPO", ex);
                return null;
            }
        }

        public string ParametroGeneral(Int64 pCodigo, Usuario vUsuario)
        {
            try
            {
                return DAPar_Cue_LinCred.ParametroGeneral(pCodigo, vUsuario);
            }
            catch 
            {
                return null;
            }
        }


    }
}