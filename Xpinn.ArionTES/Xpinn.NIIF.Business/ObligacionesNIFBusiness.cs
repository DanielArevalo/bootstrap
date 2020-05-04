using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.NIIF.Data;
using Xpinn.NIIF.Entities;
using System.Transactions;


namespace Xpinn.NIIF.Business
{
    public class ObligacionesNIFBusiness : GlobalBusiness
    {
        ObligacionesNIFData BAObliga;

        public ObligacionesNIFBusiness()
        {
            BAObliga = new ObligacionesNIFData();
        }

        public Boolean ConsultarFECHAIngresada(DateTime pFecha, Usuario vUsuario)
        {
            try
            {
                BAObliga.ConsultarFECHAIngresada(pFecha, vUsuario);
                return true;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionesNIFBusiness", "ConsultarFECHAIngresada", ex);
                return false; ;
            }
        }


        public void GENERAR_ObligacionesNIF(ObligacionesNIF pObli, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    BAObliga.GENERAR_ObligacionesNIF(pObli, vUsuario);
                    ts.Complete();
                }                
            }
            catch
            {
                //BOExcepcion.Throw("ObligacionesNIFBusiness", "GENERAR_ObligacionesNIF", ex);              
            }
        }


        public List<ObligacionesNIF> Listar_TEMP_CostoAMortizado(DateTime pFecha, Usuario vUsuario)
        {
            try
            {
                return BAObliga.Listar_TEMP_CostoAMortizado(pFecha, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionesNIFBusiness", "Listar_TEMP_CostoAMortizado", ex);
                return null;
            }
        }



        public ObligacionesNIF ModificarFechaCTOAMORTIZACION_NIF(ObligacionesNIF pCosto, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCosto = BAObliga.ModificarFechaCTOAMORTIZACION_NIF(pCosto, vUsuario);
                    ts.Complete();
                }
                return pCosto;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionesNIFBusiness", "ModificarFechaCTOAMORTIZACION_NIF", ex);
                return null;
            }
        }


    }
}
