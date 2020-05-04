using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Xpinn.Cartera.Entities;
using Xpinn.Cartera.Business;
using Xpinn.Util;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Cartera.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CargaMovimientosService
    {
        public string CodigoProgramaCarga { get { return "60116"; } }

        private ExcepcionBusiness BOExcepcion;
        private CargaMovimientosBusiness BOMovimientos = new CargaMovimientosBusiness();
        public Boolean CargaMasivoMovimientos(List<CargaMovimientos> carga, Usuario pUsuario, Operacion pOperacion, ref string Error)
        {
            try
            {
                BOMovimientos.CargaMasivoMovimientos(carga, pUsuario, pOperacion, ref Error);
                return true;
            }
            catch (Exception e)
            {
                BOExcepcion.Throw("CargaMovimientos", "CargaMovimientosBusiness", e);
                return false;
            }

        }

        public TipoProducto ConsultarProducto(string tipo_producto, Usuario pUsuario)
        {
            try
            {
                return BOMovimientos.ConsultarProducto(tipo_producto, pUsuario);
            }
            catch (Exception e)
            {
                BOExcepcion.Throw("CargaMovimientos", "CargaMovimientosBusiness", e);
                return null;
            }
        }

        public TipoProducto ConsultaNProducto(string Query, Usuario pUsuario)
        {
            try
            {
                return BOMovimientos.ConsultaNProducto(Query, pUsuario);
            }
            catch (Exception e)
            {
                BOExcepcion.Throw("CargaMovimientos", "ConsultaNProducto", e);
                return null;
            }
        }

        public TipoProducto ConsultaSaldo(Int64 numero_radicacion, Usuario pUsuario)
        {
            try
            {
                return BOMovimientos.ConsultaSaldo(numero_radicacion, pUsuario);
            }
            catch (Exception e)
            {
                BOExcepcion.Throw("CargaMovimientos", "ConsultaSaldo", e);
                return null;
            }
        }

    }
}
