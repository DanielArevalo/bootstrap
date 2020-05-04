using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.NIIF.Business;
using Xpinn.NIIF.Entities;
using Xpinn.Util;
using System.Data;
using System.ServiceModel;

namespace Xpinn.NIIF.Services
{
    public class CarteraNIFService
    {

        private CarteraNIFBusiness BOCartera;
        private ExcepcionBusiness BOExcepcion;

        public CarteraNIFService()
        {
            BOCartera = new CarteraNIFBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }
       
        public string CodigoProgramaoriginal { get { return "210104"; } }
        

        public List<CarteraNIF> ListarCarteraNIIF(DateTime vFecha, string Orden, Usuario vUsuario)
        {
            try
            {
                return BOCartera.ListarCarteraNIIF(vFecha,Orden, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraNIIFService", "ListarCarteraNIIF", ex);
                return null;
            }
        }


        public void EliminarCarteraNIIF(CarteraNIF pCarteraNIIF, DateTime vFecha, Usuario vUsuario)
        {
            try
            {
                BOCartera.EliminarCarteraNIIF(pCarteraNIIF, vFecha, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraNIIFServices", "EliminarCarteraNIIF", ex);
            }
        }


        public void ConsultarCarteraNIIF(DateTime vFecha, Usuario vUsuario)
        {
            try
            {
                BOCartera.ConsultarCarteraNIIF(vFecha, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraNIIFServices", "ConsultarCarteraNIIF", ex);
            }
        }


        public void ModificarEstadoFechaNIIF(DateTime vFecha, Usuario vUsuario)
        {
            try
            {
                BOCartera.ModificarEstadoFechaNIIF(vFecha, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AmortizacionNIIFService", "ModificarEstadoFechaNIIF", ex);
            }
        }

    }
}