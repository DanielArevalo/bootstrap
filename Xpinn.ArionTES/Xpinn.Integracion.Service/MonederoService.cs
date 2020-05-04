using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Integracion.Business;
using Xpinn.Integracion.Entities;
using Xpinn.Util;
using System.Data;
using System.ServiceModel;

namespace Xpinn.Integracion.Services
{
    public class MonederoService
    {

        private MonederoBusiness BOMonedero;
        private ExcepcionBusiness BOExcepcion;

        public MonederoService()
        {
            BOMonedero = new MonederoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }              


        public Monedero consultarMonedero(int cod_persona, Usuario pUsuario)
        {
            try
            {
                return BOMonedero.consultarMonedero(cod_persona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MonederoService", "consultarMonedero", ex);
                return null;
            }
        }

        public PersonaMonedero consultarPersonaMonedero(string identificacion, Usuario pUsuario)
        {
            try
            {
                return BOMonedero.consultarPersonaMonedero(identificacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MonederoServices", "consultarPersonaMonedero", ex);
                return null;
            }
        }

        public Monedero crearMonedero(int cod_persona, Usuario pUsuario)
        {
            try
            {
                return BOMonedero.crearMonedero(cod_persona, pUsuario);                
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MonederoService", "crearMonedero", ex);
                return null;
            }
        }

        public List<Operaciones> consultarOperaciones(bool soloActivos, Usuario pUsuario)
        {
            try
            {
                return BOMonedero.consultarOperaciones(soloActivos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MonederoService", "consultarOperaciones", ex);
                return null;
            }
        }

        public List<ProductoOrigen> consultarProductosOrigen(long cod_persona, Usuario pUsuario)
        {
            try
            {
                return BOMonedero.consultarProductosOrigen(cod_persona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MonederoService", "consultarProductosOrigen", ex);
                return null;
            }
        }


        public TranMonedero guardarTransaccionMonedero(TranMonedero transac, Usuario vUsuario)
        {
            try
            {
                return BOMonedero.guardarTransaccionMonedero(transac, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MonederoService", "guardarTransaccionMonedero", ex);
                return null;
            }

        }

        public List<TranMonedero> listarTranMonederoPersona(string cod_persona, Usuario pUsuario)
        {
            try
            {
                return BOMonedero.listarTranMonederoPersona(cod_persona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MonederoService", "listarTranMonederoPersona", ex);
                return null;
            }
        }
    }
}