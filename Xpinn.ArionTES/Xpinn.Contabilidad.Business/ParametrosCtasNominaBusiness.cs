using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Contabilidad.Data;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Business
{

    public class ParametrosCtasNominaBusiness : GlobalBusiness
    {

        private ParametrosCtasNominaData DAPar_Cue_Nomina;

        public ParametrosCtasNominaBusiness()
        {
            DAPar_Cue_Nomina = new ParametrosCtasNominaData();
        }

        public Par_Cue_Nomina CrearPar_Cue_Nomina(Par_Cue_Nomina pPar_Cue_Nomina, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPar_Cue_Nomina = DAPar_Cue_Nomina.CrearPar_Cue_Nomina(pPar_Cue_Nomina, pusuario);

                    ts.Complete();

                }

                return pPar_Cue_Nomina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_NominaBusiness", "CrearPar_Cue_Nomina", ex);
                return null;
            }
        }


        public Par_Cue_Nomina ModificarPar_Cue_Nomina(Par_Cue_Nomina pPar_Cue_Nomina, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPar_Cue_Nomina = DAPar_Cue_Nomina.ModificarPar_Cue_Nomina(pPar_Cue_Nomina, pusuario);

                    ts.Complete();

                }

                return pPar_Cue_Nomina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_NominaBusiness", "ModificarPar_Cue_Nomina", ex);
                return null;
            }
        }


        public void EliminarPar_Cue_Nomina(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAPar_Cue_Nomina.EliminarPar_Cue_Nomina(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_NominaBusiness", "EliminarPar_Cue_Nomina", ex);
            }
        }


        public Par_Cue_Nomina ConsultarPar_Cue_Nomina(Int64 pId, Usuario pusuario)
        {
            try
            {
                Par_Cue_Nomina Par_Cue_Nomina = new Par_Cue_Nomina();
                Par_Cue_Nomina = DAPar_Cue_Nomina.ConsultarPar_Cue_Nomina(pId, pusuario);
                return Par_Cue_Nomina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_NominaBusiness", "ConsultarPar_Cue_Nomina", ex);
                return null;
            }
        }


        public List<Par_Cue_Nomina> ListarPar_Cue_Nomina(string filtro, Usuario pusuario)
        {
            try
            {
                return DAPar_Cue_Nomina.ListarPar_Cue_Nomina(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_NominaBusiness", "ListarPar_Cue_Nomina", ex);
                return null;
            }
        }


    }
}