using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;
namespace Xpinn.Asesores.Services
{
     [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
  public  class InformacionFinancieraService
    {
      
        private ExcepcionBusiness BOExcepcion;
        private InformacionFinancieraBusiness  BOSinformacionfinanciera;



        public InformacionFinancieraService()
        {
            BOSinformacionfinanciera = new InformacionFinancieraBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }
    
      public string CodigoPrograma { get { return "110121"; } }


      public InformacionFinanciera ListarInformacionFinanciera(int codigo, Usuario pUsuario)
      {
         return BOSinformacionfinanciera.ListarInformacionFinanciera(codigo, pUsuario);
      }
    }
}
