using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Tesoreria.Business;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Services
{

    public class ImpresionMasivaServices
    {

        private ImpresionMasivaBusiness BOImpresion;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Acceso
        /// </summary>
        public ImpresionMasivaServices()
        {
            BOImpresion = new ImpresionMasivaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "40806"; } }

        public List<Xpinn.Contabilidad.Entities.Comprobante> ListarComprobante(Xpinn.Contabilidad.Entities.Comprobante pComprobante, Usuario pUsuario, String filtro, String orden)
        {
            try
            {
                return BOImpresion.ListarComprobante(pComprobante, pUsuario, filtro, orden);
            }
            catch
            {
                return null;
            }
        }

    }
}
