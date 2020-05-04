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
    
    public class CambioMonedaBusiness : GlobalBusiness
    {
        private CambioMonedaData DACambio;

        public CambioMonedaBusiness()
        {
            DACambio = new CambioMonedaData();
        }

        public CambioMoneda CrearCambioMoneda(CambioMoneda pCambio, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCambio = DACambio.CrearCambioMoneda(pCambio, vUsuario);

                    ts.Complete();
                }
                return pCambio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CambioMonedaBusiness", "CrearCambioMoneda", ex);
                return null;
            }
        }

        public CambioMoneda ModificarCambioMoneda(CambioMoneda pCambio, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCambio = DACambio.ModificarCambioMoneda(pCambio, vUsuario);

                    ts.Complete();
                }
                return pCambio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CambioMonedaBusiness", "ModificarCambioMoneda", ex);
                return null;
            }
        }


        public CambioMoneda ConsultarCambioMoneda(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return DACambio.ConsultarCambioMoneda(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CambioMonedaBusiness", "ConsultarCambioMoneda", ex);
                return null;
            }
        }

        public List<CambioMoneda> ListarCambioMoneda(string filtro, Usuario vUsuario)
        {
            try
            {
                return DACambio.ListarCambioMoneda(filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CambioMonedaBusiness", "ListarCambioMoneda", ex);
                return null;
            }
        }


        public void EliminarCambioMoneda(Int64 pId, Usuario vUsuario)
        {
            try
            {
                DACambio.EliminarCambioMoneda(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CambioMonedaBusiness", "EliminarCambioMoneda", ex);               
            }
        }


    }
}