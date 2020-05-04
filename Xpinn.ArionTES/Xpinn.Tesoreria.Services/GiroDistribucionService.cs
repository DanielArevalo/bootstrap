using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Tesoreria.Entities;
using Xpinn.Tesoreria.Business;

namespace Xpinn.Tesoreria.Services
{

    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class GiroDistribucionService
    {

        private GiroDistribucionBusiness BOGiroDistribucion;
        private ExcepcionBusiness BOExcepcion;

        public GiroDistribucionService()
        {
            BOGiroDistribucion = new GiroDistribucionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "40307"; } }

        //public GiroDistribucion CrearGiroDistribucion(GiroDistribucion pGiroDistribucion, Usuario pusuario)
        //{
        //    try
        //    {
        //        pGiroDistribucion = BOGiroDistribucion.CrearGiroDistribucion(pGiroDistribucion, pusuario);
        //        return pGiroDistribucion;
        //    }
        //    catch (Exception ex)
        //    {
        //        BOExcepcion.Throw("GiroDistribucionService", "CrearGiroDistribucion", ex);
        //        return null;
        //    }
        //}


        public GiroDistribucion ModificarGiroDistribucion(GiroDistribucion pGiroDistribucion, Usuario pusuario)
        {
            try
            {
                pGiroDistribucion = BOGiroDistribucion.ModificarGiroDistribucion(pGiroDistribucion, pusuario);
                return pGiroDistribucion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GiroDistribucionService", "ModificarGiroDistribucion", ex);
                return null;
            }
        }

        public void InsertarGiros(List<Xpinn.Tesoreria.Entities.Giro> lstGiros, Int64 idGiro, Usuario pUsuario)
        {
            try
            {
                BOGiroDistribucion.InsertarGiros(lstGiros, idGiro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GiroDistribucionService", "InsertarGiros", ex);
            }
        }


        public void EliminarGiroDistribucion(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOGiroDistribucion.EliminarGiroDistribucion(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GiroDistribucionService", "EliminarGiroDistribucion", ex);
            }
        }


        public GiroDistribucion ConsultarGiroDistribucion(Int64 pId, Usuario pusuario)
        {
            try
            {
                GiroDistribucion GiroDistribucion = new GiroDistribucion();
                GiroDistribucion = BOGiroDistribucion.ConsultarGiroDistribucion(pId, pusuario);
                return GiroDistribucion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GiroDistribucionService", "ConsultarGiroDistribucion", ex);
                return null;
            }
        }


        public List<GiroDistribucion> ListarGiroDistribucion(GiroDistribucion pGiroDistribucion, Usuario pusuario)
        {
            try
            {
                return BOGiroDistribucion.ListarGiroDistribucion(pGiroDistribucion, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GiroDistribucionService", "ListarGiroDistribucion", ex);
                return null;
            }
        }

        public List<GiroDistribucion> listarDDlTipoComServices(Usuario pUsuario, int opcion) 
        {
            try
            {
                return BOGiroDistribucion.listarDDlTipoComBusines(pUsuario, opcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GiroDistribucionService", "ListarGiroDistribucion", ex);
                return null;
            }
        }

        public List<GiroDistribucion> listarDDlGeneradoEnServices(Usuario pUsuario) 
        {
            try
            {
                return BOGiroDistribucion.listarDDlGeneradoEnBusines(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GiroDistribucionService", "listarDDlGeneradoEnBusines", ex);
                return null;
            }
        }

        public List<GiroDistribucion> getListaGiroServices(Usuario pUsuario, string pFiltro) 
        {
            try
            {
                return BOGiroDistribucion.getListaGiroBusines(pUsuario, pFiltro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GiroDistribucionService", "getListaGiroServices", ex);
                return null;
            }
        }

        public GiroDistribucion ConsultarGiroDistribucionServices(Int64 pId, Usuario vUsuario) 
        {
            try
            {
                return BOGiroDistribucion.ConsultarGiroDistribucionBusines(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GiroDistribucionService", "ConsultarGiroDistribucionServices", ex);
                return null;
            }
        }

        public List<GiroDistribucion> listarDDlFormaPagoInv(Usuario pUsuario)
        {
            try
            {
                return BOGiroDistribucion.listarDDlFormaPagoInv(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GiroDistribucionBusiness", "listarDDlFormaPagoInv", ex);
                return null;
            }
        }



    }
}
