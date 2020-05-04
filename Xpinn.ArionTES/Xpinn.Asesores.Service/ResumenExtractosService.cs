using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;
using Xpinn.Util;
using System.Data;

namespace Xpinn.Asesores.Services
{
    /// <summary>
    /// Listado de clientes generado para la generación de extracto
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ResumenExtractosService
    {
        private ResumenExtractosBusiness BOResumenExtractos;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Caja
        /// </summary>
        public ResumenExtractosService()
        {
            BOResumenExtractos = new ResumenExtractosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<ResumenExtractos> ListarResumenExtractos(ResumenExtractos ResumenExtractos, Int64 vNumRadic, out DataTable dDataTable, Usuario pUsuario)
        {
            try
            {
                return BOResumenExtractos.ListarResumenExtractos(ResumenExtractos, vNumRadic, out dDataTable, pUsuario);
            }
            catch (Exception ex)
            {
                dDataTable = new DataTable();
                BOExcepcion.Throw("ResumenExtractosService", "ListarResumenExtractoss", ex);
                return null;
            }
        }



        public List<ResumenExtractos> GeneraryListarResumen(ResumenExtractos entidad, Usuario pUsuario)
        {
            try
            {
                return BOResumenExtractos.GeneraryListarResumen(entidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ResumenExtractosService", "GeneraryListarResumen", ex);
                return null;
            }
        }

        public List<ResumenExtractos> ListarBancos(string codlinea, Usuario pUsuario)
        {
            try
            {
                return BOResumenExtractos.ListarBancos(codlinea, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ResumenExtractosService", "ListarBancos", ex);
                return null;
            }
        }



        public List<ResumenExtractos> GeneraryListarResumenDetalle(ResumenExtractos entidad, Usuario pUsuario)
        {
            try
            {
                return BOResumenExtractos.GeneraryListarResumenDetalle(entidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ResumenExtractosService", "GeneraryListarResumenDetalle", ex);
                return null;
            }
        }

    }
}