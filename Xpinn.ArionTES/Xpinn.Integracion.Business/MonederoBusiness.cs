using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Integracion.Entities;
using Xpinn.Integracion.Data;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Xpinn.Integracion.Business
{
    public class MonederoBusiness : GlobalData
    {
        private MonederoData BOMonederoData;

        public MonederoBusiness()
        {
            BOMonederoData = new MonederoData();
        }
        
        public Monedero consultarMonedero(int cod_persona, Usuario pUsuario)
        {
            try
            {
                return BOMonederoData.consultarMonedero(cod_persona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MonederoBusiness", "consultarMonedero", ex);
                return null;
            }
        }
        
        public PersonaMonedero consultarPersonaMonedero(string identificacion, Usuario pUsuario)
        {
            try
            {
                return BOMonederoData.consultarPersonaMonedero(identificacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MonederoBusiness", "consultarPersonaMonedero", ex);
                return null;
            }
        }

        public List<Operaciones> consultarOperaciones(bool soloActivos, Usuario pUsuario)
        {
            try
            {
                return BOMonederoData.consultarOperaciones(soloActivos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MonederoBusiness", "consultarOperaciones", ex);
                return null;
            }
        }

        public List<ProductoOrigen> consultarProductosOrigen(long cod_persona, Usuario pUsuario)
        {
            try
            {
                return BOMonederoData.consultarProductosOrigen(cod_persona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MonederoBusiness", "consultarProductosOrigen", ex);
                return null;
            }
        }

        public Monedero crearMonedero(int cod_persona, Usuario pUsuario)
        {
            try
            {
                int id_monedero = BOMonederoData.crearMonedero(cod_persona, pUsuario);
                if(id_monedero > 0)
                    return BOMonederoData.consultarMonedero(cod_persona, pUsuario);
                return null;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MonederoBusiness", "consultarMonedero", ex);
                return null;
            }
        }


        public TranMonedero guardarTransaccionMonedero(TranMonedero transac, Usuario vUsuario)
        {
            try
            {
                return BOMonederoData.guardarTransaccionMonedero(transac, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MonederoBusiness", "guardarTransaccionMonedero", ex);
                return null;
            }
                
        }

        public List<TranMonedero> listarTranMonederoPersona(string cod_persona, Usuario pUsuario)
        {
            try
            {
                return BOMonederoData.listarTranMonederoPersona(cod_persona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IntegracionBusiness", "listarTranMonederoPersona", ex);
                return null;
            }                
        }


    }
}


