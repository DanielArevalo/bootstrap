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
    public class AmortizacionNIFService
    {

        private AmortizacionNIFBusiness BOAmortizacion;
        private ExcepcionBusiness BOExcepcion;

        public AmortizacionNIFService()
        {
            BOAmortizacion = new AmortizacionNIFBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }
       
        public string CodigoProgramaoriginal { get { return "210103"; } }
        

        public List<AmortizacionNIF> ListarAmortizacionNIIF(DateTime vFecha, Usuario vUsuario)
        {
            try
            {
                return BOAmortizacion.ListarAmortizacionNIIF(vFecha, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AmortizacionNIIFService", "ListarAmortizacionNIIF", ex);
                return null;
            }
        }


        public void ModificarEstadoFechaNIIF(DateTime vFecha, Usuario vUsuario)
        {
            try
            {
                BOAmortizacion.ModificarEstadoFechaNIIF(vFecha, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AmortizacionNIIFService", "ModificarEstadoFechaNIIF", ex);               
            }
        }

        public List<DetalleAmortizacionNIIF> ListarDetalleAmortizacionNIIF(DateTime vFecha, Int64 vNumeroRadicacion, Usuario vUsuario)
        {
            try
            {
                return BOAmortizacion.ListarDetalleAmortizacionNIIF(vFecha, vNumeroRadicacion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AmortizacionNIIFService", "ListarDetalleAmortizacionNIIF", ex);
                return null;
            }
        }

    }
}