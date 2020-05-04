using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Nomina.Data;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Business
{

    public class AumentoSueldoBusiness : GlobalBusiness
    {

        private AumentoSueldoData DAAumentoSueldo;

        public AumentoSueldoBusiness()
        {
            DAAumentoSueldo = new AumentoSueldoData();
        }

        public AumentoSueldo CrearAumentoSueldo(AumentoSueldo pAumentoSueldo, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAumentoSueldo = DAAumentoSueldo.CrearAumentoSueldo(pAumentoSueldo, pusuario);

                    if (pAumentoSueldo.lista != null)
                    {
                        int num = 0;
                        foreach (AumentoSueldo aAumento in pAumentoSueldo.lista)
                        {
                            AumentoSueldo naumento = new AumentoSueldo();
                            naumento = DAAumentoSueldo.CrearAumentoSueldomasivo(aAumento, pusuario);
                            num += 1;
                        
                         }
                    }



                    ts.Complete();

                }

                return pAumentoSueldo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AumentoSueldoBusiness", "CrearAumentoSueldo", ex);
                return null;
            }
        }


        public AumentoSueldo ModificarAumentoSueldo(AumentoSueldo pAumentoSueldo, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAumentoSueldo = DAAumentoSueldo.ModificarAumentoSueldo(pAumentoSueldo, pusuario);

                    ts.Complete();

                }

                return pAumentoSueldo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AumentoSueldoBusiness", "ModificarAumentoSueldo", ex);
                return null;
            }
        }


        public void EliminarAumentoSueldo(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAAumentoSueldo.EliminarAumentoSueldo(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AumentoSueldoBusiness", "EliminarAumentoSueldo", ex);
            }
        }


        public AumentoSueldo ConsultarAumentoSueldo(Int64 pId, Usuario pusuario)
        {
            try
            {
                AumentoSueldo AumentoSueldo = new AumentoSueldo();
                AumentoSueldo = DAAumentoSueldo.ConsultarAumentoSueldo(pId, pusuario);
                return AumentoSueldo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AumentoSueldoBusiness", "ConsultarAumentoSueldo", ex);
                return null;
            }
        }


        public AumentoSueldo ConsultarAumentoSueldoXEmpleado(Int64 pId, Usuario pusuario)
        {
            try
            {
                AumentoSueldo AumentoSueldo = new AumentoSueldo();
                AumentoSueldo = DAAumentoSueldo.ConsultarAumentoSueldoXEmpleado(pId, pusuario);
                return AumentoSueldo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AumentoSueldoBusiness", "ConsultarAumentoSueldoXEmpleado", ex);
                return null;
            }
        }


        public List<AumentoSueldo> ListarAumentoSueldo(string filtro, Usuario pusuario)
        {
            try
            {
                return DAAumentoSueldo.ListarAumentoSueldo(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AumentoSueldoBusiness", "ListarAumentoSueldo", ex);
                return null;
            }
        }


    }
}