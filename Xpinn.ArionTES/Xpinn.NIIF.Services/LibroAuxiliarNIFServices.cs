using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.NIIF.Business;
using Xpinn.NIIF.Entities;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.NIIF.Services
{


    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class LibroAuxiliarNIFServices
    {
        private LibroAuxiliarNIFBussines BOLibro;
        private ExcepcionBusiness BOExcepcion;


        public LibroAuxiliarNIFServices()
        {
            BOLibro = new LibroAuxiliarNIFBussines();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "210111"; } }


        public List<LibroAuxiliar> ListarAuxiliar(Int64 CenIni, Int64 CenFin, DateTime FecIni, DateTime FecFin, string cod_cuenta_ini, string cod_cuenta_fin, Boolean por_rango, Int32 moneda, string pOrdenar, Usuario vUsuario)
        {
            return BOLibro.ListarAuxiliar(CenIni, CenFin, FecIni, FecFin, cod_cuenta_ini, cod_cuenta_fin, por_rango, moneda, pOrdenar, vUsuario);
        }
    }
}