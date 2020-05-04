using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.CDATS.Data;
using Xpinn.CDATS.Entities;
 
namespace Xpinn.CDATS.Business
{
 
    public class FormaCaptacionBusiness : GlobalBusiness
    {

        private FormaCaptacionCDATData DAFormaCaptacion;
 
        public FormaCaptacionBusiness()
        {
            DAFormaCaptacion = new FormaCaptacionCDATData();
        }
 
        public FormaCaptacion CrearFormaCaptacion(FormaCaptacion pFormaCaptacion, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pFormaCaptacion = DAFormaCaptacion.CrearFormaCaptacion(pFormaCaptacion, pusuario);
 
                    ts.Complete();
 
                }
 
                return pFormaCaptacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FormaCaptacionBusiness", "CrearFormaCaptacion", ex);
                return null;
            }
        }
 
 
        public FormaCaptacion ModificarFormaCaptacion(FormaCaptacion pFormaCaptacion, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pFormaCaptacion = DAFormaCaptacion.ModificarFormaCaptacion(pFormaCaptacion, pusuario);
 
                    ts.Complete();
 
                }
 
                return pFormaCaptacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FormaCaptacionBusiness", "ModificarFormaCaptacion", ex);
                return null;
            }
        }
 
 
        public void EliminarFormaCaptacion(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAFormaCaptacion.EliminarFormaCaptacion(pId, pusuario);
 
                    ts.Complete();
 
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FormaCaptacionBusiness", "EliminarFormaCaptacion", ex);
            }
        }
 
 
        public FormaCaptacion ConsultarFormaCaptacion(Int64 pId, Usuario pusuario)
        {
            try
            {
                FormaCaptacion FormaCaptacion = new FormaCaptacion();
                FormaCaptacion = DAFormaCaptacion.ConsultarFormaCaptacion(pId, pusuario);
                return FormaCaptacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FormaCaptacionBusiness", "ConsultarFormaCaptacion", ex);
                return null;
            }
        }
 
 
        public List<FormaCaptacion> ListarFormaCaptacion(FormaCaptacion pFormaCaptacion, Usuario pusuario)
        {
            try
            {
                return DAFormaCaptacion.ListarFormaCaptacion(pFormaCaptacion, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FormaCaptacionBusiness", "ListarFormaCaptacion", ex);
                return null;
            }
        }
 
 
    }
}

