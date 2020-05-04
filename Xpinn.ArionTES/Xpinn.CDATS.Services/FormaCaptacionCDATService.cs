using System; 
using System.Collections.Generic; 
using System.Text; 
using Xpinn.Util; 
using System.ServiceModel; 
using Xpinn.CDATS.Entities; 
using Xpinn.CDATS.Business; 
 
namespace Xpinn.CDATS.Services 
{ 
        [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
        public class FormaCaptacionService
        {
 
            private FormaCaptacionBusiness BOFormaCaptacion;
            private ExcepcionBusiness BOExcepcion;
 
            public FormaCaptacionService()
            {
                BOFormaCaptacion = new FormaCaptacionBusiness();
                BOExcepcion = new ExcepcionBusiness();
            }
 
            public string CodigoPrograma { get { return "220106"; } }
 
            public FormaCaptacion CrearFormaCaptacion(FormaCaptacion pFormaCaptacion, Usuario pusuario)
            {
                try
                {
                    pFormaCaptacion = BOFormaCaptacion.CrearFormaCaptacion(pFormaCaptacion, pusuario);
                    return pFormaCaptacion;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("FormaCaptacionService", "CrearFormaCaptacion", ex);
                    return null;
                }
            }
 
 
            public FormaCaptacion ModificarFormaCaptacion(FormaCaptacion pFormaCaptacion, Usuario pusuario)
            {
                try
                {
                    pFormaCaptacion = BOFormaCaptacion.ModificarFormaCaptacion(pFormaCaptacion, pusuario);
                    return pFormaCaptacion;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("FormaCaptacionService", "ModificarFormaCaptacion", ex);
                    return null;
                }
            }
 
 
            public void EliminarFormaCaptacion(Int64 pId, Usuario pusuario)
            {
                try
                {
                    BOFormaCaptacion.EliminarFormaCaptacion(pId, pusuario);
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("FormaCaptacionService", "EliminarFormaCaptacion", ex);
                }
            }
 
 
            public FormaCaptacion ConsultarFormaCaptacion(Int64 pId, Usuario pusuario)
            {
                try
                {
                    FormaCaptacion FormaCaptacion = new FormaCaptacion();
                    FormaCaptacion = BOFormaCaptacion.ConsultarFormaCaptacion(pId, pusuario);
                    return FormaCaptacion;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("FormaCaptacionService", "ConsultarFormaCaptacion", ex);
                    return null;
                }
            }
 
 
            public List<FormaCaptacion> ListarFormaCaptacion(FormaCaptacion pFormaCaptacion, Usuario pusuario)
            {
                try
                {
                   return BOFormaCaptacion.ListarFormaCaptacion(pFormaCaptacion, pusuario);
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("FormaCaptacionService", "ListarFormaCaptacion", ex);
                    return null;
                }
            }
 
 
        
    }
}
