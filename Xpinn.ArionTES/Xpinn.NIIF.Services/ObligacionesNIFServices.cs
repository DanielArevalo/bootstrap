using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.NIIF.Business;
using Xpinn.NIIF.Entities;

namespace Xpinn.NIIF.Services
{
    
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ObligacionesNIFServices
    {
        private ObligacionesNIFBusiness BOObliga;
        private ExcepcionBusiness BOExcepcion;

      
        public ObligacionesNIFServices()
        {
            BOObliga = new ObligacionesNIFBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "210105"; } }


        public Boolean ConsultarFECHAIngresada(DateTime pFecha, Usuario vUsuario)
        {
            try
            {
                BOObliga.ConsultarFECHAIngresada(pFecha, vUsuario);
                return true;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionesNIFServices", "ConsultarFECHAIngresada", ex);
                return false; ;
            }
        }


        public void GENERAR_ObligacionesNIF(ObligacionesNIF pObli, Usuario vUsuario)
        {
            try
            {
                BOObliga.GENERAR_ObligacionesNIF(pObli, vUsuario);
            }
            catch 
            {
                //BOExcepcion.Throw("ObligacionesNIFServices", "GENERAR_ObligacionesNIF", ex);               
            }
        }

        public List<ObligacionesNIF> Listar_TEMP_CostoAMortizado(DateTime pFecha, Usuario vUsuario)
        {
            try
            {
                return BOObliga.Listar_TEMP_CostoAMortizado(pFecha, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionesNIFServices", "Listar_TEMP_CostoAMortizado", ex);
                return null;
            }
        }


        public ObligacionesNIF ModificarFechaCTOAMORTIZACION_NIF(ObligacionesNIF pCosto, Usuario vUsuario)
        {
            try
            {
                return BOObliga.ModificarFechaCTOAMORTIZACION_NIF(pCosto, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionesNIFServices", "ModificarFechaCTOAMORTIZACION_NIF", ex);
                return null;
            }
        }

    }
}