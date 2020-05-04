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
    /// Objeto de negocio para concepto
    /// </summary>
    public class Par_Cue_OtrosBusiness : GlobalBusiness
    {
        private ParametroCtasOtrosData DAParametro;

        /// <summary>
        /// Constructor del objeto de negocio para concepto
        /// </summary>
        public Par_Cue_OtrosBusiness()
        {
            DAParametro = new ParametroCtasOtrosData();
        }


        public List<Par_Cue_Otros> ListarPar_Cue_Otros(string filtro, Usuario vUsuario)
        {
            try
            {
                return DAParametro.ListarPar_Cue_Otros(filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_OtrosBusiness", "ListarPar_Cue_Otros", ex);
                return null;
            }
        }


        public Par_Cue_Otros ConsultarParametroCtasOtros(Int32 pId, Usuario vUsuario)
        {
            try
            {
                return DAParametro.ConsultarParametroCtasOtros(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_OtrosBusiness", "ConsultarParametroCtasOtros", ex);
                return null;
            }
        }


        public Par_Cue_Otros CrearPar_Cue_Otros(Par_Cue_Otros pParam, Usuario vUsuario, int opcion)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pParam = DAParametro.CrearPar_Cue_Otros(pParam, vUsuario, opcion);
                    
                    ts.Complete();
                }
                return pParam;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_OtrosBusiness", "CrearPar_Cue_Otros", ex);
                return null;
            }
        }


        public void EliminarParametroCtasOtros(Int32 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAParametro.EliminarParametroCtasOtros(pId, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_OtrosBusiness", "EliminarParametroCtasOtros", ex);               
            }
        }



        public List<Par_Cue_LinAho> getListParametrosBusiness(Usuario pusuario, String pfiltro )
        {
            try
            {
                return DAParametro.getListParametros(pusuario, pfiltro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_OtrosBusiness", "getListParametrosbusiness", ex);
                return null;
            }
        }

        public void EliminarParametroBusiness(Usuario pUsuario, Int64 idcodigo) 
        {
            try
            {
                DAParametro.EliminarParametro(pUsuario, idcodigo);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_OtrosBusiness", "getListParametrosbusiness", ex);
            }
        }

        public void insertarParametroBusiness(Usuario pUsuario, Par_Cue_LinAho pEntidadParametro) 
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAParametro.InsertParametro(pUsuario, pEntidadParametro);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_OtrosBusiness", "insertarParametroBusiness", ex);
            }
        }

        public Par_Cue_LinAho getParametroByIdBusiness(Usuario pusuario, Int64 idParametro) 
        {
            try
            {
                return DAParametro.getParametroById(pusuario, idParametro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_OtrosBusiness", "getListParametrosbusiness", ex);
                return null;
            }
        }

        public void updateParametroBusinness(Usuario pUsuario, Par_Cue_LinAho entidadcrea) 
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAParametro.updateParametro(pUsuario, entidadcrea);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_OtrosBusiness", "getListParametrosbusiness", ex);
            }
        }

        public Int64 ObtenerSiguienteCodigo_ParCue_LinAHO(Usuario vUsuario)
        {
            try
            {
                return DAParametro.ObtenerSiguienteCodigo_ParCue_LinAHO(vUsuario);
            }
            catch
            {
                return 1;
            }
        }

    }
}

