using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Nomina.Data;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Business
{

    public class Experiencia_LaboralBusiness : GlobalBusiness
    {

        private Experiencia_LaboralData DAExperiencia_Laboral;

        public Experiencia_LaboralBusiness()
        {
            DAExperiencia_Laboral = new Experiencia_LaboralData();
        }

        public Experiencia_Laboral CrearExperiencia_Laboral(Experiencia_Laboral pExperiencia_Laboral, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pExperiencia_Laboral = DAExperiencia_Laboral.CrearExperiencia_Laboral(pExperiencia_Laboral, pusuario);

                    ts.Complete();

                }

                return pExperiencia_Laboral;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Experiencia_LaboralBusiness", "CrearExperiencia_Laboral", ex);
                return null;
            }
        }


        public Experiencia_Laboral ModificarExperiencia_Laboral(Experiencia_Laboral pExperiencia_Laboral, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pExperiencia_Laboral = DAExperiencia_Laboral.ModificarExperiencia_Laboral(pExperiencia_Laboral, pusuario);

                    ts.Complete();

                }

                return pExperiencia_Laboral;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Experiencia_LaboralBusiness", "ModificarExperiencia_Laboral", ex);
                return null;
            }
        }


        public void EliminarExperiencia_Laboral(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAExperiencia_Laboral.EliminarExperiencia_Laboral(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Experiencia_LaboralBusiness", "EliminarExperiencia_Laboral", ex);
            }
        }


        public Experiencia_Laboral ConsultarExperiencia_Laboral(Int64 pId, Usuario pusuario)
        {
            try
            {
                Experiencia_Laboral Experiencia_Laboral = new Experiencia_Laboral();
                Experiencia_Laboral = DAExperiencia_Laboral.ConsultarExperiencia_Laboral(pId, pusuario);
                return Experiencia_Laboral;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Experiencia_LaboralBusiness", "ConsultarExperiencia_Laboral", ex);
                return null;
            }
        }


        public List<Experiencia_Laboral> ListarExperiencia_Laboral(long cod_persona, Usuario pusuario)
        {
            try
            {
                return DAExperiencia_Laboral.ListarExperiencia_Laboral(cod_persona, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Experiencia_LaboralBusiness", "ListarExperiencia_Laboral", ex);
                return null;
            }
        }


    }
}

