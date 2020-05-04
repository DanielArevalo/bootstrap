using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Contabilidad.Business;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class InversionesService
    {
        private InversionesBusiness BOInversiones;
        private ExcepcionBusiness BOExcepcion;


        public InversionesService()
        {
            BOInversiones = new InversionesBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "30806"; } }
        public string CodigoProgramaTipoInv { get { return "30706"; } }

        public TipoInversiones CrearTipoInversion(TipoInversiones pTipoInversion, int pOpcion, Usuario vUsuario)
        {
            try
            {
                return BOInversiones.CrearTipoInversion(pTipoInversion, pOpcion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InversionesService", "CrearTipoInversion", ex);
                return null;
            }
        }

        public List<TipoInversiones> ListarTipoInversiones(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOInversiones.ListarTipoInversiones(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InversionesService", "ListarTipoInversiones", ex);
                return null;
            }
        }

        public TipoInversiones ConsultarTipoInversiones(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOInversiones.ConsultarTipoInversiones(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InversionesService", "ConsultarTipoInversiones", ex);
                return null;
            }
        }

        public void EliminarTipoInversiones(Int32 pId, Usuario vUsuario)
        {
            try
            {
                BOInversiones.EliminarTipoInversiones(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InversionesService", "EliminarTipoInversiones", ex);
            }           
        }



        public Inversiones CrearInversiones(Inversiones pInversiones, int pOpcion, Usuario vUsuario)
        {
            try
            {
                return BOInversiones.CrearInversiones(pInversiones, pOpcion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InversionesService", "CrearInversiones", ex);
                return null;
            }
        }

        public Inversiones ConsultarInversiones(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOInversiones.ConsultarInversiones(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InversionesService", "ConsultarInversiones", ex);
                return null;
            }
        }

        public List<Inversiones> ListarInversiones(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOInversiones.ListarInversiones(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InversionesService", "ListarInversiones", ex);
                return null;
            }
        }

        public void EliminarInversiones(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOInversiones.EliminarInversiones(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InversionesService", "EliminarInversiones", ex);
            }
        }


    }
}