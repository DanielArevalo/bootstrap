using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Tesoreria.Business;
using Xpinn.Tesoreria.Entities;
using System.Web;

namespace Xpinn.Tesoreria.Services
{
    public class TrasladoDevolucionServices
    {
        private TrasladoDevolucionBusiness BOTraslado;
        private ExcepcionBusiness BOExcepcion;


        public TrasladoDevolucionServices()
        {
            BOTraslado = new TrasladoDevolucionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "180301"; } }
        public string CodigoProgramaMasivo { get { return "180304"; } }
        public string CodigoProgramaMenor { get { return "180305"; } }
        public string CodigoProgramaAplMasivo { get { return "180306"; } }

        public TrasladoDevolucion Crear_TrasladoDevolucion(TrasladoDevolucion pTraslado, Usuario vUsuario)
        {
            try
            {
                return BOTraslado.Crear_TrasladoDevolucion(pTraslado, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TrasladoDevolucionBusiness", "Crear_TrasladoDevolucion", ex);
                return null;
            }
        }

        public void Crear_TrasladoDevolucionALL(ref Int64 pCOd_OPE, Operacion pOperacion, List<TrasladoDevolucion> lstTraslado,Xpinn.FabricaCreditos.Entities.Giro pGiro, Usuario vUsuario)
        {
            try
            {
                BOTraslado.Crear_TrasladoDevolucionALL(ref pCOd_OPE, pOperacion, lstTraslado, pGiro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TrasladoDevolucionBusiness", "Crear_TrasladoDevolucion", ex);
            }
        }  


        public List<TrasladoDevolucion> ListarTrasladoDevolucion(String orden,string filtro, Usuario vUsuario)
        {
            try
            {
                return BOTraslado.ListarTrasladoDevolucion(orden,filtro, vUsuario);
            }
            catch
            {
                return null;
            }
        }


        public List<TrasladoDevolucion> ConsultarTrasladoDevolucion(Int64 cod_persona, Usuario vUsuario)
        {
            try
            {
                return BOTraslado.ConsultarTrasladoDevolucion(cod_persona, vUsuario);
            }
            catch
            {
                return null;
            }
        }



        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return BOTraslado.ObtenerSiguienteCodigo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DevolucionServices", "ObtenerSiguienteCodigo", ex);
                return 0;
            }
        }


        public List<TrasladoDevolucion> ListarDevolucionesMenores(String orden, string filtro, decimal valor_menor, Usuario vUsuario)
        {
            try
            {
                return BOTraslado.ListarDevolucionesMenores(orden, filtro, valor_menor, vUsuario);
            }
            catch
            {
                return null;
            }
        }

        public List<TrasladoDevolucion> ListarDevolucionesMasivas(String orden, string filtro, Usuario vUsuario)
        {
            try
            {
                return BOTraslado.ListarDevolucionesMasivas(orden, filtro, vUsuario);
            }
            catch
            {
                return null;
            }
        }

        public List<TrasladoDevolucion> ListarDevolucionesPersona(String pIdentificacion, Usuario pUsuario)
        {
            try
            {
                return BOTraslado.ListarDevolucionesPersona(pIdentificacion, pUsuario);
            }
            catch
            {
                return null;
            }
        }

        public bool TrasladoDevolucionesMenores(DateTime pFecha, List<TrasladoDevolucion> plstTraslado, int pTipoOpe, ref Int64 pCodOpe, ref string pError, Usuario pUsuario)
        {
            try
            {
                return BOTraslado.TrasladoDevolucionesMenores(pFecha, plstTraslado, pTipoOpe, ref pCodOpe, ref pError, pUsuario);
            }
            catch (Exception ex)
            {
                pError += ex.Message;
                return false;
            }
        }



        public bool AplicacionMasivaDevoluciones(DateTime pFecha, List<TrasladoDevolucion> plstDevoluciones, int pTipoOpe, ref Int64 pCodOpe, ref string pError, Usuario pUsuario)
        {
            try
            {
                return BOTraslado.AplicacionMasivaDevoluciones(pFecha, plstDevoluciones, pTipoOpe, ref pCodOpe, ref pError, pUsuario);
            }
            catch (Exception ex)
            {
                pError += ex.Message;
                return false;
            }
        }

    }
}
