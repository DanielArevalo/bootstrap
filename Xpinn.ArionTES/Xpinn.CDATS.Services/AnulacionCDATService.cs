using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using Xpinn.Util;
using Xpinn.CDATS.Entities;
using Xpinn.CDATS.Business;


namespace Xpinn.CDATS.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class AnulacionCDATService
    {
        AnulacionCDATBusiness BOAnula ;
        ExcepcionBusiness BOException;

        public AnulacionCDATService()
        {
            BOAnula = new AnulacionCDATBusiness();
            BOException = new ExcepcionBusiness();
        }

        public string CodigoProgramaANU { get { return "220304"; } }
        public string CodigoProgramaCaus{ get { return "220318"; } }

        public Cdat ModificarCDATAnulacion(Cdat pCdat, Usuario vUsuario)
        {
            try
            {
                return BOAnula.ModificarCDATAnulacion(pCdat, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AnulacionCDATService", "ModificarCDATAnulacion", ex);
                return null;
            }
        }

        

    }
}
