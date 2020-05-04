using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Business;

namespace Xpinn.FabricaCreditos.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class AmortizaCreService
    {

        private AmortizaCreBusiness BOAmortizaCre;
        private ExcepcionBusiness BOExcepcion;

        public AmortizaCreService()
        {
            BOAmortizaCre = new AmortizaCreBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100157"; } }

        public AmortizaCre CrearAmortizaCre(AmortizaCre pAmortizaCre, Usuario pusuario)
        {
            try
            {
                pAmortizaCre = BOAmortizaCre.CrearAmortizaCre(pAmortizaCre, pusuario);
                return pAmortizaCre;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AmortizaCreService", "CrearAmortizaCre", ex);
                return null;
            }
        }


        public AmortizaCre ModificarAmortizaCre(AmortizaCre pAmortizaCre, Usuario pusuario)
        {
            try
            {
                pAmortizaCre = BOAmortizaCre.ModificarAmortizaCre(pAmortizaCre, pusuario);
                return pAmortizaCre;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AmortizaCreService", "ModificarAmortizaCre", ex);
                return null;
            }
        }


        public void EliminarAmortizaCre(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOAmortizaCre.EliminarAmortizaCre(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AmortizaCreService", "EliminarAmortizaCre", ex);
            }
        }


        public AmortizaCre ConsultarAmortizaCre(Int64 pId, Usuario pusuario)
        {
            try
            {
                AmortizaCre AmortizaCre = new AmortizaCre();
                AmortizaCre = BOAmortizaCre.ConsultarAmortizaCre(pId, pusuario);
                return AmortizaCre;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AmortizaCreService", "ConsultarAmortizaCre", ex);
                return null;
            }
        }


        public List<AmortizaCre> ListarAmortizaCre(AmortizaCre pAmortizaCre, Usuario pusuario)
        {
            try
            {
                return BOAmortizaCre.ListarAmortizaCre(pAmortizaCre, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AmortizaCreService", "ListarAmortizaCre", ex);
                return null;
            }
        }
    }
}
